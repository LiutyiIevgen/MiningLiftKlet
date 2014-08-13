// #############################################################################
// *****************************************************************************
//                  Copyright (c) 2011, Advantech Automation Corp.
//      THIS IS AN UNPUBLISHED WORK CONTAINING CONFIDENTIAL AND PROPRIETARY
//               INFORMATION WHICH IS THE PROPERTY OF ADVANTECH AUTOMATION CORP.
//
//    ANY DISCLOSURE, USE, OR REPRODUCTION, WITHOUT WRITTEN AUTHORIZATION FROM
//               ADVANTECH AUTOMATION CORP., IS STRICTLY PROHIBITED.
// *****************************************************************************

// #############################################################################
//
// File:    AdvCANIO.cs
// Created: 4/8/2009
// Revision:6/5/2009
// Version: 1.0
//          - Initial version
//          2.0
//          - Compatible with 64-bit and 32-bit system
//          2.1 (2011-5-19)
//          - Fix bug of  WaitCommEvent
// Description: Implements IO function about how to access CAN WDM&CE driver
//
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using ML.Can;
using ML.Can.Interfaces;

namespace ML.AdvCan
{
    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class AdvCANIO : ICanIO
    {
  
        private IntPtr hDevice;                                                           //Device handle
        private IntPtr orgWriteBuf = IntPtr.Zero;                                         //Unmanaged buffer for write
        private IntPtr orgReadBuf = IntPtr.Zero;                                          //Unmanaged buffer for read
        private IntPtr lpCommandBuffer = IntPtr.Zero;                                     //Unmanaged buffer Command 
        private IntPtr lpConfigBuffer = IntPtr.Zero;                                      //Unmanaged buffer Config
        private IntPtr lpStatusBuffer = IntPtr.Zero;                                      //Unmanaged buffer Status
        private CanDriver.Command_par_t Command = new CanDriver.Command_par_t();                //Managed buffer for Command
        private CanDriver.Config_par_t Config = new CanDriver.Config_par_t();                   //Managed buffer for Cofig
        private int OutLen;                                                               //Out data length for DeviceIoControl
        private int OS_TYPE = System.IntPtr.Size;                                         //For judge x86 or x64 
        private uint EventCode = 0;                                                       //Event code for WaitCommEvent
        private IntPtr lpEventCode = IntPtr.Zero;                                         //Unmanaged buffer for Event code
        private IntPtr INVALID_HANDLE_VALUE = IntPtr.Zero;                                //Invalid handle
        public const int SUCCESS = 0;                                                     //Status definition : success
        public const int OPERATION_ERROR = -1;                                            //Status definition : device error or parameter error
        public const int TIME_OUT = -2;                                                   //Status definition : time out
        private uint MaxReadMsgNumber;                                                    //Max number of message in unmanaged buffer for read 
        private uint MaxWriteMsgNumber;                                                   //Max number of message in unmanaged buffer for write

        // Fields
        private Win32Events events;
        private Win32Ovrlap ioctlOvr;
        private ManualResetEvent ioctlEvent;
        private Win32Ovrlap txOvr;
        private ManualResetEvent writeEvent;
        private Win32Ovrlap rxOvr;
        private Win32Ovrlap eventOvr;
        AutoResetEvent readEvent;

        public AdvCANIO( )
        {

            hDevice = INVALID_HANDLE_VALUE;

            lpCommandBuffer = Marshal.AllocHGlobal(CanDriver.CAN_COMMAND_LENGTH);
            lpConfigBuffer = Marshal.AllocHGlobal(CanDriver.CAN_CONFIG_LENGTH);
            lpStatusBuffer = Marshal.AllocHGlobal(CanDriver.CAN_CANSTATUS_LENGTH);
            lpEventCode = Marshal.AllocHGlobal(Marshal.SizeOf(EventCode));
            Marshal.StructureToPtr(EventCode, lpEventCode, true);

            this.ioctlEvent = new ManualResetEvent(false);
            this.ioctlOvr = new Win32Ovrlap(this.ioctlEvent.SafeWaitHandle.DangerousGetHandle());

            this.writeEvent = new ManualResetEvent(false);
            this.txOvr = new Win32Ovrlap(this.writeEvent.SafeWaitHandle.DangerousGetHandle());

            readEvent = new AutoResetEvent(false);
            this.rxOvr = new Win32Ovrlap(readEvent.SafeWaitHandle.DangerousGetHandle());

            this.eventOvr = new Win32Ovrlap(readEvent.SafeWaitHandle.DangerousGetHandle());
            this.events = new Win32Events(this.eventOvr.memPtr);
        }

        ~AdvCANIO()
        {
            if (hDevice != INVALID_HANDLE_VALUE)
            {
                CanDriver.CloseHandle(hDevice);
                Thread.Sleep(100);

                Marshal.FreeHGlobal(lpCommandBuffer);
                Marshal.FreeHGlobal(lpConfigBuffer);
                Marshal.FreeHGlobal(lpStatusBuffer);
                Marshal.FreeHGlobal(lpEventCode);
                Marshal.FreeHGlobal(orgWriteBuf);
                Marshal.FreeHGlobal(orgReadBuf);
                this.ioctlEvent = null;
                this.ioctlOvr = null;
                this.writeEvent = null;
                this.txOvr = null;
                this.eventOvr = null;
                this.events = null;
                hDevice = INVALID_HANDLE_VALUE;
            }

        }



        public string OpenCAN(string canName, int baundRate)
        {
            string CanPortName;
            string StatusMsg = "";
            UInt16 BaudRateValue;
            int nRet = 0;
            uint WriteTimeOutValue;
            uint ReadTimeOutValue;
            CanPortName = canName; //Get CAN port name
            /**********************************************************************************************
                 *  NOTE: acCanOpen Usage
                 * 
                 *	  Description:
                 *		 Open can port by name, and indicate the max send and receive Frame number each time.
                 * 
                 *    acCanOpen arguments:
                 *		  PortName			         - port name
                 *		  synchronization	         - TRUE, synchronization ; FALSE, asynchronous
                 *		  MsgNumberOfReadBuffer	   - The max frames number to read each time
                 *		  MsgNumberOfWriteBuffer	- The max frames number to write each time
                 * 
                 *    When open port, user must pass the value of 'MsgNumberOfReadBuffer' and 'MsgNumberOfWriteBuffer' 
                 *    auguments to indicate the max sent and received packages number of each time.
                 *    In this example, we send 100 CAN frames by default
                 *    User can change the value of 'nMsgCount' to send different frames each time in this examples.
                 **********************************************************************************************/
            nRet = acCanOpen(CanPortName, false, 500, 500); //Open CAN port
            if (nRet < 0)
            {
                StatusMsg = "Failed to open the CAN port, please check the CAN port name!";
                return StatusMsg;
            }

            nRet = acEnterResetMode(); //Enter reset mode          
            if (nRet < 0)
            {
                StatusMsg = "Failed to stop opertion!";
                acCanClose();
                return StatusMsg;
            }
            //Set baud rate
            BaudRateValue = (ushort)baundRate; //Get baud rate
            nRet = acSetBaud(BaudRateValue); //Set Baud Rate
            if (nRet < 0)
            {
                StatusMsg = "Failed to set baud!";
                acCanClose();
                return StatusMsg;
            }


            WriteTimeOutValue = 3000; //Get timeout
            ReadTimeOutValue = WriteTimeOutValue;
            nRet = acSetTimeOut(ReadTimeOutValue, WriteTimeOutValue); //Set timeout
            if (nRet < 0)
            {
                StatusMsg = "Failed to set Timeout!";
                acCanClose();
                return StatusMsg;
            }

            nRet = acEnterWorkMode(); //Enter work mdoe
            if (nRet < 0)
            {
                StatusMsg = "Failed to restart operation!";
                acCanClose();
                return StatusMsg;
            }


            return "No_Err";
        }

        public string CloseCan()
        {
            acCanClose();
            return "No_Err";
        }

        public void SendData(List<CanDriver.canmsg_t> data)
        {
            string SendStatus;
            int nRet;
            var msgWrite = data; //Package for write   
            uint pulNumberofWritten = 0;
            SendStatus = "Package ";
            /**********************************************************************************************
                  *  NOTE: acCanWrite usage
                  * 
                  *    Description£º
                  *       Users can use this interface to send data to CAN port which was opened. 
                  *       One or more frames can be selected each time.
                  * 
                  *    Parameters:
                  *       msgWrite                - managed buffer to write
                  *       nWriteCount             - CAN frame number want to write each time
                  *       pulNumberofWritten      - Real number of frames sent to driver.
                  *    
                  *    In this example, we send 100 CAN frames defined by 'nMsgCount' each time by default.             
                  *    If user want to send one or more frames eache time, user can also change it as follows:
                  *    Firstly, open CAN port and pass the value of 'MsgNumberOfReadBuffer' and 'MsgNumberOfWriteBuffer'arguments.
                  *    About 'MsgNumberOfReadBuffer' and 'MsgNumberOfWriteBuffer', please see 'acCanPort' usage above.
                  *    Secondly, define the msgWrite according to the frame number user want to send each time.
                  *    Thirdly, define the value of 'nWriteCount'according to the frame number user want to send each time.
                  *    In this examples, user can only change the value of 'nMsgCount' to change the count of frame to send each time. 
                 /**********************************************************************************************/
            nRet = acCanWrite(msgWrite.ToArray(), (uint)data.Count, ref pulNumberofWritten); //Send frames
            if (nRet == AdvCANIO.TIME_OUT)
            {
                SendStatus += " sending timeout!";
            }
            else if (nRet == AdvCANIO.OPERATION_ERROR)
            {
                SendStatus += " sending error!";
            }
            else
            {
                SendStatus += " sending ok!";
            }
        }

        public List<CanDriver.canmsg_t> ReceiveMsgBlock(int msgCount)
        {
            string ReceiveStatus;
            int nRet;
            uint nReadCount = (uint)msgCount;
            uint pulNumberofRead = 0;

            var msgRead = new CanDriver.canmsg_t[msgCount];
            for (int i = 0; i < msgCount; i++)
            {
                msgRead[i].data = new byte[8];
            }

            nRet = acCanRead(msgRead, nReadCount, ref pulNumberofRead); //Receiving frames
            if (nRet == AdvCANIO.TIME_OUT)
            {
                ReceiveStatus = "Package ";
                ReceiveStatus += "receiving timeout!";
                return new List<CanDriver.canmsg_t>();
            }
            else if (nRet == AdvCANIO.OPERATION_ERROR)
            {
                ReceiveStatus = "Package ";
                ReceiveStatus += " error!";
                return null;
            }
            else
            {
                var ret = msgRead.ToList();
                //var ret = msgRead.ToList().Where(m=>m.id!=0xF).ToList();
                //if(ret.Count!=0)
                    ret.RemoveRange((int) pulNumberofRead, (int) (msgRead.Count() - pulNumberofRead));
                return ret;
            }
        }
        /*****************************************************************************
   *
   *    acCanOpen
   *
   *    Purpose:
   *    open can port by name 
   *		
   *
   *    Arguments:
   *        PortName                - port name
   *        synchronization         - true, synchronization ; false, asynchronous
   *        MsgNumberOfReadBuffer   - message number of read intptr
   *        MsgNumberOfWriteBuffer  - message number of write intptr
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/
        private int acCanOpen(string CanPortName, bool synchronization, uint MsgNumberOfReadBuffer, uint MsgNumberOfWriteBuffer)
        {
            CanPortName = "\\\\.\\" + CanPortName;                              
            if ( !synchronization )
                hDevice = CanDriver.CreateFile(CanPortName, CanDriver.GENERIC_READ + CanDriver.GENERIC_WRITE, 0, IntPtr.Zero, CanDriver.OPEN_EXISTING, CanDriver.FILE_ATTRIBUTE_NORMAL + CanDriver.FILE_FLAG_OVERLAPPED, IntPtr.Zero); 
            else
                hDevice = CanDriver.CreateFile(CanPortName, CanDriver.GENERIC_READ + CanDriver.GENERIC_WRITE, 0, IntPtr.Zero, CanDriver.OPEN_EXISTING, CanDriver.FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);
            if (hDevice.ToInt32() == -1)
            {
                hDevice = INVALID_HANDLE_VALUE;
                return OPERATION_ERROR;
            }
            if (hDevice != INVALID_HANDLE_VALUE )
            {
                MaxReadMsgNumber = MsgNumberOfReadBuffer;
                MaxWriteMsgNumber = MsgNumberOfWriteBuffer;
                orgReadBuf = Marshal.AllocHGlobal((int)(CanDriver.CAN_MSG_LENGTH * 10000));
                orgWriteBuf = Marshal.AllocHGlobal((int)(CanDriver.CAN_MSG_LENGTH * 10000));
                return SUCCESS;
            }
            else
                return OPERATION_ERROR;
        }

        /*****************************************************************************
   *
   *    acCanClose
   *
   *    Purpose:
   *        Close can port 
   *		
   *
   *    Arguments:
   *
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/
        private int acCanClose()
        {
            if (hDevice != INVALID_HANDLE_VALUE)
            {
                CanDriver.CloseHandle(hDevice);
                Thread.Sleep(100);
                Marshal.FreeHGlobal(orgWriteBuf);
                Marshal.FreeHGlobal(orgReadBuf);
                hDevice = INVALID_HANDLE_VALUE;
            }
                            

            return SUCCESS;
        }

        /*****************************************************************************
   *
   *    acEnterResetMode
   *
   *    Purpose:
   *        Enter reset mode.
   *		
   *
   *    Arguments:
   *
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/
        private int acEnterResetMode()            
        {
            bool flag;
            Command.cmd = CanDriver.CMD_STOP;
            Marshal.StructureToPtr(Command, lpCommandBuffer, true);
            flag = CanDriver.DeviceIoControl(hDevice, CanDriver.CAN_IOCTL_COMMAND, lpCommandBuffer, CanDriver.CAN_COMMAND_LENGTH, IntPtr.Zero, 0, ref OutLen, this.ioctlOvr.memPtr);
            if (!flag)
            {
                return OPERATION_ERROR;
            }
            return SUCCESS;
        }

        /*****************************************************************************
   *
   *    acEnterWorkMode
   *
   *    Purpose:
   *        Enter work mode 
   *		
   *
   *    Arguments:
   *
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/
        private int acEnterWorkMode()              
        {
            bool flag;
            Command.cmd = CanDriver.CMD_START;
            Marshal.StructureToPtr(Command, lpCommandBuffer, true);
            flag = CanDriver.DeviceIoControl(hDevice, CanDriver.CAN_IOCTL_COMMAND, lpCommandBuffer, CanDriver.CAN_COMMAND_LENGTH, IntPtr.Zero, 0, ref OutLen, this.ioctlOvr.memPtr);
            if (!flag)
            {
                return OPERATION_ERROR;
            }
            return SUCCESS;
        }

        /*****************************************************************************
   *
   *    acClearRxFifo
   *
   *    Purpose:
   *        Clear can port receive buffer
   *		
   *
   *    Arguments:
   *
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/
        private int acClearRxFifo()
        {
            bool flag = false;
            Command.cmd = CanDriver.CMD_CLEARBUFFERS;
            Marshal.StructureToPtr(Command, lpCommandBuffer, true);
            flag = CanDriver.DeviceIoControl(hDevice, CanDriver.CAN_IOCTL_COMMAND, lpCommandBuffer, CanDriver.CAN_COMMAND_LENGTH, IntPtr.Zero, 0, ref OutLen, this.ioctlOvr.memPtr);
            if (!flag)
            {
                return OPERATION_ERROR;
            }
            return SUCCESS;
        }

        /*****************************************************************************
   *
   *    acSetBaud
   *
   *    Purpose:
   *	     Set baudrate of the CAN Controller.The two modes of configuring
   *     baud rate are custom mode and standard mode.
   *     -   Custom mode
   *         If Baud Rate value is user defined, driver will write the first 8
   *         bit of low 16 bit in BTR0 of SJA1000.
   *         The lower order 8 bit of low 16 bit will be written in BTR1 of SJA1000.
   *     -   Standard mode
   *         Target value     BTR0      BTR1      Setting value 
   *           10K            0x31      0x1c      10 
   *           20K            0x18      0x1c      20 
   *           50K            0x09      0x1c      50 
   *          100K            0x04      0x1c      100 
   *          125K            0x03      0x1c      125 
   *          250K            0x01      0x1c      250 
   *          500K            0x00      0x1c      500 
   *          800K            0x00      0x16      800 
   *         1000K            0x00      0x14      1000 
   *		
   *
   *    Arguments:
   *        BaudRateValue     - baudrate will be set
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/
        private int acSetBaud(uint BaudRateValue)
        {
            bool flag;
            Config.target = CanDriver.CONF_TIMING;
            Config.val1 = BaudRateValue;
            Marshal.StructureToPtr(Config, lpConfigBuffer, true);
            flag = CanDriver.DeviceIoControl(hDevice, CanDriver.CAN_IOCTL_CONFIG, lpConfigBuffer, CanDriver.CAN_CONFIG_LENGTH, IntPtr.Zero, 0, ref OutLen, this.ioctlOvr.memPtr);
            if (!flag)
            {
                return OPERATION_ERROR;
            }
            return SUCCESS;
        }

        /*****************************************************************************
   *
   *    acSetBaudRegister
   *
   *    Purpose:
   *        Configures baud rate by custom mode.
   *		
   *
   *    Arguments:
   *        Btr0           - BTR0 register value.
   *        Btr1           - BTR1 register value.
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/
        private int acSetBaudRegister(Byte Btr0, Byte Btr1)
        {
            uint BaudRateValue = (uint)(Btr0 * 256 + Btr1);
            return acSetBaud(BaudRateValue);
        }

        /*****************************************************************************
   *
   *    acSetTimeOut
   *
   *    Purpose:
   *        Set timeout for read and write  
   *		
   *
   *    Arguments:
   *        ReadTimeOutValue                   - ms
   *        WriteTimeOutValue                  - ms
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/
        private int acSetTimeOut(uint ReadTimeOutValue, uint WriteTimeOutValue)
        {
            bool flag;
            Config.target = CanDriver.CONF_TIMEOUT;
            Config.val1 = WriteTimeOutValue;
            Config.val2 = ReadTimeOutValue;
            Marshal.StructureToPtr(Config, lpConfigBuffer, true);
            flag = CanDriver.DeviceIoControl(hDevice, CanDriver.CAN_IOCTL_CONFIG, lpConfigBuffer, CanDriver.CAN_CONFIG_LENGTH, IntPtr.Zero, 0, ref OutLen, this.ioctlOvr.memPtr);
            if (!flag)
            {
                return OPERATION_ERROR;
            }
            return SUCCESS;
        }

        /*****************************************************************************
   *
   *    acSetSelfReception
   *
   *    Purpose:
   *        Set support for self reception 
   *		
   *
   *    Arguments:
   *        SelfFlag      - true, open self reception; false, close self reception
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/
        private int acSetSelfReception(bool SelfFlag)
        {
            bool flag;
            Config.target = CanDriver.CONF_SELF_RECEPTION;
            if (SelfFlag)
                Config.val1 = 1;
            else
                Config.val1 = 0;
            Marshal.StructureToPtr(Config, lpConfigBuffer, true);
            flag = CanDriver.DeviceIoControl(hDevice, CanDriver.CAN_IOCTL_CONFIG, lpConfigBuffer, CanDriver.CAN_CONFIG_LENGTH, IntPtr.Zero, 0, ref OutLen, this.ioctlOvr.memPtr);
            if (!flag)
            {
                return OPERATION_ERROR;
            }
            return SUCCESS;
        }

        /*****************************************************************************
   *
   *    acSetListenOnlyMode
   *
   *    Purpose:
   *        Set listen only mode of the CAN Controller
   *		
   *
   *    Arguments:
   *        ListenOnly        - true, open only listen mode; false, close only listen mode
   *    Returns:
   *        =0 succeeded; or <0 Failed 
   *
   *****************************************************************************/
        private int acSetListenOnlyMode(bool ListenOnly)
        {
            bool flag;
            Config.target = CanDriver.CONF_LISTEN_ONLY_MODE;
            if (ListenOnly)
                Config.val1 = 1;
            else
                Config.val1 = 0;
            Marshal.StructureToPtr(Config, lpConfigBuffer, true);
            flag = CanDriver.DeviceIoControl(hDevice, CanDriver.CAN_IOCTL_CONFIG, lpConfigBuffer, CanDriver.CAN_CONFIG_LENGTH, IntPtr.Zero, 0, ref OutLen, this.ioctlOvr.memPtr);
            if (!flag)
            {
                return OPERATION_ERROR;
            }
            return SUCCESS;
        }

        /*****************************************************************************
   *
   *    acSetAcceptanceFilterMode
   *
   *    Purpose:
   *        Set acceptance filter mode of the CAN Controller
   *		
   *
   *    Arguments:
   *        FilterMode     - PELICAN_SINGLE_FILTER, single filter mode; PELICAN_DUAL_FILTER, dule filter mode
   *    Returns:
   *        =0 succeeded; or <0 Failed 
   *
   *****************************************************************************/
        private int acSetAcceptanceFilterMode(uint FilterMode)
        {
            bool flag = false;
            Config.target = CanDriver.CONF_ACC_FILTER;
            Config.val1 = FilterMode;
            Marshal.StructureToPtr(Config, lpConfigBuffer, true);
            flag = CanDriver.DeviceIoControl(hDevice, CanDriver.CAN_IOCTL_CONFIG, lpConfigBuffer, CanDriver.CAN_CONFIG_LENGTH, IntPtr.Zero, 0, ref OutLen, this.ioctlOvr.memPtr);
            if (!flag)
            {
                return OPERATION_ERROR;
            }
            return SUCCESS;
        }

        /*****************************************************************************
   *
   *    acSetAcceptanceFilterMask
   *
   *    Purpose:
   *        Set acceptance filter mask of the CAN Controller
   *		
   *
   *    Arguments:
   *        Mask              - acceptance filter mask
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/
        private int acSetAcceptanceFilterMask(uint Mask)
        {
            bool flag = false;
            Config.target = CanDriver.CONF_ACCM;
            Config.val1 = Mask;
            Marshal.StructureToPtr(Config, lpConfigBuffer, true);
            flag = CanDriver.DeviceIoControl(hDevice, CanDriver.CAN_IOCTL_CONFIG, lpConfigBuffer, CanDriver.CAN_CONFIG_LENGTH, IntPtr.Zero, 0, ref OutLen, this.ioctlOvr.memPtr);
            if (!flag)
            {
                return OPERATION_ERROR;
            }
            return SUCCESS;
        }

        /*****************************************************************************
   *
   *    acSetAcceptanceFilterCode
   *
   *    Purpose:
   *        Set acceptance filter code of the CAN Controller
   *		
   *
   *    Arguments:
   *        Code        - acceptance filter code
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/
        private int acSetAcceptanceFilterCode(uint Code)
        {
            bool flag = false;
            Config.target = CanDriver.CONF_ACCC;
            Config.val1 = Code;
            Marshal.StructureToPtr(Config, lpConfigBuffer, true);
            flag = CanDriver.DeviceIoControl(hDevice, CanDriver.CAN_IOCTL_CONFIG, lpConfigBuffer, CanDriver.CAN_CONFIG_LENGTH, IntPtr.Zero, 0, ref OutLen, this.ioctlOvr.memPtr);
            if (!flag)
            {
                return OPERATION_ERROR;
            }
            return SUCCESS;
        }

        /*****************************************************************************
   *
   *    acSetAcceptanceFilter
   *
   *    Purpose:
   *        Set acceptance filter code and mask of the CAN Controller 
   *		
   *
   *    Arguments:
   *        Mask              - acceptance filter mask
   *        Code              - acceptance filter code
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/
        private int acSetAcceptanceFilter(uint Mask, uint Code)
        {
            bool flag = false;
            Config.target = CanDriver.CONF_ACC;
            Config.val1 = Mask;
            Config.val2 = Code;
            Marshal.StructureToPtr(Config, lpConfigBuffer, true);
            flag = CanDriver.DeviceIoControl(hDevice, CanDriver.CAN_IOCTL_CONFIG, lpConfigBuffer, CanDriver.CAN_CONFIG_LENGTH, IntPtr.Zero, 0, ref OutLen, this.ioctlOvr.memPtr);
            if (!flag)
            {
                return OPERATION_ERROR;
            }
            return SUCCESS;
        }

        /*****************************************************************************
   *
   *    acGetStatus
   *
   *    Purpose:
   *        Get the current status of the driver and the CAN Controller
   *		
   *
   *    Arguments:
   *        Status    - status buffer
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/
        private int acGetStatus(ref Can.CanDriver.CanStatusPar_t Status)
        {
            bool flag = false;
            flag = CanDriver.DeviceIoControl(hDevice, CanDriver.CAN_IOCTL_STATUS, IntPtr.Zero, 0, lpStatusBuffer, CanDriver.CAN_CANSTATUS_LENGTH, ref OutLen, this.ioctlOvr.memPtr);
            if (!flag)
            {
                return OPERATION_ERROR;
            }
            Status = (CanDriver.CanStatusPar_t)(Marshal.PtrToStructure(lpStatusBuffer, typeof(CanDriver.CanStatusPar_t)));
            return SUCCESS;
        }


        /*****************************************************************************
   *
   *    acCanWrite
   *
   *    Purpose:
   *        Write can msg
   *		
   *
   *    Arguments:
   *        msgWrite              - managed buffer for write
   *        nWriteCount           - msg number for write
   *        pulNumberofWritten    - real msgs have written
   *
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/

        private int acCanWrite(Can.CanDriver.canmsg_t[] msgWrite, uint nWriteCount, ref uint pulNumberofWritten)
        {
            bool flag;
            int nRet;
            uint dwErr;
            if (nWriteCount > MaxWriteMsgNumber)
                nWriteCount = MaxWriteMsgNumber;
            pulNumberofWritten = 0;
            flag = CanDriver.WriteFile(hDevice, msgWrite, nWriteCount, out pulNumberofWritten, this.txOvr.memPtr);
            if (flag)
            {
                if (nWriteCount > pulNumberofWritten)
                    nRet = TIME_OUT;                          //Sending data timeout
                else
                    nRet = SUCCESS;                               //Sending data ok
            }
            else
            {
                dwErr = (uint)Marshal.GetLastWin32Error(); 
                if (dwErr == CanDriver.ERROR_IO_PENDING)
                {
                    if (CanDriver.GetOverlappedResult(hDevice, this.txOvr.memPtr, out pulNumberofWritten, true))
                    {
                        if (nWriteCount > pulNumberofWritten)
                            nRet = TIME_OUT;                    //Sending data timeout
                        else
                            nRet = SUCCESS;                         //Sending data ok
                    }
                    else
                        nRet = OPERATION_ERROR;                         //Sending data error
                }
                else
                    nRet = OPERATION_ERROR;                            //Sending data error
            }
            return nRet;
        }
    

        /*****************************************************************************
   *
   *    acCanRead
   *
   *    Purpose:
   *        Read can message.
   *		
   *
   *    Arguments:
   *        msgRead           - managed buffer for read
   *        nReadCount        - msg number that unmanaged buffer can preserve
   *        pulNumberofRead   - real msgs have read
   *		
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/
        private int acCanRead(Can.CanDriver.canmsg_t[] msgRead, uint nReadCount, ref uint pulNumberofRead)
        {
            bool flag;
            int nRet;
            uint i;
            if (nReadCount > MaxReadMsgNumber)
                nReadCount = MaxReadMsgNumber;
            pulNumberofRead = 0;
            flag = CanDriver.ReadFile(hDevice, orgReadBuf, nReadCount, out pulNumberofRead, this.rxOvr.memPtr);
            if (flag)
            {
                if (pulNumberofRead == 0)
                {
                    nRet = TIME_OUT;
                }
                else
                {
                    for (i = 0; i < pulNumberofRead; i++)
                    {
                        if (OS_TYPE == 8)
                            msgRead[i] = (CanDriver.canmsg_t)(Marshal.PtrToStructure(new IntPtr(orgReadBuf.ToInt64() + CanDriver.CAN_MSG_LENGTH * i), typeof(CanDriver.canmsg_t)));
                        else
                            msgRead[i] = (CanDriver.canmsg_t)(Marshal.PtrToStructure(new IntPtr(orgReadBuf.ToInt32() + CanDriver.CAN_MSG_LENGTH * i), typeof(CanDriver.canmsg_t)));

                    }
                    nRet = SUCCESS;
                }
            }
            else
            {
                if (CanDriver.GetOverlappedResult(hDevice, this.rxOvr.memPtr, out pulNumberofRead, true))
                {
                    if (pulNumberofRead == 0)
                    {
                        nRet = TIME_OUT;                               //Package receiving timeout
                    }
                    else
                    {
                        for (i = 0; i < pulNumberofRead; i++)
                        {
                            if (OS_TYPE == 8)
                                msgRead[i] = (CanDriver.canmsg_t)(Marshal.PtrToStructure(new IntPtr(orgReadBuf.ToInt64() + CanDriver.CAN_MSG_LENGTH * i), typeof(CanDriver.canmsg_t)));
                            else
                                msgRead[i] = (CanDriver.canmsg_t)(Marshal.PtrToStructure(new IntPtr(orgReadBuf.ToInt32() + CanDriver.CAN_MSG_LENGTH * i), typeof(CanDriver.canmsg_t)));
                        }
                        nRet = SUCCESS;
                    }
                }
                else
                    nRet = OPERATION_ERROR;                                    //Package receiving error
            }
            return nRet;
        }
        /*****************************************************************************
   *
   *    acClearCommError
   *
   *    Purpose:
   *        Execute ClearCommError of AdvCan.
   *		
   *
   *    Arguments:
   *        ErrorCode      - error code if the CAN Controller occur error
   * 
   * 
   *    Returns:
   *        true SUCCESS; or false failure 
   *
   *****************************************************************************/
        private bool acClearCommError(ref uint ErrorCode)
        {
            CanDriver.COMSTAT lpState = new CanDriver.COMSTAT();
            return CanDriver.ClearCommError(hDevice, out ErrorCode, out lpState);
        }

        /*****************************************************************************
   *
   *    acSetCommMask
   *
   *    Purpose:
   *        Execute SetCommMask of AdvCan.
   *		
   *
   *    Arguments:
   *        EvtMask    - event type
   * 
   * 
   *    Returns:
   *        true SUCCESS; or false failure 
   *
   *****************************************************************************/
        private bool acSetCommMask(uint EvtMask)
        {
            if (!CanDriver.SetCommMask(hDevice, EvtMask))
            {
                int num1 = Marshal.GetLastWin32Error();
                return false;
            }
            Marshal.WriteInt32(this.events.evPtr, 0);
            return true;

        }

        /*****************************************************************************
   *
   *    acGetCommMask
   *
   *    Purpose:
   *        Execute GetCommMask of AdvCan.
   *		
   *
   *    Arguments:
   *        EvtMask     - event type
   * 
   * 
   *    Returns:
   *        true SUCCESS; or false failure 
   *
   *****************************************************************************/
        private bool acGetCommMask(ref uint EvtMask)
        {
            return CanDriver.GetCommMask(hDevice, ref EvtMask);
        }

        /*****************************************************************************
   *
   *    acWaitEvent
   *
   *    Purpose:
   *        Wait can message or error of the CAN Controller.
   *		
   *
   *    Arguments:
   *        msgRead           - managed buffer for read
   *        nReadCount        - msg number that unmanaged buffer can preserve
   *        pulNumberofRead   - real msgs have read
   *        ErrorCode         - return error code when the CAN Controller has error
   * 
   *    Returns:
   *        =0 SUCCESS; or <0 failure 
   *
   *****************************************************************************/
        private int acWaitEvent(Can.CanDriver.canmsg_t[] msgRead, uint nReadCount, ref uint pulNumberofRead, ref uint ErrorCode)
        {
            int nRet = OPERATION_ERROR;
            ErrorCode = 0;
            pulNumberofRead = 0;
            if (CanDriver.WaitCommEvent(hDevice, this.events.evPtr, this.events.olPtr) == true)
            {
                EventCode = (uint)Marshal.ReadInt32(this.events.evPtr, 0);
                if ((EventCode & CanDriver.EV_RXCHAR) != 0)
                {
                    nRet = acCanRead(msgRead, nReadCount, ref pulNumberofRead);
                }
                if ((EventCode & CanDriver.EV_ERR) != 0)
                {
                    nRet = OPERATION_ERROR;
                    acClearCommError(ref ErrorCode);
                }
            }
            else
            {
                int err =  Marshal.GetLastWin32Error();
                if (CanDriver.ERROR_IO_PENDING == err)
                {
                    if (CanDriver.GetOverlappedResult(hDevice, this.eventOvr.memPtr, out pulNumberofRead, true))
                    {
                        EventCode = (uint)Marshal.ReadInt32(this.events.evPtr, 0);
                        if ((EventCode & CanDriver.EV_RXCHAR) != 0)
                        {
                            nRet = acCanRead(msgRead, nReadCount, ref pulNumberofRead);
                        }
                        if ((EventCode & CanDriver.EV_ERR) != 0)
                        {
                            nRet = OPERATION_ERROR;
                            acClearCommError(ref ErrorCode);
                        }
                    }
                    else
                        nRet = OPERATION_ERROR;
                }
                else
                    nRet = OPERATION_ERROR;
            }

            return nRet;
        }

        
    }

    internal class Win32Events
    {
        // Methods
        internal Win32Events(IntPtr ol)
        {
            this.olPtr = ol;
            this.evPtr = Marshal.AllocHGlobal(sizeof(uint));
            Marshal.WriteInt32(this.evPtr, 0);
        }

        ~Win32Events()
        {
            Marshal.FreeHGlobal(this.evPtr);
        }

        public IntPtr evPtr;
        public IntPtr olPtr;
    }

    internal class Win32Ovrlap
    {
        // Methods
        internal Win32Ovrlap(IntPtr evHandle)
        {
            this.ol = new CanDriver.OVERLAPPED();
            this.ol.offset = 0;
            this.ol.offsetHigh = 0;
            this.ol.hEvent = evHandle;
            if (evHandle != IntPtr.Zero)
            {
                this.memPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this.ol));
                Marshal.StructureToPtr(this.ol, this.memPtr, false);
            }
        }

        ~Win32Ovrlap()
        {
            if (this.memPtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.memPtr);
            }
        }

        public IntPtr memPtr;
        public Can.CanDriver.OVERLAPPED ol;

    }
}