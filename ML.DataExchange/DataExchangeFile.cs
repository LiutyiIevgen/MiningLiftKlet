using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;
using ML.Can.Interfaces;
using ML.DataExchange.Interfaces;
using ML.DataExchange.Model;
using Timer = System.Timers.Timer;

namespace ML.DataExchange
{
    public class DataExchangeFile:IDataExchange
    {
        public bool StartExchange(string strPort, int portSpeed, ICanIO device)
        {                     
            _fileName = strPort;
            var timeThread = new Thread(TimerEvent) { IsBackground = true};
            timeThread.Start();
            return true;
        }

        public bool StopExchange()
        {
            _timer.Stop();
            return true;
        }

        private void TimerEvent()
        {
            while (true)
            {
                double[] param;
                int k;
                try
                {
                    mymutex = Mutex.OpenExisting("NonPersisterMemoryMappedFilemutex");
                    myNonPersisterMemoryMappedFile = MemoryMappedFile.OpenExisting(_fileName);
                    mymutex.WaitOne();
                    k = 0;
                    param = new double[30];
                    StreamReader sr = new StreamReader(myNonPersisterMemoryMappedFile.CreateViewStream());
                    while (sr.EndOfStream != true)
                    {
                        string s = sr.ReadLine();
                        double data;
                        if (double.TryParse(s, out data))
                            param[k] = data;
                        else
                            break;
                        k++;
                    }
                    sr.Close();
                    mymutex.ReleaseMutex();
                    myNonPersisterMemoryMappedFile.Dispose();
                }
                catch (Exception ex)
                {
                    if (mymutex != null)
                        mymutex.ReleaseMutex();
                    Thread.Sleep(500);
                    continue;
                }
                Parameters parameters = new Parameters(param);
                if (ReceiveEvent != null)
                    ReceiveEvent(parameters);
                if ((parameters.f_ostanov == 1 && parameters.load_state == 1) && DrawLoad != null)
                {
                    DrawLoad();
                    _drawLoad = DrawLoad;
                    DrawLoad = null;
                }
                Thread.Sleep(10);
            }
        }
        
        public bool GetParameter(ushort controllerId, ushort parameterId, byte subindex)
        {
            throw new NotImplementedException();
        }


        


        public bool SetParameter(CanParameter canParameter)
        {
            throw new NotImplementedException();
        }

        public event ReceiveHandler ReceiveEvent;
        public event Action<List<Parameters>> AllCanDataEvent;
        public event Action<List<CanParameter>> ParameterReceive;

        public event Action DrawLoad;
        private event Action _drawLoad;
        private Timer _timer;
        private string _fileName;
        private const int Interval = 10;
        private Mutex mymutex;
        private MemoryMappedFile myNonPersisterMemoryMappedFile;


    }
}
