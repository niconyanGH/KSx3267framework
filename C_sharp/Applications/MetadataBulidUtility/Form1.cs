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


namespace MetadataBulidUtility
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KSX_Device newdev;

            KSX_NodeMeta mNmeta = new KSX_NodeMeta();


            mNmeta.Class = KSX326xMetadata.ELEMENT_classnode;
            mNmeta.Type = KSX326xMetadata.ELEMENT_integratednode;
            mNmeta.Name = "디폴트양액기노드";
            mNmeta.Model = "DEF-NUTRIENTNODE-001";
            


            //양액기 노드 상태정보
            mNmeta.CommSpec.KS_X_3267_2018.read = new reg_read();
            mNmeta.CommSpec.KS_X_3267_2018.read.starting_register = 201;
            mNmeta.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_opid);
            mNmeta.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_status);
            mNmeta.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_control);


            //양액기노드 제어
            mNmeta.CommSpec.KS_X_3267_2018.write = new reg_write();
            mNmeta.CommSpec.KS_X_3267_2018.write.starting_register = 501;
            mNmeta.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_operation);
            mNmeta.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_opid);
            mNmeta.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_control);




            for (int i = 0; i < 3; i++)
            {
                newdev = new KSX_Device(StandardSensors.EC);
                newdev.CommSpec.KS_X_3267_2018.read.starting_register = 204 + i * 3;
                mNmeta.Devices.Add(newdev);
            }

            for (int i = 0; i < 3; i++)
            {
                newdev = new KSX_Device(StandardSensors.PH);
                newdev.CommSpec.KS_X_3267_2018.read.starting_register = 213 + i * 3;
                mNmeta.Devices.Add(newdev);
            }

            for (int i = 0; i < 1; i++)
            {
                newdev = new KSX_Device(StandardSensors.PYRANOMETER);
                newdev.CommSpec.KS_X_3267_2018.read.starting_register = 222 + i * 3;
                mNmeta.Devices.Add(newdev);
            }





            for (int i = 0; i < 13; i++)
            {
                newdev = new KSX_Device(StandardSensors.FLOW);
               
                if (i == 0)
                {
                    newdev.Name = "전체유량센서";
                }
                else
                {
                    newdev.Name = "" + i + "구역유량센서";
                    newdev.Channel = i;
                }

                newdev.CommSpec.KS_X_3267_2018.read.starting_register = 225 + i * 3;
                mNmeta.Devices.Add(newdev);
            }




            newdev = new KSX_Device();
            newdev.Class = KSX326xMetadata.ELEMENT_classactuator;
            newdev.Type = KSX326xMetadata.ELEMENT_typenutrient + "/" + KSX326xMetadata.ELEMENT_level2;
            newdev.Name = "양액기";
            newdev.Model = "NUTRIENT";
            newdev.CommSpec.KS_X_3267_2018.read = new reg_read();
            newdev.CommSpec.KS_X_3267_2018.read.starting_register = 401;
            newdev.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_status);
            newdev.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_irrgationarea);
            newdev.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_alertinformation);
            newdev.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_opid);


            newdev.CommSpec.KS_X_3267_2018.write = new reg_write();
            newdev.CommSpec.KS_X_3267_2018.write.starting_register = 504;
            newdev.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_operation);
            newdev.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_opid);
            newdev.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_startarea);
            newdev.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_endarea);
            newdev.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_onesec);

            //json 해당필드 삭제를 위해
            newdev.SignificantDigit = 99999;


            mNmeta.Devices.Add(newdev);








           String mstr = JsonFiles.Serialize(mNmeta);

           String deletestr = "\"" + "SignificantDigit" + "\"" + ": 99999,\r\n      ";
           mstr = mstr.Replace(deletestr, "");


           richTextBox1.Text = mstr;

            

        //    File.WriteAllText("h:\\디폴트양액기.json", mstr);
             //KSX_NodeMeta md = JsonFiles.DeSerialize(mstr);

            Debug.WriteLine(mstr);
        }

        private void button_sensornode_Click(object sender, EventArgs e)
        {


            KSX_Device newdev;

            KSX_NodeMeta mNmeta = new KSX_NodeMeta();


            mNmeta.Class = KSX326xMetadata.ELEMENT_classnode;
            mNmeta.Type = KSX326xMetadata.ELEMENT_sensornode;
            mNmeta.Name = "디폴트센서노드";
            mNmeta.Model = "DEF-SENSORNODE-001";


            //센서 노드 상태정보
            mNmeta.CommSpec.KS_X_3267_2018.read = new reg_read();
            mNmeta.CommSpec.KS_X_3267_2018.read.starting_register = 201;
            mNmeta.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_opid);
            mNmeta.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_status);

            //센서노드 제어
            mNmeta.CommSpec.KS_X_3267_2018.write = new reg_write();
            mNmeta.CommSpec.KS_X_3267_2018.write.starting_register = 401;
            mNmeta.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_operation);
            mNmeta.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_opid);




            int read_start_addr_index = 0;

            //온도센서 3개
            for (int i = 0; i < 3; i++)
            {
                newdev = new KSX_Device(StandardSensors.AIR_TEMPERATURE);
                newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index)* 3;
                mNmeta.Devices.Add(newdev);
            }

           
             newdev = new KSX_Device(StandardSensors.HUMIDITY);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);


             newdev = new KSX_Device(StandardSensors.DEWPOINT);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);


             newdev = new KSX_Device(StandardSensors.RAIN_DETECTOR);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);


             newdev = new KSX_Device(StandardSensors.FLOW);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);


             newdev = new KSX_Device(StandardSensors.RAIN_GAUGE);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);


             newdev = new KSX_Device(StandardSensors.PYRANOMETER);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);



             newdev = new KSX_Device(StandardSensors.WINDSPEED);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);

             newdev = new KSX_Device(StandardSensors.WINDDIRECTION);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);

             newdev = new KSX_Device(StandardSensors.VOLTAGE);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);


             newdev = new KSX_Device(StandardSensors.CO2);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);

             newdev = new KSX_Device(StandardSensors.EC);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);

             newdev = new KSX_Device(StandardSensors.QUANTUM);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);


             newdev = new KSX_Device(StandardSensors.SOIL_MOISTURE);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);


             newdev = new KSX_Device(StandardSensors.TENSIOMETER);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);

             newdev = new KSX_Device(StandardSensors.PH);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);


             newdev = new KSX_Device(StandardSensors.SOIL_TEMPERATURE);
             newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
             mNmeta.Devices.Add(newdev);

             for (int i = 0; i < 7; i++)
             {
                 newdev = new KSX_Device(StandardSensors.AIR_TEMPERATURE);
                 newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
                 mNmeta.Devices.Add(newdev);
             }


             for (int i = 0; i < 2; i++)
             {
                 newdev = new KSX_Device(StandardSensors.HUMIDITY);
                 newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
                 mNmeta.Devices.Add(newdev);
             }

             for (int i = 0; i < 2; i++)
             {
                 newdev = new KSX_Device(StandardSensors.WEIGHT);
                 newdev.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (++read_start_addr_index) * 3;
                 mNmeta.Devices.Add(newdev);
             }


            String mstr = JsonFiles.Serialize(mNmeta);
            richTextBox1.Text = mstr;




        }

        private void button_actnode_Click(object sender, EventArgs e)
        {


            KSX_Device newdev;

            KSX_NodeMeta mNmeta = new KSX_NodeMeta();


            mNmeta.Class = KSX326xMetadata.ELEMENT_classnode;
            mNmeta.Type = KSX326xMetadata.ELEMENT_actuatornode;
            mNmeta.Name = "디폴트구동기노드";
            mNmeta.Model = "DEF-ACTNODE-001";


            //센서 노드 상태정보
            mNmeta.CommSpec.KS_X_3267_2018.read = new reg_read();
            mNmeta.CommSpec.KS_X_3267_2018.read.starting_register = 201;
            mNmeta.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_opid);
            mNmeta.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_status);

            //센서노드 제어
            mNmeta.CommSpec.KS_X_3267_2018.write = new reg_write();
            mNmeta.CommSpec.KS_X_3267_2018.write.starting_register = 501;
            mNmeta.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_operation);
            mNmeta.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_opid);





            //스위치 16개
            for (int i = 0; i < 16; i++)
            {

                newdev = new KSX_Device();
                newdev.Class = KSX326xMetadata.ELEMENT_classactuator;
                newdev.Type = KSX326xMetadata.ELEMENT_typeswitch + "/" + KSX326xMetadata.ELEMENT_level1;
                newdev.Channel = i;
                newdev.Name = "스위치" + (i+1);
                newdev.Model = "SWITCH-LV1";

             

                newdev.CommSpec.KS_X_3267_2018.read = new reg_read();
                newdev.CommSpec.KS_X_3267_2018.read.starting_register = 203 + i*4;
                newdev.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_opid);
                newdev.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_status);
                newdev.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_remaintime);


                newdev.CommSpec.KS_X_3267_2018.write = new reg_write();
                newdev.CommSpec.KS_X_3267_2018.write.starting_register = 503+i*4;
                newdev.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_operation);
                newdev.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_opid);
                newdev.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_holdtime);


                //json 해당필드 삭제를 위해
                newdev.SignificantDigit = 99999;

                mNmeta.Devices.Add(newdev);
            }


            //개폐기 8개
            for (int i = 0; i < 8; i++)
            {

                newdev = new KSX_Device();
                newdev.Class = KSX326xMetadata.ELEMENT_classactuator;
                newdev.Type = KSX326xMetadata.ELEMENT_typeretractable + "/" + KSX326xMetadata.ELEMENT_level1;
                newdev.Channel = i;
                newdev.Name = "개폐기" + (i + 1);
                newdev.Model = "MOTOR-LV1";
                newdev.CommSpec.KS_X_3267_2018.read = new reg_read();
                newdev.CommSpec.KS_X_3267_2018.read.starting_register = 267 + i * 4;
                newdev.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_opid);
                newdev.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_status);
                newdev.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_remaintime);


                newdev.CommSpec.KS_X_3267_2018.write = new reg_write();
                newdev.CommSpec.KS_X_3267_2018.write.starting_register = 567 + i * 4;
                newdev.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_operation);
                newdev.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_opid);
                newdev.CommSpec.KS_X_3267_2018.write.items.Add(KSX326xMetadata.ELEMENT_time);



                //json 해당필드 삭제를 위해
                newdev.SignificantDigit = 99999;


                mNmeta.Devices.Add(newdev);
            }


          

            String mstr = JsonFiles.Serialize(mNmeta);

            //구동기 노드에서 필요없는 SignificantDigit 필드 삭제
            String deletestr = "\""+"SignificantDigit" +"\""+ ": 99999,\r\n      ";
            mstr = mstr.Replace(deletestr, "");

            richTextBox1.Text = mstr;


        }

        private void button_base64_Click(object sender, EventArgs e)
        {
            String mstr = JsonFiles.encodingbase64(richTextBox1.Text);


            richTextBox_base64.Text = mstr;

        }
    }
}
