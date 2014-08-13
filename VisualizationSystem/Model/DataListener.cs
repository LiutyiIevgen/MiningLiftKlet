using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComCan;
using ML.AdvCan;
using ML.ConfigSettings.Services;
using ML.DataExchange;
using ML.DataExchange.Interfaces;
using ML.DataExchange.Model;
using Ninject.Parameters;
using VisualizationSystem.View.Forms;

namespace VisualizationSystem.Model
{
    class DataListener
    {
        private IDataExchange _dataExchange;
        public DataListener()
        {
            _dataExchange = IoC.Resolve<IDataExchange>(new ConstructorArgument("mineConfig",IoC.Resolve<MineConfig>()));
        }

        /* ~DataListener()
        {
            var MineCon = IoC.Resolve<MineConfig>();
            MineCon.Save();
        } */

        public void Init(ReceiveHandler Function)
        {
            _dataExchange.ReceiveEvent += Function;
            if(IoC.Resolve<MineConfig>().CanName.Contains("CAN"))
                _dataExchange.StartExchange(IoC.Resolve<MineConfig>().CanName, 
                    IoC.Resolve<MineConfig>().CanSpeed, new AdvCANIO());
            else if (IoC.Resolve<MineConfig>().CanName.Contains("COM"))
                _dataExchange.StartExchange(IoC.Resolve<MineConfig>().CanName,
                    IoC.Resolve<MineConfig>().CanSpeed, new ComCANIO());
            //_dataExchange.StartExchange("COM7",50, new ComCANIO());
            //_dataExchange.StartExchange("myNonPersisterMemoryMappedFile");         
        }

        public void SetReceiveFunction(ReceiveHandler Function)
        {
            _dataExchange.ReceiveEvent += Function;
        }

        public void SetParameterReceive(Action<List<CanParameter>> receiveFunction)
        {
            _dataExchange.ParameterReceive += receiveFunction;
        }

        public void SetAllCanDataReceive(Action<List<Parameters>> receiveFunction)
        {
            _dataExchange.AllCanDataEvent += receiveFunction;
        }

        public bool GetParameter(ushort controllerId, ushort parameterId, byte subindex)
        {
            return _dataExchange.GetParameter(controllerId, parameterId, subindex);
        }

        public bool SetParameter(ushort controllerId, ushort parameterId, byte subindex, byte[] data)
        {

            return _dataExchange.SetParameter(new CanParameter
            {
                ControllerId = controllerId,
                ParameterId = parameterId,
                ParameterSubIndex = subindex,
                Data = data
            });

        }
    }
}
