using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ML.Can;
using ML.ConfigSettings.Services;
using ML.DataExchange.Model;
using ML.DataExchange.Services;

namespace ML.DataExchange
{
    public class CanParser
    {
        public CanParser(MineConfig mineConfig)
        {
            _config = mineConfig;
        }
        public List<Parameters> GetParametersList(List<CanDriver.canmsg_t> msgData)
        {
            var parametersList = new List<Parameters>();
            for (byte i = 1; i < 4; i++)
            {
                Parameters parameters = GetParameters(msgData, i);
                parametersList.Add(parameters);
            }
            return parametersList;
        }

        public bool Majorization(List<Parameters> parametersList)
        {
            var errorCounter = new List<byte>(){new byte(),new byte(),new byte()};
            for(int i =0;i<3;i++)
                for (int j = 0; j < 3; j++)
                {
                    if(i==j)
                        continue;
                    if (parametersList[i] == null || parametersList[j] == null)
                        errorCounter[i]++;
                    else if (parametersList[i].s < parametersList[j].s - _config.MaxDopMismatch || parametersList[i].s > parametersList[j].s + _config.MaxDopMismatch)
                        errorCounter[i]++;
                }
            if (errorCounter.All(e => e == 2)) //mistake, set leading controller to not null parameter
            {
                int i = 0;
                for (i = 0; i < 2; i++)
                    if (parametersList[i] != null)
                        break;
                _config.LeadingController = i+1;
                return false;
            }
                
            if (errorCounter.All(e => e == 0)) //evrithing is correct
                return true;
            int index = errorCounter.FindIndex(e => e == 2);
            if (index == _config.LeadingController - 1)
            {
                if (index == 0)
                    _config.LeadingController = 2;
                else if(index == 1)
                    _config.LeadingController = 3;
                else
                    _config.LeadingController = 1;
            }
            return true;
        }
        public Parameters GetParameters(List<CanDriver.canmsg_t> msgData, byte controllerId)
        {

            if (!CheckMsgCount(msgData,controllerId)) //check that all data form controller were received
                return null;
            var param = new double[30];
            var outputSignals = new List<byte?>();
            var inputSignals = new List<byte?>();
            for (int i = 0; i < param.Length; i++)
            {
                param[i] = 0;
            }
            try
            {
                param[0] = GetS1(msgData, controllerId);
                double v = GetV(msgData, controllerId);
                param[1] = Math.Abs(v);
                param[2] = GetA(msgData, controllerId);
                param[5] = GetStart(v);
                param[8] = GetBack(v);
                param[9] = GetOstanov(msgData, controllerId);
                param[10] = GetUnload(msgData, controllerId);
                param[11] = GetLoad(msgData, controllerId);
                param[12] = GetS2(msgData, controllerId);
                param[13] = GetDefenceDiagram(msgData, controllerId);
                var pressures = GetBrakeRabPressure(msgData, controllerId);
                for (int i = 0; i < pressures.Count(); i++)
                    param[i + 14] = pressures[i];
                inputSignals = GetAllInputSignals(msgData, controllerId);
                outputSignals = GetAllOutputSignals(msgData, controllerId);
            }
            catch (Exception)
            {
                throw new Exception();
            }
            var parameters = new Parameters(param);
            parameters.SetAuziDISignalsState(inputSignals);
            parameters.SetAuziDOSignalsState(outputSignals);
            
            return parameters;
        }

        private bool CheckMsgCount(List<CanDriver.canmsg_t> msgData,byte controllerId)
        {
            if(msgData.All(p => p.id != (0x380 + controllerId)))
                return false;
            if (msgData.All(p => p.id != (0x280 + controllerId)))
                return false;
            if (msgData.All(p => p.id != (0x180 + controllerId)))
                return false;
            return true;
        }

        private List<byte?> GetAllOutputSignals(List<CanDriver.canmsg_t> msgData, byte controllerId)
        {
            byte[] tpdo3 = msgData.FindLast(p => p.id == (0x380 + controllerId)).data;
            //byte tpdo1 = msgData.FindLast(p => p.id == (0x180 + controllerId)).data[6];
            byte[] tpdo4_1 = msgData.FindLast(p => p.id == (0x485)).data;
            byte[] tpdo4_2 = msgData.FindLast(p => p.id == (0x486)).data;
            byte[] tpdo4_3 = msgData.FindLast(p => p.id == (0x487)).data;

            var byteList = new List<byte?>() { null, null, null, null, null };

            byteList[0] = tpdo3[4];

            byteList[1] = tpdo4_1 == null ? _prevOutputSignals[1] : ( tpdo4_1[1]);
            byteList[2] = tpdo4_2 == null ? _prevOutputSignals[2] : ( tpdo4_2[1]);
            byteList[3] = tpdo4_3 == null ? _prevOutputSignals[3] : ( tpdo4_3[1]);

            _prevOutputSignals = byteList;

            
            return byteList;

        }

        private List<byte?> GetAllInputSignals(List<CanDriver.canmsg_t> msgData, byte controllerId)
        {
            byte[] tpdo3 = msgData.FindLast(p => p.id == (0x380 + controllerId)).data;
            byte[] tpdo4_1 = msgData.FindLast(p => p.id == (0x485)).data;
            byte[] tpdo4_2 = msgData.FindLast(p => p.id == (0x486)).data;
            byte[] tpdo4_3 = msgData.FindLast(p => p.id == (0x487)).data;


            var byteList = new List<byte?>() { null, null, null, null, null };

            byteList[0] = tpdo3[0];

            byteList[1] = tpdo4_1 == null ? _prevInputSignals[1] : ( tpdo4_1[0]);
            byteList[2] = tpdo4_2 == null ? _prevInputSignals[2] : ( tpdo4_2[0]);
            byteList[3] = tpdo4_3 == null ? _prevInputSignals[3] : ( tpdo4_3[0]);


            _prevInputSignals = byteList;

            if (tpdo4_1 == null && tpdo4_2 == null && tpdo4_3 == null)
                _nullSignalFromFPK++;
            else
                _nullSignalFromFPK = 0;

            if (_nullSignalFromFPK == 5)
            {
                _prevInputSignals = new List<byte?>() {null, null, null, null};
                _prevOutputSignals = new List<byte?>() { null, null, null, null, null };
            }

            return byteList;
        }

        private double GetS1(List<CanDriver.canmsg_t> msgData, byte controllerId)
        {
            double s = 0;
            byte[] tpdo1 = msgData.FindLast(p => p.id == (0x180 + controllerId)).data;
            s = (tpdo1[3] << 8) + (tpdo1[4] << 16) + (tpdo1[5] << 24);
            s /= 256 * 1000;
            return s;
        }
        private double GetS2(List<CanDriver.canmsg_t> msgData, byte controllerId)
        {
            double s = 0;
            byte[] tpdo1 = msgData.FindLast(p => p.id == (0x180 + controllerId)).data;
            s = (tpdo1[0] << 8) + (tpdo1[1] << 16) + (tpdo1[2] << 24);
            s /= 256 * 1000;
            return s;
        }
        private double GetDefenceDiagram(List<CanDriver.canmsg_t> msgData, byte controllerId)
        {
            int d = 0;
            float ret;
            byte[] tpdo2 = msgData.FindLast(p => p.id == (0x280 + controllerId)).data;
            d = tpdo2[4] + (tpdo2[5] << 8);
            ret = (float)d/1000;
            return ret;
        }
        private double GetV(List<CanDriver.canmsg_t> msgData, byte controllerId)
        {
            double v = 0;
            short sv = 0;
            byte[] tpdo2 = msgData.FindLast(p => p.id == (0x280 + controllerId)).data;
            sv = (short)((tpdo2[2]) + (tpdo2[3] << 8));
            v = sv;
            return v/1000;
        }
        private double GetA(List<CanDriver.canmsg_t> msgData, byte controllerId)
        {
            double a = 0;
            short sa = 0;
            byte[] tpdo2 = msgData.FindLast(p => p.id  == (0x280 + controllerId)).data;
            sa = (short)((tpdo2[6]) + (tpdo2[7] << 8));
            a = sa;
            return a / 1000;
        }
        private double GetStart(double v)
        {
            if (v < 0)
                return 1;
            return 0;
        }
        private double GetBack(double v)
        {
            if (v > 0)
                return 1;
            return 0;
        }
        private double GetOstanov(List<CanDriver.canmsg_t> msgData, byte controllerId)
        {
            byte[] tpdo3 = msgData.FindLast(p => p.id == (0x380 + controllerId)).data;
            if ((tpdo3[0]&1) >= 1 || (tpdo3[0]&2)>=1)
            {
                return 1;
            }
            return 0;
        }

        private double[] GetBrakeRabPressure(List<CanDriver.canmsg_t> msgData, byte controllerId)
        {
            var ret = new double[4];
            byte[] tpdo4_1 = msgData.FindLast(p => p.id == (0x485)).data;
            if (tpdo4_1 != null)
            {
                for (int i = 0; i < ret.Count(); i++)
                    ret[i] = tpdo4_1[i + 4]*16*0.00122;
                _prevPressures = ret;
            }
            else
                ret = _prevPressures;
            return ret;
        }

        private double GetUnload(List<CanDriver.canmsg_t> msgData, byte controllerId)
        {

            return 0;
        }
        private double GetLoad(List<CanDriver.canmsg_t> msgData, byte controllerId)
        {
            return 0;
        }

        //private double _dS = 10; //m
        private MineConfig _config;
        private int _nullSignalFromFPK = 0;
        private List<byte?> _prevInputSignals = new List<byte?>() { null, null, null, null };
        private List<byte?> _prevOutputSignals = new List<byte?>() { null, null, null, null, null };
        private double[] _prevPressures = new double[4]{0,0,0,0};
    }
}
