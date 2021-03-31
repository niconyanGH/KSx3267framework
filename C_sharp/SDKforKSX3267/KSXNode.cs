using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS485ModbusLibary;


namespace SDKforKSX3267
{
    /// <summary>
    /// KSX3267를 지원하는 노드기본 객체
    /// </summary>
    public class KSXNode
    {

        public const int OPERATION_NODE_CONTROL = 2;
        public const int RESPONSE_TIME_MSEC = 300;
        public const int REGISTER_DEVICECODE_READ_START_ADDRESS = 101;
        public const int REGISTER_DEVICECODE_READ_WORD_LENGTH = 200;


        /// <summary>
        /// 노드의 RS485 슬레이브 주소
        /// </summary>
        public readonly int StationNumber = 0;
        /// <summary>
        /// RS485통신용 객체
        /// </summary>
        public readonly STDModbusMaster mModbusMaster;
        /// <summary>
        /// 노드 메타데이터
        /// </summary>
        public KSX_NodeMeta mNodeMeta;
   
       /// <summary>
       /// 노드 기본정보
       /// </summary>
        public NodeInfoByModbus mNodeInfo;
        /// <summary>
        /// 노드에 연결된 장치코드목록
        /// </summary>
        public List<int> mDeviceCodes;

     

        /// <summary>
        /// 노드에 포함된 장치 목록
        /// </summary>
        public List<Object> mDevices;

        /// <summary>
        /// 노드에 연결된 장치중 센서장치목록
        /// </summary>
        public List<SensorDev> mSensorDevices;
        /// <summary>
        /// 노드에 연결된 장치중 구동기장치목록
        /// </summary>
        public List<ActuatorDev> mActuatorDevices;
        /// <summary>
        /// 노드에 연결된 장치중 양액기장치목록
        /// </summary>
        public List<NutrientDev> mNutrientDevices;


        /// <summary>
        /// 노드 생성자
        /// </summary>
        /// <param name="stationnum"> 노드의  슬레이브 ID</param>
        /// <param name="mMaster">RS485 통신 객체</param>
        public KSXNode(int stationnum, STDModbusMaster mMaster)
        {
            StationNumber = stationnum;
            mModbusMaster = mMaster;
            

            mNodeInfo = new NodeInfoByModbus();
            mDeviceCodes = new List<int>();

            mDevices  = new List<Object>();

            mSensorDevices = new List<SensorDev>();
            mActuatorDevices = new List<ActuatorDev>();
            mNutrientDevices = new List<NutrientDev>();


        }
        /// <summary>
        /// 노드의 기본 정보를 읽어온다.
        /// </summary>
        /// <returns>true= 성공, false= 실패</returns>
        public bool ReadNodeInformation()
        {

            STDModbusResponse rv = mModbusMaster.StandardWordRead_F3(StationNumber, NodeInfoByModbus.REGISTER_START_ADDRESS, NodeInfoByModbus.REGISTER_WORD_LENGTH, 1000);

            if (rv != null && rv.rep_function == MDFunction.MODBUS_FC_READ_HOLDING_REGISTERS)
            {
                if (mNodeInfo.deSerialize(rv.byteDatas) == true)
                {
                    mNodeMeta = KSX326xMetadata.GetNodeMetaData(mNodeInfo);
                    return true;
                }
            }

            return false;


        }

        /// <summary>
        /// 센서장치의 상태정보를 읽어온다.
        /// </summary>
        /// <param name="mdevice">상태정보를 읽어올 장치객체</param>
        /// <returns>true= 성공, false= 실패</returns>
        public bool readDeviceStatus(SensorDev mdevice)
        {

            STDModbusResponse rv1 = mModbusMaster.StandardWordRead_F3(StationNumber, mdevice.REGISTER_READ_START_ADDRESS, mdevice.REGISTER_READ_WORD_LENGTH, RESPONSE_TIME_MSEC);
            if (rv1 != null)
            {
                mdevice.deSerialize(rv1.byteDatas);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 구동기장치의 상태정보를 읽어온다.
        /// </summary>
        /// <param name="mdevice">상태정보를 읽어올 장치객체</param>
        /// <returns>true= 성공, false= 실패</returns>
        public bool readDeviceStatus(ActuatorDev mdevice)
        {

            STDModbusResponse rv1 = mModbusMaster.StandardWordRead_F3(StationNumber, mdevice.REGISTER_READ_START_ADDRESS, mdevice.REGISTER_READ_WORD_LENGTH, RESPONSE_TIME_MSEC);
            if (rv1 != null)
            {
                mdevice.deSerialize(rv1.byteDatas);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 양액기장치의 상태정보를 읽어온다.
        /// </summary>
        /// <param name="mdevice">상태정보를 읽어올 장치객체</param>
        /// <returns>true= 성공, false= 실패</returns>
        public bool readDeviceStatus(NutrientDev mdevice)
        {

            STDModbusResponse rv1 = mModbusMaster.StandardWordRead_F3(StationNumber, mdevice.REGISTER_READ_START_ADDRESS, mdevice.REGISTER_READ_WORD_LENGTH, RESPONSE_TIME_MSEC);
            if (rv1 != null)
            {
                mdevice.deSerialize(rv1.byteDatas);
                return true;
            }
            return false;
        }
        


        
        private bool devicecodeDeSerialize(byte[] buffer)
        {
            ByteUtil wrapper = ByteUtil.wrap(buffer);
            
            mDeviceCodes.Clear();

            for (int i = 0; i < buffer.Length / 2; i++)
            {
                int md =(int) wrapper.getUShort();

                mDeviceCodes.Add(md);
            }



            return true;
        }


        /// <summary>
        /// 노드의 장치코드 목록을 읽어온다.
        /// </summary>
        /// <returns>true= 성공, false= 실패</returns>
        public bool ReadDeviceCodeList()
        {

            if (mNodeMeta != null)
            {
                int devicecount = mNodeMeta.Devices.Count;
                STDModbusResponse rv1 = mModbusMaster.StandardWordRead_F3(StationNumber, REGISTER_DEVICECODE_READ_START_ADDRESS, devicecount, 1000);

                if (rv1 != null)
                {
                    return devicecodeDeSerialize(rv1.byteDatas);
                }
            }
            return false;
        }

        /// <summary>
        /// 노드에 연결된 장치에 대한 객체를 생성한다.
        /// </summary>
        /// <returns>true= 성공, false= 실패</returns>
        public bool CreateDevices()
        {

            mDevices.Clear();

            mActuatorDevices.Clear();
            mSensorDevices.Clear();
            mNutrientDevices.Clear();

               


            for (int i = 0; i < mDeviceCodes.Count; i++)
            {

                if (mDeviceCodes[i] != 0 && i < mNodeMeta.Devices.Count)
                {
                    KSX_Device mDevice = mNodeMeta.Devices[i];
                    if (mDevice != null)
                    {
                        Object mdev=null;

                        if (mDevice.Class.Contains(KSX326xMetadata.ELEMENT_classsensor) == true)
                        {
                            mdev = new SensorDev(mDevice);

                            mSensorDevices.Add((SensorDev)mdev);
                        }
                        else if (mDevice.Class.Contains(KSX326xMetadata.ELEMENT_classactuator) == true)
                        {
                            //구동기 분류에서 양액기인지 구동기인지 구별
                            if (mDevice.Type.Contains(KSX326xMetadata.ELEMENT_typenutrient) == true)
                            {
                                mdev = new NutrientDev(mDevice);
                                mNutrientDevices.Add((NutrientDev)mdev);
                            }
                            else
                            {
                                mdev = new ActuatorDev(mDevice);
                                mActuatorDevices.Add((ActuatorDev)mdev);
                            }

                            
                        }
                        if (mdev != null)
                        {
                            mDevices.Add(mdev);
                        }
                    }

                }

            }

            return true;

        }



        /// <summary>
        /// 개폐형 장치를 제어한다.
        /// </summary>
        /// <param name="mactdevice">제어할 개폐형 장치</param>
        /// <param name="mOperation">제어명령</param>
        /// <param name="mOpid">제어 OPID</param>
        /// <param name="mHoldtime">구동시간</param>
        /// <param name="mPosition">개폐위치</param>
        /// <param name="mOpentime">개폐기설정 열림시간</param>
        /// <param name="mClosetime">개폐기설정 닫힘 시간</param>
        /// <returns>true= 성공, false= 실패</returns>
        public bool controlReactable(ActuatorDev mactdevice, ACTUATOR_COMMAND mOperation, int mOpid, long mHoldtime, int mPosition, int mOpentime, int mClosetime)
        {

            byte[] mbuffer;

           

            mbuffer = mactdevice.bulid_control_msg(mOperation, mOpid, mHoldtime, (short)0 , mPosition,mOpentime, mClosetime);

            if (mbuffer != null)
            {

                STDModbusResponse rv1 = mModbusMaster.StandardWordWrite_F10(StationNumber, mactdevice.REGISTER_WRITE_START_ADDRESS, mbuffer, RESPONSE_TIME_MSEC);

                if (rv1 != null)
                {
                    return true;
                }


            }


            return false;
        }


        /// <summary>
        /// 스위치형 장치를 제어한다.
        /// </summary>
        /// <param name="mactdevice">제어할 스위치형장치</param>
        /// <param name="mOperation">제어명령</param>
        /// <param name="mOpid">제어 OPID</param>
        /// <param name="mHoldtime">제어 켜짐 시간</param>
        /// <param name="mRatio">제어 강도</param>
        /// <returns>true= 성공, false= 실패</returns>
        public bool controlSwitch(ActuatorDev mactdevice, ACTUATOR_COMMAND mOperation, UInt16 mOpid, UInt32 mHoldtime, short mRatio)
        {
            bool ret = false;

            byte[] mbuffer = mactdevice.bulid_control_msg(mOperation, mOpid, mHoldtime, mRatio, 0,0,0);


            if (mbuffer != null)
            {

                STDModbusResponse rv1 = mModbusMaster.StandardWordWrite_F10(StationNumber, mactdevice.REGISTER_WRITE_START_ADDRESS, mbuffer, RESPONSE_TIME_MSEC);

                if (rv1 != null)
                {
                    ret = true;
                }


            }


            return ret;
        }
        /// <summary>
        /// 양액기장치를 제어한다.
        /// </summary>
        /// <param name="mNut">제어할 양액기장치</param>
        /// <param name="mOperation">제어명령</param>
        /// <param name="mOpid">제어 OPID</param>
        /// <param name="startarea">시작구역</param>
        /// <param name="endarea">종료구역</param>
        /// <param name="onsec">관수시간</param>
        /// <param name="ec">설정 EC값</param>
        /// <param name="ph">설정 pH값</param>
        /// <returns>true= 성공, false= 실패</returns>
        public bool controlNutrient(NutrientDev mNut, NUTRIENT_COMMAND mOperation, int mOpid, int startarea, int endarea, int onsec, float ec, float ph)
        {

            byte[] mbuffer = mNut.bulid_control_msg(mOperation, mOpid, startarea, endarea, onsec, ec, ph);
            STDModbusResponse rv1 = mModbusMaster.StandardWordWrite_F10(StationNumber, mNut.REGISTER_WRITE_START_ADDRESS, mbuffer, RESPONSE_TIME_MSEC);
            if (rv1 != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 노드를 제어한다.
        /// </summary>
        /// <param name="mOpid">노드제어 명령어</param>
        /// <param name="control">제어방식</param>
        /// <returns>true= 성공, false= 실패</returns>
        public bool controlNode(int mOpid, NODECONTROL_COMMAND control)
        {
            byte[] buffer = null;
            int mindex = 0;

            buffer = new byte[6];

            mindex += ByteUtil.cast_value_to_bytes_insert_buffer((double)OPERATION_NODE_CONTROL, buffer, mindex);
            mindex += ByteUtil.cast_value_to_bytes_insert_buffer((double)mOpid, buffer, mindex);
            mindex += ByteUtil.cast_value_to_bytes_insert_buffer((double)control, buffer, mindex);



            STDModbusResponse rv1 = mModbusMaster.StandardWordWrite_F10(StationNumber, mNodeMeta.CommSpec.KS_X_3267_2018.write.starting_register, buffer, RESPONSE_TIME_MSEC);

            if (rv1 != null)
            {
                return true;
            }
            return false;

        }



    }
}
