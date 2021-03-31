using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS485ModbusLibary;


namespace SDKforKSX3267
{
    /// <summary>
    /// 메타데이터 공용객체
    /// </summary>
    public class KSX326xCommon
    {

        public static bool isvalidcode(int  certificatecode, int companycode)
        {
            bool ret = false;

            if (certificatecode == 0 && companycode == 0) //디폴트 맵노드
            {
                ret = true;
            }
            else if (certificatecode == 0 && companycode == 8877) //코리아디지탈
            {
                ret = true;
            }


            return ret;
        }

        public static PRODUCT_TYPE IsKSXNode(int slaveid, STDModbusMaster mRTUMaster)
        {
            PRODUCT_TYPE mtype = PRODUCT_TYPE.PTYPE_NONE;


            STDModbusResponse rv = mRTUMaster.StandardWordRead_F3(slaveid, NodeInfoByModbus.REGISTER_START_ADDRESS, NodeInfoByModbus.REGISTER_WORD_LENGTH, 200);

            if (rv != null && rv.rep_function == MDFunction.MODBUS_FC_READ_HOLDING_REGISTERS)
            {
                NodeInfoByModbus mNodeInfo = new NodeInfoByModbus();

                if (mNodeInfo.deSerialize(rv.byteDatas) == true)
                {
                    if( isvalidcode(mNodeInfo.CertificateAuthority,mNodeInfo.CompanyCode) == true)
                    {
                        mtype =(PRODUCT_TYPE) mNodeInfo.ProductType;
                    }
                }
            }




            return mtype;
        }
    }
}
