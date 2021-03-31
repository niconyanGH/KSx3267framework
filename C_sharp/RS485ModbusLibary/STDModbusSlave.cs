using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Collections;
using System.Diagnostics;



namespace RS485ModbusLibary
{
    public class WriteCallbackArg : EventArgs
    {
        public int startaddress;
        public int regsize;

        public WriteCallbackArg(int mStartaddress, int mRegsize)
        {
            this.startaddress = mStartaddress;
            this.regsize = mRegsize;
        }
    }


    public class STDModbusSlave
    {

        private const string TAG = "STDModbusRTUSlave";
        public SerialPort mSerialPort;
        private byte[] mSerialPortReadBuffer;
        private ByteCircularBuffer mRecvBytesQueue_modbus;

        private short[] myRegisterMap;
        private int myStationNumber = 0;

        public event EventHandler WriteEvent;



        // Read Thread 관련 변수들
        private Thread mReadThread;
        private bool mReadThreadRun;
        


        public STDModbusSlave(int slave, int regsize) 
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

            myRegisterMap = new short[regsize];
            myStationNumber = slave;

            mReadThread = new Thread(new ThreadStart(ReadThreadProc));
            mReadThreadRun = true;
            mReadThread.Start();


        }


        ~STDModbusSlave()
		{

            mReadThreadRun = false;

            if (null != mReadThread)
            {
                mReadThread.Interrupt();
                mReadThread.Join();
            }

            mReadThread = null;

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
                    Debug.WriteLine("mSerialPort.BytesToRead =" + mSerialPort.BytesToRead + ",readCount =" + readCount);
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

            mReadThreadRun = false;
			mSerialPort.Close();
		
		}

        private bool Write(byte[] mdatas, bool queclear=true)
        {
            if (IsOpen() == true && mdatas!=null)
            {
                try
                {
                    if (queclear==true)
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
    

        public void SetRegsiterValues(int startaddress, short []values)
        {

            for(int i=0;i<values.Length;i++  )
            {
                int idex = i+ startaddress;
                if (idex < myRegisterMap.Length)
                {
                    myRegisterMap[idex] = values[i];
                }
            }

        }



        public void ReadThreadProc()
        {
            long tick_last=0;
            long tick_current=0;
            int readsize_old=0;

            try
            {
                while (mReadThreadRun)
                {
                    Thread.Sleep(1);
                    int readsize = mRecvBytesQueue_modbus.size() ;

                    if (readsize > 0)
                    {
                        tick_current = DateTime.UtcNow.Ticks;
                        if(readsize !=readsize_old)
                        {
                            readsize_old = readsize;
                            tick_last = tick_current;
                        }
                        else
                        {
                            long diff_tick = (tick_current - tick_last) / TimeSpan.TicksPerMillisecond; 

                            if(diff_tick >100)
                            {



                                modbus_reply();
                                mRecvBytesQueue_modbus.clear();
                                readsize_old = 0;
                                Debug.WriteLine("패킷 입력....");
                            }



                        }
                    }

                    

                    


                }
            }
            catch (ThreadInterruptedException)
            {
            }

        }

        private void modbus_reply()
        {

            if (mRecvBytesQueue_modbus.size() >= 8)
            {
                byte[] query = new byte[mRecvBytesQueue_modbus.size()];
                mRecvBytesQueue_modbus.popBytes(query, 0, query.Length);

                int slave;
                int  function;
                int address;
                int regsize;


                slave = query[0];
                function = (int)(query[1]);
                address = (int)((query[2] << 8) + query[3]);
                regsize = (int)((query[4] << 8) + query[5]);





                switch (function)
                {

                    case MDFunction.MODBUS_FC_READ_HOLDING_REGISTERS:
                        {
                            int rep_size = 3 + regsize * 2 + 2;
                            byte[] rsp = new byte[rep_size];
                            int rsp_length = 0;
                            rsp[rsp_length++] = (byte)slave;
                            rsp[rsp_length++] = (byte)function;

                            rsp[rsp_length++] = (byte)(regsize * 2);
                            for (int i = address; i < address + regsize; i++)
                            {
                                rsp[rsp_length++] = (byte)(myRegisterMap[i] >> 8);
                                rsp[rsp_length++] = (byte)(myRegisterMap[i] & 0xFF);
                            }
                            int crc_calculated = STDModbusCommon.GetCRC16(rsp, rsp_length);

                            rsp[rsp_length++] = (byte)(crc_calculated >> 8);
                            rsp[rsp_length++] = (byte)(crc_calculated & 0x00FF);

                            Write(rsp);



                        }


                        break;
                    case MDFunction.MODBUS_FC_WRITE_MULTIPLE_REGISTERS:
                        {
                            int rep_size = 2 +4+ 2;
                            byte[] rsp = new byte[rep_size];
                            int rsp_length = 0;
                            int bytecount = query[6];

                            rsp[rsp_length++] = (byte)slave;
                            rsp[rsp_length++] = (byte)function;



                            for (int i = address, j=7; i < (address + regsize); i++,j+=2)
                            {
                                int regv=(int )((query[j] << 8)  | query[j + 1]);
                                myRegisterMap[i] = (short)(regv&0xFFFF);
                                
                            }


                            rsp[rsp_length++] = (byte)query[2];
                            rsp[rsp_length++] = (byte)query[3];
                            rsp[rsp_length++] = (byte)query[4];
                            rsp[rsp_length++] = (byte)query[5];





                            int crc_calculated = STDModbusCommon.GetCRC16(rsp, rsp_length);

                            rsp[rsp_length++] = (byte)(crc_calculated >> 8);
                            rsp[rsp_length++] = (byte)(crc_calculated & 0x00FF);

                            Write(rsp);


                            WriteEvent(this, new  WriteCallbackArg(address ,regsize));


                        }
                        break;
                }


            }

        }

    }

   

}
