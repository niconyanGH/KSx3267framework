using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS485ModbusLibary;

namespace SDKforKSX3267
{
    /// <summary>
    /// 센서장치
    /// </summary>
    public class SensorDev
    {
        public readonly int REGISTER_READ_WORD_LENGTH = 3;
        public readonly int REGISTER_READ_START_ADDRESS = 0;
        public readonly KSX_Device mDevice;

        public float value;
        public UInt16 status;

        /// <summary>
        /// 센서장치 생성자
        /// </summary>
        /// <param name="mdev">해당장치의 메타데이터 객체</param>
        public SensorDev(KSX_Device mdev)
        {
            mDevice = mdev;
            this.REGISTER_READ_START_ADDRESS = mDevice.CommSpec.KS_X_3267_2018.read.starting_register;
            this.REGISTER_READ_WORD_LENGTH = 3;
        }
        /// <summary>
        /// 버퍼로 부터 센서 값, 상태 정보를 가져옴.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public bool deSerialize(byte[] buffer)
        {
            ByteUtil wrapper = ByteUtil.wrap(buffer);
            if (buffer.Length != (REGISTER_READ_WORD_LENGTH * 2))
            {
                return false;
            }
            this.value = wrapper.getFloat();
            this.status = wrapper.getUShort();

            return true;
        }
        /// <summary>
        /// 센서값을 문자열로 변환함. 해당 메타데이터 기반
        /// </summary>
        /// <param name="iswithunit">문자열 변환시 센서유닛정보 포함 여부</param>
        /// <returns>문자열 센서값 </returns>
        public String GetValuestring(bool iswithunit)
        {
            String cs= KSX326xMetadata.GetStringValueByDigit(mDevice.SignificantDigit, value);
            if (iswithunit == true)
            {
                cs += " "+mDevice.ValueUnit;
            }

            return cs;
        }


    }
}
