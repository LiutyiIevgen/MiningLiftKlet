using System;
using System.Collections.Generic;
using System.Drawing;
using ML.ConfigSettings.Services;
using ML.DataExchange.Model;
using ML.DataExchange.Services;
using VisualizationSystem.Model;
using GeneralLogEventType = ML.DataExchange.Model.GeneralLogEventType;

namespace VisualizationSystem.ViewModel.MainViewModel
{
    class AuziDInOutSignalsVm
    {
        public AuziDInOutSignalsVm()
        {
            _mineConfig = IoC.Resolve<MineConfig>();
            InputNames = new List<string>();
            InputMeanings = new List<Color>();
            OutputNames = new List<string>();
            OutputMeanings = new List<Color>();
            codes = new string[144];

            InitSignalCodes();
        }

        private void InitSignalCodes()
        {
            codes[0] = "1-X7DI-1";
            codes[1] = "1-X7DI-2";
            codes[2] = "1-X7DI-3";
            codes[3] = "1-X7DI-4";
            codes[4] = "1-X7DI-5";
            codes[5] = "1-X7DI-6";
            codes[6] = "1-X7DI-7";
            codes[7] = "1-X7DI-8";
            codes[8] = "2-X7DI-1";
            codes[9] = "2-X7DI-2";
            codes[10] = "2-X7DI-3";
            codes[11] = "2-X7DI-4";
            codes[12] = "2-X7DI-5";
            codes[13] = "2-X7DI-6";
            codes[14] = "2-X7DI-7";
            codes[15] = "2-X7DI-8";
            codes[16] = "3-X7DI-1";
            codes[17] = "3-X7DI-2";
            codes[18] = "3-X7DI-3";
            codes[19] = "3-X7DI-4";
            codes[20] = "3-X7DI-5";
            codes[21] = "3-X7DI-6";
            codes[22] = "3-X7DI-7";
            codes[23] = "3-X7DI-8";
            codes[24] = "4-X5IN1-1";
            codes[25] = "4-X5IN1-2";
            codes[26] = "4-X5IN1-3";
            codes[27] = "4-X5IN1-4";
            codes[28] = "4-X5IN1-5";
            codes[29] = "4-X5IN1-6";
            codes[30] = "4-X5IN1-7";
            codes[31] = "4-X5IN1-8";
            codes[32] = "4-X10IN2-1";
            codes[33] = "4-X10IN2-2";
            codes[34] = "4-X10IN2-3";
            codes[35] = "4-X10IN2-4";
            codes[36] = "4-X10IN2-5";
            codes[37] = "4-X10IN2-6";
            codes[38] = "4-X10IN2-7";
            codes[39] = "4-X10IN2-8";
            codes[40] = "4-X15IN3-1";
            codes[41] = "4-X15IN3-2";
            codes[42] = "4-X15IN3-3";
            codes[43] = "4-X15IN3-4";
            codes[44] = "4-X15IN3-5";
            codes[45] = "4-X15IN3-6";
            codes[46] = "4-X15IN3-7";
            codes[47] = "4-X15IN3-8";
            codes[48] = "5-X5IN1-1";
            codes[49] = "5-X5IN1-2";
            codes[50] = "5-X5IN1-3";
            codes[51] = "5-X5IN1-4";
            codes[52] = "5-X5IN1-5";
            codes[53] = "5-X5IN1-6";
            codes[54] = "5-X5IN1-7";
            codes[55] = "5-X5IN1-8";
            codes[56] = "5-X10IN2-1";
            codes[57] = "5-X10IN2-2";
            codes[58] = "5-X10IN2-3";
            codes[59] = "5-X10IN2-4";
            codes[60] = "5-X10IN2-5";
            codes[61] = "5-X10IN2-6";
            codes[62] = "5-X10IN2-7";
            codes[63] = "5-X10IN2-8";
            codes[64] = "5-X15IN3-1";
            codes[65] = "5-X15IN3-2";
            codes[66] = "5-X15IN3-3";
            codes[67] = "5-X15IN3-4";
            codes[68] = "5-X15IN3-5";
            codes[69] = "5-X15IN3-6";
            codes[70] = "5-X15IN3-7";
            codes[71] = "5-X15IN3-8";
            codes[72] = "1-X9DO-1";
            codes[73] = "1-X9DO-2";
            codes[74] = "1-X9DO-3";
            codes[75] = "1-X9DO-4";
            codes[76] = "1-X9DO-5";
            codes[77] = "1-X9DO-6";
            codes[78] = "1-X9DO-7";
            codes[79] = "1-X9DO-8";
            codes[80] = "2-X9DO-1";
            codes[81] = "2-X9DO-2";
            codes[82] = "2-X9DO-3";
            codes[83] = "2-X9DO-4";
            codes[84] = "2-X9DO-5";
            codes[85] = "2-X9DO-6";
            codes[86] = "2-X9DO-7";
            codes[87] = "2-X9DO-8";
            codes[88] = "3-X9DO-1";
            codes[89] = "3-X9DO-2";
            codes[90] = "3-X9DO-3";
            codes[91] = "3-X9DO-4";
            codes[92] = "3-X9DO-5";
            codes[93] = "3-X9DO-6";
            codes[94] = "3-X9DO-7";
            codes[95] = "3-X9DO-8";
            codes[96] = "4-X6OUT1-1";
            codes[97] = "4-X6OUT1-2";
            codes[98] = "4-X6OUT1-3";
            codes[99] = "4-X6OUT1-4";
            codes[100] = "4-X6OUT1-5";
            codes[101] = "4-X6OUT1-6";
            codes[102] = "4-X6OUT1-7";
            codes[103] = "4-X6OUT1-8";
            codes[104] = "4-X11OUT2-1";
            codes[105] = "4-X11OUT2-2";
            codes[106] = "4-X11OUT2-3";
            codes[107] = "4-X11OUT2-4";
            codes[108] = "4-X11OUT2-5";
            codes[109] = "4-X11OUT2-6";
            codes[110] = "4-X11OUT2-7";
            codes[111] = "4-X11OUT2-8";
            codes[112] = "4-X16OUT3-1";
            codes[113] = "4-X16OUT3-2";
            codes[114] = "4-X16OUT3-3";
            codes[115] = "4-X16OUT3-4";
            codes[116] = "4-X16OUT3-5";
            codes[117] = "4-X16OUT3-6";
            codes[118] = "4-X16OUT3-7";
            codes[119] = "4-X16OUT3-8";
            codes[120] = "5-X6OUT1-1";
            codes[121] = "5-X6OUT1-2";
            codes[122] = "5-X6OUT1-3";
            codes[123] = "5-X6OUT1-4";
            codes[124] = "5-X6OUT1-5";
            codes[125] = "5-X6OUT1-6";
            codes[126] = "5-X6OUT1-7";
            codes[127] = "5-X6OUT1-8";
            codes[128] = "5-X11OUT2-1";
            codes[129] = "5-X11OUT2-2";
            codes[130] = "5-X11OUT2-3";
            codes[131] = "5-X11OUT2-4";
            codes[132] = "5-X11OUT2-5";
            codes[133] = "5-X11OUT2-6";
            codes[134] = "5-X11OUT2-7";
            codes[135] = "5-X11OUT2-8";
            codes[136] = "5-X16OUT3-1";
            codes[137] = "5-X16OUT3-2";
            codes[138] = "5-X16OUT3-3";
            codes[139] = "5-X16OUT3-4";
            codes[140] = "5-X16OUT3-5";
            codes[141] = "5-X16OUT3-6";
            codes[142] = "5-X16OUT3-7";
            codes[143] = "5-X16OUT3-8";
        }

        public void UpDateSignals(Parameters parameters)
        {
            InputNames.Clear();
            InputMeanings.Clear();
            OutputNames.Clear();
            OutputMeanings.Clear();
            foreach (string addSignal in _mineConfig.AuziDSignalsConfig.AddedSignals)
            {
                int index;
                if (Int32.TryParse(addSignal, out index))
                {
                    if (index < 72)
                    {
                        InputNames.Add(codes[index] + " " + _mineConfig.AuziDSignalsConfig.SignalsNames[index]);
                        if (parameters.AuziDIOSignalsState[index] == AuziDState.On)
                        {
                            InputMeanings.Add(Color.Green);
                        }
                        else if (parameters.AuziDIOSignalsState[index] == AuziDState.Off)
                        {
                            InputMeanings.Add(Color.Silver);
                        }
                        else if (parameters.AuziDIOSignalsState[index] == AuziDState.Undefind)
                        {
                            InputMeanings.Add(Color.White);
                        }
                    }
                    else
                    {
                        OutputNames.Add(codes[index] + " " + _mineConfig.AuziDSignalsConfig.SignalsNames[index]);
                        if (parameters.AuziDIOSignalsState[index] == AuziDState.On)
                        {
                            OutputMeanings.Add(Color.Green);
                        }
                        else if (parameters.AuziDIOSignalsState[index] == AuziDState.Off)
                        {
                            OutputMeanings.Add(Color.Silver);
                        }
                        else if (parameters.AuziDIOSignalsState[index] == AuziDState.Undefind)
                        {
                            OutputMeanings.Add(Color.White);
                        }
                    }
                    
                }
                if (parameters.AuziDIOSignalsState[73] == AuziDState.Off)
                {
                    if (_leftPerepodiom == 0)
                    {
                        var dataBaseService = new DataBaseService();
                        dataBaseService.FillGeneralLog("Левый переподъём", "Левый переподъём", GeneralLogEventType.Demage);
                    }
                    _leftPerepodiom = 1;
                }
                else
                {
                    _leftPerepodiom = 0;
                }
                if (parameters.AuziDIOSignalsState[72] == AuziDState.Off)
                {
                    if (_rightPerepodiom == 0)
                    {
                        var dataBaseService = new DataBaseService();
                        dataBaseService.FillGeneralLog("Правый переподъём", "Правый переподъём", GeneralLogEventType.Demage);
                    }
                    _rightPerepodiom = 1;
                }
                else
                {
                    _rightPerepodiom = 0;
                }
                if (parameters.AuziDIOSignalsState[78] == AuziDState.Off)
                {
                    if (_prevSpeed == 0)
                    {
                        var dataBaseService = new DataBaseService();
                        dataBaseService.FillGeneralLog("Превышение скорости", "Превышение скорости", GeneralLogEventType.Demage);
                    }
                    _prevSpeed = 1;
                }
                else
                {
                    _prevSpeed = 0;
                }
            }
           /* int numEdded = InputNames.Count;
            for (int i = numEdded; i < 32; i++)
            {
                InputNames.Add("");
                InputMeanings.Add(Color.Gray);
            }
            numEdded = OutputNames.Count;
            for (int i = numEdded; i < 16; i++)
            {
                OutputNames.Add("");
                OutputMeanings.Add(Color.Gray);
            } */
        }

        public List<string> InputNames { get; private set; }
        public List<Color> InputMeanings { get; private set; }
        public List<string> OutputNames { get; private set; }
        public List<Color> OutputMeanings { get; private set; }

        string[] codes;

        private MineConfig _mineConfig;
        private int _prevSpeed;
        private int _leftPerepodiom;
        private int _rightPerepodiom;
    }
}
