using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using SDKforKSX3267;
using RS485ModbusLibary;
using System.Threading;
using System.IO;


namespace NutrientNodeMonitor
{
    public partial class Form1 : Form
    {

        public STDModbusMaster mRTUMaster = new STDModbusMaster();

        public IntergratedNode mIntergatedNode;

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



            for (int i = 1; i < 16; i++)
            {
                String cs = "" + i + "구역";
                comboBox_start.Items.Add(cs);
                comboBox_end.Items.Add(cs);
            }

            comboBox_start.SelectedIndex = 0;
            comboBox_end.SelectedIndex = 0;

            radioButton_one.Checked = true;


            comboBox_control.Items.Add("로컬제어");
            comboBox_control.Items.Add("원격제어");
            comboBox_control.SelectedIndex = 1;



        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comopenbtn_Click(object sender, EventArgs e)
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
            //   mReadThreadRun = true;
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
                    else if (mtype == PRODUCT_TYPE.PTYPE_INTEGRATEDNODE)
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
            mIntergatedNode = new IntergratedNode(stationnumber, mRTUMaster);

            String cs = "";

            if (mIntergatedNode.ReadNodeInformation() == true)
            {

                cs += " CertificateAuthority = " + mIntergatedNode.mNodeInfo.CertificateAuthority + "\r\n";
                cs += " CompanyCode = " + mIntergatedNode.mNodeInfo.CompanyCode + "\r\n";
                cs += " ProductType = " + mIntergatedNode.mNodeInfo.ProductType + "\r\n";
                cs += " ProductCode = " + mIntergatedNode.mNodeInfo.ProductCode + "\r\n";
                cs += " ChannelNumber = " + mIntergatedNode.mNodeInfo.ChannelNumber + "\r\n";
                cs += " ProtocolVersion = " + mIntergatedNode.mNodeInfo.ProtocolVersion + "\r\n";

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

        private void button_read_basic_Click(object sender, EventArgs e)
        {


            if (mIntergatedNode.ReadNodeInformation() == true)
            {
                label_info.Text = " 장비번호 = " + mIntergatedNode.mNodeInfo.CompanyCode;

            }



            if (mIntergatedNode.ReadDeviceCodeList() == true)
            {
                label_info.Text = " 장비번호 = " + mIntergatedNode.mDeviceCodes.Count;

            }

            if (mIntergatedNode.CreateDevices() == true)
            {
                label_info.Text = " 센서번호 = " + mIntergatedNode.mDevices.Count;

            }
        }

        private void button_readsensor_Click(object sender, EventArgs e)
        {

            if (mIntergatedNode.readNodeStatus() == true)
            {
                label_info.Text = " 노드상태 = " + mIntergatedNode.status;
            }
        }

        private void button_device_Click(object sender, EventArgs e)
        {
            if (mIntergatedNode.ReadDeviceCodeList() == true)
            {

                if (mIntergatedNode.CreateDevices() == true)
                {

                    String cs = " 센서갯수  = " + mIntergatedNode.mSensorDevices.Count + ", 양액기 = " + mIntergatedNode.mNutrientDevices.Count + " \r\n";

                    richTextBox1.Text += cs;
                    groupBox_sensor.Enabled = true;
                    groupBox_control.Enabled = true;


                    foreach (SensorDev ms in mIntergatedNode.mSensorDevices)
                    {
                        UC_Sensor mUCS = new UC_Sensor(ms);
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

                        if (mIntergatedNode.readDeviceStatus(mUCS.mSensor) == true)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                mUCS.UpdateUI();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_one.Checked == true)
            {
                button_on.Text = "1회관수(On)";
                comboBox_start.Enabled = false;
                comboBox_end.Enabled = false;
                textBox_on_sec.Enabled = false;
                textBox_ec.Enabled = false;
                textBox_ph.Enabled = false;

            }



        }

        private void radioButton_area_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_area.Checked == true)
            {
                button_on.Text = "구역관수(On)";
                comboBox_start.Enabled = true;
                comboBox_end.Enabled = true;
                textBox_on_sec.Enabled = true;
                textBox_ec.Enabled = false;
                textBox_ph.Enabled = false;

            }
        }

        private void radioButton_param_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_param.Checked == true)
            {
                button_on.Text = "설정관수(On)";
                comboBox_start.Enabled = true;
                comboBox_end.Enabled = true;
                textBox_on_sec.Enabled = true;
                textBox_ec.Enabled = true;
                textBox_ph.Enabled = true;

            }
        }

        private void button_on_Click(object sender, EventArgs e)
        {
            NutrientDev mND = mIntergatedNode.mNutrientDevices[0];
            NUTRIENT_COMMAND mcmd = NUTRIENT_COMMAND.OPERATION_NUITRIENT_OFF;
            int mopid = 0;
            int mstart = 0;
            int mend = 0;
            int msec = 0;
            float mec = 0;
            float mph = 0;



            if (radioButton_one.Checked == true)
            {
                mcmd = NUTRIENT_COMMAND.OPERATION_NUITRIENT_ON;
                mopid = 11;
            }
            else if (radioButton_area.Checked == true)
            {
                mcmd = NUTRIENT_COMMAND.OPERATION_NUITRIENT_AREA_ON;
                mopid = 22;
                mstart = comboBox_start.SelectedIndex + 1;
                mend = comboBox_end.SelectedIndex + 1;
                msec = Int32.Parse(textBox_on_sec.Text);

            }
            else if (radioButton_param.Checked == true)
            {
                mcmd = NUTRIENT_COMMAND.OPERATION_NUITRIENT_PARAM_ON;
                mopid = 33;
                mstart = comboBox_start.SelectedIndex + 1;
                mend = comboBox_end.SelectedIndex + 1;
                msec = Int32.Parse(textBox_on_sec.Text);
                mec = float.Parse(textBox_ec.Text);
                mph = float.Parse(textBox_ph.Text);
            }


            if (mcmd != NUTRIENT_COMMAND.OPERATION_NUITRIENT_OFF)
            {
                mIntergatedNode.controlNutrient(mND, mcmd, mopid, mstart, mend, msec, mec, mph);

                ReadNutrientStatus();

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

            mIntergatedNode.controlNutrient(mIntergatedNode.mNutrientDevices[0], NUTRIENT_COMMAND.OPERATION_NUITRIENT_OFF, 0, 0, 0, 0, 0, 0);
            ReadNutrientStatus();

        }

        private void ReadNutrientStatus()
        {
            String cs = "";

            if (mIntergatedNode.readDeviceStatus(mIntergatedNode.mNutrientDevices[0]) == true)
            {
                cs += "OPID: " + mIntergatedNode.mNutrientDevices[0].opid + "\r\n";
                cs += "상태: " +KSX326xMetadata.GetStatusDescrition((STATUS_CODE) mIntergatedNode.mNutrientDevices[0].status) + "\r\n";
                cs += "관수구역: " + mIntergatedNode.mNutrientDevices[0].irrigation_area + "\r\n";
                cs += "경보정보: " + mIntergatedNode.mNutrientDevices[0].alert_information + "\r\n";
            }
            else
            {
                cs = " 제어상태 읽기 실패..";
            }

            richTextBox_controlstatus.Text = cs;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ReadNutrientStatus();
        }

        private void button_control_Click(object sender, EventArgs e)
        {

            if (comboBox_control.SelectedIndex == 0)
            {
                mIntergatedNode.controlNode(200, NODECONTROL_COMMAND.OPERATION_LOCAL);
            }
            else
            {

                mIntergatedNode.controlNode(300, NODECONTROL_COMMAND.OPERATION_REMOTE);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (mIntergatedNode.readNodeStatus() == true)
            {
                String cs = "";
                cs += "상태: " + KSX326xMetadata.GetStatusDescrition((STATUS_CODE)mIntergatedNode.status) + "\r\n";
                cs += "OPID: " + mIntergatedNode.opid + "\r\n";

                if ((NODECONTROL_COMMAND)mIntergatedNode.control == NODECONTROL_COMMAND.OPERATION_LOCAL)
                {
                    cs += "제어권: " + comboBox_control.Items[0].ToString();
                }
                else if ((NODECONTROL_COMMAND)mIntergatedNode.control == NODECONTROL_COMMAND.OPERATION_REMOTE)
                {
                    cs += "제어권: " + comboBox_control.Items[1].ToString();
                }
                else
                {
                    cs += "제어권: " +"지원하지않음." + "\r\n";
                }
                

                label_nodestatus.Text = cs;

            }



        }


    }
}

