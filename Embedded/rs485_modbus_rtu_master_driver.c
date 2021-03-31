/* Includes ------------------------------------------------------------------*/

#include "rs485_modbus_rtu_driver.h"

#include <stdio.h>
#include <string.h>
#include <stdlib.h>

//kbm �ɰ������� ����
static modbus_mapping_t my_mb_map;

static int ErrorNumber = 0;

static uint8_t table_crc_hi[0x100];
static uint8_t table_crc_lo[0x100];

modbus_t local_modbus_t;




static void initCRCTable()
{
		int crc_data;
		int  crc_crc;

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
			table_crc_hi[crc_i] = (uint8_t)(crc_crc & 0xFF);
			table_crc_lo[crc_i] = (uint8_t)((crc_crc >> 8) & 0xFF);

		}
}

static int GetCRC16(uint8_t *buffer, int buffer_length)
{
	int index;
	char crc_hi = 0xFF;
	char crc_lo = 0xFF;

	for(int i=0;i<buffer_length ;i++)
	{
		index = crc_hi ^ buffer[i];
		crc_hi = crc_lo ^ table_crc_hi[index];
		crc_lo = table_crc_lo[index];
	}

	return (crc_hi << 8 | crc_lo);

}


static int STD_ModbusRTU_MSG_crc16(uint8_t *req, int req_length)
{
	int  crc = GetCRC16(req, req_length);
	req[req_length++] = crc >> 8;
	req[req_length++] = crc & 0x00FF;

	return req_length;
}

/* Builds a RTU request header */
static int STD_ModbusRTU_build_request_basis(int mslave, int function, int addr, int nb, uint8_t *req)
{

	req[0] = mslave;
	req[1] = function;
	req[2] = addr >> 8;
	req[3] = addr & 0x00ff;
	req[4] = nb >> 8;
	req[5] = nb & 0x00ff;

	return 6;
}



static int STD_ModbusRTU_send(modbus_t *ctx, uint8_t *req, int req_length)
{

	return RS485_modbus_write(ctx->rs485_device, req, req_length);

	//  return write(ctx->s, req, req_length);

}

static int STD_ModbusRTU_receive(modbus_t *ctx, uint8_t *req)
{
	int rc;
	//modbus_rtu_t *ctx_rtu = ctx->confirmation_to_ignore;

	if (ctx->confirmation_to_ignore)
	{
		_modbus_receive_msg(ctx, req, MSG_CONFIRMATION);
		/* Ignore errors and reset the flag */
		ctx->confirmation_to_ignore = FALSE;
		rc = 0;




	}
	else
	{
		rc = _modbus_receive_msg(ctx, req, MSG_INDICATION);
		if (rc == 0)
		{
			/* The next expected message is a confirmation to ignore */
			ctx->confirmation_to_ignore = TRUE;
		}


	}
	return rc;
}

static int STD_ModbusRTU_recv(modbus_t *ctx, uint8_t *rsp, int rsp_length)
{

	return RS485_modbus_read(ctx->rs485_device, rsp, rsp_length);

	//  return read(ctx->rs485_device, rsp, rsp_length);

}

static int STD_ModbusRTU_ID_check(uint8_t *req, uint8_t *rsp)
{
	/* Check responding slave is the slave we requested (except for broacast
	 * request) */
	if (req[0] != rsp[0] && req[0] != MODBUS_BROADCAST_ADDRESS)
	{

		ErrorNumber = EMBBADSLAVE;
		return -1;
	}
	else
	{
		return 0;
	}
}

static int STD_ModbusRTU_check_integrity(int mslave, uint8_t *msg, const int msg_length)
{
	uint16_t crc_calculated;
	uint16_t crc_received;
	int slave = msg[0];


	if (slave != mslave  && slave != MODBUS_BROADCAST_ADDRESS)
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

		ErrorNumber = EMBBADCRC;
		return -1;
	}

}


modbus_t* modbus_new_rtu(int rs485_device, int mslave, modbus_register_map *regmap)
{
	modbus_t *ctx;

	initCRCTable();

	ctx = (modbus_t *) &local_modbus_t;

	ctx->slave = mslave;
	ctx->rs485_device = rs485_device;
	ctx->confirmation_to_ignore = FALSE;

	modbus_register_mapping_slave(modbus_readwrite_reg_start_address, modbus_readwrite_reg_size, modbus_readonly_reg_start_address, modbus_readonly_reg_size, regmap);

	return ctx;
}





/* 3 steps are used to parse the query */
typedef enum
{
	_STEP_FUNCTION, _STEP_META, _STEP_DATA
} _step_t;
/*
 char *modbus_strerror(int errnum)
 {

 return "ME"; //kbm code size ���̱�

 switch (errnum) {
 case EMBXILFUN:
 return "Illegal function";
 case EMBXILADD:
 return "Illegal data address";
 case EMBXILVAL:
 return "Illegal data value";
 case EMBXSFAIL:
 return "Slave device or server failure";
 case EMBXACK:
 return "Acknowledge";
 case EMBXSBUSY:
 return "Slave device or server is busy";
 case EMBXNACK:
 return "Negative acknowledge";
 case EMBXMEMPAR:
 return "Memory parity error";
 case EMBXGPATH:
 return "Gateway path unavailable";
 case EMBXGTAR:
 return "Target device failed to respond";
 case EMBBADCRC:
 return "Invalid CRC";
 case EMBBADDATA:
 return "Invalid data";
 case EMBBADEXC:
 return "Invalid exception code";
 case EMBMDATA:
 return "Too many data";
 case EMBBADSLAVE:
 return "Response not from requested slave";
 default:
 return strerror(errnum);
 }

 }

 */


static unsigned int compute_response_length_from_request( uint8_t *req)
{
	int length=0;
	const int offset = 1;

	switch (req[offset])
	{
		case MODBUS_FC_READ_COILS:
		case MODBUS_FC_READ_DISCRETE_INPUTS:

			break;
		case MODBUS_FC_WRITE_AND_READ_REGISTERS:
		case MODBUS_FC_READ_HOLDING_REGISTERS:
		case MODBUS_FC_READ_INPUT_REGISTERS:
			/* Header + 2 * nb values */
			length = 2 + 2 * (req[offset + 3] << 8 | req[offset + 4]);
			break;
		case MODBUS_FC_READ_EXCEPTION_STATUS:
			length = 3;
			break;
		case MODBUS_FC_REPORT_SLAVE_ID:

			return -1;
		case MODBUS_FC_MASK_WRITE_REGISTER:
			length = 7;
			break;
		default:
			length = 5;
	}

	return offset + length + 2;
}

/* Sends a request/response */
static int send_msg(modbus_t *ctx, uint8_t *msg, int msg_length)
{
	int rc;
	msg_length = STD_ModbusRTU_MSG_crc16(msg, msg_length);

	rc = STD_ModbusRTU_send(ctx, msg, msg_length);
	if (rc == -1)
	{

	}

	if (rc > 0 && rc != msg_length)
	{
		ErrorNumber = EMBBADDATA;
		return -1;
	}

	return rc;
}

/*
 *  ---------- Request     Indication ----------
 *  | Client | ---------------------->| Server |
 *  ---------- Confirmation  Response ----------
 */

/* Computes the length to read after the function received */
static uint8_t compute_meta_length_after_function(int function, msg_type_t msg_type)
{
	int length = 0;

	if (msg_type == MSG_INDICATION)
	{
		if (function <= MODBUS_FC_WRITE_SINGLE_REGISTER)
		{
			length = 4;
		}
		else if (function == MODBUS_FC_WRITE_MULTIPLE_COILS || function == MODBUS_FC_WRITE_MULTIPLE_REGISTERS)
		{
			length = 5;
		}
		else if (function == MODBUS_FC_MASK_WRITE_REGISTER)
		{
			length = 6;
		}
		else if (function == MODBUS_FC_WRITE_AND_READ_REGISTERS)
		{
			length = 9;
		}

	}
	else
	{
		/* MSG_CONFIRMATION */
		switch (function)
		{
			case MODBUS_FC_WRITE_SINGLE_COIL:
			case MODBUS_FC_WRITE_SINGLE_REGISTER:
			case MODBUS_FC_WRITE_MULTIPLE_COILS:
			case MODBUS_FC_WRITE_MULTIPLE_REGISTERS:
				length = 4;
				break;
			case MODBUS_FC_MASK_WRITE_REGISTER:
				length = 6;
				break;
			default:
				length = 1;
		}
	}

	return length;
}

/* Computes the length to read after the meta information (address, count, etc) */
static int compute_data_length_after_meta( uint8_t *msg, msg_type_t msg_type)
{
	int function = msg[1];
	int length;

	if (msg_type == MSG_INDICATION)
	{
		switch (function)
		{
			case MODBUS_FC_WRITE_MULTIPLE_COILS:
			case MODBUS_FC_WRITE_MULTIPLE_REGISTERS:
				length = msg[6];
				break;
			case MODBUS_FC_WRITE_AND_READ_REGISTERS:
				length = msg[10];
				break;
			default:
				length = 0;
		}
	}
	else
	{

		if (function <= MODBUS_FC_READ_INPUT_REGISTERS || function == MODBUS_FC_REPORT_SLAVE_ID || function == MODBUS_FC_WRITE_AND_READ_REGISTERS)
		{
			length = msg[2];
		}
		else
		{
			length = 0;
		}
	}

	length += 2;

	return length;
}

/* Waits a response from a modbus server or a request from a modbus client.
 This function blocks if there is no replies (3 timeouts).

 The function shall return the number of received characters and the received
 message in an array of uint8_t if successful. Otherwise it shall return -1
 and errno is set to one of the values defined below:
 - ECONNRESET
 - EMBBADDATA
 - EMBUNKEXC
 - ETIMEDOUT
 - read() or recv() error codes
 */

int _modbus_receive_msg(modbus_t *ctx, uint8_t *msg, msg_type_t msg_type)
{

	int length_to_read;
	int rc;
	int tv_response_timeout = 1000;
	int msg_length = 0;
	_step_t step;

	/* We need to analyse the message step by step.  At the first step, we want
	 * to reach the function code because all packets contain this
	 * information. */
	step = _STEP_FUNCTION;
	length_to_read =2;

	while (length_to_read != 0)
	{

		rc = STD_ModbusRTU_recv(ctx, msg + msg_length, length_to_read);

		msg_length += rc;
		/* Computes remaining bytes */
		length_to_read -= rc;

		if (length_to_read <= 0)
		{
			switch (step)
			{
				case _STEP_FUNCTION:
					/* Function code position */
					length_to_read = compute_meta_length_after_function(msg[1], msg_type);
					if (length_to_read != 0)
					{
						step = _STEP_META;
						tv_response_timeout = 1000;
						break;
					} /* else switches straight to the next step */
				case _STEP_META:
					length_to_read = compute_data_length_after_meta( msg, msg_type);
					if ((msg_length + length_to_read) > (int) MODBUS_RTU_MAX_ADU_LENGTH)
					{
						ErrorNumber = EMBBADDATA;

						return -1;
					}
					step = _STEP_DATA;
					tv_response_timeout = 1000;
					break;
				default:
					break;
			}
		}

		if (tv_response_timeout > 0)
		{

			TimerTickDelay_us(10);
			tv_response_timeout -= 10;
		}
		else
		{

			ErrorNumber = EMBBADDATA;
			return -1;

		}

	}

	return STD_ModbusRTU_check_integrity(ctx->slave, msg, msg_length);

}

/* Receive the request from a modbus master */
int modbus_receive(modbus_t *ctx, uint8_t *req)
{

	return STD_ModbusRTU_receive(ctx, req);
}

static int check_confirmation( uint8_t *req, uint8_t *rsp, int rsp_length)
{
	int rc;
	int rsp_length_computed;
	const int offset = 1;
	const int function = rsp[offset];

	rc = STD_ModbusRTU_ID_check(req,rsp);
	if (rc == -1)
	{

		return -1;
	}

	rsp_length_computed = compute_response_length_from_request(req);

	/* Exception code */
	if (function >= 0x80)
	{
		if (rsp_length == (offset + 2 + (int) 2) && req[offset] == (rsp[offset] - 0x80))
		{
			/* Valid exception code received */

			int exception_code = rsp[offset + 1];
			if (exception_code < MODBUS_EXCEPTION_MAX)
			{
				ErrorNumber = MODBUS_ENOBASE + exception_code;
			}
			else
			{
				ErrorNumber = EMBBADEXC;
			}

			return -1;
		}
		else
		{
			ErrorNumber = EMBBADEXC;

			return -1;
		}
	}

	/* Check length */
	if ((rsp_length == rsp_length_computed || rsp_length_computed == -1) && function < 0x80)
	{
		int req_nb_value=0;
		int rsp_nb_value=0;

		/* Check function code */
		if (function != req[offset])
		{

			ErrorNumber = EMBBADDATA;
			return -1;
		}

		/* Check the number of values is corresponding to the request */
		switch (function)
		{
			case MODBUS_FC_READ_COILS:
			case MODBUS_FC_READ_DISCRETE_INPUTS:

				break;
			case MODBUS_FC_WRITE_AND_READ_REGISTERS:
			case MODBUS_FC_READ_HOLDING_REGISTERS:
			case MODBUS_FC_READ_INPUT_REGISTERS:
				/* Read functions 1 value = 2 bytes */
				req_nb_value = (req[offset + 3] << 8) + req[offset + 4];
				rsp_nb_value = (rsp[offset + 1] / 2);
				break;
			case MODBUS_FC_WRITE_MULTIPLE_COILS:
			case MODBUS_FC_WRITE_MULTIPLE_REGISTERS:
				/* N Write functions */
				req_nb_value = (req[offset + 3] << 8) + req[offset + 4];
				rsp_nb_value = (rsp[offset + 3] << 8) | rsp[offset + 4];
				break;
			case MODBUS_FC_REPORT_SLAVE_ID:

				break;
			default:
				/* 1 Write functions & others */
				req_nb_value = rsp_nb_value = 1;
		}

		if (req_nb_value == rsp_nb_value)
		{
			rc = rsp_nb_value;
		}
		else
		{

			ErrorNumber = EMBBADDATA;
			rc = -1;
		}
	}
	else
	{

		ErrorNumber = EMBBADDATA;
		rc = -1;
	}

	return rc;
}


static int bulid_response_exception(int mslave, int mfunction, int exception_code, uint8_t *rsp)
{
	int rsp_length = 0;
	rsp[rsp_length++] = mslave;
	rsp[rsp_length++] = mfunction + 0x80;
	rsp[rsp_length++] = exception_code;

	return rsp_length;
}

int modbus_reply(modbus_t *ctx, const uint8_t *req, int req_length)
{
	//int offset;
	int slave;
	int function;
	uint16_t address;
	uint8_t rsp[MAX_MESSAGE_LENGTH];
	int rsp_length = 0;


	modbus_mapping_t *mb_mapping = &my_mb_map;

	//offset = 1;
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
				start_registers = mb_mapping->start_input_registers;
				nb_registers = mb_mapping->nb_input_registers;
				tab_registers = mb_mapping->tab_input_registers;
			}
			else
			{
				start_registers = mb_mapping->start_registers;
				nb_registers = mb_mapping->nb_registers;
				tab_registers = mb_mapping->tab_registers;
			}

			int nb = (req[4] << 8) + req[5];
			/* The mapping can be shifted to reduce memory consumption and it
			 doesn't always start at address zero. */
			int mapping_address = address - start_registers;

			if (nb < 1 || MODBUS_MAX_READ_REGISTERS < nb)
			{
				rsp_length = bulid_response_exception(slave, function, MODBUS_EXCEPTION_ILLEGAL_DATA_VALUE, rsp);
			}
			else if (mapping_address < 0 || (mapping_address + nb) > nb_registers)
			{
				rsp_length = bulid_response_exception( slave, function, MODBUS_EXCEPTION_ILLEGAL_DATA_ADDRESS, rsp);

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
		case MODBUS_FC_WRITE_SINGLE_COIL:

			break;
		case MODBUS_FC_WRITE_SINGLE_REGISTER:
		{
			int mapping_address = address - mb_mapping->start_registers;

			if (mapping_address < 0 || mapping_address >= mb_mapping->nb_registers)
			{
				rsp_length = bulid_response_exception( slave, function, MODBUS_EXCEPTION_ILLEGAL_DATA_ADDRESS, rsp);
			}
			else
			{
				int data = (req[4] << 8) + req[5];
				mb_mapping->tab_registers[mapping_address] = data;
				memcpy(rsp, req, req_length);
				rsp_length = req_length;
			}
		}
			break;
		case MODBUS_FC_WRITE_MULTIPLE_COILS:
			break;
		case MODBUS_FC_WRITE_MULTIPLE_REGISTERS:
		{
			int nb = (req[4] << 8) + req[5];
			int bytecount = req[6];
			int mapping_address = address - mb_mapping->start_registers;

			if (nb < 1 || MODBUS_MAX_WRITE_REGISTERS < nb)
			{
				rsp_length = bulid_response_exception( slave, function, MODBUS_EXCEPTION_ILLEGAL_DATA_VALUE, rsp);
			}
			else if (mapping_address < 0 || (mapping_address + nb) > mb_mapping->nb_registers)
			{
				rsp_length = bulid_response_exception( slave, function, MODBUS_EXCEPTION_ILLEGAL_DATA_ADDRESS, rsp);
			}
			else
			{
				int i, j;
				for (i = mapping_address, j = 7; i <( mapping_address + nb); i++, j += 2)
				{
					/* 6 and 7 = first value */
					mb_mapping->tab_registers[i] = (req[j] << 8) + req[ j +1];
				}

				rsp_length = 0;
				rsp[rsp_length++] = slave;
				rsp[rsp_length++] = function;

				/* 4 to copy the address (2) and the no. of registers */
				memcpy(rsp + rsp_length, req + rsp_length, 4);
				rsp_length += 4;
			}
		}
			break;

		case MODBUS_FC_REPORT_SLAVE_ID:

			break;

		case MODBUS_FC_READ_EXCEPTION_STATUS:

			ErrorNumber = ENOPROTOOPT;
			return -1;
			break;
		case MODBUS_FC_MASK_WRITE_REGISTER:

			break;
		case MODBUS_FC_WRITE_AND_READ_REGISTERS:

			break;

		default:
			rsp_length = bulid_response_exception( slave, function, MODBUS_EXCEPTION_ILLEGAL_FUNCTION, rsp);
			break;
	}

	/* Suppress any responses when the request was a broadcast */
	return (slave == MODBUS_BROADCAST_ADDRESS) ? 0 : send_msg(ctx, rsp, rsp_length);
}

int modbus_reply_exception(modbus_t *ctx, const uint8_t *req, unsigned int exception_code)
{
	int offset;
	int slave;
	int function;
	uint8_t rsp[MAX_MESSAGE_LENGTH];
	int rsp_length = 0;

	offset = 1;
	slave = req[offset - 1];
	function = req[offset];

	rsp[rsp_length++] = slave;
	rsp[rsp_length++] = function + 0x80;

	/* Positive exception code */
	if (exception_code < MODBUS_EXCEPTION_MAX)
	{
		rsp[rsp_length++] = exception_code;
		return send_msg(ctx, rsp, rsp_length);
	}
	else
	{
		ErrorNumber = EINVAL;
		return -1;
	}
}

/* Reads the data from a remove device and put that data into an array */
static int read_registers(modbus_t *ctx, int function, int addr, int nb, uint16_t *dest)
{
	int rc;
	int req_length;
	uint8_t req[MIN_REQ_LENGTH];
	uint8_t rsp[MAX_MESSAGE_LENGTH];

	if (nb > MODBUS_MAX_READ_REGISTERS)
	{

		ErrorNumber = EMBMDATA;
		return -1;
	}

	req_length = STD_ModbusRTU_build_request_basis(ctx->slave, function, addr, nb, req);

	rc = send_msg(ctx, req, req_length);
	if (rc > 0)
	{
		int offset;
		int i;

		rc = _modbus_receive_msg(ctx, rsp, MSG_CONFIRMATION);
		if (rc == -1) return -1;

		rc = check_confirmation( req, rsp, rc);
		if (rc == -1) return -1;

		offset = 1;

		for (i = 0; i < rc; i++)
		{
			/* shift reg hi_byte to temp OR with lo_byte */
			dest[i] = (rsp[offset + 2 + (i << 1)] << 8) | rsp[offset + 3 + (i << 1)];
		}
	}

	return rc;
}

/* Reads the holding registers of remote device and put the data into an
 array */
int modbus_read_registers(modbus_t *ctx, int addr, int nb, uint16_t *dest)
{
	int status;

	if (nb > MODBUS_MAX_READ_REGISTERS)
	{

		ErrorNumber = EMBMDATA;
		return -1;
	}

	status = read_registers(ctx, MODBUS_FC_READ_HOLDING_REGISTERS, addr, nb, dest);
	return status;
}

/* Reads the input registers of remote device and put the data into an array */
int modbus_read_input_registers(modbus_t *ctx, int addr, int nb, uint16_t *dest)
{
	int status;

	if (nb > MODBUS_MAX_READ_REGISTERS)
	{

		ErrorNumber = EMBMDATA;
		return -1;
	}

	status = read_registers(ctx, MODBUS_FC_READ_INPUT_REGISTERS, addr, nb, dest);

	return status;
}

/* Write a value to the specified register of the remote device.
 Used by write_bit and write_register */
static int write_single(modbus_t *ctx, int function, int addr, int value)
{
	int rc;
	int req_length;
	uint8_t req[MIN_REQ_LENGTH];

	req_length = STD_ModbusRTU_build_request_basis(ctx->slave, function, addr, value, req);

	rc = send_msg(ctx, req, req_length);
	if (rc > 0)
	{
		/* Used by write_bit and write_register */
		uint8_t rsp[MAX_MESSAGE_LENGTH];

		rc = _modbus_receive_msg(ctx, rsp, MSG_CONFIRMATION);
		if (rc == -1) return -1;

		rc = check_confirmation( req, rsp, rc);
	}

	return rc;
}

/* Writes a value in one register of the remote device */
int modbus_write_register(modbus_t *ctx, int addr, int value)
{

	return write_single(ctx, MODBUS_FC_WRITE_SINGLE_REGISTER, addr, value);
}

/* Write the values from the array to the registers of the remote device */
int modbus_write_registers(modbus_t *ctx, int addr, int nb, const uint16_t *src)
{
	int rc;
	int i;
	int req_length;
	int byte_count;
	uint8_t req[MAX_MESSAGE_LENGTH];

	if (nb > MODBUS_MAX_WRITE_REGISTERS)
	{
		ErrorNumber = EMBMDATA;
		return -1;
	}


	req_length = STD_ModbusRTU_build_request_basis(ctx->slave, MODBUS_FC_WRITE_MULTIPLE_REGISTERS, addr, nb, req);
	byte_count = nb * 2;
	req[req_length++] = byte_count;

	for (i = 0; i < nb; i++)
	{
		req[req_length++] = src[i] >> 8;
		req[req_length++] = src[i] & 0x00FF;
	}

	rc = send_msg(ctx, req, req_length);
	if (rc > 0)
	{
		uint8_t rsp[MAX_MESSAGE_LENGTH];

		rc = _modbus_receive_msg(ctx, rsp, MSG_CONFIRMATION);
		if (rc == -1) return -1;

		rc = check_confirmation( req, rsp, rc);
	}

	return rc;
}


///��ü �޸� �������� �ɻ���� 1kbyte�� ���� �ʵ��� �����ؾ���
void modbus_register_mapping_slave(unsigned int start_registers, unsigned int nb_registers, unsigned int start_input_registers, unsigned int nb_input_registers, modbus_register_map *regmap)
{

	modbus_mapping_t *mb_mapping;
	mb_mapping = (modbus_mapping_t *) &my_mb_map;

	mb_mapping->nb_registers = nb_registers;
	mb_mapping->start_registers = start_registers;
	if (nb_registers == 0)
	{
		mb_mapping->tab_registers = NULL;
	}
	else
	{
		mb_mapping->tab_registers = (uint16_t *) regmap->registers_readwrite;
		memset(mb_mapping->tab_registers, 0, nb_registers * sizeof(uint16_t));
	}

	mb_mapping->nb_input_registers = nb_input_registers;
	mb_mapping->start_input_registers = start_input_registers;
	if (nb_input_registers == 0)
	{
		mb_mapping->tab_input_registers = NULL;
	}
	else
	{
		mb_mapping->tab_input_registers = (uint16_t*) regmap->registers_readonly;
		memset(mb_mapping->tab_input_registers, 0, nb_input_registers * sizeof(uint16_t));

	}


}

/******************* (C) COPYRIGHT 2011 STMicroelectronics *****END OF FILE****/
