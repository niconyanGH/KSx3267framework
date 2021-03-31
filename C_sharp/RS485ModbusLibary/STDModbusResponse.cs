using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS485ModbusLibary
{
    /// <summary>
    /// 모드버스 응답메시지에 대한 객체
    /// </summary>
    public class STDModbusResponse
    {
        /// <summary>
        /// 응답기능코드
        /// </summary>
        public int rep_function;
        /// <summary>
        /// 응답메시지 크기
        /// </summary>
        public int byte_length;
        /// <summary>
        /// 응답 바이트버퍼
        /// </summary>
        public byte[] byteDatas;
        /// <summary>
        /// 응답데이터  short 형 버퍼
        /// </summary>
        public int[] wordDatas;
        /// <summary>
        /// 응답데이터의 float형 버퍼
        /// </summary>
        public float[] floatDatas;
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="mResponseMsg">모드버스 응답메시지 버퍼</param>
        public STDModbusResponse(byte[] mResponseMsg)
        {
            byte_length = mResponseMsg[2];


            rep_function = (int)mResponseMsg[1];
            bool is_reg_read = false;

            switch (rep_function)
            {

                case MDFunction.MODBUS_FC_READ_HOLDING_REGISTERS:
                case MDFunction.MODBUS_FC_READ_INPUT_REGISTERS:

                    is_reg_read = true;
                    break;
                default:

                    break;
            }



            if (is_reg_read == true)
            {
                byteDatas = new byte[byte_length];
                wordDatas = new int[byte_length / 2];


                for (int i = 0; i < wordDatas.Length; i++)
                {
                    byteDatas[i * 2 + 1] = mResponseMsg[3 + i * 2];
                    byteDatas[i * 2 + 0] = mResponseMsg[4 + i * 2];
                }
                for (int i = 0; i < wordDatas.Length; i++)
                {
                    short regv = (short)(mResponseMsg[3 + (i * 2)] << 8 | mResponseMsg[4 + (i * 2)]);
                    wordDatas[i] = (int)regv;
                }
                if (byte_length >= 4)
                {
                    floatDatas = new float[byte_length / 4];
                    byte[] bytefloat = new byte[4];


                    for (int i = 0; i < floatDatas.Length; i++)
                    {
                        bytefloat[0] = mResponseMsg[4 + (i * 4)];
                        bytefloat[1] = mResponseMsg[3 + (i * 4)];
                        bytefloat[2] = mResponseMsg[6 + (i * 4)];
                        bytefloat[3] = mResponseMsg[5 + (i * 4)];

                        floatDatas[i] = BitConverter.ToSingle(bytefloat, 0);

                    }

                }
            }

        }
    }
}
