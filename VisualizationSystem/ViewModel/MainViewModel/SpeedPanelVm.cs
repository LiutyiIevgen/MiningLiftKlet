using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ML.ConfigSettings.Services;
using ML.DataExchange.Model;
using VisualizationSystem.Model;
using VisualizationSystem.Model.PanelData;
using VisualizationSystem.Model.Settings;

namespace VisualizationSystem.ViewModel.MainViewModel
{
    class SpeedPanelVm
    {
        public SpeedPanelVm(int panelWidth, int panelHeight)
        {
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;
            _mineConfig = IoC.Resolve<MineConfig>();
            _parametersSettingsVm = new ParametersSettingsVm();
            _interCodtDomainDatas = new List<CodtDomainData>();
            pen = new Pen(Color.Black, 2);
            green_pen = new Pen(Color.FromArgb(255, 0, 255, 0), 1);
            red_pen = new Pen(Color.FromArgb(255, 250, 0, 0), 2);
            drawFont_two = new Font("Arial", 16);
            black = new SolidBrush(Color.Black);
            green = new SolidBrush(Color.FromArgb(255, 0, 255, 0));
            p_green = new SolidBrush(Color.FromArgb(125, 0, 255, 0));

            RuleDatas = new List<RuleData>();
            RuleInscriptions = new List<RuleInscription>();
            RulePointerLine = new List<RuleData>();
            RulePointer = new List<Pointer>();
            RuleFillPointer = new List<FillPointer>();
            SpeedMeaningZone = new List<CageAndRuleZone>();
        }

        public void InitVm(Parameters parameters)
        {
            RuleDatas.Clear();
            RuleInscriptions.Clear();
            RulePointerLine.Clear();
            RulePointer.Clear();
            RuleFillPointer.Clear();
            SpeedMeaningZone.Clear();

            _parameters = parameters;
            SetLength();
            SetPointsValue();
        }
        private void SetLength()
        {
            long_desh_width = panelHeight / 3;
            middle_desh_width = panelHeight / 5;
            small_desh_width = panelHeight / 8;
            rule_hight = panelWidth - 20;
            pixel_pro_meter = rule_hight / _mineConfig.MainViewConfig.MaxSpeed.Value;
        }

        private void SetPointsValue()
        {
            x1_long = Convert.ToInt32(panelHeight / 2 - long_desh_width / 2);
            x2_long = Convert.ToInt32(panelHeight / 2 + long_desh_width / 2);
            x1_middle = Convert.ToInt32(panelHeight / 2 - middle_desh_width / 2);
            x2_middle = Convert.ToInt32(panelHeight / 2 + middle_desh_width / 2);
            x1_small = Convert.ToInt32(panelHeight / 2 - small_desh_width / 2);
            x2_small = Convert.ToInt32(panelHeight / 2 + small_desh_width / 2);
        }

        public List<RuleData> GetSpeedRuleDatas()
        {
            double speedBound = _parameters.defence_diagram;
            RuleDatas.Add(new RuleData
            {
                Pen = pen,
                FirstPoint = new Point(0, panelHeight / 2),
                SecondPoint = new Point(panelWidth, panelHeight / 2)
            });
            for (int i = 0; i <= _mineConfig.MainViewConfig.MaxSpeed.Value * 10; i++)
            {
                if (i % 10 == 0)
                {
                    RuleDatas.Add(new RuleData
                    {
                        Pen = (double)(i)/10 >= speedBound ? red_pen : pen,
                        FirstPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * i / 10)), x1_long),
                        SecondPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * i / 10)), x2_long)
                    });
                    if (i / 10 >= 10)
                        RuleInscriptions.Add(new RuleInscription
                                {
                                    Text = Convert.ToString(i / 10),
                                    Font = drawFont_two,
                                    Brush = black,
                                    Position = new Point(Convert.ToInt32(pixel_pro_meter * i / 10) - 10, x2_long)
                                });
                    else
                        RuleInscriptions.Add(new RuleInscription
                        {
                            Text = Convert.ToString(i / 10),
                            Font = drawFont_two,
                            Brush = black,
                            Position = new Point(Convert.ToInt32(pixel_pro_meter * i / 10), x2_long)
                        });
                }
                else if (i % 5 == 0)
                {
                    RuleDatas.Add(new RuleData
                    {
                        Pen = (double)(i) / 10 >= speedBound ? red_pen : pen,
                        FirstPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * i / 10)), x1_middle),
                        SecondPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * i / 10)), x2_middle)
                    });
                }
                else if (i % 1 == 0)
                {
                    RuleDatas.Add(new RuleData
                    {
                        Pen = (double)(i) / 10 >= speedBound ? red_pen : pen,
                        FirstPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * i / 10)), x1_small),
                        SecondPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * i / 10)), x2_small)
                    });
                }
            }
            return RuleDatas;
        }

        public List<RuleInscription> GetSpeedRuleInscription()
        {
            return RuleInscriptions;
        }

        public List<RuleData> GetSpeedRulePointerLine()
        {
                RulePointerLine.Add(new RuleData
                {
                    Pen = green_pen,
                    FirstPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * _parameters.v)), x2_long),
                    SecondPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * _parameters.v)), 3)
                });
                RulePointer.Add(new Pointer
                {
                    Pen = green_pen,
                    Triangle = new Point[3]
                            {
                                new Point((10 + Convert.ToInt32(pixel_pro_meter * _parameters.v)), x1_long - 2),
                                new Point((5 + Convert.ToInt32(pixel_pro_meter * _parameters.v)), 3),
                                new Point((15 + Convert.ToInt32(pixel_pro_meter * _parameters.v)), 3)
                            }
                });
                RuleFillPointer.Add(new FillPointer
                {
                    Brush = green,
                    Triangle = new Point[3]
                            {
                                new Point((10 + Convert.ToInt32(pixel_pro_meter * _parameters.v)), x1_long - 2),
                                new Point((5 + Convert.ToInt32(pixel_pro_meter * _parameters.v)), 3),
                                new Point((15 + Convert.ToInt32(pixel_pro_meter * _parameters.v)), 3)
                            }
                });
            return RulePointerLine;
        }

        public List<Pointer> GetSpeedRulePointer()
        {
            return RulePointer;
        }

        public List<FillPointer> GetSpeedRuleFillPointer()
        {
            return RuleFillPointer;
        }

        public List<CageAndRuleZone> GetSpeedRuleSpeedMeaningZone()
        {
                SpeedMeaningZone.Add(new CageAndRuleZone
                {
                    Brush = p_green,
                    LeftTopX = 10,
                    LeftTopY = x1_middle,
                    Width = Convert.ToInt32(pixel_pro_meter * _parameters.v),
                    Height = Convert.ToInt32(middle_desh_width)
                });
                return SpeedMeaningZone;
        }

        public void DisposeDrawingAttributes()
        {
            pen.Dispose();
            green_pen.Dispose();
            red_pen.Dispose();
            drawFont_two.Dispose();
            black.Dispose();
            green.Dispose();
            p_green.Dispose();
        }

        private double GetSpeedBoundaryValue()
        {
            try
            {
                if (_interCodtDomainDatas.Count == 0)
                {
                    _parametersSettingsVm.ReadFromFile(_mineConfig.ParametersConfig.ParametersFileName);
                    var codtDomainDatas = _parametersSettingsVm.ParametersSettingsDatas[53].CodtDomainArray.ToList();
                    //interplation
                    int vStep = -100; //0.1 m/sec
                    int i;
                    for (i = 0; codtDomainDatas[i].Coordinate != 2147483647; i++)
                    {
                        _interCodtDomainDatas.Add(codtDomainDatas[i]);
                        int vSub = codtDomainDatas[i + 1].Speed - codtDomainDatas[i].Speed;
                        int intervalsNum = Math.Abs(vSub/vStep);
                        if (intervalsNum == 0)
                        {
                            intervalsNum = 0;
                            continue;
                        }
                        int sStep =
                            Math.Abs((codtDomainDatas[i + 1].Coordinate - codtDomainDatas[i].Coordinate)/intervalsNum);
                        for (int j = 1; j < intervalsNum; j++)
                        {
                            _interCodtDomainDatas.Add(new CodtDomainData
                            {
                                Coordinate = codtDomainDatas[i].Coordinate + j*sStep,
                                Speed = codtDomainDatas[i].Speed + j*vStep
                            });
                        }
                    }

                    _interCodtDomainDatas.Add(new CodtDomainData
                    {
                        Coordinate = -(int) _mineConfig.MainViewConfig.BorderZero.Value*1000,
                        Speed = codtDomainDatas[i].Speed
                    });
                }
                // end interpolation
                double currentS;
                if (_parameters.f_ostanov == 1)
                    currentS = -10000000;
                else
                    currentS = _parameters.f_start == 1 ? -_parameters.s_two : -_parameters.s;
                var speed = _interCodtDomainDatas.First(a => (double)(a.Coordinate) / 1000 > currentS).Speed;
                return (double)(speed) / 1000;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

        public List<RuleData> RuleDatas { get; private set; }
        public List<RuleInscription> RuleInscriptions { get; private set; }
        public List<RuleData> RulePointerLine { get; private set; }
        public List<Pointer> RulePointer { get; private set; }
        public List<FillPointer> RuleFillPointer { get; private set; }
        public List<CageAndRuleZone> SpeedMeaningZone { get; private set; }

        private List<CodtDomainData> _interCodtDomainDatas;

        private Parameters _parameters;
        private MineConfig _mineConfig;
        private ParametersSettingsVm _parametersSettingsVm;
        private int panelWidth;
        private int panelHeight;
        private double long_desh_width;
        private double middle_desh_width;
        private double small_desh_width;
        private double rule_hight;
        private double pixel_pro_meter;
        private double pixel_pro_ten_santimeter;
        private int x1_long;
        private int x2_long;
        private int x1_middle;
        private int x2_middle;
        private int x1_small;
        private int x2_small;
        private Pen pen;
        private Pen green_pen;
        private Pen red_pen;
        private Font drawFont_two;
        private SolidBrush black;
        private SolidBrush green;
        SolidBrush p_green;

    }
}
