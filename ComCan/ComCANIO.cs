using System;
using System.Collections.Generic;
using System.Linq;
using ML.Can;
using ML.Can.Interfaces;
using VCI_CAN_DotNET;

namespace ComCan
{
    public class ComCANIO : ICanIO
    {
        public string OpenCAN(string canName, int baundRate)
        {
            byte canNum = byte.Parse(canName.Substring(3, canName.Length - 3));
            _canNum = canNum;
            int Ret;
            byte[] Mod_CfgData = new byte[512];

            //Listen Only Mode
            Mod_CfgData[0] = 0; //CAN1 => 0:Disable, 1:Enable
            Mod_CfgData[1] = 0; //CAN2 => 0:Disable, 1:Enable
            VCI_SDK.VCI_Set_MOD_Ex(Mod_CfgData);

            Ret = VCI_SDK.VCI_OpenCAN_NoStruct(canNum, 1, //7 - CAN7, 1-DevType = 7565-H1
                (uint)(Convert.ToSingle(baundRate) * 1000), // baund rate = 50
                (uint)(Convert.ToSingle(1000) * 1000));
            return ErrMsg[Ret];
        }

        public string CloseCan()
        {
            int Ret;
            Ret = VCI_SDK.VCI_CloseCAN(_canNum);
            return ErrMsg[Ret];
        }

        public void SendData(List<CanDriver.canmsg_t> data)
        {
            byte CAN_No, Mode, RTR, DLC;
            CAN_No = 1;
            Mode = RTR = 0;
            DLC = 8;
            foreach (var canmsg in data)
            {
                int Ret;
                Ret = VCI_SDK.VCI_SendCANMsg_NoStruct(CAN_No,Mode,RTR,DLC,canmsg.id,canmsg.data);
            }
        }
        public List<CanDriver.canmsg_t> ReceiveMsgBlock(int msgCount)
        {
            int Ret = 0;
            byte CAN_No, Mode, RTR, DLC;
            byte[] Data = new byte[8];
            UInt32 CANID, TH, TL, DL, DH;
            var msgBlock = new List<CanDriver.canmsg_t>();
            UInt32 realCount = 0;
            CAN_No = 1;
            Mode = RTR = DLC = 0;
            CANID = TH = TL = 0;
            int ret;
            while (Ret++ < msgCount)
            {
                Mode = RTR = DLC = 0;
                CANID = TH = TL = 0;
                ret = VCI_CAN_DotNET.VCI_SDK.VCI_RecvCANMsg_NoStruct(CAN_No, ref Mode, ref RTR, ref DLC, ref CANID, ref TL, ref TH, Data);
                if ((ret == 14 && Ret==1)||ret==5)
                {
                    return msgBlock;
                }
                var msg = new CanDriver.canmsg_t
                {
                    id = CANID,
                    length = (short) Data.Count(),
                    data = new byte[Data.Count()]
                };
                for (int i = 0; i < msg.length; i++)
                {
                    msg.data[i] = Data[i];
                }
                msgBlock.Add(msg);
            }
            return msgBlock;
        }
        private readonly string[] ErrMsg = 
        {
	        "No_Err",				"DEV_ModName_Err",			"DEV_ModNotExist_Err",
	        "DEV_PortNotExist_Err",	"DEV_PortInUse_Err", 		"DEV_PortNotOpen_Err",		
	        "CAN_ConfigFail_Err",	"CAN_HARDWARE_Err",			"CAN_PortNo_Err",			
	        "CAN_FIDLength_Err",	"CAN_DevDisconnect_Err",	"CAN_TimeOut_Err",			
	        "CAN_ConfigCmd_Err",	"CAN_ConfigBusy_Err",		"CAN_RxBufEmpty",
	        "CAN_TxBufFull",        "CAN_UserDefISRNo_Err" ,    "CAN_HWSendTimerNo_Err"
        };

        private byte _canNum;
    }
}
