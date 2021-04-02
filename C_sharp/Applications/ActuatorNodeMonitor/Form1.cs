using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDKforKSX3267;
using System.Threading;
using RS485ModbusLibary;
using System.Diagnostics;

namespace ActuatorNodeMonitorApp
{
	public partial class Form1 : Form
	{
        public STDModbusMaster mRTUMaster = new STDModbusMaster();
		ActuatorNode mActuatorNode;
		public List<UC_StatePanel> mUCSList = new List<UC_StatePanel>();
		ActuatorDev mSelectedActuator;

		public Color focusColor = Color.DarkCyan;
        public Color refreshColor = Color.LightCyan;
        public Color basicColor = Color.LightBlue ;

		int switchCount;
		int retractableCount;
		bool cmdRequest = false;
		int refreshTime = 1000;

		bool SwitchTime = false;
		bool SwitchRatio = false;
		bool RetracTableTime = false;
		bool RetracTablePosition = false;

        public bool isrefreshThread_run=false;

		public Form1()
		{
			InitializeComponent();

			groupBox1.Enabled = false;


            

            string[] comlist = System.IO.Ports.SerialPort.GetPortNames();
            comboBox_comlist.Items.AddRange(comlist);



			rb_switchBasicControl.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
			rb_switchTimeControl.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
			rb_switchRatioControl.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
			rb_retbasicControl.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
			rb_retTimeControl.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
			rb_retPositionControl.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
		}



		private void Form1_Load(object sender, EventArgs e)
		{

		}

		public void initLayout()
		{
			gb_switch.Visible = true;
			gb_retractable.Visible = true;
			gb_switch.Enabled = false;
			gb_retractable.Enabled = false;
			rb_switchBasicControl.Checked = true;
			rb_retbasicControl.Checked = true;

			bt_switchOn.Enabled = false;
			bt_switchOff.Enabled = false;
			bt_retOn.Enabled = false;
			bt_retStop.Enabled = false;
			bt_retOff.Enabled = false;
		}

		public void switchInitLayout()
		{
			rb_switchBasicControl.Checked = true;
			gb_switch.Enabled = true;
			gb_retractable.Enabled = false;

			rb_switchBasicControl.Visible = false;
			rb_switchTimeControl.Visible = false;
			rb_switchRatioControl.Visible = false;

			tb_switchTime.Visible = false;
			tb_switchRatio.Visible = false;

			bt_switchOn.Enabled = false;
			bt_switchOff.Enabled = false;

		}

		public void retractableInitLayout()
		{
			rb_retbasicControl.Checked = true;
			gb_switch.Enabled = false;
			gb_retractable.Enabled = true;

			rb_retbasicControl.Visible = false;
			rb_retTimeControl.Visible = false;
			rb_retPositionControl.Visible = false;

			tb_retTime.Visible = false;
			tb_retPosition.Visible = false;

			gb_setConfig.Visible = false;

			bt_retOn.Enabled = false;
			bt_retStop.Enabled = false;
			bt_retOff.Enabled = false;
		}

		//개별 패널 클릭시 Ui 변경
        public void panelClickControlViewUpdate(ACTUATOR_TYPE type, CONTROL_LEVEL level)
		{
			Debug.WriteLine("panelClickControlViewUpdate ");
			if( type == ACTUATOR_TYPE.SWITCH )
			{
				switchInitLayout();			// 레벨 0은 InitLayout 상태이다 같다.

                if (level == CONTROL_LEVEL.LV_1)
				{
					rb_switchBasicControl.Visible = true;
					rb_switchTimeControl.Visible = true;
					bt_switchOn.Enabled = true;
					bt_switchOff.Enabled = true;
				}
                else if (level == CONTROL_LEVEL.LV_2)
				{
					rb_switchBasicControl.Visible = true;
					rb_switchTimeControl.Visible = true;
					rb_switchRatioControl.Visible = true;
					bt_switchOn.Enabled = true;
					bt_switchOff.Enabled = true;
				}
			}
			else if( type == ACTUATOR_TYPE.RETRACTABLE )
			{
				retractableInitLayout();

                if (level == CONTROL_LEVEL.LV_1)
				{
					rb_retbasicControl.Visible = true;
					rb_retTimeControl.Visible = true;
					bt_retOn.Enabled = true;
					bt_retStop.Enabled = true;
					bt_retOff.Enabled = true;
				}
                else if (level == CONTROL_LEVEL.LV_2)
				{
					rb_retbasicControl.Visible = true;
					rb_retTimeControl.Visible = true;
					rb_retPositionControl.Visible = true;
					bt_retOn.Enabled = true;
					bt_retStop.Enabled = true;
					bt_retOff.Enabled = true;
					gb_setConfig.Visible = true;
				}
			}
		}

		//라디오 버튼 클릭시 Ui 변경
		private void radioButtons_CheckedChanged(object sender, EventArgs e)
		{
			Debug.WriteLine("radioButtons_CheckedChanged ");
			if( ((RadioButton) sender).Checked )
			{
				SwitchTime = false;
				SwitchRatio = false;
				RetracTableTime = false;
				if( ((RadioButton) sender) == rb_switchBasicControl )
				{
					tb_switchTime.Visible = false;
					tb_switchRatio.Visible = false;
				}
				else if( ((RadioButton) sender) == rb_switchTimeControl )
				{
					tb_switchTime.Visible = true;
					tb_switchRatio.Visible = false;

					SwitchTime = true;
				}
				else if( ((RadioButton) sender) == rb_switchRatioControl )
				{
					tb_switchTime.Visible = true;
					tb_switchRatio.Visible = true;

					SwitchRatio = true;
				}
				else if( ((RadioButton) sender) == rb_retbasicControl )
				{
					tb_retTime.Visible = false;
					tb_retPosition.Visible = false;
				}
				else if( ((RadioButton) sender) == rb_retTimeControl )
				{
					tb_retTime.Visible = true;
					tb_retPosition.Visible = false;

					RetracTableTime = true;
				}
				else if( ((RadioButton) sender) == rb_retPositionControl )
				{
					tb_retTime.Visible = true;
					tb_retPosition.Visible = true;

					RetracTablePosition = true;
				}
			}
		}

		private void comopenbtn_Click(object sender, EventArgs e)
		{
			
		}


        public void setData(ActuatorDev actuator)
		{
			mSelectedActuator = actuator;
		}

		// 실행
		private void sendCmd(ACTUATOR_COMMAND cmd)
		{
			Debug.WriteLine("실행타입 : " + cmd.ToString());

			cmdRequest = true;

			UInt32 switchHoldTime = 0;
			short ratio = 0;
			UInt32 retractableHoldTime = 0;
			int retracPos = 0;
			int openTime = 0;
			int closeTime = 0;

			switch( cmd )
			{
			case ACTUATOR_COMMAND.OPERATION_OFF_STOP:
			case ACTUATOR_COMMAND.OPERATION_SWITCH_ON:
			case ACTUATOR_COMMAND.OPERATION_RETRACTABLE_OPEN:
			case ACTUATOR_COMMAND.OPERATION_RETRACTABLE_CLOSE:
				break;
			case ACTUATOR_COMMAND.OPERATION_SWITCH_TIMED_ON:
				switchHoldTime = (UInt32) textboxNullCheck(tb_switchTime);
				break;
			case ACTUATOR_COMMAND.OPERATION_SWITCH_DIREATIONAL_ON:
				switchHoldTime = (UInt32) textboxNullCheck(tb_switchTime);
				ratio = (Int16) textboxNullCheck(tb_switchRatio);
				break;
			case ACTUATOR_COMMAND.OPERATION_RETRACTABLE_TIMED_OPEN:
			case ACTUATOR_COMMAND.OPERATION_RETRACTABLE_TIMED_CLOSE:
				retractableHoldTime = (UInt32) textboxNullCheck(tb_retTime);
				break;
			case ACTUATOR_COMMAND.OPERATION_RETRACTABLE_SET_POSITION:
				retractableHoldTime = (UInt32) textboxNullCheck(tb_retTime);
				retracPos = textboxNullCheck(tb_retPosition);
				break;
			case ACTUATOR_COMMAND.OPERATION_RETRACTABLE_SET_CONFIG:
				openTime = textboxNullCheck(tb_openTime);
				closeTime = textboxNullCheck(tb_closeTime);
				break;
			}

            ACTUATOR_TYPE actuatorType = (ACTUATOR_TYPE)mSelectedActuator.mDevice.getActuatorType();
			if( actuatorType == ACTUATOR_TYPE.SWITCH )
			{
				mActuatorNode.controlSwitch(mSelectedActuator, cmd, (UInt16) cmd, switchHoldTime, ratio);
				Debug.WriteLine("cmd : " + cmd.ToString() + " switchHoldTime : " + switchHoldTime + " ratio : " + ratio);
			}
			else if( actuatorType == ACTUATOR_TYPE.RETRACTABLE )
			{
				mActuatorNode.controlReactable(mSelectedActuator, cmd, (UInt16) cmd, retractableHoldTime, retracPos, openTime, closeTime);
				Debug.WriteLine("cmd : " + cmd.ToString() + " retractableHoldTime : " + retractableHoldTime + " retracPos : " + retracPos + " openTime : " + openTime + " closeTime : " + closeTime);
			}

			mActuatorNode.readDeviceStatus(mSelectedActuator);

			cmdRequest = false;
		}

		private void resetPanelColor()
		{
			foreach( UC_StatePanel panel in mUCSList )
			{
				setPanelColor(panel, basicColor);
			}
		}

        private void setStatusTextUpdate(FlowLayoutPanel panelTable, int sensorIndex, ActuatorDev actuator)
		{
            if (isrefreshThread_run == true)
            {
                UC_StatePanel panel = (UC_StatePanel)panelTable.Controls[sensorIndex];
                panel.setStatus(actuator);
            }
		}

		private void refreshPanelThread()
		{
			while( true )
			{
				for( int i=0; i < flowLayoutPanel1.Controls.Count + flowLayoutPanel2.Controls.Count; i++ )
				{

                    if (isrefreshThread_run ==false)
                    {
                        return;
                    }
					Thread.Sleep(refreshTime);
                    if (cmdRequest)
                    {
                        continue;
                    }

                    ActuatorDev actuator = mActuatorNode.mActuatorDevices[i];
					mActuatorNode.readDeviceStatus(actuator);
					ACTUATOR_TYPE type = (ACTUATOR_TYPE) actuator.mDevice.getActuatorType();

					UC_StatePanel panel = mUCSList[i];
					resetPanelColor();
					setPanelColor(panel, refreshColor);

					this.Invoke((MethodInvoker) delegate
					{
						if( type == ACTUATOR_TYPE.SWITCH )
						{
							setStatusTextUpdate(flowLayoutPanel1, i, actuator);
						}
						else if( type == ACTUATOR_TYPE.RETRACTABLE )
						{
							setStatusTextUpdate(flowLayoutPanel2, i - flowLayoutPanel1.Controls.Count, actuator);
						}
					});

				}

			}
		}

		private void setPanelColor(UC_StatePanel panel, Color color)
		{
			if( panel.BackColor != focusColor )
			{
				panel.BackColor = color;
			}
		}

		//스위치 on
		private void button1_Click(object sender, EventArgs e)
		{
			if( mSelectedActuator == null )
				return;

			if( SwitchTime )
			{
				sendCmd(ACTUATOR_COMMAND.OPERATION_SWITCH_TIMED_ON);
			}
			else if( SwitchRatio )
			{
				sendCmd(ACTUATOR_COMMAND.OPERATION_SWITCH_DIREATIONAL_ON);
			}
			else
			{
				sendCmd(ACTUATOR_COMMAND.OPERATION_SWITCH_ON);
			}
		}

		//스위치 off
		private void button2_Click(object sender, EventArgs e)
		{
			sendCmd(ACTUATOR_COMMAND.OPERATION_OFF_STOP);
		}

		//개폐기 on
		private void button3_Click(object sender, EventArgs e)
		{
			if( RetracTableTime )
			{
				sendCmd(ACTUATOR_COMMAND.OPERATION_RETRACTABLE_TIMED_OPEN);
			}
			else if( RetracTablePosition )
			{
				sendCmd(ACTUATOR_COMMAND.OPERATION_RETRACTABLE_SET_POSITION);
			}
			else
			{
				sendCmd(ACTUATOR_COMMAND.OPERATION_RETRACTABLE_OPEN);
			}
		}

		//개폐기 stop
		private void button4_Click(object sender, EventArgs e)
		{
			sendCmd(ACTUATOR_COMMAND.OPERATION_OFF_STOP);
		}

		//개폐기 off
		private void button5_Click(object sender, EventArgs e)
		{
			if( RetracTableTime )
			{
				sendCmd(ACTUATOR_COMMAND.OPERATION_RETRACTABLE_TIMED_CLOSE);
			}
			else
			{
				sendCmd(ACTUATOR_COMMAND.OPERATION_RETRACTABLE_CLOSE);
			}
		}

		//개폐 폭 설정
		private void button6_Click(object sender, EventArgs e)
		{
			sendCmd(ACTUATOR_COMMAND.OPERATION_RETRACTABLE_SET_POSITION);
		}

		//개폐 구동속도 설정
		private void button7_Click(object sender, EventArgs e)
		{
			sendCmd(ACTUATOR_COMMAND.OPERATION_RETRACTABLE_SET_CONFIG);
		}

		private Int32 textboxNullCheck(TextBox tb)
		{
			if( tb.Text != null && !string.IsNullOrWhiteSpace(tb.Text) )
			{
				return Int32.Parse(tb.Text);
			}
			else
				return 0;
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
                   
                    if (mtype == PRODUCT_TYPE.PTYPE_NONE)
                    {
                        strinfo += " , 장비없음...";
                    }
                    else if (mtype == PRODUCT_TYPE.PTYPE_ACTUATORNODE)
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
            mActuatorNode = new ActuatorNode(stationnumber, mRTUMaster);

            String cs = "";

            if (mActuatorNode.ReadNodeInformation() == true)
            {

                cs += " CertificateAuthority = " + mActuatorNode.mNodeInfo.CertificateAuthority + "\r\n";
                cs += " CompanyCode = " + mActuatorNode.mNodeInfo.CompanyCode + "\r\n";
                cs += " ProductType = " + mActuatorNode.mNodeInfo.ProductType + "\r\n";
                cs += " ProductCode = " + mActuatorNode.mNodeInfo.ProductCode + "\r\n";
                cs += " ChannelNumber = " + mActuatorNode.mNodeInfo.ChannelNumber + "\r\n";
                cs += " ProtocolVersion = " + mActuatorNode.mNodeInfo.ProtocolVersion + "\r\n";

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

        private void button_device_Click(object sender, EventArgs e)
        {



            if (mActuatorNode.ReadDeviceCodeList() == true)
            {

                if (mActuatorNode.CreateDevices() == true)
                {


                    comopenbtn.Enabled = false;
                    //	listBox_Log.Items.Add("장치 개수 = " + mActuatorNode.mActuatorDevices.Count);

                    mActuatorNode.readNodeStatus();
                    mActuatorNode.readAllactuatorstatus();

                    foreach (ActuatorDev actuator in mActuatorNode.mActuatorDevices)
                    {
                        ACTUATOR_TYPE type = (ACTUATOR_TYPE)actuator.mDevice.getActuatorType();

                        UC_StatePanel mUCS = new UC_StatePanel(this, actuator);

                        if (type == ACTUATOR_TYPE.SWITCH)
                        {
                            flowLayoutPanel1.Controls.Add(mUCS);
                            switchCount++;
                        }
                        else if (type == ACTUATOR_TYPE.RETRACTABLE)
                        {
                            flowLayoutPanel2.Controls.Add(mUCS);
                            retractableCount++;
                        }

                        mUCSList.Add(mUCS);

                    }

                    initLayout();

                    Thread rTh = new Thread(refreshPanelThread);
                    isrefreshThread_run = true;
                    
                    rTh.Start();



                }

            }

        }

        private void fclose(object sender, FormClosingEventArgs e)
        {

            isrefreshThread_run = false;
            Thread.Sleep(1000);


            
        }

        private void tb_retTime_TextChanged(object sender, EventArgs e)
        {

        }


	}
}
