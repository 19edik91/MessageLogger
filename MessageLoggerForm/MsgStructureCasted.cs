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

        public struct tsMsgOutputStateCS
        {
            public byte b7Brightness;           //Value in percent 0-100
            public byte bInitMenuActiveInv;     //Inverted bit of the Init menu active bit
            public byte bLedStatus;
            public byte bInitMenuActive;        //Bit of the init menu active
            public byte bAutomaticModeActive;
        };


        public struct tsMsgOutputStateResponseCS
        {
            public UInt16 uiVoltage;
            public UInt16 uiCurrent;
            public Int16 siTemperature;
        };


        public struct tsMsgVersionCS
        {
            public UInt16 uiVersion;
        };


        public struct tsMsgFaultMessageCS
        {
            public UInt16 uiErrorCode;
        };


        public struct tsMsgManualInitCS
        {
            public byte bSetMinValue;
            public byte bSetMaxValue;
        };


        public struct tsMsgUserTimerCS
        {
            public byte ucStartHour;
            public byte ucStopHour;
            public byte ucStartMin;
            public byte ucStopMin;
            public byte b7TimerIdx;
        };
    }
}
