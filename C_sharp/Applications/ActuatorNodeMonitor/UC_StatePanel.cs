using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDKforKSX3267;
using System.Diagnostics;

namespace ActuatorNodeMonitorApp
{
	public partial class UC_StatePanel : UserControl
	{
		public ActuatorDev mActuator;
		public readonly Form1 mainform;

        public UC_StatePanel(Form1 parentForm, ActuatorDev actuator)
		{
			InitializeComponent();
			mActuator = actuator;
			mainform = parentForm;

			UpdateUI();
		}

        public void setStatus(ActuatorDev actuator)
		{
			mActuator.status = actuator.status;
			label3.Text = "상태: " + KSX326xMetadata.GetStatusDescrition((STATUS_CODE) mActuator.status);
			label4.Text = "남은시간 : " + (UInt32) mActuator.remain_time;
			label5.Text = "마지막 실행명령 : " + (UInt16) mActuator.opid;
		}

		public void UpdateUI()
		{
            ACTUATOR_TYPE type = (ACTUATOR_TYPE)mActuator.mDevice.getActuatorType();
			label1.Text = mActuator.mDevice.Name;
			//label2.Text = type.ToString();
			label3.Text = "상태: " + KSX326xMetadata.GetStatusDescrition((STATUS_CODE) mActuator.status);
			label4.Text = "남은시간 : " + (UInt32) mActuator.remain_time;
			label5.Text = "마지막 실행명령 : " + (UInt16) mActuator.opid;
		}

		private void click(object sender, EventArgs e)
		{
		}

		//public void SetBKColor(bool isread)
		//{
		//	if( isread == true )
		//	{
		//		this.BackColor = Color.LightSkyBlue;
		//	}
		//	else
		//	{
		//		this.BackColor = Color.LightGreen;
		//	}
		//}

		private void UC_StatePanel_Load(object sender, EventArgs e)
		{
		}

		private void UC_StatePanel_Click(object sender, EventArgs e)
		{
            ACTUATOR_TYPE type = (ACTUATOR_TYPE)mActuator.mDevice.getActuatorType();
            CONTROL_LEVEL level = mActuator.mDevice.getControllevel();
			mainform.panelClickControlViewUpdate(type, level);

			mainform.setData(mActuator);

			foreach( UC_StatePanel panel in mainform.mUCSList )
			{
				if( panel.BackColor != mainform.refreshColor )
				{
					panel.BackColor = mainform.basicColor;
				}
			}

			this.BackColor = mainform.focusColor;
		}
	}
}
