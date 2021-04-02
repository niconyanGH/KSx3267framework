using RS485ModbusLibary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using SDKforKSX3267;


namespace SensorNodeMonitor
{
    public partial class Form1 : Form
    {

        public STDModbusMaster mRTUMaster = new STDModbusMaster();
        public SensorNode mSensorNode;
        public bool sensorThread_run = false;
        public int sensor_read_interval = 0;
        public List<UC_Sensor> mUCSList = new List<UC_Sensor>();




        public Form1()
        {
            InitializeComponent();
            groupBox1.Enabled = false;

            string[] comlist = System.IO.Ports.SerialPort.GetPortNames();
            comboBox_comlist.Items.AddRange(comlist);


            comboBox_interval.Items.Add("읽지않음");
            for (int i = 1; i < 60; i++)
            {
                String cs = "" + i + "초";
                comboBox_interval.Items.Add(i);
            }
            comboBox_interval.SelectedIndex = 0;


        }

       

		private void Form1_Load(object sender, EventArgs e)
		{

		}

        private void comopenbtn_Click_1(object sender, EventArgs e)
        {

            String cs = (String)comboBox_comlist.SelectedItem;
            if (cs == null) return;
            cs = cs.Replace("COM", "");
            int num = Int32.Parse(cs);


            if (mRTUMaster.Open(num, 9600) == false)
            {
                MessageBox.Show("Could not open serial port.");
            }
            else
            {
                groupBox1.Enabled = true;
                button_device.Enabled = false;

            }

        }

        private void button_scan_Click(object sender, EventArgs e)
        {

            Thread mReadThread = new Thread(new ThreadStart(ScanThreadProc));
            mReadThread.Start();

        }



        public void ScanThreadProc()
        {

            string strinfo;
            int stnum = 0;
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    strinfo = " 장비검색 국번= " + i;
                    stnum = 0;

                    PRODUCT_TYPE mtype = KSX326xCommon.IsKSXNode(i, mRTUMaster);
                    //STDModbusResponse rv = mRTUMaster.StandardWordRead_F3(i, 1, 2, 300);
                    if (mtype == PRODUCT_TYPE.PTYPE_NONE)
                    {
                        strinfo += " , 장비없음...";
                    }
                    else if (mtype == PRODUCT_TYPE.PTYPE_SENSORNODE)
                    {

                        strinfo += " , 장비연결됨...";
                        //exit thread
                        stnum = i;
                        i = 1000;
                    }

                    this.Invoke((MethodInvoker)delegate
                    {

                        if (stnum > 0)
                        {
                            ConnectedNode(stnum);
                        }

                        label_info.Text = strinfo;

                    });


                }
            }
            catch (ThreadInterruptedException)
            {
            }

        }

        private void ConnectedNode(int stationnumber)
        {
            mSensorNode = new SensorNode(stationnumber, mRTUMaster);

            String cs = "";

            if (mSensorNode.ReadNodeInformation() == true)
            {

                cs += " CertificateAuthority = " + mSensorNode.mNodeInfo.CertificateAuthority + "\r\n";
                cs += " CompanyCode = " + mSensorNode.mNodeInfo.CompanyCode + "\r\n";
                cs += " ProductType = " + mSensorNode.mNodeInfo.ProductType + "\r\n";
                cs += " ProductCode = " + mSensorNode.mNodeInfo.ProductCode + "\r\n";
                cs += " ChannelNumber = " + mSensorNode.mNodeInfo.ChannelNumber + "\r\n";
                cs += " ProtocolVersion = " + mSensorNode.mNodeInfo.ProtocolVersion + "\r\n";

                comopenbtn.Enabled = false;
                button_scan.Enabled = false;

                button_device.Enabled = true;



            }
            else
            {
                cs = " 기본정보 읽기 실패.";
            }

            richTextBox1.Text = cs;
        }




        public void SensorThreadProc()
        {


            int stnum = 0;
            try
            {
                while (true)
                {

                    foreach (UC_Sensor mUCS in mUCSList)
                    {

                        if (sensorThread_run == false)
                        {
                            return;
                        }

                        mUCS.SetBKColor(true);

                        if (mSensorNode.readDeviceStatus(mUCS.mSensor) == true)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                mUCS.UpdateUI(true);
                            });
                        }

                        Thread.Sleep(sensor_read_interval * 1000);
                        mUCS.SetBKColor(false);

                    }

                }
            }
            catch (ThreadInterruptedException)
            {
            }

        }



        private void button_device_Click(object sender, EventArgs e)
        {
            if (mSensorNode.ReadDeviceCodeList() == true)
            {

                if (mSensorNode.CreateDevices() == true)
                {

                    String cs = " 센서갯수  = " + mSensorNode.mSensorDevices.Count + ", 양액기 = " + mSensorNode.mNutrientDevices.Count + " \r\n";

                    richTextBox1.Text += cs;
                    groupBox_sensor.Enabled = true;
                    


                    foreach (SensorDev ms in mSensorNode.mSensorDevices)
                    {
                        UC_Sensor mUCS = new UC_Sensor(this, ms);
                        mUCSList.Add(mUCS);
                        flowLayoutPanel_sensor.Controls.Add(mUCS);
                    }


                }

            }
        }

        private void comboBox_interval_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox_interval.SelectedIndex == 0)
            {
                sensorThread_run = false;
            }
            else
            {
                if (sensorThread_run == false)
                {

                    Thread mReadThread = new Thread(new ThreadStart(SensorThreadProc));
                    sensorThread_run = true;
                    sensor_read_interval = comboBox_interval.SelectedIndex;
                    mReadThread.Start();
                }
                else
                {
                    sensor_read_interval = comboBox_interval.SelectedIndex;

                }

            }


        }

        private void onUCClick(object sender, EventArgs e)
        {
            
        }

        private void onMClick(object sender, MouseEventArgs e)
        {
            
        }
		

        public void OnSelectedSensor(UC_Sensor msensor)
        {
            Debug.WriteLine("thisOnSelectedSensor = " + msensor.mSensor.mDevice.Name);

            chart1.Series.Clear();

            chart1.Series.Add(msensor.chartserie);

            
            
        }

    }

    
}
