using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLoggerForm
{
    public class MsgStructureCasted
    {
        /* This file is for the casted message structures which are used by the C# project. 
           Booleans have to be changed into "bytes" otherwise the casting will fail! */

        public struct tsMsgOutputState
        {
            public byte b7Brightness;           //Value in percent 0-100
            public byte bInitMenuActiveInv;     //Inverted bit of the Init menu active bit
            public byte bLedStatus;
            public byte bInitMenuActive;        //Bit of the init menu active
            public byte bAutomaticModeActive;
        };


        public struct tsMsgOutputStateResponse
        {
            public UInt16 uiVoltage;
            public UInt16 uiCurrent;
            public Int16 siTemperature;
        };


        public struct tsMsgVersion
        {
            public UInt16 uiVersion;
        };


        public struct tsMsgFaultMessage
        {
            public UInt16 uiErrorCode;
        };


        public struct tsMsgManualInit
        {
            public byte bSetMinValue;
            public byte bSetMaxValue;
        };


        public struct tsMsgUserTimer
        {
            public byte ucStartHour;
            public byte ucStopHour;
            public byte ucStartMin;
            public byte ucStopMin;
            public byte b7TimerIdx;
        };




#warning Initialisierung des arrays notwendig!
        public struct tsAllDlbSenseAdcResponse
        {
            public struct tsDlbVal
            {
                public Int16 siAvgVal;
                public Int16 siMinVal;
                public Int16 siMaxVal;
            };
            public tsDlbVal[] sDlbVal;
        };

        public struct tsDlbSenseAdcResponse
        {
            public Int16 siAvgVal;
            public Int16 siMinVal;
            public Int16 siMaxVal;
            public byte ucDlbIndex;
            public byte ucDigitalSense;
            public byte ucSenseIdx;
        };


        public struct tsRawAdcValBuff
        {
            public Int16[] siAdcVal;
            public Int16 siMaxVal;
            public Int16 siMinVal;
            public Int16 siAvgVal;
        };

#warning Initialisierung des arrays notwendig!
        public struct tsRawAdcValuesResponse
        {
            public tsRawAdcValBuff sRawAdcValBuff;
            public byte ucDlbIdx;
        };
    }
}
