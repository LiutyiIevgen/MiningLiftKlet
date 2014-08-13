using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML.ConfigSettings.Services;
using ML.DataExchange.Model;
using ML.DataRepository.Models;
using ML.DataRepository.Models.GenericRepository;
using Ninject.Parameters;
using VisualizationSystem.Model;
using VisualizationSystem.Model.DataBase;
using GeneralLogEventType = ML.DataExchange.Model.GeneralLogEventType;

namespace VisualizationSystem.Services
{
    public class DataBaseService
    {
        public DataBaseService()
        {
            _mineConfig = IoC.Resolve<MineConfig>();
            _fillDataBase = 0;
        }
        public List<int> GetBlocksIds(DateTime from, DateTime till)
        {
            var ids = new List<int>();
            using (var repoUnit = new RepoUnit())
            {
                ids.AddRange(repoUnit.BlockLog.Load(blc => blc.Date > from && blc.Date < till).Select(r => r.Id));
            }
            return ids;
        }
        public List<DateTime> GetBlocksDateTimes(DateTime from, DateTime till)
        {
            var dates = new List<DateTime>();
            using (var repoUnit = new RepoUnit())
            {
                dates.AddRange(repoUnit.BlockLog.Load(blc => blc.Date > from && blc.Date < till).Select(r => r.Date));
            }
            return dates;
        }
        public List<List<ParameterData>> GetAnalogSignalsById(int id)
        {
            var parameterData = new List<List<ParameterData>>();
            //init parameterData
            for (int j = 0; j < 3; j++)
            {
                parameterData.Add(new List<ParameterData>());
            }
            //write data from arhive to parameterData
            using (var repoUnit = new RepoUnit())
            {
                var block = repoUnit.BlockLog.FindFirstBy(blc => blc.Id == id);
                for (int i = 0; i < 3; i++)
                {
                    parameterData[i].AddRange(block.AnalogSignalLogs.Where(signal => signal.NodeId == i + 1).Select(s => new ParameterData
                    {
                        Name = s.SignalType.Type,
                        Value = s.SignalValue.ToString()
                    }));
                }
               /* parameterData.AddRange(block.AnalogSignalLogs.Select(signal => new ParameterData
                {
                    Name = signal.SignalType.Type,
                    Value = signal.SignalValue.ToString()
                })); */
            }
            return parameterData;
        }

        public List<ParameterData> GetInputSignalsById(int id)
        {
            var parameterData = new List<ParameterData>();
            using (var repoUnit = new RepoUnit())
            {
                var block = repoUnit.BlockLog.FindFirstBy(blc => blc.Id == id);
                var byteList = new List<byte?> { block.InputSignalsLogs.First().Vio0, block.InputSignalsLogs.First().Vio1, block.InputSignalsLogs.First().Vio2, block.InputSignalsLogs.First().Vio3 };
                foreach (var _byte in byteList)
                {
                    byte? b = _byte;
                    if (b == null)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            parameterData.Add(new ParameterData
                            {
                                Name = "",
                                Value = "нет данных"
                            });
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            parameterData.Add(new ParameterData
                            {
                                Name = "",
                                Value = (b & 0x01) == 1 ? "Лог '0'" : "Лог '1'"
                            });
                            b = (byte)(b >> 1);
                        } 
                    } 
                }
            }
            return parameterData;
        }

        public List<ParameterData> GetInputSignalsByIdArchiv(int id)
        {
            var parameterData = new List<ParameterData>();
            using (var repoUnit = new RepoUnit())
            {
                var block = repoUnit.BlockLog.FindFirstBy(blc => blc.Id == id);
                var byteList = new List<byte?> { block.InputSignalsLogs.First().Vio0, block.InputSignalsLogs.First().Vio1, block.InputSignalsLogs.First().Vio2, block.InputSignalsLogs.First().Vio3 };
                foreach (var _byte in byteList)
                {
                    byte? b = _byte;
                    if (b == null)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            parameterData.Add(new ParameterData
                            {
                                Name = "",
                                Value = "нет данных"
                            });
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            parameterData.Add(new ParameterData
                            {
                                Name = "",
                                Value = (b & 0x01) == 1 ? Convert.ToString(0) : Convert.ToString(1)
                            });
                            b = (byte)(b >> 1);
                        }
                    }
                }
            }
            return parameterData;
        }

        public List<ParameterData> GetOutputSignalsById(int id)
        {
            var parameterData = new List<ParameterData>();
            using (var repoUnit = new RepoUnit())
            {
                var block = repoUnit.BlockLog.FindFirstBy(blc => blc.Id == id);
                var byteList = new List<byte?> { block.OutputSignalsLogs.First().Vio7, block.OutputSignalsLogs.First().Vio8, block.OutputSignalsLogs.First().Vio9, 
                    block.OutputSignalsLogs.First().Vio11, block.OutputSignalsLogs.First().Vio12 };
                foreach (var _byte in byteList)
                {
                    byte? b = _byte;
                    if (b == null)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            parameterData.Add(new ParameterData
                            {
                                Name = "",
                                Value = "нет данных"
                            });
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            parameterData.Add(new ParameterData
                            {
                                Name = "",
                                Value = (b & 0x01) == 1 ? "Лог '0'" : "Лог '1'"
                            });
                            b = (byte)(b >> 1);
                        }
                    }
                }
            }
            return parameterData;
        }

        public List<ParameterData> GetOutputSignalsByIdArchiv(int id)
        {
            var parameterData = new List<ParameterData>();
            using (var repoUnit = new RepoUnit())
            {
                var block = repoUnit.BlockLog.FindFirstBy(blc => blc.Id == id);
                var byteList = new List<byte?> { block.OutputSignalsLogs.First().Vio7, block.OutputSignalsLogs.First().Vio8, block.OutputSignalsLogs.First().Vio9, 
                    block.OutputSignalsLogs.First().Vio11, block.OutputSignalsLogs.First().Vio12 };
                foreach (var _byte in byteList)
                {
                    byte? b = _byte;
                    if (b == null)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            parameterData.Add(new ParameterData
                            {
                                Name = "",
                                Value = "нет данных"
                            });
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            parameterData.Add(new ParameterData
                            {
                                Name = "",
                                Value = (b & 0x01) == 1 ? Convert.ToString(0) : Convert.ToString(1)
                            });
                            b = (byte)(b >> 1);
                        }
                    }
                }
            }
            return parameterData;
        }

        public BlockLog GetBlockLogById(int id)
        {
            BlockLog block;
            using (var repoUnit = new RepoUnit())
            {
                block = repoUnit.BlockLog.FindFirstBy(blc => blc.Id == id);
            }
            return block;
        }
        public List<List<ParameterData>> GetAnalogSignals(DateTime from, DateTime till)
        {
            var parameterData = new List<List<ParameterData>>();
            using (var repoUnit = new RepoUnit())
            {
                var blocks = repoUnit.BlockLog.Load(blc => blc.Date > from && blc.Date < till);
                parameterData.AddRange(blocks.Select(block => block.AnalogSignalLogs.Select(b => new ParameterData { Name = b.SignalType.Type, Value = b.SignalValue.ToString() }).ToList()).ToList());
            }
            return parameterData;
        }

        public List<string> GetAnalogSignalsNames()
        {
            var names = new List<string>();
            using (var repoUnit = new RepoUnit())
            {
                names.AddRange(repoUnit.AnalogSignal.Load().Select(an => an.Type));
            }
            return names;
        }

        public void LetFillDataBase()
        {
            _fillDataBase = 1;
        }

        public void FillDataBase(List<Parameters> parameters)
        {
            if (_fillDataBase == 1)
            {
                var analogSignals = new List<AnalogSignalLog>();
                int j = 0;
                foreach (var param in parameters)
                {
                    analogSignals.Add(new AnalogSignalLog {NodeId = j + 1, SignalTypeId = 1, SignalValue = -param.s});
                    analogSignals.Add(new AnalogSignalLog {NodeId = j + 1, SignalTypeId = 2, SignalValue = -param.s_two});
                    analogSignals.Add(new AnalogSignalLog {NodeId = j + 1, SignalTypeId = 3, SignalValue = param.v});
                    analogSignals.Add(new AnalogSignalLog {NodeId = j + 1, SignalTypeId = 4, SignalValue = param.a});
                    analogSignals.Add(new AnalogSignalLog {NodeId = j + 1, SignalTypeId = 5, SignalValue = param.tok_anchor});
                    analogSignals.Add(new AnalogSignalLog {NodeId = j + 1, SignalTypeId = 6, SignalValue = param.tok_excitation});
                    analogSignals.Add(new AnalogSignalLog { NodeId = j + 1, SignalTypeId = 7, SignalValue = param.defence_diagram});
                    j++;
                }
                var inputSignals = new InputSignalsLog
                {
                    Vio0 = parameters[_mineConfig.LeadingController - 1].AuziDIByteList[0],
                    Vio1 = parameters[_mineConfig.LeadingController - 1].AuziDIByteList[1],
                    Vio2 = parameters[_mineConfig.LeadingController - 1].AuziDIByteList[2],
                    Vio3 = parameters[_mineConfig.LeadingController - 1].AuziDIByteList[3]
                };
                var outputSignals = new OutputSignalsLog
                {
                    Vio7 = parameters[_mineConfig.LeadingController - 1].AuziDOByteList[0],
                    Vio8 = parameters[_mineConfig.LeadingController - 1].AuziDOByteList[1],
                    Vio9 = parameters[_mineConfig.LeadingController - 1].AuziDOByteList[2],
                    Vio11 = parameters[_mineConfig.LeadingController - 1].AuziDOByteList[3],
                    Vio12 = parameters[_mineConfig.LeadingController - 1].AuziDOByteList[4]
                };
                var blockLog = new BlockLog
                {
                    Date = DateTime.Now,
                    AnalogSignalLogs = analogSignals,
                    InputSignalsLogs = new Collection<InputSignalsLog> {inputSignals},
                    OutputSignalsLogs = new Collection<OutputSignalsLog> {outputSignals}
                };
                using (var repoUnit = new RepoUnit())
                {
                    repoUnit.BlockLog.Save(blockLog);
                }
                _fillDataBase = 0;
            }
        }

        public void FillGeneralLog(string line, string shortLine, GeneralLogEventType type)
        {
            var generalLog = new GeneralLog
            {
                Date = DateTime.Now,
                GeneralLogTypeId = Convert.ToInt32(type),
                LogLine = line,
                LogShortLine = shortLine
            };
            using (var repoUnit = new RepoUnit())
            {
                repoUnit.GeneralLog.Save(generalLog);
            }
        }

        public List<int> GetGeneralLogIds(DateTime from, DateTime till)
        {
            var ids = new List<int>();
            using (var repoUnit = new RepoUnit())
            {
                ids.AddRange(repoUnit.GeneralLog.Load(gl => gl.Date > from && gl.Date < till).Select(r => r.Id));
            }
            return ids;
        }

        public GeneralLogData GetGeneralLogLineById(int id)
        {
            GeneralLog glog;
            GeneralLogData logData = new GeneralLogData();
            string line;
            using (var repoUnit = new RepoUnit())
            {
                glog = repoUnit.GeneralLog.FindFirstBy(gl => gl.Id == id);
            }
            logData.Text = glog.Date.ToShortDateString() + "   " + glog.Date.ToString("HH:mm:ss") + "     " + glog.LogLine;
            logData.ShortText = glog.LogShortLine;
            switch (glog.GeneralLogTypeId)
            {
                case 1:
                    logData.TypeColor = Color.LimeGreen;
                    break;
                case 2:
                    logData.TypeColor = Color.Orange;
                    break;
                case 3:
                    logData.TypeColor = Color.Red;
                    break;
            }
            return logData;
        }

        private MineConfig _mineConfig;
        private int _fillDataBase;
    }
}
