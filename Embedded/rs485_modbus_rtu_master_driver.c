/* Includes ------------------------------------------------------------------*/


#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "rs485_modbus_rtu_driver.h"

static int master_rs485_hardware_port = 0;

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

	index = (crc_hi << 8 | crc_lo) & 0xFFFF;

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

static int STD_ModbusRTU_build_request_basis(int slave, int function, int addr, int nb, uint8_t *req)
{

	req[0] = (uint8_t) slave;
	req[1] = (uint8_t) function;
	req[2] = (uint8_t) (addr >> 8);
	req[3] = (uint8_t) (addr & 0x00ff);
	req[4] = (uint8_t) (nb >> 8);
	req[5] = (uint8_t) (nb & 0x00ff);

	return 6;
}

static BOOL bulid_read_registers(int slave, int function, int addr, int nb, uint8_t *req)
{
	int req_length;
	if (nb > MODBUS_MAX_READ_REGISTERS)
	{

		return FALSE;
	}

	req_length = STD_ModbusRTU_build_request_basis(slave, (int) function, addr, nb, req);
	req_length = STD_ModbusRTU_MSG_crc16(req, req_length);

	return TRUE;
}

static int bulid_write_registers(int slave, int function, int addr, uint16_t *wordValues, int wordlength, uint8_t *req)
{
	int req_length;
	int nb = wordlength * 2;

	if (wordlength > MODBUS_MAX_WRITE_REGISTERS)
	{

		return 0;
	}

	req_length = STD_ModbusRTU_build_request_basis(slave, (int) function, addr, wordlength, req);

	req[req_length++] = (uint8_t) (nb & 0xFF);

	for (int i = 0; i < wordlength; i++)
	{

		req[req_length++] = (uint8_t) ((wordValues[i] >> 8) & 0xFF);
		req[req_length++] = (uint8_t) (wordValues[i] & 0xFF);

	}
	req_length = STD_ModbusRTU_MSG_crc16(req, req_length);

	return req_length;
}

void STD_ModbusRTU_New_Master(int rs485_device)
{

	initCRCTable();
	master_rs485_hardware_port = rs485_device;

}

#define  READ_REQ_LENGTH  8
#define  WRITE_REQ_LENGTH  9

uint8_t modbusbuffer[0x400];



BOOL StandardWordRead_F3(int Slave, int StartAddr, int WordLength, int waitMillisecond, uint16_t *WordDatas)
{

	uint8_t reqdata[READ_REQ_LENGTH];

	if (bulid_read_registers(Slave, MODBUS_FC_READ_HOLDING_REGISTERS, StartAddr, WordLength, reqdata) == TRUE)
	{

		RS485_modbus_write(master_rs485_hardware_port, reqdata, READ_REQ_LENGTH);



		if (STD_ModbusRTU_Master_Polling(waitMillisecond * 1000) == TRUE)
		{
			int readlength = RS485_modbus_read(master_rs485_hardware_port, modbusbuffer, 0x400);

			if (STD_ModbusRTU_check(Slave, modbusbuffer, readlength) > 0)
			{

				for (int i = 0; i < WordLength; i++)
				{
					uint16_t regv = (uint16_t) (modbusbuffer[4 + (i * 2)] << 8 | modbusbuffer[3 + (i * 2)]);
					WordDatas[i] = (uint16_t) regv;
				}


				return TRUE;
			}

		}
		else
		{
			//time out
		}

	}

}


BOOL StandardWordWrite_F10(int Slave, int StartAddr, uint16_t *WordDatas, int Wordlength, int waitMillisecond)
{

	int wbytesize = bulid_write_registers(Slave, MODBUS_FC_WRITE_MULTIPLE_REGISTERS, StartAddr, WordDatas, Wordlength, modbusbuffer);

	RS485_modbus_write(master_rs485_hardware_port, modbusbuffer, wbytesize);





	if (STD_ModbusRTU_Master_Polling(waitMillisecond * 1000) == TRUE)
	{
		int readlength = RS485_modbus_read(master_rs485_hardware_port, modbusbuffer, 0x400);

		if (STD_ModbusRTU_check(Slave, modbusbuffer, readlength) > 0)
		{



			return TRUE;
		}

	}
	else
	{
		//time out
	}

	return FALSE;

}

/******************* (C) COPYRIGHT 2011 STMicroelectronics *****END OF FILE****/
