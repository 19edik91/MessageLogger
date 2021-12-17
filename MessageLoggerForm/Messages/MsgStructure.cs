using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace MessageLoggerForm
{
    public class MsgStructure
    {
        /********************************** Message frame **********************************/
        //! Format of whole message frame
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsMessageFrame
        {
            //header
            public byte ucPreamble;
            public byte ucDestAddress;
            public byte ucSourceAddress;
            public byte ucMsgType;

            //Payload
            public byte ucMsgId;        //Name of the message
            public byte ucCommand;      //Command of the message
            public byte ucQueryID;      //Query ID - Counter which increments for each message
            public byte ucPayloadLen;   //Payload length
            public byte[] aucData;      //Data array

            //CRC
            public uint ulCrc32;

            /// <summary>
            /// Creates an array with the set values.
            /// </summary>
            /// <returns></returns>
            public byte[] ToArray()
            {
                var stream = new MemoryStream();
                var writer = new BinaryWriter(stream);

                writer.Write(this.ucPreamble);
                writer.Write(this.ucDestAddress);
                writer.Write(this.ucSourceAddress);
                writer.Write(this.ucMsgType);

                writer.Write(this.ucMsgId);
                writer.Write(this.ucCommand);
                writer.Write(this.ucQueryID);
                writer.Write(this.ucPayloadLen);
                for (int byteIdx = 0; byteIdx < this.ucPayloadLen; byteIdx++)
                    writer.Write(aucData[byteIdx]);

                writer.Write(this.ulCrc32);

                return stream.ToArray();
            }

            /// <summary>
            /// Creates a structure with the given bytestream
            /// </summary>
            /// <param name="array"></param>
            /// <returns></returns>
            public static tsMessageFrame FromArray(byte[] array)
            {
                var reader = new BinaryReader(new MemoryStream(array));

                var str = default(tsMessageFrame);
                str.ucPreamble = reader.ReadByte();
                str.ucDestAddress = reader.ReadByte();
                str.ucSourceAddress = reader.ReadByte();
                str.ucMsgType = reader.ReadByte();

                str.ucMsgId = reader.ReadByte();
                str.ucCommand = reader.ReadByte();
                str.ucQueryID = reader.ReadByte();
                str.ucPayloadLen = reader.ReadByte();
                str.aucData = reader.ReadBytes(str.ucPayloadLen);

                str.ulCrc32 = reader.ReadUInt32();
                return str;
            }
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
        public struct tMsgUpdateOutputState
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
        public struct tsMsgOutputStateResponse
        {
            public uint    ulVoltage;          /* Voltage in millivolt */
            public ushort  uiCurrent;          /* Current in milli ampere */
            public ushort  siTemperature;
            public byte    ucOutputIndex;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tMsgCurrentTime
        {
            public uint ulTicks;
            public byte ucHour;
            public byte ucMinutes;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsMsgVersion
        {
            public ushort uiVersion;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsMsgFaultMessage
        {
            public ushort uiErrorCode;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsMsgManualInit
        {
            public byte ucSetMinValue;
            public byte ucSetMaxValue;
            public byte ucOutputIndex;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsMsgUserTimer
        {
            public byte ucStartHour;
            public byte ucStopHour;
            public byte ucStartMin;
            public byte ucStopMin;
            public byte ucTimerIdx;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct tsMsgStillAlive
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
        public struct tsMsgDebug
        {
            public char[] aucDebugMsg;
        };
    }
}
