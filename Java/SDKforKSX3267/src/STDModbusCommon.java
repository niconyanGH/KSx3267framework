

public class STDModbusCommon {

    public static final int MIN_REQ_LENGTH = 12;
    public static final int READ_REQ_LENGTH = 8;
    public static final int WRITE_REQ_LENGTH = 9;
    public static final int MODBUS_MAX_READ_REGISTERS = 125;
    public static final int MODBUS_MAX_WRITE_REGISTERS = 250;

    public static int GetCRC16(byte[] buffer, int buffer_length) {

        int[] auchCRCHi = { 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
                0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
                0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00,
                0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
                0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
                0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
                0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00,
                0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
                0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80,
                0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
                0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00,
                0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
                0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81,
                0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
                0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00,
                0xC1, 0x81, 0x40 };

        int[] auchCRCLo = { 0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7, 0x05, 0xC5, 0xC4,
                0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 0x08, 0xC8,
                0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC, 0x14,
                0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3, 0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30,
                0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32, 0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD,
                0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29,
                0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF, 0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27,
                0xE7, 0xE6, 0x26, 0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1, 0x63, 0xA3,
                0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 0x6E,
                0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA,
                0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5, 0x77, 0xB7, 0xB6, 0x76, 0x72,
                0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0, 0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56,
                0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B,
                0x5B, 0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89, 0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F,
                0x8D, 0x4D, 0x4C, 0x8C, 0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83, 0x41,
                0x81, 0x80, 0x40 };
        int uIndex;
        int uchCRCHi = 0xff;
        int uchCRCLo = 0xff;
        for (int i = 0; i < buffer_length; i++) {
            uIndex = (uchCRCHi ^ buffer[i]) & 0xFF;
            uchCRCHi = (int) ((uchCRCLo ^ auchCRCHi[uIndex]) & 0xFF);
            uchCRCLo = auchCRCLo[uIndex];
        }
        int CRC = (uchCRCHi << 8) | uchCRCLo;

        return CRC;

    }

    static int STD_ModbusRTU_MSG_crc16(byte[] req, int req_length) {
        int crc = GetCRC16(req, req_length);
        req[req_length++] = (byte) (crc >> 8);
        req[req_length++] = (byte) (crc & 0x00FF);

        return req_length;
    }

    static int STD_ModbusRTU_build_request_basis(int slave, int function, int addr, int nb, byte[] req) {

        req[0] = (byte) (slave & 0xFF);
        req[1] = (byte) (function & 0xFF);
        req[2] = (byte) ((addr >> 8) & 0xFF);
        req[3] = (byte) (addr & 0x00ff);
        req[4] = (byte) (nb >> 8);
        req[5] = (byte) (nb & 0x00ff);

        return 6;
    }

    static public byte[] bulid_read_registers(int slave, int function, int addr, int nb) {
        int req_length;
        byte[] req = new byte[READ_REQ_LENGTH];
        if (nb > MODBUS_MAX_READ_REGISTERS) {

            return null;
        }

        req_length = STD_ModbusRTU_build_request_basis(slave, function, addr, nb, req);
        req_length = STD_ModbusRTU_MSG_crc16(req, req_length);

        return req;
    }

    static public byte[] bulid_write_registers(int slave, int function, int addr, byte[] Values) {
        int req_length;
        byte[] req = new byte[WRITE_REQ_LENGTH + (Values.length)];
        if (Values.length > MODBUS_MAX_WRITE_REGISTERS) {

            return null;
        }

        req_length = STD_ModbusRTU_build_request_basis(slave, (int) function, addr, Values.length / 2, req);
        int nb = Values.length;
        req[req_length++] = (byte) (nb & 0xFF);

        for (int i = 0; i < Values.length; i++) {

            req[req_length++] = Values[i];

        }

        req_length = STD_ModbusRTU_MSG_crc16(req, req_length);

        return req;
    }

}
