using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKforKSX3267
{
    /// <summary>
    /// 장치의 메타데이터 객체
    /// </summary>
    public class KSX_Device
    {
        public String Class { get; set; }
        public String Type { get; set; }
        public String Model { get; set; }
        public String Name { get; set; }
        public String ValueUnit { get; set; }
        public String ValueType { get; set; }
        public int SignificantDigit { get; set; }
        public int Channel { get; set; }


        public KSX_CommSpec CommSpec { get; set; }

        public KSX_Device()
        {
            CommSpec = new KSX_CommSpec();
            //default 소수점 3자리
            SignificantDigit = 3;
        }

        public KSX_Device(StandardSensors mSensor)
        {
            CommSpec = new KSX_CommSpec();

            //공통
            this.Class = KSX326xMetadata.ELEMENT_classsensor;
            this.ValueType = "float";
            this.SignificantDigit = 0;

            this.CommSpec.KS_X_3267_2018.read = new reg_read();
            this.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_value);
            this.CommSpec.KS_X_3267_2018.read.items.Add(KSX326xMetadata.ELEMENT_status);
            this.CommSpec.KS_X_3267_2018.read.starting_register = 200 + (int)mSensor * 3;


            switch (mSensor)
            {
                case StandardSensors.AIR_TEMPERATURE:
                    this.Type = "temperature-sensor";
                    this.ValueUnit = "℃";
                    this.ValueType = "float";
                    this.Model = "SEN-TEMP";
                    this.Name = "온도센서";
                    this.SignificantDigit = 1;

                    break;
                case StandardSensors.HUMIDITY:
                    this.Type = "humidity-sensor";
                    this.ValueUnit = "%";
                    this.Model = "SEN-HUM";
                    this.Name = "습도센서";
                    this.SignificantDigit = 0;
                    break;
                case StandardSensors.CO2:
                    this.Type = "co2-sensor";
                    this.ValueUnit = "ppm";
                    this.Model = "SEN-CO2";
                    this.Name = "이산화탄소센서";
                    this.SignificantDigit = 0;
                    break;
                case StandardSensors.PYRANOMETER:
                    this.Type = "pyranometer-sensor";
                    this.ValueUnit = "W/m²";
                    this.Model = "SEN-RAD";
                    this.Name = "일사센서";
                    this.SignificantDigit = 0;
                    break;
                case StandardSensors.WINDDIRECTION:
                    this.Type = "winddirection-sensor";
                    this.ValueUnit = "°";
                    this.Model = "SEN-WDIR";
                    this.Name = "풍향센서";
                    this.SignificantDigit = 0;
                    break;

                case StandardSensors.WINDSPEED:
                    this.Type = "windspeed-sensor";
                    this.ValueUnit = "m/s";
                    this.Model = "SEN-WSPEED";
                    this.Name = "풍속센서";
                    this.SignificantDigit = 1;
                    break;

                case StandardSensors.RAIN_DETECTOR:
                    this.Type = "raindetector-sensor";
                    this.ValueUnit = " ";
                    this.Model = "SEN-RAIN-D";
                    this.Name = "감우센서";
                    this.SignificantDigit = 0;
                    break;
                case StandardSensors.QUANTUM:
                    this.Type = "quantum-sensor";
                    this.ValueUnit = "μmol/m²/s";
                    this.Model = "SEN-QUAN";
                    this.Name = "광량자센서";
                    this.SignificantDigit = 0;
                    break;
                case StandardSensors.SOIL_MOISTURE:
                    this.Type = "soilmoisture-sensor";
                    this.ValueUnit = "%vol.";
                    this.Model = "SEN-SMOI";
                    this.Name = "토향함수율센서";
                    this.SignificantDigit = 1;
                    break;
                case StandardSensors.TENSIOMETER:
                    this.Type = "tensiometer-sensor";
                    this.ValueUnit = "kPa.";
                    this.Model = "SEN-TEN";
                    this.Name = "토양장력센서";
                    this.SignificantDigit = 1;
                    break;

                case StandardSensors.EC:
                    this.Type = "ec-sensor";
                    this.ValueUnit = "dS/m";
                    this.ValueType = "float";
                    this.Model = "SEN-EC";
                    this.Name = "EC센서";
                    this.SignificantDigit = 2;
                    break;

                case StandardSensors.PH:
                    this.Type = "ph-sensor";
                    this.ValueUnit = " ";
                    this.ValueType = "float";
                    this.Model = "SEN-PH";
                    this.Name = "pH센서";
                    this.SignificantDigit = 2;
                    break;

                case StandardSensors.SOIL_TEMPERATURE:
                    this.Type = "soiltemperature-sensor";
                    this.ValueUnit = "℃";
                    this.ValueType = "float";
                    this.Model = "SEN-TEMP";
                    this.Name = "지온센서";
                    this.SignificantDigit = 2;
                    break;
                case StandardSensors.DEWPOINT:
                    this.Type = "dewpoint-sensor";
                    this.ValueUnit = "℃";
                    this.ValueType = "float";
                    this.Model = "SEN-DEW";
                    this.Name = "이슬점센서";
                    this.SignificantDigit = 1;
                    break;
                case StandardSensors.FLOW:
                    this.Type = "flow-sensor";
                    this.ValueUnit = "L";
                    this.ValueType = "float";
                    this.Model = "SEN-FLOW";
                    this.Name = "유량센서";
                    this.SignificantDigit = 1;
                    break;
                case StandardSensors.RAIN_GAUGE:
                    this.Type = "raingaue-sensor";
                    this.ValueUnit = "mm";
                    this.ValueType = "float";
                    this.Model = "SEN-RAIN-G";
                    this.Name = "강우량센서";
                    this.SignificantDigit = 1;
                    break;

                case StandardSensors.VOLTAGE:
                    this.Type = "voltage-sensor";
                    this.ValueUnit = "V";
                    this.ValueType = "float";
                    this.Model = "SEN-VOL";
                    this.Name = "전압센서";
                    this.SignificantDigit = 3;
                    break;
                case StandardSensors.WEIGHT:
                    this.Type = "weight-sensor";
                    this.ValueUnit = "Kg";
                    this.ValueType = "float";
                    this.Model = "SEN-WEIG";
                    this.Name = "무게센서";
                    this.SignificantDigit = 3;
                    break;

                case StandardSensors.CURRENT:
                    this.Type = "current-sensor";
                    this.ValueUnit = "A";
                    this.ValueType = "float";
                    this.Model = "SEN-CUR";
                    this.Name = "전류센서";
                    this.SignificantDigit = 3;
                    break;

                default:
                    this.Type = "default-sensor";
                    this.ValueUnit = " ";
                    this.ValueType = "float";
                    this.Model = "SEN-DEF";
                    this.Name = "디폴트센서";
                    this.SignificantDigit = 3;
                    break;


            }




        }


        public ACTUATOR_TYPE getActuatorType()
        {

            ACTUATOR_TYPE mtype = ACTUATOR_TYPE.SWITCH;
            if (Type.Contains(KSX326xMetadata.ELEMENT_typeswitch) == true)
            {
                mtype = ACTUATOR_TYPE.SWITCH;
            }
            if (Type.Contains(KSX326xMetadata.ELEMENT_typeretractable) == true)
            {
                mtype = ACTUATOR_TYPE.RETRACTABLE;
            }
            if (Type.Contains(KSX326xMetadata.ELEMENT_typenutrient) == true)
            {
                mtype = ACTUATOR_TYPE.NUTRIENT;
            }
            return mtype;
        }

        public CONTROL_LEVEL getControllevel()
        {

            CONTROL_LEVEL mlevel = CONTROL_LEVEL.LV_0;

            if (Type.Contains(KSX326xMetadata.ELEMENT_level1) == true)
            {
                mlevel = CONTROL_LEVEL.LV_1;
            }
            if (Type.Contains(KSX326xMetadata.ELEMENT_level2) == true)
            {
                mlevel = CONTROL_LEVEL.LV_2;
            }
            if (Type.Contains(KSX326xMetadata.ELEMENT_level3) == true)
            {
                mlevel = CONTROL_LEVEL.LV_3;
            }
            return mlevel;
        }

    }
    
}
