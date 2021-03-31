using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS485ModbusLibary;

namespace SDKforKSX3267
{

    public enum NUTRIENT_COMMAND
    {
        OPERATION_NUITRIENT_OFF = 0,
        OPERATION_NUITRIENT_ON = 401,
        OPERATION_NUITRIENT_AREA_ON = 402,
        OPERATION_NUITRIENT_PARAM_ON = 403,


    };

    /// <summary>
    /// 양액기 장치
    /// </summary>
    public class NutrientDev
    {

       



        public readonly int REGISTER_READ_START_ADDRESS = 0;
        public readonly int REGISTER_READ_WORD_LENGTH = 0;
        public readonly int REGISTER_WRITE_START_ADDRESS = 0;

        public readonly KSX_Device mDevice;


        public UInt16 status;
        public UInt16 irrigation_area;
        public UInt16 alert_information;
        public UInt16 opid;


        /// <summary>
        /// 양액기장치 생성자
        /// </summary>
        /// <param name="mdev">해당장치의 메타데이터 객체</param>
        public NutrientDev(KSX_Device mdev)
		{


            mDevice = mdev;

            this.REGISTER_READ_START_ADDRESS = mDevice.CommSpec.KS_X_3267_2018.read.starting_register;
            this.REGISTER_WRITE_START_ADDRESS = mDevice.CommSpec.KS_X_3267_2018.write.starting_register;



            if (mDevice.getControllevel() == CONTROL_LEVEL.LV_0)
            {
                this.REGISTER_READ_WORD_LENGTH = 3;
            }
            else 
            {
                this.REGISTER_READ_WORD_LENGTH = 4;
                
            }
			
		}



        /// <summary>
        /// 통신용 메시지 생성한다.
        /// </summary>
        /// <param name="mOperation">명령어코드</param>
        /// <param name="mOpid">제어 OPID</param>
        /// <param name="mStartarea">시작구역</param>
        /// <param name="mEndarea">종료구역</param>
        /// <param name="mOnsec">관수시간</param>
        /// <param name="ec">ec센서값</param>
        /// <param name="ph"> ph센서값</param>
        /// <returns>메시지 byte형 배열</returns>
        public byte[] bulid_control_msg(NUTRIENT_COMMAND mOperation, int mOpid, int mStartarea, int mEndarea, long mOnsec, float ec, float ph)
        {
            byte[] buffer = null;
            
            int mindex = 0;


            //각데이터 형에 맞추어서 타입을 변경함.
            double set_operation = (double)mOperation;
            double set_opid = mOpid;

            double set_startarea = mStartarea;
            double set_endarea = mEndarea;





            switch (mOperation)
            {
                case NUTRIENT_COMMAND.OPERATION_NUITRIENT_ON:
                case NUTRIENT_COMMAND.OPERATION_NUITRIENT_OFF:
                    buffer = new byte[4];

                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_operation, buffer, mindex);
                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_opid, buffer, mindex);
                    break;


                case NUTRIENT_COMMAND.OPERATION_NUITRIENT_AREA_ON:
                    buffer = new byte[12];
                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_operation, buffer, mindex);
                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_opid, buffer, mindex);

                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_startarea, buffer, mindex);
                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_endarea, buffer, mindex);
                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(mOnsec, buffer, mindex);

                    break;

                case NUTRIENT_COMMAND.OPERATION_NUITRIENT_PARAM_ON:
                    buffer = new byte[20];
                   
                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_operation, buffer, mindex);
                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_opid, buffer, mindex);

                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_startarea, buffer, mindex);
                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_endarea, buffer, mindex);
                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(mOnsec, buffer, mindex);

                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(ec, buffer, mindex);
                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(ph, buffer, mindex);


                    break;

            }



            return buffer;

        }

        /// <summary>
        /// 버퍼로부터 양액기장치 정보를 가져온다.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public bool deSerialize(byte[] buffer)
        {
            ByteUtil wrapper = ByteUtil.wrap(buffer);


            if (REGISTER_READ_WORD_LENGTH * 2 != buffer.Length)
            {
                return false;
            }

            this.status = wrapper.getUShort();

            if (mDevice.getControllevel() == CONTROL_LEVEL.LV_0)
            {

            }
            else
            {
                this.irrigation_area =wrapper.getUShort();
                this.alert_information = wrapper.getUShort();
                this.opid = wrapper.getUShort();
            }


            return true;


        }





    }
}
