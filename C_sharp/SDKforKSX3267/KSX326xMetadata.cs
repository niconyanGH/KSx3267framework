using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace SDKforKSX3267
{
    /// <summary>
    /// KSX3267 기본센서 목록
    /// </summary>
    public enum StandardSensors
    {
        NONE = 0,
        AIR_TEMPERATURE = 1,
        HUMIDITY = 2,
        CO2 = 3,
        PYRANOMETER=4,
        WINDDIRECTION=5,
        WINDSPEED=6,
        RAIN_DETECTOR=7,
        QUANTUM=8,
        SOIL_MOISTURE=9,
        TENSIOMETER=10,
        EC=11,
        PH=12,
        SOIL_TEMPERATURE=13,
        DEWPOINT=14,
        FLOW=15,
        RAIN_GAUGE=16,
        VOLTAGE=17,
        WEIGHT=18,
        CURRENT=19,
    };



    public enum PRODUCT_TYPE
    {
        PTYPE_NONE = 0,
        PTYPE_SENSORNODE = 1,
        PTYPE_ACTUATORNODE = 2,
        PTYPE_INTEGRATEDNODE = 3,
    };


    public class reg_read
    {
        public int  starting_register { get; set; }

        public List<string> items { get; set; }

        public reg_read()
        {
            items=new List<string>();
        }
    }
    public class reg_write
    {
        public int starting_register { get; set; }

        public List<string> items { get; set; }
        public reg_write()
        {
            items=new List<string>();
        }
    }

    public class KSX_3267
    {
        public reg_read read { get; set; }
        public reg_write write { get; set; }



    }



    public class KSX_CommSpec
    {
        public KSX_3267 KS_X_3267_2018 { get; set; }
        
        public KSX_CommSpec()
        {
            KS_X_3267_2018 = new KSX_3267();
        }
        
    }

   
    
    
    
    public class KSX_NodeMeta
    {

          [JsonIgnore]
        public int CertificateAuthority;
         [JsonIgnore]
        public int CompanyCode;


        public string Class { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }

        public KSX_CommSpec CommSpec { get; set; }

        public List<KSX_Device> Devices { get; set; }

        public KSX_NodeMeta()
        {
            CommSpec = new KSX_CommSpec();
            Devices = new List<KSX_Device>();


        }
    }




    public class KSX326xMetadata
    {

        public static String ELEMENT_classnode = "node";


        public static String ELEMENT_sensornode = "sensor-node";
        public static String ELEMENT_actuatornode = "actuator-node";
        public static String ELEMENT_integratednode = "integrated-node";

        public static String ELEMENT_classsensor = "sensor";
        public static String ELEMENT_classactuator = "actuator";
        //public static String ELEMENT_classnutrient = "nutrient";

        public static String ELEMENT_typeswitch = "switch";
        public static String ELEMENT_typeretractable = "retractable";
        public static String ELEMENT_typenutrient = "nutrient-supply";


        public static String ELEMENT_level1 = "level1";
        public static String ELEMENT_level2 = "level2";
        public static String ELEMENT_level3 = "level3";


        public static String ELEMENT_value = "value";
        public static String ELEMENT_opid = "opid";
        public static String ELEMENT_operation = "operation";
        public static String ELEMENT_control = "control";
        public static String ELEMENT_status = "status";
        public static String ELEMENT_remaintime = "remain-time";
        public static String ELEMENT_holdtime = "hold-time";
        public static String ELEMENT_time = "time";

        public static String ELEMENT_irrgationarea = "irrgation-area";
        public static String ELEMENT_alertinformation = "alert-information";


        public static String ELEMENT_stateholdtime = "state-hold-time";
        public static String ELEMENT_position = "position";


        public static String ELEMENT_startarea = "start-area";
        public static String ELEMENT_endarea = "end-area";
        public static String ELEMENT_onesec = "on-sec";
        public static String ELEMENT_ec = "EC";
        public static String ELEMENT_ph = "pH";





        public static String GetStatusDescrition( STATUS_CODE mStatus)
        {
            String cs = "";

            switch (mStatus)
            {

                case STATUS_CODE.READY: cs = "정상"; break;
                case STATUS_CODE.ERROR: cs = "오류"; break;
                case STATUS_CODE.BUSY: cs = "처리 불능"; break;
                case STATUS_CODE.VOLTAGE_ERROR: cs = "동작 전압 이상"; break;
                case STATUS_CODE.CURRENT_ERROR: cs = "동작 전류 이상"; break;
                case STATUS_CODE.TEMPERATURE_ERROR: cs = "동작 온도 이상"; break;
                case STATUS_CODE.FUSE_ERROR: cs = "휴즈 이상"; break;
                case STATUS_CODE.COMMON_RESERVED: cs = "공통 예약"; break;

                case STATUS_CODE.SENSOR_NEED_REPLACE: cs = "센서 및 소모품 교체 요망"; break;
                case STATUS_CODE.SENSOR_NEED_CALIBRATION: cs = "센서 교정 요망"; break;
                case STATUS_CODE.SENSOR_NEED_CHECK: cs = "센서 점검 필요"; break;

                case STATUS_CODE.SWITCH_ON: cs = "작동 중"; break;
                case STATUS_CODE.SWITCH_USER_CONTROL: cs = "사용자 제어 중"; break;

                case STATUS_CODE.REACTABLE_OPENING: cs = "여는 중"; break;
                case STATUS_CODE.REACTABLE_CLOSING: cs = "닫는 중"; break;
                case STATUS_CODE.REACTABLE_MANUAL_CONTROL: cs = "사용자 제어 중"; break;

                case STATUS_CODE.NUTRIENT_PREPARING: cs = "준비 중"; break;
                case STATUS_CODE.NUTRIENT_SUPPLYING: cs = "제공 중"; break;
                case STATUS_CODE.NUTRIENT_STOPPING: cs = "정지 중"; break;
                                    
                case STATUS_CODE.VENDOR_SPECIFIC_ERROR: cs = "제조사 정의 에러 코드"; break;

           
                default:
                    cs = "제조사정의";
                    break;
            }

            
            return cs;

        }



        public static String GetStringValueByDigit(int mdigit, float value)
        {
            String formatstr="";

            switch(mdigit)
            {

                case 0: formatstr ="{0:0}"; break;
                case 1: formatstr = "{0:0.0}"; break;
                case 2: formatstr = "{0:0.00}"; break;
                case 3: formatstr = "{0:0.000}"; break;
                case 4: formatstr = "{0:0.0000}"; break;
                case 5: formatstr = "{0:0.00000}"; break;
                case 6: formatstr = "{0:0.000000}"; break;
                default:
                    formatstr="{0:0.000}";
                    break;
            }

           String cs = String.Format(formatstr, value);
          return cs;

        }


        /// <summary>
        /// 노드에서 응답메시지의 크기를 결정하기 위해 옵션으로 설정되어있는 항목이 있는지 확인함.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="checkstr"></param>
        /// <param name="retsize"></param>
        /// <returns></returns>
        public static int getsizeWithItemcheck(List<string> items, string checkstr, int retsize)
        {
            int retint = 0;
            foreach (string istr in items)
            {
                if (istr.Contains(checkstr) == true)
                {

                    retint = retsize;
                    break;
                }
            }

            return retint;


        }


       
        
        /// <summary>
        ///  노드의 기본정보와 맞는 메타데이터를 리턴한다.
        /// </summary>
        /// <param name="minfo"></param>
        /// <returns></returns>
        public static KSX_NodeMeta GetNodeMetaData(NodeInfoByModbus minfo)
        {

            List<KSX_NodeMeta> mLMata = JsonFiles.getNodeMetadatas();


            foreach (KSX_NodeMeta mAMata in mLMata)
            {

                //회사코드와 기관코드 , 제품타입을 비교
                if (minfo.CompanyCode == mAMata.CompanyCode && minfo.CertificateAuthority == mAMata.CertificateAuthority)
                {
                    if (mAMata.Class.Contains("node") == true)
                    {
                        PRODUCT_TYPE ptype = (PRODUCT_TYPE)minfo.ProductType;

                        switch (ptype)
                        {
                            case PRODUCT_TYPE.PTYPE_SENSORNODE:
                                if (mAMata.Type.Contains(ELEMENT_sensornode) == true)
                                {
                                    return  mAMata;
                                }

                                break;
                            case PRODUCT_TYPE.PTYPE_ACTUATORNODE:
                                if (mAMata.Type.Contains(ELEMENT_actuatornode) == true)
                                {
                                    return mAMata;
                                }

                                break;
                            case PRODUCT_TYPE.PTYPE_INTEGRATEDNODE:

                                if (mAMata.Type.Contains(ELEMENT_integratednode) == true)
                                {
                                    return mAMata;
                                }
                                

                                break;
                            default:
                                break;
                        }



                    }

                }

            }



            return null;
        }

      

    }
}
