using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLoggerForm
{
    public class MsgStructure
    {
        /* This file is for the casted message structures which are used by the C# project. 
           Booleans have to be changed into "bytes" otherwise the casting will fail! */

        public struct tMsgRequestOutputState
        {
            public byte ucBrightness;              /* Requested brightness for this output */
            public byte ucInitMenuActiveInv;       /* Shows if Initializing menu is active (inverted bit) */
            public byte ucLedStatus;               /* The requested status of the output (On / Off) */
            public byte ucInitMenuActive;          /* Shows if Initializing menu is active */
            public byte ucAutomaticModeActive;     /* Shows if the automatic mode is enabled /disabled */
            public byte ucNightModeOnOff;          /* Night mode should be switched on or off */
            public byte ucMotionDetectionOnOff;    /* PIR detection enabled or disabled */
            public byte ucOutputIndex;             /* The which output shall be enabled */
            public byte ucBurnTime;                /* Requested burning time */
        };

        public struct tMsgHeartBeatOutput
        {
            public byte ucBrightness;
            public byte ucLedStatus;
            public byte ucOutputIndex;
        };

        //public struct tMsgInitOutputState tMsgRequestOutputState;

        public struct tMsgUpdateOutputStateCS
        {
            public byte ucBrightness;
            public byte ucLedStatus;            
            public byte ucNightModeOnOff;
            public byte ucMotionDetectionOnOff;
            public byte ucAutomaticModeOnOff;
            public byte ucOutputIndex;
            public int  slRemainingBurnTime;
        };

        public struct tsMsgOutputStateResponseCS
        {
            public uint    ulVoltage;          /* Voltage in millivolt */
            public ushort  uiCurrent;          /* Current in milli ampere */
            public ushort  siTemperature;
            public byte    ucOutputIndex;
        };


        public struct tMsgCurrentTimeCS
        {
            public uint ulTicks;
            public byte ucHour;
            public byte ucMinutes;
        };

        public struct tsMsgVersionCS
        {
            public ushort uiVersion;
        };


        public struct tsMsgFaultMessageCS
        {
            public ushort uiErrorCode;
        };


        public struct tsMsgManualInitCS
        {
            public byte ucSetMinValue;
            public byte ucSetMaxValue;
            public byte ucOutputIndex;
        };


        public struct tsMsgUserTimerCS
        {
            public byte ucStartHour;
            public byte ucStopHour;
            public byte ucStartMin;
            public byte ucStopMin;
            public byte ucTimerIdx;
        };

        public struct tsMsgStillAliveCS
        {
	        public byte bResponse;
	        public byte bRequest;
        };

        public struct tsMsgErrorCode
        {
            public short uiErrorCode;
        };

        public struct tsMsgDebu
        {
            public char[] aucDebugMsg;
        };
    }
}
