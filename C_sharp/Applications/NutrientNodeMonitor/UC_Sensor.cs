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


namespace NutrientNodeMonitor
{
    public partial class UC_Sensor : UserControl
    {
        public readonly SensorDev mSensor;

        public UC_Sensor(SensorDev msensor)
        {
            InitializeComponent();
            mSensor = msensor;
            UpdateUI();


        }

        public void UpdateUI()
        {
            label_name.Text = mSensor.mDevice.Name;
            label_value.Text = mSensor.GetValuestring(true);
            label_status.Text = "상태: " + KSX326xMetadata.GetStatusDescrition((STATUS_CODE)mSensor.status);

        }

        private void click(object sender, EventArgs e)
        {
            
        }

        public void SetBKColor(bool isread)
        {
            if(isread == true)
            {
                this.BackColor = Color.LightSkyBlue;
            }
                else
                {
                    this.BackColor = Color.LightGreen;
                }
        }

        private void label_value_Click(object sender, EventArgs e)
        {

        }

    }
}
