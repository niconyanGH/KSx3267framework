using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS485ModbusLibary;

namespace SDKforKSX3267
{
    /// <summary>
    /// 노드기본정보 객체
    /// </summary>
    public class NodeInfoByModbus
    {
        public const int REGISTER_WORD_LENGTH = 8;
        public const int REGISTER_START_ADDRESS = 1;


        public UInt16 CertificateAuthority;
        public UInt16 CompanyCode;
        public UInt16 ProductType;
        public UInt16 ProductCode;
        public UInt16 ProtocolVersion;
        public UInt16 ChannelNumber;
        public UInt32 SerialNumber;




    


        public bool deSerialize(byte[] buffer)
        {
            ByteUtil wrapper = ByteUtil.wrap(buffer);

            if (buffer.Length != (REGISTER_WORD_LENGTH * 2))
            {
                return false;
            }
            this.CertificateAuthority = wrapper.getUShort();
            this.CompanyCode = wrapper.getUShort();
            this.ProductType = wrapper.getUShort();
            this.ProductCode = wrapper.getUShort();
            this.ProtocolVersion = wrapper.getUShort();
            this.ChannelNumber = wrapper.getUShort();

            this.SerialNumber = wrapper.getUInt();


            return true;
        }



    }

}
