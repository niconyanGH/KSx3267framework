using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS485ModbusLibary
{
    /// <summary>
    ///  모드버스 기능코드 정의
    /// </summary>
    public class MDFunction
    {
        public const int MODBUS_FC_READ_COILS = 0x01;
        public const int MODBUS_FC_READ_DISCRETE_INPUTS = 0x02;
        public const int MODBUS_FC_READ_HOLDING_REGISTERS = 0x03;     //읽기용
        public const int MODBUS_FC_READ_INPUT_REGISTERS = 0x04;
        public const int MODBUS_FC_WRITE_SINGLE_COIL = 0x05;
        public const int MODBUS_FC_WRITE_SINGLE_REGISTER = 0x06;  // 제어용(write)
        public const int MODBUS_FC_READ_EXCEPTION_STATUS = 0x07;
        public const int MODBUS_FC_WRITE_MULTIPLE_COILS = 0x0F;
        public const int MODBUS_FC_WRITE_MULTIPLE_REGISTERS = 0x10;  // 제어용(write)
        public const int MODBUS_FC_REPORT_SLAVE_ID = 0x11;
        public const int MODBUS_FC_MASK_WRITE_REGISTER = 0x16;
        public const int MODBUS_FC_WRITE_AND_READ_REGISTERS = 0x17;

    }
}
