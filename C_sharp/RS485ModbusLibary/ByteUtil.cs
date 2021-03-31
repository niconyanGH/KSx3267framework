using System;
using System.Globalization;
using System.Text;
using System.Diagnostics;

namespace RS485ModbusLibary
{
	
	public class ByteUtil
	{
        private int mLimit; // mBuffer 의 상대 index
        private int mPosition;
        private int mMark;


        private byte[] mBuffer;



        private ByteUtil(byte[] arr, int startOffset, int byteCount)
		{

			mLimit = startOffset + byteCount;
			mMark = mPosition = startOffset;

			mBuffer = arr;
		}

        public static ByteUtil wrap(byte[] array)
        {
            return new ByteUtil(array, 0, array.Length);
        } 


        public int getWord()
        {

            int low = (mBuffer[mPosition + 0]&0xFF);
            int high = (mBuffer[mPosition + 1] & 0xFF);
            int value = (high << 8 | low)&0xFFFF;
            mPosition += 2;
            return value;
        }

        public int getDWord()
        {

            int ba1 = (mBuffer[mPosition + 0] & 0xFF);
            int ba2 = (mBuffer[mPosition + 1] & 0xFF);
            int ba3 = (mBuffer[mPosition + 2] & 0xFF);
            int ba4 = (mBuffer[mPosition + 3] & 0xFF);


            int value = (ba4 << 24) | (ba3 << 16) | (ba2 << 8) | (ba1);
            mPosition += 4;
            return value;
        }


        public short getShort()
        {

            int value = getWord();
            short svalue = (short)(value & 0xFFFF);
            return svalue;
        }

        public UInt16 getUShort()
        {

            int value = getWord();
            UInt16 svalue = (UInt16)(value & 0xFFFF);
            return svalue;
        }

        public int getInt()
        {

            int value = getDWord();
            int svalue = value;
            return svalue;
        }
        public UInt32 getUInt()
        {

            int value = getDWord();
            UInt32 svalue = (UInt32)(value&0xFFFFFFFF);
            return svalue;
        }

        public float getFloat()
        {

            byte[] buffer = new byte[4];
            buffer[0] = mBuffer[mPosition + 0];
            buffer[1] = mBuffer[mPosition + 1];
            buffer[2] = mBuffer[mPosition + 2];
            buffer[3] = mBuffer[mPosition + 3];
            float value = BitConverter.ToSingle(buffer, 0);

            mPosition += 4;
            return value;
        }


         //KSX3267 표준 레지스터 저장방식
        //한 워드 내 바이트 순서: 빅 엔디언(big-endian) 방식을 사용한다.
        //- 워드간 순서: 리틀 엔디언 (little-endian) 방식을 사용하도록 한다.
        public static int cast_value_to_bytes_insert_buffer(int value, byte[] array, int windex)
        {
            return cast_to_byte_intobuffer(false,value,array,windex);
        }
        public static int cast_value_to_bytes_insert_buffer(short value, byte[] array, int windex)
        {
            int cvalue = (int)(value & 0xFFFF);
            return cast_to_byte_intobuffer(true, cvalue, array, windex);
        }


        //long형 unsiged  int 타입
        public static int cast_value_to_bytes_insert_buffer(long value, byte[] array, int windex)
        {
            int cvalue = (int)(value & 0xFFFFFFFF);
            return cast_to_byte_intobuffer(false, cvalue, array, windex);
        }

        //double형 unsiged  short 타입
        public static int cast_value_to_bytes_insert_buffer(double value, byte[] array, int windex)
        {
            int nvalue = (int)value;
            int cvalue = (int)(nvalue & 0xFFFF);
            return cast_to_byte_intobuffer(true, cvalue, array, windex);
        }

        public static int cast_value_to_bytes_insert_buffer(float value, byte[] array, int windex)
        {
            byte[] tmp = BitConverter.GetBytes(value);
            int cvalue = BitConverter.ToInt32(tmp,0);


            return cast_to_byte_intobuffer(false, cvalue, array, windex);
        }



        private static int cast_to_byte_intobuffer(bool isshort, int value, byte[] array, int windex)
        {
            array[windex++] = (byte)(value >> 8);
            array[windex++] = (byte)(value >> 0);
            if (isshort == false)
            {
                array[windex++] = (byte)(value >> 24);
                array[windex++] = (byte)(value >> 16);
                return 4;
            }

            return 2;
            
            

        }
        
        	public static void Debugout_hexstring(byte[] ba)
	{

		Debug.WriteLine( "Length : " +ba.Length);

		foreach (byte b in ba) 
        {
            int intb = b;
            String st = intb.ToString("X");
            Debug.WriteLine(st);
        }

		

	}


	}
}
