using System;
using System.Collections.Generic;
using System.Threading;
using ML.ConfigSettings.Services;

namespace ML.DataExchange.Model
{
    public class  Parameters
    {
        public Parameters()
        {
            
        }
        public Parameters(double[] param)
        {
            signal = new int[24];
            AuziDIOSignalsState = new List<AuziDState>();
            set_parameters(param);
            SetSignals();
            SetAuziDIOSignalsState();
            DefenceDiagramRegime = 1;//"груз"
        }

        private void set_parameters(double[] param)
        {
            s = -param[0]; 
            v = param[1];
            a = param[2];
            //f_slowdown_zone = Convert.ToInt32(param[3]);
            //f_dot_zone = Convert.ToInt32(param[4]);
            f_start = Convert.ToInt32(param[5]);
            //f_slowdown_zone_back = Convert.ToInt32(param[6]);
            //f_dot_zone_back = Convert.ToInt32(param[7]);
            f_back = Convert.ToInt32(param[8]);
            f_ostanov = Convert.ToInt32(param[9]);
            //unload_state = Convert.ToInt32(param[10]);
            //load_state = Convert.ToInt32(param[11]);
            s_two = -param[12];
            defence_diagram = param[13];
            //
            BrakeRabCyl1Pressure = param[14];
            BrakeRabCyl2Pressure = param[15];
            BrakePredCyl1Pressure = param[16];
            BrakePredCyl2Pressure = param[17];
        }

        public void SetSignals()
        {
            for (int i = 0; i < 24; i++)
            {
                signal[i] = 0;
            }
            //signal[11] = 1;
        }

        private void SetAuziDIOSignalsState()
        {
            for (int i = 0; i < 144; i++)
            {
                AuziDIOSignalsState.Add(AuziDState.Undefind);
            }
            AuziDIByteList = new List<byte?>();
            AuziDOByteList = new List<byte?>();
            for (int i = 0; i < 9; i++)
            {
                AuziDIByteList.Add(new byte());
                AuziDOByteList.Add(new byte());
            }
        }

        public void SetAuziDOSignalsState(List<byte?> byteList)
        {
            AuziDOByteList = byteList;
            var signals = new List<AuziDState>();
            foreach (var _byte in byteList)
            {
                byte? b = _byte;
                for (int i = 0; i < 8; i++)
                {
                    signals.Add((b & 0x01) == 1 ? AuziDState.Off : ((b & 0x01) == null ? AuziDState.Undefind : AuziDState.On));
                    if (b != null)
                        b = (byte)(b >> 1);
                }
            }
            for (int i = 0; i < signals.Count && i < 72; i++)
            {
                AuziDIOSignalsState[i + 72] = signals[i];
            }
            //slowdown and dotiajka zones flags
            SetSlowdownAndDotZonesFlags(AuziDIOSignalsState[93], AuziDIOSignalsState[94], AuziDIOSignalsState[1], AuziDIOSignalsState[0]);
        }

        public void SetAuziDISignalsState(List<byte?> byteList)                                            
        {
            AuziDIByteList = byteList;
            var signals = new List<AuziDState>();
            foreach (var _byte in byteList)
            {
                byte? b = _byte;
                for (int i = 0; i < 8; i++)
                {
                    signals.Add((b & 0x01) == 1 ? AuziDState.Off : ((b & 0x01) == null ? AuziDState.Undefind : AuziDState.On));
                    if(b!=null)
                        b = (byte)(b >> 1);
                }
            }
            for (int i = 0; i < signals.Count && i < 72; i++)
            {
                AuziDIOSignalsState[i] = signals[i];
            }
            unload_state = SetUnloadState(AuziDIOSignalsState[14], AuziDIOSignalsState[15]);
            load_state = SetLoadState(AuziDIOSignalsState[13]);
        }

        private int SetUnloadState(AuziDState RKZD, AuziDState RKR)
        {
            if (RKZD == AuziDState.On && RKR == AuziDState.On && tek_unload_state == 5)
                tek_unload_state = 0;
            else if (RKZD == AuziDState.Off && RKR == AuziDState.On && tek_unload_state == 0)
                tek_unload_state = 1;
            else if (RKZD == AuziDState.Off && RKR == AuziDState.Off && tek_unload_state == 1)
                tek_unload_state = 2;
            else if (RKZD == AuziDState.Off && RKR == AuziDState.On && tek_unload_state == 2)
                tek_unload_state = 3;
            else if (RKZD == AuziDState.On && RKR == AuziDState.On && (tek_unload_state == 3 || tek_unload_state == 1 || (tek_unload_state == 4 && unload_delay != 0)))
            {
                tek_unload_state = 4;
                unload_delay++;
                if (unload_delay > 200)
                    unload_delay = 0;
            }
            else if (RKZD == AuziDState.On && RKR == AuziDState.On && tek_unload_state == 4 && unload_delay == 0)
                tek_unload_state = 5;
            return tek_unload_state;
        }

        private int SetLoadState(AuziDState RZA)
        {
            if (RZA == AuziDState.On && tek_load_state == 5)
                tek_load_state = 0;
            else if (RZA == AuziDState.On && tek_load_state == 0)
                tek_load_state = 1;
            else if (RZA == AuziDState.Off && tek_load_state == 1)
                tek_load_state = 2;
            else if (RZA == AuziDState.On && (tek_load_state == 2) || (tek_load_state == 3 && load_delay != 0))
            {
                tek_load_state = 3;
                load_delay++;
                if (load_delay > 200)
                    load_delay = 0;
            }
            else if (RZA == AuziDState.On && ((tek_load_state == 3 && load_delay == 0) || (tek_load_state == 4 && load_delay != 0)))
            {
                tek_load_state = 4;
                load_delay++;
                if (load_delay > 200)
                    load_delay = 0;
            }
            else if (RZA == AuziDState.On && tek_load_state == 4 && load_delay == 0)
                tek_load_state = 5;
            return tek_load_state;
        }

        private void SetSlowdownAndDotZonesFlags(AuziDState slowdownSignal, AuziDState dotSignal, AuziDState vOstanov, AuziDState nOstanov)
        {
            if (slowdownSignal == AuziDState.On && _previousSlowdownZone == AuziDState.Off && (f_start == 1 || nOstanov == AuziDState.Off))
                _fSlowdown = 1;
            else if (slowdownSignal == AuziDState.Off && _previousSlowdownZone == AuziDState.On)
                _fSlowdown = 0;
            if (dotSignal == AuziDState.On && _previousDotZone == AuziDState.Off && (f_start == 1 || nOstanov == AuziDState.Off))
                _fDot = 1;
            else if (dotSignal == AuziDState.Off && _previousDotZone == AuziDState.On)
                _fDot = 0;
            if (slowdownSignal == AuziDState.On && _previousSlowdownZone == AuziDState.Off && (f_back == 1 || vOstanov == AuziDState.Off))
                _fSlowdownBack = 1;
            else if (slowdownSignal == AuziDState.Off && _previousSlowdownZone == AuziDState.On)
                _fSlowdownBack = 0;
            if (dotSignal == AuziDState.On && _previousDotZone == AuziDState.Off && (f_back == 1 || vOstanov == AuziDState.Off))
                _fDotBack = 1;
            else if (dotSignal == AuziDState.Off && _previousDotZone == AuziDState.On)
                _fDotBack = 0;
            _previousSlowdownZone = slowdownSignal;
            _previousDotZone = dotSignal;
            f_slowdown_zone = _fSlowdown;
            f_dot_zone = _fDot;
            f_slowdown_zone_back = _fSlowdownBack;
            f_dot_zone_back = _fDotBack;
        }

        public double s { get; private set; }//текущее значение пути клеть 1
        public double v { get; private set; }//текущее значение скорости
        public double a { get; private set; } //текущее значение ускорения
        public int f_slowdown_zone { get; private set; } //флаг зоны замедления
        public int f_dot_zone { get; private set; }// флаг зоны дотяжки
        public int f_start { get; private set; }
        public int f_slowdown_zone_back { get; private set; } //флаг зоны замедления
        public int f_dot_zone_back { get; private set; }// флаг зоны дотяжки
        public int f_back { get; private set; }
        public int f_ostanov { get; private set; }
        public int unload_state { get; private set; }
        public int load_state { get; private set; }
        private static int tek_unload_state { get; set; }
        private static int unload_delay { get; set; }
        private static int tek_load_state { get; set; }
        private static int load_delay { get; set; }
        public double s_two { get; private set; }//текущее значение пути клеть 2
        public double defence_diagram { get; private set; }//защитная диаграма
        //
        public double tok_anchor { get; set; } //ток якоря 
        public double tok_excitation { get; set; } //ток возбуждения
        //signals in central part of screen
        public int[] signal { get; private set; }
        // AUZI-D iput and output signals
        public List<AuziDState> AuziDIOSignalsState { get; private set; }
        public List<byte?> AuziDIByteList { get; private set; }
        public List<byte?> AuziDOByteList { get; private set; }
        //номер режима защитной диаграммы
        public int DefenceDiagramRegime { get; private set; }
        public double BrakeRabCyl1Pressure { get; private set; } //давление воздуха в 1-м рабочем цилиндре тормозной системы
        public double BrakeRabCyl2Pressure { get; private set; }
        public double BrakePredCyl1Pressure { get; private set; } //давление воздуха в 1-м предохранительном цилиндре тормозной системы
        public double BrakePredCyl2Pressure { get; private set; }

        private static AuziDState _previousSlowdownZone;
        private static AuziDState _previousDotZone;
        private static int _fSlowdown;
        private static int _fDot;
        private static int _fSlowdownBack;
        private static int _fDotBack;
    }
}
