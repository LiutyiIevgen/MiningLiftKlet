using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ML.AdvCan;
using ML.Can;
using ML.Can.Interfaces;
using ML.ConfigSettings.Services;
using ML.DataExchange.Interfaces;
using ML.DataExchange.Model;
using ML.DataExchange.Services;

namespace ML.DataExchange
{
    public class DataExchangeCan : IDataExchange
    {
        public DataExchangeCan(MineConfig mineConfig)
        {
            _mineConfig = mineConfig;
            _codtDomainArray = new List<byte>();
        }

        public bool StartExchange(string strPort,int portSpeed, ICanIO device)
        {
            
            _portName = strPort;
            _portSpeed = portSpeed;
            _device = device;
            string ret = _device.OpenCAN(strPort, portSpeed);
            if (ret != "No_Err")
            {
                MessageBox.Show(ret);
                return false;
            }
            Thread.Sleep(200);
            ReceiveThread = new Thread(ReceiveThreadMethod);
            ReceiveThread.Priority = ThreadPriority.Highest;
            ReceiveThread.IsBackground = true;
            ReceiveThread.Start(); //New thread starts
            return true;
        }

        public bool StopExchange()
        {
            m_bRun = false;
            _device.CloseCan();
            try
            {
                if ((ReceiveThread.ThreadState & (ThreadState.Stopped | ThreadState.Unstarted)) == 0)
                {
                    ReceiveThread.Abort();
                    ReceiveThread.Join(); //Thread stops
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool GetParameter(ushort controllerId, ushort parameterId, byte subindex)
        {
            int repeatCount = 0;
            var dataList = new List<CanDriver.canmsg_t>();
            var msg = new CanDriver.canmsg_t();
            msg.flags = CanDriver.MSG_BOVR;
            msg.cob = 0;
            msg.id = (uint)(0x600 + controllerId); //rSdo + id=1
            msg.length = (short)CanDriver.DATALENGTH;
            msg.data = new byte[CanDriver.DATALENGTH];
            msg.data[0] = 0x40;//read data
            msg.data[1] = (byte)(parameterId);//id low
            msg.data[2] = (byte)(parameterId>>8);//id high
            msg.data[3] = subindex;//subindex
            dataList.Add(msg);
            while (true)
            {
                _device.SendData(dataList);
                if (!_isUnloaded.WaitOne(TimeSpan.FromMilliseconds(250)))
                {
                    repeatCount++;
                    if (repeatCount == 10)
                    {
                        MessageBox.Show(@"Превышен лимит ожидания вычитки параметра 0x" + Convert.ToString(parameterId, 16) + @"!!!", @"Ошибка");
                        return false;
                    }
                    continue;
                }
                break;
            }            
            return true;
        }



        public bool SetParameter(CanParameter canParameter)
        {
            int repeatCount = 0;
            var msg = new CanDriver.canmsg_t();
            msg.flags = CanDriver.MSG_BOVR;
            msg.cob = 0;
            msg.id = (uint)(0x600 + canParameter.ControllerId); //rSdo + id=1
            msg.length = (short)CanDriver.DATALENGTH;
            msg.data = new byte[CanDriver.DATALENGTH];
            msg.data[1] = (byte)(canParameter.ParameterId);//id low
            msg.data[2] = (byte)(canParameter.ParameterId >> 8);//id high
            msg.data[3] = canParameter.ParameterSubIndex;//subindex
            if (canParameter.Data.Count() == 4)
            {
                msg.data[0] = 0x22; //write 4 byte e=1 s=0;
                msg.data[4] = canParameter.Data[0];
                msg.data[5] = canParameter.Data[1];
                msg.data[6] = canParameter.Data[2];
                msg.data[7] = canParameter.Data[3];
            }
            else if (canParameter.Data.Count() == 3)
            {
                msg.data[0] = 0x26; //write 3 byte e=1 s=0;
                msg.data[4] = canParameter.Data[0];
                msg.data[5] = canParameter.Data[1];
                msg.data[6] = canParameter.Data[2];
            }
            else if (canParameter.Data.Count() == 2)
            {
                msg.data[0] = 0x2A; //write 2 byte e=1 s=0;
                msg.data[4] = canParameter.Data[0];
                msg.data[5] = canParameter.Data[1];
            }
            else if (canParameter.Data.Count() > 4) //codtDomain
            {
                _codtDomainThread = new Thread(SetSegment) { IsBackground = true };
                _codtDomainThread.Start(canParameter);
                return true;
            }
            while ((true))
            {
                if (repeatCount == 10)
                {
                    MessageBox.Show("Превышен лимит ожидания записи параметра 0x" + Convert.ToString(canParameter.ParameterId,16) + "!!!", "Ошибка");
                    return false;
                }
                _device.SendData(new List<CanDriver.canmsg_t> { msg });
                if (!_isLoaded.WaitOne(TimeSpan.FromMilliseconds(500)))
                {
                    repeatCount++;
                    continue;
                }
                break;
            }                  
            return true;
        }

        public event ReceiveHandler ReceiveEvent;

        public event Action<List<CanParameter>> ParameterReceive;

        public event Action<List<Parameters>> AllCanDataEvent;

        private List<CanParameter> TryGetParameterValue(List<CanDriver.canmsg_t> msgData)
        {
            var canParameters = new List<CanParameter>();
            var msgList = msgData.Where(m => (m.id & 0xF80) == 0x580).ToList();
            if (msgList.Count != 0)
            {
                foreach (var canmsgT in msgList)
                {
                    if ((canmsgT.data[0] & 0x73) == 0x43) // value block
                    {                        
                        var canParameter = new CanParameter()
                        {
                            ControllerId = (ushort) (canmsgT.id & 0x7F),
                            ParameterId = (ushort) (canmsgT.data[1] + (canmsgT.data[2] << 8)),
                            ParameterSubIndex = canmsgT.data[3]
                        };
                        int unusedBytes = (canmsgT.data[0] & 0x0C) >> 2;
                        canParameter.Data = new byte[4 - unusedBytes];
                        for (int j = 0; j < 4 - unusedBytes; j++)
                            canParameter.Data[j] = canmsgT.data[4 + j];
                        canParameters.Add(canParameter);
                        _isUnloaded.Set();
                    }
                    else if (canmsgT.data[0] == 0x41) //get codtDomain
                    {
                        _codtDomainArray.Clear();
                        _codtDomainId = (ushort)(canmsgT.data[1] + (canmsgT.data[2] << 8));
                        _codtDomainSubIndex = canmsgT.data[3];
                        _codtDomainThread = new Thread(GetSegment) { IsBackground = true };
                        _codtDomainThread.Start(canmsgT);  
                        _isUnloaded.Set();
                    }
                    else if ((canmsgT.data[0] & 0xE0) == 0) //get codtDomain block
                    {
                        _isUnloaded.Set();
                        int unusedBytes = (canmsgT.data[0] & 0x0E) >> 1;
                        for (int j = 0; j < 7 - unusedBytes; j++)
                            _codtDomainArray.Add(canmsgT.data[j+1]);
                        if ((canmsgT.data[0]&0x01) == 1)//last codtDomain Element
                        {
                            if (_codtDomainArray.Count<5)
                                continue;
                            canParameters.Add(new CanParameter
                            {
                                ControllerId = (ushort)(canmsgT.id & 0x7F),
                                ParameterId = _codtDomainId,
                                ParameterSubIndex = _codtDomainSubIndex,
                                Data = _codtDomainArray.ToArray()
                            });
                            _codtDomainArray.Clear();
                        }  
                    }
                    else if(canmsgT.data[0] == 0x20 || canmsgT.data[0] == 0x30) //segment was accepted
                        _isLoaded.Set();
                    else if ((canmsgT.data[0] & 0xE0) == 0x80)
                    {
                        _isUnloaded.Set();
                    }
                    else if (canmsgT.data[0] == 0x60) //parameter was seted
                    {
                        _isLoaded.Set();
                        if (_codtDomainThread != null)
                            if (_codtDomainThread.IsAlive)
                                continue;
                        canParameters.Add(new CanParameter
                        {
                            ControllerId = (ushort) (canmsgT.id & 0x7F),
                            ParameterId = (ushort) (canmsgT.data[1] + (canmsgT.data[2] << 8)),
                            ParameterSubIndex = canmsgT.data[3]
                        });
                    }
                }
            }
            return canParameters;
        }

        private void ReceiveThreadMethod()
        {
            List<Parameters> parametersList;
            var param = new double[30];
            var msgRead = new List<CanDriver.canmsg_t>();
            var canParser = new CanParser(_mineConfig);
            int i = 0;
            while (true)
            {

                try
                {

                    var msg = _device.ReceiveMsgBlock(MsgCount);
                    if (msg == null)
                    {
                        _device.OpenCAN(_portName, _portSpeed);
                        Thread.Sleep(50);
                        continue;
                    }
                    /*if (msg.Count == 0)
                    {
                        Thread.Sleep(5);
                        continue;
                    }*/

                    msgRead.AddRange(msg);
                    parametersList = canParser.GetParametersList(msgRead);
                    for (int j = 0; j < parametersList.Count; j++)
                    {
                        if (parametersList[j] == null)
                        {
                            if (_wasError[j] == 0)
                            {
                                var dataBaseService = new DataBaseService();
                                dataBaseService.FillGeneralLog("Вышел из строя канал ОС" + (j + 1).ToString(), "Вышел из строя канал ОС" + (j + 1).ToString(), GeneralLogEventType.Demage);
                            }
                            _wasError[j] = 1;
                        }
                        else
                            _wasError[j] = 0;
                    }
                    var maj = canParser.Majorization(parametersList);
                    if (AllCanDataEvent != null && (i % 3) == 0)
                    {
                        AllCanDataEvent(parametersList);
                        i = 0;
                    }
                    if (!maj)
                        parametersList = null;
                    if(parametersList != null)
                        ReceiveEvent(parametersList[_mineConfig.LeadingController - 1]);                    
                    List<CanParameter> canParameters = TryGetParameterValue(msgRead); //параметры can
                    if (canParameters.Count != 0)
                        ParameterReceive(canParameters);
                    Thread.Sleep(_mineConfig.ReceiveDataDelay);
                    msgRead.Clear();
                    i++;
                }
                catch (Exception exception)
                {
                     msgRead.Clear();
                }
            }
        }

        private void GetSegment(object canmsgT)//if codt domain send qeury for codtDomain blocks
        {
            int repeatCount = 0;
            var msg = (CanDriver.canmsg_t)canmsgT;
            double byteNumber = msg.data[4];
            var _sendNumber = (int)Math.Ceiling((double)(byteNumber / 7));
            int i = 0;
            while(i<_sendNumber)
            {
                if (i%2 == 0)
                    _device.SendData(new List<CanDriver.canmsg_t>
                    {
                        new CanDriver.canmsg_t
                        {
                            id = 0x600 + (msg.id & 0x07F),
                            length = 8,
                            flags = CanDriver.MSG_BOVR,
                            data = new byte[]
                            {
                                0x60, msg.data[1], msg.data[2], msg.data[3], 0, 0, 0, 0
                            }
                        }
                    });
                else
                    _device.SendData(new List<CanDriver.canmsg_t>
                    {
                        new CanDriver.canmsg_t
                        {
                            id = 0x600 + (msg.id & 0x07F),
                            length = 8,
                            flags = CanDriver.MSG_BOVR,
                            data = new byte[]
                            {
                                0x70, msg.data[1], msg.data[2], msg.data[3], 0, 0, 0, 0
                            }
                        }
                    });

                if (!_isUnloaded.WaitOne(TimeSpan.FromMilliseconds(250)))
                {
                    repeatCount++;
                    if (repeatCount == 10)
                    {
                        MessageBox.Show("Превышен лимит ожидания вычитки сегмента №" + i + "!!!", "Ошибка");
                        return;
                    }
                    continue;
                }
                repeatCount = 0;
                i++;
                Thread.Sleep(30);
            }
        }

        private void SetSegment(object parameter)
        {
            int repeatCount = 0;
            //start block
            var canParameter = parameter as CanParameter;
            var msg = new CanDriver.canmsg_t();
            msg.flags = CanDriver.MSG_BOVR;
            msg.cob = 0;
            msg.id = (ushort)(0x600 + canParameter.ControllerId); //rSdo + id=1
            msg.length = (short)CanDriver.DATALENGTH;
            msg.data = new byte[CanDriver.DATALENGTH];
            msg.data[0] = 0x20; //write segment e=0 s=0;
            msg.data[1] = (byte)(canParameter.ParameterId);//id low
            msg.data[2] = (byte)(canParameter.ParameterId >> 8);//id high
            msg.data[3] = canParameter.ParameterSubIndex;//subindex     
            msg.data[4] = (byte)canParameter.Data.Count();
            //Device.SendData(new List<CanDriver.canmsg_t> { msg });// start block
            while (true)
            {
                if (repeatCount == 10)
                {
                    MessageBox.Show("Превышен лимит ожидания записи параметра 0x" + Convert.ToString(canParameter.ParameterId, 16) + "!!!", "Ошибка");
                    return;
                }
                Thread.Sleep(20);
                _device.SendData(new List<CanDriver.canmsg_t> { msg });// start block
                if (!_isLoaded.WaitOne(TimeSpan.FromMilliseconds(2000)))
                {
                    repeatCount++;
                    continue;
                }
                repeatCount = 0;
                Thread.Sleep(200);
                break;
            }
            
            //if (!_isLoaded.WaitOne(TimeSpan.FromMilliseconds(1000)))
            //{
              //  return;
            //}
            double byteCount = canParameter.Data.Count();
            var _sendNumber = (int)Math.Ceiling((double)(byteCount / 7));
            //send data
            int i = 0;
            while (i < _sendNumber)
            {
                var block = new byte[8];
                for (int j = 0; j < 7; j++)
                {
                    if (i * 7 + j >= canParameter.Data.Count()) //set last bute to 0
                        block[j + 1] = 0;
                    else
                        block[j + 1] = canParameter.Data[i * 7 + j];
                }
                if(i == _sendNumber - 1)//last block + 6 bit unused
                    block[0] = 0x1D;
                else if (i%2 == 0) //change count bit
                    block[0] = 0;
                else
                    block[0] = 0x10;
                _device.SendData(new List<CanDriver.canmsg_t>
                {
                    new CanDriver.canmsg_t
                    {
                        id = (ushort)(0x600 + canParameter.ControllerId),
                        length = 8,
                        flags = CanDriver.MSG_BOVR,
                        data = block
                    }
                });
                if (i == _sendNumber - 1) // if it was last block
                {
                    ParameterReceive(new List<CanParameter> //parameter was seted
                    {
                        new CanParameter
                        {
                            ControllerId = canParameter.ControllerId,
                            ParameterId = canParameter.ParameterId,
                            ParameterSubIndex = canParameter.ParameterSubIndex
                        }
                    });
                }
                if (!_isLoaded.WaitOne(TimeSpan.FromMilliseconds(500)))
                {
                    repeatCount++;
                    if (repeatCount == 10)
                    {
                        MessageBox.Show("Превышен лимит ожидания записи сегмента №" + i + "!!!", "Ошибка");
                        return;
                    }
                    continue;
                }
                repeatCount = 0;
                i++;
            }
        }

        private ICanIO _device;

        EventWaitHandle _isUnloaded = new AutoResetEvent(false);//for getting codt domain
        EventWaitHandle _isLoaded = new AutoResetEvent(false);//for setting codt domain

        private MineConfig _mineConfig;

        private bool m_bRun;
        private bool syncflag;
        private int MsgCount = 300;
        private string _portName;
        private int _portSpeed;
        private Thread ReceiveThread;
        private Thread _codtDomainThread;
        private List<byte> _codtDomainArray;
        private ushort _codtDomainId;
        private byte _codtDomainSubIndex;
        private int[] _wasError = new int[3]; // for Log
    }
}
