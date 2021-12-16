using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MessageLoggerForm
{
    public class MsgStructure
    {
        /********************************** Message frame **********************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsFrameHeader
        {
            public byte ucPreamble;
            public byte ucDestAddress;
            public byte ucSourceAddress;
            public byte ucMsgType;
            public byte ucPayloadLen;
        };

        //! Payload content
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsPayload
        {
            public byte ucMsgId;        //Name of the message
            public byte ucCommand;      //Command of the message
            public byte ucQueryID;      //Query ID - Counter which increments for each message
            public byte[] aucData;      //Data array
        };

        //! Format of whole message frame
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsMessageFrame
        {
            public tsFrameHeader sHeader;
            public tsPayload sPayload;
            public uint ulCrc32;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
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

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tMsgHeartBeatOutput
        {
            public byte ucBrightness;
            public byte ucLedStatus;
            public byte ucOutputIndex;
        };

        //public struct tMsgInitOutputState tMsgRequestOutputState;
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
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
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsMsgOutputStateResponseCS
        {
            public uint    ulVoltage;          /* Voltage in millivolt */
            public ushort  uiCurrent;          /* Current in milli ampere */
            public ushort  siTemperature;
            public byte    ucOutputIndex;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tMsgCurrentTimeCS
        {
            public uint ulTicks;
            public byte ucHour;
            public byte ucMinutes;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsMsgVersionCS
        {
            public ushort uiVersion;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsMsgFaultMessageCS
        {
            public ushort uiErrorCode;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsMsgManualInitCS
        {
            public byte ucSetMinValue;
            public byte ucSetMaxValue;
            public byte ucOutputIndex;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsMsgUserTimerCS
        {
            public byte ucStartHour;
            public byte ucStopHour;
            public byte ucStartMin;
            public byte ucStopMin;
            public byte ucTimerIdx;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsMsgStillAliveCS
        {
	        public byte bResponse;
	        public byte bRequest;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsMsgErrorCode
        {
            public short uiErrorCode;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsMsgDebu
        {
            public char[] aucDebugMsg;
        };
    }
}
