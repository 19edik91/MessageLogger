using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using MessageLoggerForm.Messages;

namespace MessageLoggerForm
{
    public static class Class_Interpreter
    {
        /****************************************************
         * @brief: Interprates the command and returns a string
         * @param: ucCommand - Command as a byte
         * @return: sCmd - Interprated command as a string
         ***************************************************/
        public static string GetCommand(MsgEnum.teMessageCmd eCmd) =>
            eCmd switch
            {
                MsgEnum.teMessageCmd.eCmdGet => "Get",
                MsgEnum.teMessageCmd.eCmdSet => "Set",
                MsgEnum.teMessageCmd.eNoCmd => "No cmd",
                _ => "No cmd found"
            };


        /****************************************************
         * @brief: Interprates the type and returns a string
         * @param: ucType - Type as a byte
         * @return: sType - Interprated type as a string
         ***************************************************/
        public static string GetType(MsgEnum.teMessageType eType) =>
            eType switch
            {
                MsgEnum.teMessageType.eTypeRequest => "Request",        /* Request type. Expects a response */
                MsgEnum.teMessageType.eTypeResponse => "Response",       /* Response type to a request */
                MsgEnum.teMessageType.eTypeDenied => "Denied",         /* Denied message */
                MsgEnum.teMessageType.eTypeUnknown => "Unknown",        /* Unknown type ??? */
                MsgEnum.teMessageType.eTypeAck => "ACK",            /* Message received ACK (Request and response) */
                MsgEnum.teMessageType.eTypeDebug => "Debug",           /* Debug message type */
                _ => "Invalid type"
            };


        /****************************************************
         * @brief: Interprates the address and returns a string
         * @param: ucAddress - Address as a byte
         * @return: sAddress - Interprated address as a string
         ***************************************************/
        public static string GetAddress(byte ucAddress) =>
            ucAddress switch
            {
                MsgEnum.ADDRESS_MASTER => "Cypress",
                MsgEnum.ADDRESS_SLAVE1 => "ESP",
                _ => "Invalid address"
            };


        /// <summary>
        /// Gets the interpretation for the message IDs
        /// </summary>
        /// <param name="eMsgID"></param>
        /// <returns></returns>
        public static string GetID(MsgEnum.teMessageId eMsgID) =>
            eMsgID switch
            {
                MsgEnum.teMessageId.eMsgRequestOutputStatus => "Request output status",     /*<-- Request for output values. Slave => Master        --> */
                MsgEnum.teMessageId.eMsgUpdateOutputStatus => "Update output status",     /*<-- Updated output values. Slave <= Master            --> */
                MsgEnum.teMessageId.eMsgVersion => "Version",     /*<-- Message for Software Version.  Slave <= Master    --> */
                MsgEnum.teMessageId.eMsgInitOutputStatus => "Init output status",     /*<-- Message with the output values from the FLASH.  Slave <= Master    --> */
                MsgEnum.teMessageId.eMsgErrorCode => "Error Message",     /*<-- Fault code message. Currently unknown handling    --> */
                MsgEnum.teMessageId.eMsgSleep => "Sleep",     /*<-- Sleep request. Slave <= Master                    --> */
                MsgEnum.teMessageId.eMsgWakeUp => "Wake up",     /*<-- Wake up request. Slave <=> Master                 --> */
                MsgEnum.teMessageId.eMsgAutoInitHardware => "Init hardware (Auto)",     /*<-- Automatic Hardware init request. Slave => Master  --> */
                MsgEnum.teMessageId.eMsgManualInitHardware => "Init hardware (Manual)",     /*<-- Message with the Min-Max-Values. Slave => Master  --> */
                MsgEnum.teMessageId.eMsgManualInitHwDone => "Init hardware done (Manual)",     /*<-- Message to trigger saving of the system data. Slave => Master  --> */
                MsgEnum.teMessageId.eMsgUserTimer => "User timer",     /*<-- Message with the user timers. Slave <=> Master    --> */
                MsgEnum.teMessageId.eMsgSystemStarted => "System started",     /*<-- Slave started message. Slave => Master            --> */
                MsgEnum.teMessageId.eMsgOutputState => "Output status",     /*<-- Sends the current, voltage and temperature values. Slave <= Master --> */
                MsgEnum.teMessageId.eMsgCurrentTime => "Time",     /*<-- The current time from the ethernet. Slave => Master  --> */
                MsgEnum.teMessageId.eMsgStillAlive => "Still alive",     /*<-- Alive check message before the slave is reseted   --> */
                MsgEnum.teMessageId.eMsgDebug => "Debug",     /*<-- Debug message --> */
                MsgEnum.teMessageId.eMsgInitDone => "System init done",     /*<-- Message for user settings sending done -->*/
                MsgEnum.teMessageId.eMsgHeartBeatOutput => "Heartbeat",     /*<-- Message for output heart beat. Shall not be handled as user input */
                _ => "Invalid message ID"
            };


        /// <summary>
        /// Validates the payload length by checking the structure size with the given byte stream length
        /// </summary>
        /// <typeparam name="T">Generic structure which shall be compared for </typeparam>
        /// <param name="byteStreamLen">The length of the bytestream which shall be compared with the structure</param>
        /// <returns>boolean - True when both sizes are identically </returns>
        private static unsafe bool ValidatePayloadLen<T>(int byteStreamLen) where T : unmanaged
        {
            if(sizeof(T) != byteStreamLen)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /****************************************************
         * @brief: Interprates the message ID and returns a string
         * @param: ucID - Message ID as a byte
         * @return: sID - Interprated ID as a string
         ***************************************************/
        public static string GetPayload(MsgStructure.tsMessageFrame sMsgFrame)
        {
            MsgEnum.cteMessageID eMsgID = new MsgEnum.cteMessageID();

            string szRet = "Invalid bytestream length";

            try
            {
                if (sMsgFrame.aucData != null)
                {
                    if (sMsgFrame.ucMsgId < Convert.ToInt16(MsgEnum.teMessageId.eMsgInvalid))
                    {

                        switch (eMsgID.ToEnum(sMsgFrame.ucMsgId))
                        {
                            case MsgEnum.teMessageId.eMsgInitOutputStatus:
                                {
                                    unsafe
                                    {
                                        int size = sizeof(MsgStructure.sOutputs) * MsgStructure.MAX_OUTPUTS + sizeof(MsgStructure.tMsgInitOutputState_SecondPart);

                                        if (sMsgFrame.aucData.Length != size)
                                        {
                                            return szRet = "Invalid bytestream length";
                                        }
                                    }

                                    MsgStructure.tMsgInitOutputState sMsgInit = new MsgStructure.tMsgInitOutputState();
                                    sMsgInit.asOutputs = new MsgStructure.sOutputs[MsgStructure.MAX_OUTPUTS];

                                    int arrayIdx = 0;

                                    //De-serialize first part of the array
                                    for (int OutputIdx = 0; OutputIdx < MsgStructure.MAX_OUTPUTS; OutputIdx++)
                                    {
                                        //Unsafe necessary because of the size of operator
                                        unsafe
                                        {
                                            arrayIdx = sizeof(MsgStructure.sOutputs) * OutputIdx;
                                            byte[] arr = new byte[sizeof(MsgStructure.sOutputs)];

                                            Array.Copy(sMsgFrame.aucData, arrayIdx, arr, 0, sizeof(MsgStructure.sOutputs));

                                            sMsgInit.asOutputs[OutputIdx] = Class_Helper.Serializer.DeserializeMarsh<MsgStructure.sOutputs>(arr);
                                        }
                                    }

                                    //De-serialize second part of the array
                                    unsafe
                                    {
                                        int sizeOf = sizeof(MsgStructure.tMsgInitOutputState_SecondPart);
                                        byte[] arr = new byte[sizeOf];

                                        Array.Copy(sMsgFrame.aucData, arrayIdx, arr, 0, sizeof(MsgStructure.tMsgInitOutputState_SecondPart));

                                        var payloadSec = Class_Helper.Serializer.DeserializeMarsh<MsgStructure.tMsgInitOutputState_SecondPart>(arr);

                                        sMsgInit.ucAutomaticModeActive = payloadSec.ucAutomaticModeActive;
                                        sMsgInit.ucBurnTime = payloadSec.ucBurnTime;
                                        sMsgInit.ucMotionDetectionOnOff = payloadSec.ucMotionDetectionOnOff;
                                        sMsgInit.ucNightModeOnOff = payloadSec.ucNightModeOnOff;
                                    }

                                    //Print the values in the interprated window
                                    foreach (MsgStructure.sOutputs sOutput in sMsgInit.asOutputs)
                                    {
                                        szRet += $"Output: {sOutput.ucOutputIndex} | Brightness: {sOutput.ucBrightness} | Status: {Convert.ToBoolean(sOutput.ucLedStatus)} | ";
                                    }

                                    szRet += $"Auto-Mode: {Convert.ToBoolean(sMsgInit.ucAutomaticModeActive)} |  Night mode: {Convert.ToBoolean(sMsgInit.ucNightModeOnOff)} | PIR Mode: {Convert.ToBoolean(sMsgInit.ucMotionDetectionOnOff)} |";
                                    szRet += $"Burntime: {sMsgInit.ucBurnTime}";
                                    break;
                                }

                            case MsgEnum.teMessageId.eMsgRequestOutputStatus:
                                {
                                    if (ValidatePayloadLen<MsgStructure.tMsgRequestOutputState>(sMsgFrame.aucData.Length))
                                    {
                                        //De-serialize payload into given structure 
                                        var payload = Class_Helper.Serializer.DeserializeMarsh<MsgStructure.tMsgRequestOutputState>(sMsgFrame.aucData);

                                        szRet = $"Output: {payload.ucOutputIndex} | Brightness: {payload.ucBrightness} | Status: {Convert.ToBoolean(payload.ucLedStatus)} | Init Menu: {Convert.ToBoolean(payload.ucInitMenuActive)} ";

                                        if (payload.ucOutputIndex < Form1.ucUsedLables)
                                        {
                                            Form1._labels[payload.ucOutputIndex].szBrightnessReq = payload.ucBrightness + "%";
                                            Form1._labels[payload.ucOutputIndex].szReqLed = Convert.ToBoolean(payload.ucLedStatus) ? "ON" : "OFF";
                                        }
                                    }
                                    break;
                                }

                            case MsgEnum.teMessageId.eMsgUpdateOutputStatus:
                                {
                                    if (ValidatePayloadLen<MsgStructure.tMsgUpdateOutputState>(sMsgFrame.aucData.Length))
                                    {
                                        //De-serialize payload
                                        var payload = Class_Helper.Serializer.DeserializeMarsh<MsgStructure.tMsgUpdateOutputState>(sMsgFrame.aucData);

                                        szRet = $"Output: {payload.ucOutputIndex} | Brightness: {payload.ucBrightness}% | Status: {Convert.ToBoolean(payload.ucLedStatus)} | Nightmode: {Convert.ToBoolean(payload.ucNightModeOnOff)} | ";
                                        szRet += $"PIR-Status: {Convert.ToBoolean(payload.ucMotionDetectionOnOff)} | Burn time: {payload.slRemainingBurnTime}";


                                        if (payload.ucOutputIndex < Form1.ucUsedLables)
                                        {
                                            Form1._labels[payload.ucOutputIndex].szBrightnessRes = payload.ucBrightness + "%";
                                            Form1._labels[payload.ucOutputIndex].szResLed = Convert.ToBoolean(payload.ucLedStatus) ? "ON" : "OFF";
                                            Form1._labels[payload.ucOutputIndex].szAutoRes = "N/A";
                                        }
                                    }
                                    break;
                                }

                            case MsgEnum.teMessageId.eMsgCurrentTime:
                                {
                                    if (ValidatePayloadLen<MsgStructure.tMsgCurrentTime>(sMsgFrame.aucData.Length))
                                    {
                                        //De-serialize payload
                                        var payload = Class_Helper.Serializer.DeserializeMarsh<MsgStructure.tMsgCurrentTime>(sMsgFrame.aucData);

                                        szRet = $"Ticks: {payload.ulTicks} | Hours: {payload.ucHour} | Minutes: {payload.ucMinutes}";
                                    }
                                    break;
                                }

                            case MsgEnum.teMessageId.eMsgOutputState:
                                {
                                    if (ValidatePayloadLen<MsgStructure.tsMsgOutputStateResponse>(sMsgFrame.aucData.Length))
                                    {
                                        //De-serializes payload
                                        var payload = Class_Helper.Serializer.DeserializeMarsh<MsgStructure.tsMsgOutputStateResponse>(sMsgFrame.aucData);

                                        szRet = $"Output: {payload.ucOutputIndex} | Voltage: {payload.ulVoltage}mV | Current: {payload.uiCurrent}mA | ";
                                        string Temp = Convert.ToString(payload.siTemperature);
                                        Temp = Temp.Insert((Temp.Length - 1), ".");
                                        szRet += "Temp: " + Temp + "°C";

                                        if (payload.ucOutputIndex < Form1.ucUsedLables)
                                        {
                                            Form1._labels[payload.ucOutputIndex].szCurrent = payload.uiCurrent + "mA";
                                            Form1._labels[payload.ucOutputIndex].szVoltage = payload.ulVoltage + "mV";
                                            Form1._labels[payload.ucOutputIndex].szPower = Convert.ToString((payload.ulVoltage * payload.uiCurrent) / 1000) + "W";
                                            Form1._labels[payload.ucOutputIndex].szTemp = Temp + "°C";
                                        }
                                    }
                                    break;
                                }

                            case MsgEnum.teMessageId.eMsgErrorCode:
                                {
                                    if (ValidatePayloadLen<MsgStructure.tsMsgErrorCode>(sMsgFrame.aucData.Length))
                                    {
                                        //De-serializes payload
                                        var payload = Class_Helper.Serializer.DeserializeMarsh<MsgStructure.tsMsgErrorCode>(sMsgFrame.aucData);

                                        //Get specific enum and cast it directly
                                        ErrorCode.ceFaultCodes ecFaultCode = new ErrorCode.ceFaultCodes();
                                        szRet = $"{ecFaultCode.ToEnum(payload.uiErrorCode)}";
                                    }
                                    break;
                                }

                            case MsgEnum.teMessageId.eMsgManualInitHardware:
                                {
                                    if (ValidatePayloadLen<MsgStructure.tsMsgManualInit>(sMsgFrame.aucData.Length))
                                    {
                                        //De-serializes payload
                                        var payload = Class_Helper.Serializer.DeserializeMarsh<MsgStructure.tsMsgManualInit>(sMsgFrame.aucData);

                                        szRet = $"SetMinVal: {payload.ucSetMinValue} | SetMaxVal: {payload.ucSetMaxValue} | Output: {payload.ucOutputIndex}";
                                    }
                                    break;
                                }

                            case MsgEnum.teMessageId.eMsgUserTimer:
                                {
                                    if (ValidatePayloadLen<MsgStructure.tsMsgUserTimer>(sMsgFrame.aucData.Length))
                                    {
                                        //De-serializes payload
                                        var payload = Class_Helper.Serializer.DeserializeMarsh<MsgStructure.tsMsgUserTimer>(sMsgFrame.aucData);

                                        szRet = $"Timer: {payload.ucTimerIdx} | Start: {payload.ucStartHour}:{payload.ucStartMin} | Stop: {payload.ucStopHour}:{payload.ucStopMin}";
                                    }
                                    break;
                                }

                            case MsgEnum.teMessageId.eMsgVersion:
                                {
                                    if (ValidatePayloadLen<MsgStructure.tsMsgVersion>(sMsgFrame.aucData.Length))
                                    {
                                        //De-serializes payload
                                        var payload = Class_Helper.Serializer.DeserializeMarsh<MsgStructure.tsMsgVersion>(sMsgFrame.aucData);
                                        szRet = $"Version: {payload.uiVersion}";
                                    }
                                    break;
                                }

                            case MsgEnum.teMessageId.eMsgStillAlive:
                                {
                                    if (ValidatePayloadLen<MsgStructure.tsMsgStillAlive>(sMsgFrame.aucData.Length))
                                    {
                                        // De - serializes payload
                                        var payload = Class_Helper.Serializer.DeserializeMarsh<MsgStructure.tsMsgStillAlive>(sMsgFrame.aucData);
                                        szRet = $"Request: {payload.bRequest:X2} | Response: {payload.bResponse:X2}";
                                    }
                                    break;
                                }

                            case MsgEnum.teMessageId.eMsgDebug:
                                {
                                    szRet = Encoding.GetEncoding("UTF-8").GetString(sMsgFrame.aucData);
                                    break;
                                }

                            case MsgEnum.teMessageId.eMsgHeartBeatOutput:
                                {
                                    if (ValidatePayloadLen<MsgStructure.tMsgHeartBeatOutput>(sMsgFrame.aucData.Length))
                                    {
                                        var payload = Class_Helper.Serializer.DeserializeMarsh<MsgStructure.tMsgHeartBeatOutput>(sMsgFrame.aucData);
                                        szRet = $"Brightness: {payload.ucBrightness} | Status: {Convert.ToBoolean(payload.ucLedStatus)} | OutputIdx: {payload.ucOutputIndex}";
                                    }
                                    break;
                                }

                            case MsgEnum.teMessageId.eMsgEnableNightMode:
                                {
                                    if (ValidatePayloadLen<MsgStructure.tsMsgEnableNightMode>(sMsgFrame.aucData.Length))
                                    {
                                        var payload = Class_Helper.Serializer.DeserializeMarsh<MsgStructure.tsMsgEnableNightMode>(sMsgFrame.aucData);
                                        szRet = $"Night mode enabled: {Convert.ToBoolean(payload.ucNightModeStatus)}";
                                    }
                                    break;
                                }

                            case MsgEnum.teMessageId.eMsgEnableMotionDetect:
                                {
                                    if (ValidatePayloadLen<MsgStructure.tsMsgEnableMotionDetectStatus>(sMsgFrame.aucData.Length))
                                    {
                                        var payload = Class_Helper.Serializer.DeserializeMarsh<MsgStructure.tsMsgEnableMotionDetectStatus>(sMsgFrame.aucData);
                                        szRet = $"PIR enabled: {Convert.ToBoolean(payload.ucMotionDetectStatus)} | Burn time: {payload.ucBurnTime}";
                                    }
                                    break;
                                }

                            case MsgEnum.teMessageId.eMsgEnableAutomaticMode:
                                {
                                    if (ValidatePayloadLen<MsgStructure.tsMsgEnableAutomaticMode>(sMsgFrame.aucData.Length))
                                    {
                                        var payload = Class_Helper.Serializer.DeserializeMarsh<MsgStructure.tsMsgEnableAutomaticMode>(sMsgFrame.aucData);
                                        szRet = $"Automatic mode enabeld: {Convert.ToBoolean(payload.ucAutomaticModeStatus)}";
                                    }

                                    //TODO: CHange label
                                    break;
                                }

                            default:
                                break;
                        }
                    }
                    else
                    {
                        szRet = "Invalid Msg ID";
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return szRet;
        }
    }
}
