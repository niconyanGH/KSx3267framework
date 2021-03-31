
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS485ModbusLibary;

namespace SDKforKSX3267
{
    /// <summary>
    /// 복합노드
    /// </summary>
    public class IntergratedNode : KSXNode
    {

        
        public UInt16 status;
        public UInt16 opid;
        public UInt16 control;

        public IntergratedNode(int stationnum, STDModbusMaster mMaster)
            : base(stationnum, mMaster)
        {
            
        }
        /// <summary>
        /// 노드상태정보를 읽어옴
        /// </summary>
        /// <returns>true= 성공, false= 실패</returns>
        public bool readNodeStatus()
        {

            //양액기노드 경우 "control" 항목이 있는지 확인
            int sizecontrol = KSX326xMetadata.getsizeWithItemcheck(mNodeMeta.CommSpec.KS_X_3267_2018.read.items, KSX326xMetadata.ELEMENT_control,1);
            STDModbusResponse rv = mModbusMaster.StandardWordRead_F3(StationNumber, mNodeMeta.CommSpec.KS_X_3267_2018.read.starting_register, 2 + sizecontrol, 1000);
            
            if (rv != null && rv.rep_function == MDFunction.MODBUS_FC_READ_HOLDING_REGISTERS)
            {
                ByteUtil wrapper = ByteUtil.wrap(rv.byteDatas);
                this.status = wrapper.getUShort();
                this.opid = wrapper.getUShort();
                if (sizecontrol > 0)
                {
                    this.control = wrapper.getUShort();
                }
                else
                {
                    this.control = (ushort)0xFFFF;//지원하지않음상태 표시
                }
                return true;
            }


            return false;
        }
        /// <summary>
        /// 노드에 연결된 모든 센서정보를 읽어온다.
        /// </summary>
        public void readAllsensors()
        {
            foreach (Object mobj in mDevices)
            {
                if (mobj.GetType() == typeof(SensorDev))
                {
                    readDeviceStatus((SensorDev)mobj);

                }

            }

        }

       

      






    }
}

