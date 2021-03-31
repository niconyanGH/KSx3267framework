/* Define to prevent recursive inclusion -------------------------------------*/
#ifndef __RS485_MODBUS_DRIVER_H
#define __RS485_MODBUS_DRIVER_H

#ifdef __cplusplus
extern "C"
  {
#endif 


/// 사용자 정의 메모리맵 지정
#define modbus_readwrite_reg_start_address       0
#define modbus_readwrite_reg_size 				0x2000
#define modbus_readonly_reg_start_address       20000
#define modbus_readonly_reg_size 				0x100

typedef struct modbus_register_user_map_
{

  uint16_t holding_readwrite[modbus_readwrite_reg_size]; /// 표준사용레지스터
  uint16_t input_readonly[modbus_readonly_reg_size];  //표쥰 사용안함

} modbus_register_user_map;

///////////////////////////////////////////////////////////////////////////////////////////////////







/* Modbus function codes */
#define MODBUS_FC_READ_COILS                0x01
#define MODBUS_FC_READ_DISCRETE_INPUTS      0x02
#define MODBUS_FC_READ_HOLDING_REGISTERS    0x03
#define MODBUS_FC_READ_INPUT_REGISTERS      0x04
#define MODBUS_FC_WRITE_SINGLE_COIL         0x05
#define MODBUS_FC_WRITE_SINGLE_REGISTER     0x06   // 제어용(write)
#define MODBUS_FC_READ_EXCEPTION_STATUS     0x07
#define MODBUS_FC_WRITE_MULTIPLE_COILS      0x0F
#define MODBUS_FC_WRITE_MULTIPLE_REGISTERS  0x10  // 제어용(write)
#define MODBUS_FC_REPORT_SLAVE_ID           0x11
#define MODBUS_FC_MASK_WRITE_REGISTER       0x16
#define MODBUS_FC_WRITE_AND_READ_REGISTERS  0x17

#define MODBUS_BROADCAST_ADDRESS    0




#define MODBUS_MAX_READ_REGISTERS          125
#define MODBUS_MAX_WRITE_REGISTERS         123
#define MODBUS_MAX_WR_WRITE_REGISTERS      121
#define MODBUS_MAX_WR_READ_REGISTERS       125


#define MODBUS_RTU_MAX_ADU_LENGTH  			260



/* Protocol exceptions */
enum
{
  MODBUS_EXCEPTION_ILLEGAL_FUNCTION = 0x01,
  MODBUS_EXCEPTION_ILLEGAL_DATA_ADDRESS=0x2,
  MODBUS_EXCEPTION_ILLEGAL_DATA_VALUE=0x3,
  MODBUS_EXCEPTION_SLAVE_OR_SERVER_FAILURE=0x4,
  MODBUS_EXCEPTION_ACKNOWLEDGE=0x5,
  MODBUS_EXCEPTION_SLAVE_OR_SERVER_BUSY=0x6,
  MODBUS_EXCEPTION_MAX
};




typedef struct
{

  int nsize_input;
  int startaddress_input;
  int nsize_holding;
  int startaddress_holding;
  uint16_t *buffer_input;
  uint16_t *buffer_holding;
} modbus_map;



typedef struct _modbus
{

  int slave; // 국번, 슬레이브 주소
  int rs485_hardware_port;  // 하드웨어 포트 인식번호
  modbus_map mRegsiterMAP;

}modbus_t;







void STD_ModbusRTU_New_Slave(int rs485_device,int mslave, modbus_register_user_map *regmap);
int STD_ModbusRTU_receive( uint8_t *req);
int STD_ModbusRTU_reply ( const uint8_t *req, int req_length);






#ifdef __cplusplus
}
#endif

#endif

/******************* (C) COPYRIGHT 2011 STMicroelectronics *****END OF FILE****/
