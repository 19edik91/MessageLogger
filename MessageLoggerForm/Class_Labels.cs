using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLoggerForm
{
    public class Class_Lables
    {
        public const byte ucLabelsUsed = 4;

        public string szReqLed;
        public string szResLed;
        public string szBrightnessReq;
        public string szBrightnessRes;
        public string szAutoReq;
        public string szAutoRes;
        public string szVoltage;
        public string szCurrent;
        public string szPower;
        public string szTemp;

        public Class_Lables()
        {
            szReqLed = "";
            szResLed = "";
            szBrightnessReq = "";
            szBrightnessRes = "";
            szAutoReq = "";
            szAutoRes = "";
            szVoltage = "";
            szCurrent = "";
            szPower = "";
            szTemp = "";
        }

        /****************************************************
         * @brief: Standard constructor for this class
         * @param: none
         * @return: none
         ***************************************************/
        public void GetLables(ref string LblReqLed, ref string LblResLed, ref string LblBrightnessReq, ref string LblBrightnessRes, ref string LblAutoReq,
                                 ref string LblAutoRes, ref string LblVoltage, ref string LblCurrent, ref string LblPower, ref string LblTemp)
        {
            LblReqLed = this.szReqLed;
            LblResLed = this.szResLed;
            LblBrightnessReq = this.szBrightnessReq;
            LblBrightnessRes = this.szBrightnessRes;
            LblAutoReq = this.szAutoReq;
            LblAutoRes = this.szAutoRes;
            LblVoltage = this.szVoltage;
            LblCurrent = this.szCurrent;
            LblPower = this.szPower;
            LblTemp = this.szTemp;
        }

        public void SetLables(ref string LblReqLed, ref string LblResLed, ref string LblBrightnessReq, ref string LblBrightnessRes, ref string LblAutoReq,
                                 ref string LblAutoRes, ref string LblVoltage, ref string LblCurrent, ref string LblPower, ref string LblTemp)
        {
            if(LblReqLed.Length > 0)        this.szReqLed = LblReqLed;
            if(LblResLed.Length > 0)        this.szResLed = LblResLed;
            if(LblBrightnessReq.Length > 0) this.szBrightnessReq = LblBrightnessReq;
            if(LblBrightnessRes.Length > 0) this.szBrightnessRes = LblBrightnessRes;
            if(LblAutoReq.Length > 0)       this.szAutoReq = LblAutoReq;
            if(LblAutoRes.Length > 0)       this.szAutoRes = LblAutoRes;
            if(LblVoltage.Length > 0)       this.szVoltage = LblVoltage;
            if(LblCurrent.Length > 0)       this.szCurrent = LblCurrent;
            if(LblPower.Length > 0)         this.szPower = LblPower;
            if(LblTemp.Length > 0)          this.szTemp = LblTemp;
        }
    }
}
