using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using RS485ModbusLibary;

namespace SDKforKSX3267
{


    public enum NODETYPE
    {
        NT_SENSOR = 0,
        NT_ACTUATOR = 1,
        NT_NUTRIENT = 2,
        
    };


    public enum CONTROL_LEVEL
    {
        LV_0 = 0,
        LV_1 = 1,
        LV_2 = 2,
        LV_3 = 3,
    };


    public enum ACTUATOR_TYPE
    {
        SWITCH = 0,
        RETRACTABLE = 1,
        NUTRIENT = 2,
        
    };


    public enum NODECONTROL_COMMAND
    {
        OPERATION_LOCAL = 1,
        OPERATION_REMOTE = 2,
        OPERATION_MANUAL = 3,


    };


    public enum ACTUATOR_COMMAND
    {
        OPERATION_OFF_STOP = 0,
        OPERATION_SWITCH_ON = 201,
        OPERATION_SWITCH_TIMED_ON = 202,
        OPERATION_SWITCH_DIREATIONAL_ON = 203,

        OPERATION_RETRACTABLE_OPEN = 301,
        OPERATION_RETRACTABLE_CLOSE = 302,
        OPERATION_RETRACTABLE_TIMED_OPEN = 303,
        OPERATION_RETRACTABLE_TIMED_CLOSE = 304,
        OPERATION_RETRACTABLE_SET_POSITION = 305,
        OPERATION_RETRACTABLE_SET_CONFIG = 306,


    };


    public enum STATUS_CODE
    {
        //공통
        READY = 0,
        ERROR = 1,
        BUSY = 2,
        VOLTAGE_ERROR = 3,
        CURRENT_ERROR = 4,
        TEMPERATURE_ERROR = 5,
        FUSE_ERROR = 6,
        COMMON_RESERVED = 7,

        //센서
        SENSOR_NEED_REPLACE = 101,
        SENSOR_NEED_CALIBRATION = 102,
        SENSOR_NEED_CHECK = 103,

        //구동기
        SWITCH_ON = 201,
        SWITCH_USER_CONTROL = 299,

        REACTABLE_OPENING = 301,
        REACTABLE_CLOSING = 302,
        REACTABLE_MANUAL_CONTROL = 399,
        //양액기

        NUTRIENT_PREPARING = 401,
        NUTRIENT_SUPPLYING = 402,
        NUTRIENT_STOPPING = 403,

        

        //기타
        VENDOR_SPECIFIC_ERROR = 900,




    };




    /// <summary>
    /// 구동기 디바이스
    /// </summary>
    public class ActuatorDev
    {
       


        
        public readonly int REGISTER_READ_START_ADDRESS = 0;
        public readonly int REGISTER_READ_WORD_LENGTH = 0;
        public readonly int REGISTER_WRITE_START_ADDRESS = 0;

        public readonly KSX_Device mDevice;


        private readonly bool is_hold_time;
        private readonly bool is_position;

      
        

        public  UInt16 status;
        public  UInt16 opid;
        public  UInt32 remain_time;
        public  UInt16 position;
        public  UInt32 hold_time;
        public  UInt16 ratio;



        public byte[] bulid_control_msg(ACTUATOR_COMMAND mOperation, int mOpid, long mHoldtime, short mRatio, int mPosition, int opentime, int closetime)
        {
            byte[] buffer=null;
            
            int mindex = 0;


            //각데이터 형에 맞추어서 타입을 변경함.
            double set_operation = (double)mOperation;
            double set_opid = mOpid;
            double set_position = mPosition;

            double set_opentime = opentime;
            double set_closetime = closetime;






            switch (mOperation)
            {
                case ACTUATOR_COMMAND.OPERATION_OFF_STOP:
                case ACTUATOR_COMMAND.OPERATION_SWITCH_ON:
                case ACTUATOR_COMMAND.OPERATION_RETRACTABLE_CLOSE:
                case ACTUATOR_COMMAND.OPERATION_RETRACTABLE_OPEN:
                    buffer = new byte[4];

                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_operation, buffer, mindex);
                    mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_opid, buffer, mindex);
                        
                    break;

                case ACTUATOR_COMMAND.OPERATION_SWITCH_TIMED_ON:
                case ACTUATOR_COMMAND.OPERATION_RETRACTABLE_TIMED_CLOSE:
                case ACTUATOR_COMMAND.OPERATION_RETRACTABLE_TIMED_OPEN:
                    if (mDevice.getControllevel() >= CONTROL_LEVEL.LV_1)
                    {
                        buffer = new byte[8];
                        mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_operation, buffer, mindex);
                        mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_opid, buffer, mindex);
                        mindex += ByteUtil.cast_value_to_bytes_insert_buffer(mHoldtime, buffer, mindex);

                        ByteUtil.Debugout_hexstring(buffer);

                    }
                    break;

                case ACTUATOR_COMMAND.OPERATION_SWITCH_DIREATIONAL_ON:
                    if (mDevice.getControllevel() >= CONTROL_LEVEL.LV_2)
                    {
                        buffer = new byte[10];

                        mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_operation, buffer, mindex);
                        mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_opid, buffer, mindex);
                        mindex += ByteUtil.cast_value_to_bytes_insert_buffer(mHoldtime, buffer, mindex);
                        mindex += ByteUtil.cast_value_to_bytes_insert_buffer(mRatio, buffer, mindex);


                    }
                    break;



                case ACTUATOR_COMMAND.OPERATION_RETRACTABLE_SET_POSITION:
                    if (mDevice.getControllevel() >= CONTROL_LEVEL.LV_2)
                    {
                        buffer = new byte[6];
                        mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_operation, buffer, mindex);
                        mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_opid, buffer, mindex);
                        mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_position, buffer, mindex);
                    }
                    break;




                case ACTUATOR_COMMAND.OPERATION_RETRACTABLE_SET_CONFIG:
                    if (mDevice.getControllevel() >= CONTROL_LEVEL.LV_2)
                    {
                        buffer = new byte[8];

                        mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_operation, buffer, mindex);
                        mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_opid, buffer, mindex);
                        mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_opentime, buffer, mindex);
                        mindex += ByteUtil.cast_value_to_bytes_insert_buffer(set_closetime, buffer, mindex);



                    }
                    break;

              

            }



            return buffer;

        }






        public ActuatorDev(KSX_Device mdev)
		{


            mDevice = mdev;

            this.REGISTER_READ_START_ADDRESS = mDevice.CommSpec.KS_X_3267_2018.read.starting_register;
            this.REGISTER_WRITE_START_ADDRESS = mDevice.CommSpec.KS_X_3267_2018.write.starting_register;

            this.is_hold_time = false;
            this.is_position = false;


            
            if (mDevice.getControllevel() == (int)CONTROL_LEVEL.LV_0)
            {
                this.REGISTER_READ_WORD_LENGTH = 1;
            }
            else if (mDevice.getControllevel() >=CONTROL_LEVEL.LV_1)
            {
                if (mDevice.getActuatorType() == ACTUATOR_TYPE.RETRACTABLE)
                {

                    int sizeholdtime = KSX326xMetadata.getsizeWithItemcheck(mDevice.CommSpec.KS_X_3267_2018.read.items, KSX326xMetadata.ELEMENT_stateholdtime, 2);
                    int sizeposition = KSX326xMetadata.getsizeWithItemcheck(mDevice.CommSpec.KS_X_3267_2018.read.items, KSX326xMetadata.ELEMENT_position, 2);

                    if (sizeholdtime > 0)
                    {
                        is_hold_time = true;
                    }

                    if (mDevice.getControllevel() >=CONTROL_LEVEL.LV_2)
                    {
                        this.REGISTER_READ_WORD_LENGTH = 1 + 1 + sizeholdtime + 1 + 2 + 1 + 1;
                       

                    }
                    else
                    {
                        this.REGISTER_READ_WORD_LENGTH = 1 + 1 + sizeholdtime + sizeposition + 2;

                        if (sizeposition > 0)
                        {
                            this.is_position = true;
                        }

                    }

                }
                else
                {
                    int sizeholdtime = KSX326xMetadata.getsizeWithItemcheck(mDevice.CommSpec.KS_X_3267_2018.read.items, KSX326xMetadata.ELEMENT_stateholdtime, 2);

                    if (sizeholdtime > 0)
                    {
                        is_hold_time = true;
                    }

                    if (mDevice.getControllevel() >= CONTROL_LEVEL.LV_2)
                    {
                        this.REGISTER_READ_WORD_LENGTH = 1 + 1 + 2 + 1 + sizeholdtime;
                    }
                    else
                    {
                        this.REGISTER_READ_WORD_LENGTH = 1 + 1 + 2 + sizeholdtime;
                    }

                }
                
            }
			
		}

        /// <summary>
        /// 버퍼로 부터 구동기 정보를 가져온다.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns>true= 성공, false= 실패</returns>
        public  bool deSerialize(byte[] buffer)
        {
            ByteUtil wrapper = ByteUtil.wrap(buffer);


            if (REGISTER_READ_WORD_LENGTH * 2 != buffer.Length)
            {
                return false;
            }

            this.status = wrapper.getUShort();


            if (mDevice.getControllevel() >= CONTROL_LEVEL.LV_1)
            {


                if (mDevice.getActuatorType() == ACTUATOR_TYPE.SWITCH)
                {
                    this.opid = wrapper.getUShort();

                    if (mDevice.getControllevel() >= CONTROL_LEVEL.LV_2)
                    {
                        this.remain_time = wrapper.getUInt();
                        this.ratio = wrapper.getUShort();

                    }
                    else
                    {
                        this.remain_time = (UInt32)wrapper.getInt();
                        if (this.is_hold_time == true)
                        {
                            this.hold_time = wrapper.getUInt();
                        }

                    }


                }


                if (mDevice.getActuatorType() == ACTUATOR_TYPE.RETRACTABLE)
                {
                    this.opid = wrapper.getUShort();




                    if (mDevice.getControllevel() >= CONTROL_LEVEL.LV_2)
                    {
                        this.remain_time = wrapper.getUInt();
                        this.position = wrapper.getUShort();


                        if (this.is_hold_time == true)
                        {
                            this.hold_time = wrapper.getUInt();

                        }

                    }
                    else
                    {
                        this.remain_time = wrapper.getUInt();

                        if (this.is_position == true)
                        {
                            this.position = wrapper.getUShort();

                        }
                        if (this.is_hold_time == true)
                        {
                            this.hold_time = wrapper.getUInt();

                        }



                    }


                }
            }

            return true;

            
        }

        

    }
}
