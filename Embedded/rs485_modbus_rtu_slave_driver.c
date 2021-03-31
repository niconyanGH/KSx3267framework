/* Includes ------------------------------------------------------------------*/

#include <stdio.h>
#include <string.h>
#include <stdlib.h>

#include "rs485_modbus_rtu_driver.h"





static modbus_t local_modbus_t;


static uint8_t table_crc_hi[0x100];
static uint8_t table_crc_lo[0x100];

static void initCRCTable()
{
	int crc_data;
	int crc_crc;

	for (int crc_i = 0; crc_i < 256; crc_i++)
	{
		crc_data = (crc_i << 1);
		crc_crc = 0;
		for (int j = 8; j > 0; j--)
		{
			crc_data >>= 1;
			if ((crc_data ^ crc_crc) & 0x0001)
			{
				crc_crc = (crc_crc >> 1) ^ 0xA001;
			}
			else
			{
				crc_crc >>= 1;
			}
		}
		table_crc_hi[crc_i] = (uint8_t) (crc_crc & 0xFF);
		table_crc_lo[crc_i] = (uint8_t) ((crc_crc >> 8) & 0xFF);

	}
}

static int GetCRC16(uint8_t *buffer, int buffer_length)
{
	int index;
	char crc_hi = 0xFF;
	char crc_lo = 0xFF;

	for (int i = 0; i < buffer_length; i++)
	{
		index = crc_hi ^ buffer[i];
		crc_hi = crc_lo ^ table_crc_hi[index];
		crc_lo = table_crc_lo[index];
	}

	index=(crc_hi << 8 | crc_lo) &0xFFFF;


	return index;

}

static int STD_ModbusRTU_MSG_crc16(uint8_t *req, int req_length)
{
	int crc = GetCRC16(req, req_length);
	req[req_length++] = crc >> 8;
	req[req_length++] = crc & 0x00FF;

	return req_length;
}


static int STD_ModbusRTU_check(int mslave, uint8_t *msg, const int msg_length)
{
	uint16_t crc_calculated;
	uint16_t crc_received;
	int slave = msg[0];

	if (slave != mslave && slave != MODBUS_BROADCAST_ADDRESS)
	{

		return 0;
	}

	crc_calculated = GetCRC16(msg, msg_length - 2);
	crc_received = (msg[msg_length - 2] << 8) | msg[msg_length - 1];

	if (crc_calculated == crc_received)
	{
		return msg_length;
	}
	else
	{


		return -1;
	}

}




int STD_ModbusRTU_receive( uint8_t *req)
{

	int length_to_read = MODBUS_RTU_MAX_ADU_LENGTH;
	int msg_length = 0;

	msg_length = RS485_modbus_read(local_modbus_t.rs485_hardware_port, req, length_to_read);

	if (msg_length >= 8 && msg_length <= MODBUS_RTU_MAX_ADU_LENGTH)
	{

		return STD_ModbusRTU_check(local_modbus_t.slave, req, msg_length);

	}

	return 0;

}



void STD_ModbusRTU_New_Slave(int rs485_device, int mslave, modbus_register_user_map *regmap)
{


	initCRCTable();

	local_modbus_t.slave = mslave;
	local_modbus_t.rs485_hardware_port = rs485_device;


	local_modbus_t.mRegsiterMAP.buffer_input = regmap->input_readonly;
	local_modbus_t.mRegsiterMAP.buffer_holding = regmap->holding_readwrite;


	local_modbus_t.mRegsiterMAP.nsize_input = modbus_readonly_reg_size;
	local_modbus_t.mRegsiterMAP.nsize_holding=modbus_readwrite_reg_size;


	local_modbus_t.mRegsiterMAP.startaddress_input = modbus_readonly_reg_start_address;
	local_modbus_t.mRegsiterMAP.startaddress_holding =modbus_readwrite_reg_start_address;



	if(local_modbus_t.mRegsiterMAP.nsize_input >0 && local_modbus_t.mRegsiterMAP.buffer_input!=0)
	{
	  memset(local_modbus_t.mRegsiterMAP.buffer_input, 0, local_modbus_t.mRegsiterMAP.nsize_input * sizeof(uint16_t));
	}

	if(local_modbus_t.mRegsiterMAP.nsize_holding >0 && local_modbus_t.mRegsiterMAP.buffer_holding!=0)
	{
	  memset(local_modbus_t.mRegsiterMAP.buffer_holding, 0, local_modbus_t.mRegsiterMAP.nsize_holding * sizeof(uint16_t));
	}


}




static int bulid_response_exception(int mslave, int mfunction, int exception_code, uint8_t *rsp)
{
	int rsp_length = 0;
	rsp[rsp_length++] = mslave;
	rsp[rsp_length++] = mfunction + 0x80;
	rsp[rsp_length++] = exception_code;

	return rsp_length;
}

int STD_ModbusRTU_reply( const uint8_t *req, int req_length)
{

	int slave;
	int function;
	uint16_t address;
	uint8_t rsp[MODBUS_RTU_MAX_ADU_LENGTH];
	int rsp_length = 0;

	modbus_map *mb_mapping = &local_modbus_t.mRegsiterMAP;


	slave = req[0];
	function = req[1];
	address = (req[2] << 8) + req[3];

	switch (function)
	{

		case MODBUS_FC_READ_HOLDING_REGISTERS:
		case MODBUS_FC_READ_INPUT_REGISTERS:
		{

			int start_registers;
			int nb_registers;
			uint16_t *tab_registers;

			if (function == MODBUS_FC_READ_INPUT_REGISTERS)
			{
				start_registers = mb_mapping->startaddress_input;
				nb_registers = mb_mapping->nsize_input;
				tab_registers = mb_mapping->buffer_input;
			}
			else
			{
				start_registers = mb_mapping->startaddress_holding;
				nb_registers = mb_mapping->nsize_holding;
				tab_registers = mb_mapping->buffer_holding;
			}

			int nb = (req[4] << 8) + req[5];


			int mapping_address = address - start_registers;

			if (nb < 1 || MODBUS_MAX_READ_REGISTERS < nb)
			{
				rsp_length = bulid_response_exception(slave, function, MODBUS_EXCEPTION_ILLEGAL_DATA_VALUE, rsp);
			}
			else if (mapping_address < 0 || (mapping_address + nb) > nb_registers)
			{
				rsp_length = bulid_response_exception(slave, function, MODBUS_EXCEPTION_ILLEGAL_DATA_ADDRESS, rsp);

			}
			else
			{
				int i;

				rsp_length = 0;
				rsp[rsp_length++] = slave;
				rsp[rsp_length++] = function;

				rsp[rsp_length++] = nb << 1;
				for (i = mapping_address; i < mapping_address + nb; i++)
				{
					rsp[rsp_length++] = tab_registers[i] >> 8;
					rsp[rsp_length++] = tab_registers[i] & 0xFF;
				}
			}
		}
			break;

		case MODBUS_FC_WRITE_SINGLE_REGISTER:
		{
			int mapping_address = address - mb_mapping->startaddress_holding;

			if (mapping_address < 0 || mapping_address >= mb_mapping->nsize_holding)
			{
				rsp_length = bulid_response_exception(slave, function, MODBUS_EXCEPTION_ILLEGAL_DATA_ADDRESS, rsp);
			}
			else
			{
				int data = (req[4] << 8) + req[5];
				mb_mapping->buffer_holding[mapping_address] = data;
				memcpy(rsp, req, req_length);
				rsp_length = req_length;
			}
		}
			break;

		case MODBUS_FC_WRITE_MULTIPLE_REGISTERS:
		{
			int nb = (req[4] << 8) + req[5];

			int mapping_address = address - mb_mapping->startaddress_holding;

			if (nb < 1 || MODBUS_MAX_WRITE_REGISTERS < nb)
			{
				rsp_length = bulid_response_exception(slave, function, MODBUS_EXCEPTION_ILLEGAL_DATA_VALUE, rsp);
			}
			else if (mapping_address < 0 || (mapping_address + nb) > mb_mapping->nsize_holding)
			{
				rsp_length = bulid_response_exception(slave, function, MODBUS_EXCEPTION_ILLEGAL_DATA_ADDRESS, rsp);
			}
			else
			{
				int i, j;
				for (i = mapping_address, j = 7; i < (mapping_address + nb); i++, j += 2)
				{
					mb_mapping->buffer_holding[i] = (req[j] << 8) + req[j + 1];
				}

				rsp_length = 0;
				rsp[rsp_length++] = slave;
				rsp[rsp_length++] = function;


				memcpy(rsp + rsp_length, req + rsp_length, 4);
				rsp_length += 4;
			}
		}
			break;




		default:
			rsp_length = bulid_response_exception(slave, function, MODBUS_EXCEPTION_ILLEGAL_FUNCTION, rsp);
			break;
	}


	{
	int crc = GetCRC16(rsp, rsp_length);
	rsp[rsp_length++] = crc >> 8;
	rsp[rsp_length++] = crc & 0x00FF;

	}

	//rsp_length = STD_ModbusRTU_MSG_crc16(rsp, rsp_length);

	return RS485_modbus_write(local_modbus_t.rs485_hardware_port, rsp, rsp_length);


}


/******************* (C) COPYRIGHT 2011 STMicroelectronics *****END OF FILE****/
