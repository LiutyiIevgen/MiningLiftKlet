

using System;
using System.Collections.Generic;
using ML.AdvCan;
using ML.Can.Interfaces;
using ML.DataExchange.Model;

namespace ML.DataExchange.Interfaces
{
    public delegate void ReceiveHandler(Parameters parameters);
    public interface IDataExchange
    {
        //server initialization
        bool StartExchange(string strPort, int portSpeed = 50, ICanIO device = null);

        bool GetParameter(ushort controllerId, ushort parameterId, byte subindex);
        bool SetParameter(CanParameter canParameter);
        //close all connection
        bool StopExchange();

        event ReceiveHandler ReceiveEvent; //visualisation info

        event Action<List<CanParameter>> ParameterReceive; //inner parameters info

        event Action<List<Parameters>> AllCanDataEvent; //all can packeges from OS
    }
}
