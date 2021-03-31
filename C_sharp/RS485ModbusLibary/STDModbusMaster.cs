using System;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;



namespace RS485ModbusLibary
{
    /// <summary>
    /// 마스터방식 통신 객체
    /// </summary>
    public class STDModbusMaster
    {

        private byte[] mSerialPortReadBuffer;
        public SerialPort mSerialPort;
        private ByteCircularBuffer mRecvBytesQueue_modbus;




        /// <summary>
        /// 모드버스 메시지 슬레이브주소와 CRC값이 맞는지 확인한다.
        /// </summary>
        /// <param name="mslave"></param>
        /// <param name="msg"></param>
        /// <returns>true=메시지정상, false=잘못된 메시지</returns>
        private bool STD_ModbusRTU_check_integrity(int mslave, byte[] msg)
        {
            int crc_calculated;
            int crc_received;
            int slave = msg[0];
            int msg_length = msg.Length;


            if (slave != mslave && slave != 0)
            {

                return false;
            }

            crc_calculated = STDModbusCommon.GetCRC16(msg, msg_length - 2);

            int hi = (int)(msg[msg_length - 2] & 0xFF);
            int lo = (int)(msg[msg_length - 1] & 0xFF);

            crc_received = ((hi << 8) | lo) & 0xFFFF;



            if (crc_calculated == crc_received)
            {


                return true;
            }
            else
            {

                return false;
            }

        }

        /// <summary>
        /// 모드버스 읽기 기능코드 3
        /// </summary>
        /// <param name="Slave">슬레이브주소</param>
        /// <param name="StartAddr">레지스터주소</param>
        /// <param name="Length">레지스터 개수</param>
        /// <param name="waitMillisecond">응답메시지를 기다리는 시간 </param>
        /// <returns>응답메시지, null: 응답없음</returns>
        public STDModbusResponse StandardWordRead_F3(int Slave, int StartAddr, int Length, int waitMillisecond = 100)
        {
            byte[] data;
            int ResponseByteCount = Length * 2 + 5;

            data = STDModbusCommon.bulid_read_registers(Slave, MDFunction.MODBUS_FC_READ_HOLDING_REGISTERS, StartAddr, Length);


            Write(data);


            for (int i = 0; i < 10; i++)
            {

                int div110msec = waitMillisecond / 10;
                Thread.Sleep(div110msec);



                if (mRecvBytesQueue_modbus.size() >= ResponseByteCount)
                {
                    byte[] ResponseBytes = new byte[ResponseByteCount];
                    mRecvBytesQueue_modbus.popBytes(ResponseBytes, 0, ResponseBytes.Length);
                    if (STD_ModbusRTU_check_integrity(Slave, ResponseBytes) == true)
                    {

                        return (new STDModbusResponse(ResponseBytes));


                    }
                    Debug.WriteLine("잘못된 응답입니다.");

                    return null;

                }


            }
            Debug.WriteLine("응답시간초과..");

            return null;



        }




        /// <summary>
        /// 모드버스 쓰기 기능코드 10
        /// </summary>
        /// <param name="Slave">슬레이브 주소</param>
        /// <param name="StartAddr">레지스터 주소</param>
        /// <param name="Values">쓰기데이터 버퍼</param>
        /// <param name="waitMillisecond">응답메시지를 기다리는 시간 </param>
        /// <returns>응답메시지, null: 응답없음</returns>
        public STDModbusResponse StandardWordWrite_F10(int Slave, int StartAddr, byte[] Values, int waitMillisecond = 100)
        {
            byte[] data;
            int ResponseByteCount = 8;

            data = STDModbusCommon.bulid_write_registers(Slave, MDFunction.MODBUS_FC_WRITE_MULTIPLE_REGISTERS, StartAddr, Values);


            Write(data);


            for (int i = 0; i < 10; i++)
            {

                int div110msec = waitMillisecond / 10;
                Thread.Sleep(div110msec);



                if (mRecvBytesQueue_modbus.size() >= ResponseByteCount)
                {
                    byte[] ResponseBytes = new byte[ResponseByteCount];
                    mRecvBytesQueue_modbus.popBytes(ResponseBytes, 0, ResponseBytes.Length);
                    if (STD_ModbusRTU_check_integrity(Slave, ResponseBytes) == true)
                    {
                        return (new STDModbusResponse(ResponseBytes));
                    }
                    Debug.WriteLine("잘못된 응답입니다.");
                    return null;

                }

            }

            Debug.WriteLine("응답시간초과..");

            return null;



        }


        private bool Write(byte[] mdatas, bool queclear = true)
        {
            if (IsOpen() == true && mdatas != null)
            {
                try
                {
                    if (queclear == true)
                    {
                        mRecvBytesQueue_modbus.clear();
                    }
                    mSerialPort.Write(mdatas, 0, mdatas.Length);
                }
                catch (Exception e1)
                {
                    Debug.WriteLine(e1.ToString());
                    return false;
                }
                return true;

            }
            return false;
        }


        public STDModbusMaster() 
        {

            mSerialPort = new SerialPort();
            mSerialPort.BaudRate = 115200;
            mSerialPort.Parity = Parity.None;
            mSerialPort.DataBits = 8;
            mSerialPort.StopBits = StopBits.One;
            mSerialPort.Handshake = Handshake.None;
            mSerialPort.Encoding = Encoding.ASCII;
            mSerialPort.WriteTimeout = 100;
            mSerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
            mSerialPortReadBuffer = new byte[mSerialPort.ReadBufferSize * 0x10];
      
            mRecvBytesQueue_modbus = new ByteCircularBuffer(mSerialPort.ReadBufferSize, true);


        }
        ~STDModbusMaster()
		{
			Close();
		}

		public bool Open(int portNumber, int mboudrate=9600)
		{
			if( mSerialPort.IsOpen )
			{
				Debug.WriteLine( "Serial::Open // 이미 open 되었습니다.");
				return true;
			}

			try
			{
				mSerialPort.PortName = "COM" + portNumber;
                mSerialPort.BaudRate = mboudrate;
				mSerialPort.Open();


			}
			catch( IOException e )
			{
				Debug.WriteLine( "Serial::Open // Fail!! // Device is not reponse. COM" + portNumber);
				Debug.WriteLine( e.ToString());
				return false;
			}
			catch( Exception e )
			{
				Debug.WriteLine( "Serial::Open // Fail!! COM" + portNumber);
				Debug.WriteLine( e.ToString());

				return false;
			}

			Debug.WriteLine( "Serial::Open // Success!! COM" + portNumber);

		

			return true;
		}

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                int readCount = mSerialPort.Read(mSerialPortReadBuffer, 0, mSerialPort.BytesToRead);

                if (readCount > 0)
                {
                  //  Debug.WriteLine("mSerialPort.BytesToRead =" + mSerialPort.BytesToRead + ",readCount =" + readCount);
                  //  Debug.WriteLine(Encoding.ASCII.GetString(mSerialPortReadBuffer, 0, readCount));

                    mRecvBytesQueue_modbus.pushBytes(mSerialPortReadBuffer, 0, readCount);
                                 
                }
                
            }
            catch (Exception e1)
            {
                Debug.WriteLine(e1.ToString());
            }
        }


		public bool IsOpen()
		{
			return mSerialPort.IsOpen;
		}

		public void Close()
		{
			mSerialPort.Close();
		
		}

        


    }

   
}
