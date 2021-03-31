using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS485ModbusLibary;


namespace SDKforKSX3267
{
    /// <summary>
    /// 센서노드
    /// </summary>
    public class SensorNode :   KSXNode
    {

        public int opid;
        public int status;


        public SensorNode(int stationnum, STDModbusMaster mMaster)
            : base(stationnum, mMaster)
        {
            
        }

        /// <summary>
        /// 센서노드 상태정보를 읽어온다.
        /// </summary>
        /// <returns>true= 성공, false= 실패</returns>
        public bool readNodeStatus()
        {

            STDModbusResponse rv = mModbusMaster.StandardWordRead_F3(StationNumber, mNodeMeta.CommSpec.KS_X_3267_2018.read.starting_register, 2, 1000);

            if (rv != null && rv.rep_function == MDFunction.MODBUS_FC_READ_HOLDING_REGISTERS)
            {

                ByteUtil wrapper = ByteUtil.wrap(rv.byteDatas);
                
                this.opid = wrapper.getUShort();
                this.status = wrapper.getUShort();



                return true;

            }

            return false;
        }

        
        /// <summary>
        /// 센서노드에 연결된 모든 센서정보를 읽어온다.
        /// </summary>
        public void readAllsensors()
        {
            foreach (SensorDev mobj in mSensorDevices)
            {
                  readDeviceStatus(mobj);
            }

        }


    }

}
