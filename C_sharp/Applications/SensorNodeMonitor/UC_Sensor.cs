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
using System.Windows.Forms.DataVisualization.Charting;

namespace SensorNodeMonitor
{
    public partial class UC_Sensor : UserControl
    {
        public readonly SensorDev mSensor;
        private readonly Form1 mMainform;

        public Series chartserie;
        

        public UC_Sensor(Form1 mform,SensorDev msensor)
        {
            InitializeComponent();
            mMainform = mform;
            mSensor = msensor;

            chartserie = new Series(mSensor.mDevice.Name);
            chartserie.LegendText = mSensor.mDevice.Name + "(" + mSensor.mDevice.ValueUnit + ")";
            chartserie.ChartType = SeriesChartType.Line;



            UpdateUI();

           




        }

        public void UpdateUI(bool isvalueset=false)
        {

            label_name.Text = mSensor.mDevice.Name;
            label_value.Text = mSensor.GetValuestring(true);
            label_status.Text = "상태: " + KSX326xMetadata.GetStatusDescrition((STATUS_CODE)mSensor.status);
            if (isvalueset == true)
            {
                chartserie.Points.AddXY(DateTime.Now, mSensor.value);
            }

        }

        private void click(object sender, EventArgs e)
        {
            Debug.WriteLine("click");
        }

        public void SetBKColor(bool isread)
        {
            if (isread == true)
            {
                this.BackColor = Color.FromArgb(255, 192, 128);

            }
            else
            {
                this.BackColor = Color.FromArgb(255, 128, 0);
                
               
            }
        }

        private void label_value_Click(object sender, EventArgs e)
        {

        }

        private void onClick(object sender, EventArgs e)
        {

            mMainform.OnSelectedSensor(this);

        }

    }
}
