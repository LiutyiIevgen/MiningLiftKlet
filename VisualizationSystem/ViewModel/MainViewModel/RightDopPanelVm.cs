using System;
using System.Collections.Generic;
using System.Drawing;
using ML.ConfigSettings.Model;
using ML.ConfigSettings.Services;
using ML.DataExchange.Model;
using VisualizationSystem.Model;
using VisualizationSystem.Model.PanelData;

namespace VisualizationSystem.ViewModel.MainViewModel
{
    class RightDopPanelVm
    {
        public RightDopPanelVm(int panelWidth, int panelHeight)
        {
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;
            _mineConfig = IoC.Resolve<MineConfig>();
            pen = new Pen(Color.Black, 2);
            red_pen = new Pen(Color.Red, 1);
            drawFont_two = new Font("Arial", 16);
            levelsFont = new Font("Arial", 10);
            black = new SolidBrush(Color.Black);
            red = new SolidBrush(Color.Red);
            white = new SolidBrush(Color.WhiteSmoke);

            RuleDatas = new List<RuleData>();
            RuleInscriptions = new List<RuleInscription>();
            RulePointerLine = new List<RuleData>();
            RulePointer = new List<Pointer>();
            RuleFillPointer = new List<FillPointer>();
            PanelBorderLine = new List<BorderLine>();
            LevelsInscriptions = new List<RuleInscription>();
        }

        public void InitVm(Parameters parameters)
        {
            RuleDatas.Clear();
            RuleInscriptions.Clear();
            RulePointerLine.Clear();
            RulePointer.Clear();
            RuleFillPointer.Clear();
            PanelBorderLine.Clear();
            LevelsInscriptions.Clear();

            _parameters = parameters;
            SetLength();
            SetPointsValue();
        }
        private void SetLength()
        {
            if (_parameters.v <= _mineConfig.MainViewConfig.MaxDopRuleSpeed.Value)
            {
                long_desh_width = panelWidth / 3 / 2;
                middle_desh_width = panelWidth / 3 / 4;
                small_desh_width = panelWidth / 3 / 6;
                rule_hight = panelHeight - 20;
                pixel_pro_ten_santimeter = rule_hight / 10;
            }
        }
        private void SetPointsValue()
        {
            if (_parameters.v <= _mineConfig.MainViewConfig.MaxDopRuleSpeed.Value)
            {
                x1_long = Convert.ToInt32(panelWidth / 2 - long_desh_width / 2);
                x2_long = Convert.ToInt32(panelWidth / 2 + long_desh_width / 2);
                x1_middle = Convert.ToInt32(panelWidth / 2 - middle_desh_width / 2);
                x2_middle = Convert.ToInt32(panelWidth / 2 + middle_desh_width / 2);
                x1_small = Convert.ToInt32(panelWidth / 2 - small_desh_width / 2);
                x2_small = Convert.ToInt32(panelWidth / 2 + small_desh_width / 2);
            }
        }

        public List<RuleData> GetDopRuleDatas()
        {
            if (_parameters.v <= _mineConfig.MainViewConfig.MaxDopRuleSpeed.Value)
            {
                RuleDatas.Add(new RuleData
                    {
                        Pen = pen,
                        FirstPoint = new Point(panelWidth/2, 0),
                        SecondPoint = new Point(panelWidth/2, panelHeight)
                    });
                for (int i = Convert.ToInt32(Math.Round(_parameters.s_two, 2) * 100) - 500;
                     i <= Convert.ToInt32(Math.Round(_parameters.s_two, 2) * 100) + 500;
                     i++)
                {
                    if (i%100 == 0)
                    {
                        RuleDatas.Add(new RuleData
                            {
                                Pen = pen,
                                FirstPoint =
                                    new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_ten_santimeter * i / 100) - Convert.ToInt32(pixel_pro_ten_santimeter * (Math.Round(_parameters.s_two, 2) - 5)))),
                                SecondPoint =
                                    new Point(x2_long, (10 + Convert.ToInt32(pixel_pro_ten_santimeter * i / 100) - Convert.ToInt32(pixel_pro_ten_santimeter * (Math.Round(_parameters.s_two, 2) - 5))))
                            });
                        if ((i/100) >= 10 && (i/100) < 100)
                            RuleInscriptions.Add(new RuleInscription
                                {
                                    Text = Convert.ToString((i / 100) * (-1)),
                                    Font = drawFont_two,
                                    Brush = black,
                                    Position =
                                        new Point(x2_long - 3, Convert.ToInt32(pixel_pro_ten_santimeter * i / 100) - Convert.ToInt32(pixel_pro_ten_santimeter * (Math.Round(_parameters.s_two, 2) - 5)) - 2)
                                });
                        else if ((i/100) <= -10)
                            RuleInscriptions.Add(new RuleInscription
                                {
                                    Text = Convert.ToString((i/100)*(-1)),
                                    Font = drawFont_two,
                                    Brush = black,
                                    Position =
                                        new Point(x2_long - 3, Convert.ToInt32(pixel_pro_ten_santimeter * i / 100) - Convert.ToInt32(pixel_pro_ten_santimeter * (Math.Round(_parameters.s_two, 2) - 5)) - 2)
                                });
                        else if ((i/100) <= 0 && (i/100) > -10)
                            RuleInscriptions.Add(new RuleInscription
                                {
                                    Text = Convert.ToString((i/100)*(-1)),
                                    Font = drawFont_two,
                                    Brush = black,
                                    Position =
                                        new Point(x2_long - 3, Convert.ToInt32(pixel_pro_ten_santimeter * i / 100) - Convert.ToInt32(pixel_pro_ten_santimeter * (Math.Round(_parameters.s_two, 2) - 5)) - 2)
                                });
                        else if ((i/100) > 0 && (i/100) < 10)
                            RuleInscriptions.Add(new RuleInscription
                                {
                                    Text = Convert.ToString((i/100)*(-1)),
                                    Font = drawFont_two,
                                    Brush = black,
                                    Position =
                                        new Point(x2_long - 3, Convert.ToInt32(pixel_pro_ten_santimeter * i / 100) - Convert.ToInt32(pixel_pro_ten_santimeter * (Math.Round(_parameters.s_two, 2) - 5)) - 2)
                                });
                        else if ((i/100) >= 100 && (i/100) < 1000)
                            RuleInscriptions.Add(new RuleInscription
                                {
                                    Text = Convert.ToString((i/100)*(-1)),
                                    Font = drawFont_two,
                                    Brush = black,
                                    Position =
                                        new Point(x2_long - 3, Convert.ToInt32(pixel_pro_ten_santimeter * i / 100) - Convert.ToInt32(pixel_pro_ten_santimeter * (Math.Round(_parameters.s_two, 2) - 5)) - 2)
                                });
                        else if ((i/100) >= 1000)
                            RuleInscriptions.Add(new RuleInscription
                                {
                                    Text = Convert.ToString((i/100)*(-1)),
                                    Font = drawFont_two,
                                    Brush = black,
                                    Position =
                                        new Point(x2_long - 3, Convert.ToInt32(pixel_pro_ten_santimeter * i / 100) - Convert.ToInt32(pixel_pro_ten_santimeter * (Math.Round(_parameters.s_two, 2) - 5)) - 2)
                                });
                    }
                    else if (i%50 == 0)
                    {
                        RuleDatas.Add(new RuleData
                            {
                                Pen = pen,
                                FirstPoint =
                                    new Point(x1_middle, (10 + Convert.ToInt32(pixel_pro_ten_santimeter * i / 100) - Convert.ToInt32(pixel_pro_ten_santimeter * (Math.Round(_parameters.s_two, 2) - 5)))),
                                SecondPoint =
                                    new Point(x2_middle, (10 + Convert.ToInt32(pixel_pro_ten_santimeter * i / 100) - Convert.ToInt32(pixel_pro_ten_santimeter * (Math.Round(_parameters.s_two, 2) - 5))))
                            });
                    }
                    else if (i%10 == 0)
                    {
                        RuleDatas.Add(new RuleData
                            {
                                Pen = pen,
                                FirstPoint =
                                    new Point(x1_small, (10 + Convert.ToInt32(pixel_pro_ten_santimeter * i / 100) - Convert.ToInt32(pixel_pro_ten_santimeter * (Math.Round(_parameters.s_two, 2) - 5)))),
                                SecondPoint =
                                    new Point(x2_small, (10 + Convert.ToInt32(pixel_pro_ten_santimeter * i / 100) - Convert.ToInt32(pixel_pro_ten_santimeter * (Math.Round(_parameters.s_two, 2) - 5))))
                            });
                    }
                }
            }
            return RuleDatas;
        }

        public List<RuleInscription> GetDopRuleInscription()
        {
            return RuleInscriptions;
        }

        public List<RuleData> GetDopRulePointerLine()
        {
            if (_parameters.v <= _mineConfig.MainViewConfig.MaxDopRuleSpeed.Value)
            {
                RulePointerLine.Add(new RuleData
                    {
                        Pen = red_pen,
                        FirstPoint = new Point(15, panelHeight / 2),
                        SecondPoint = new Point(x2_long, panelHeight / 2)
                    });
                RulePointer.Add(new Pointer
                    {
                        Pen = red_pen,
                        Triangle = new Point[3]
                            {
                                new Point(40, panelHeight / 2),
                                new Point(15, panelHeight / 2 - 10),
                                new Point(15, panelHeight / 2 + 10)
                            }
                    });
                RuleFillPointer.Add(new FillPointer
                    {
                        Brush = red,
                        Triangle = new Point[3]
                            {
                                new Point(40, panelHeight / 2),
                                new Point(15, panelHeight / 2 - 10),
                                new Point(15, panelHeight / 2 + 10)
                            }
                    });
                LevelsInscriptions.Add(new RuleInscription
                {
                    Text = "1",
                    Font = levelsFont,
                    Brush = white,
                    Position = new Point(15, panelHeight / 2 - 8)
                });
                if (_mineConfig.KletConfig.KletLevelsCount.Value > 1 && _mineConfig.MainViewConfig.RightSosud == SosudType.Skip)
                {
                    RulePointerLine.Add(new RuleData
                    {
                        Pen = red_pen,
                        FirstPoint = new Point(15, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * _mineConfig.KletConfig.FirstLevelHight.Value)),
                        SecondPoint = new Point(x2_long, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * _mineConfig.KletConfig.FirstLevelHight.Value))
                    });
                    RulePointer.Add(new Pointer
                    {
                        Pen = red_pen,
                        Triangle = new Point[3]
                            {
                                new Point(40, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * _mineConfig.KletConfig.FirstLevelHight.Value)),
                                new Point(15, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * _mineConfig.KletConfig.FirstLevelHight.Value) - 10),
                                new Point(15, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * _mineConfig.KletConfig.FirstLevelHight.Value) + 10)
                            }
                    });
                    RuleFillPointer.Add(new FillPointer
                    {
                        Brush = red,
                        Triangle = new Point[3]
                            {
                                new Point(40, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * _mineConfig.KletConfig.FirstLevelHight.Value)),
                                new Point(15, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * _mineConfig.KletConfig.FirstLevelHight.Value) - 10),
                                new Point(15, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * _mineConfig.KletConfig.FirstLevelHight.Value) + 10)
                            }
                    });
                    LevelsInscriptions.Add(new RuleInscription
                    {
                        Text = "2",
                        Font = levelsFont,
                        Brush = white,
                        Position = new Point(15, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * _mineConfig.KletConfig.FirstLevelHight.Value) - 8)
                    });
                }
                if (_mineConfig.KletConfig.KletLevelsCount.Value > 2 && _mineConfig.MainViewConfig.RightSosud == SosudType.Skip)
                {
                    RulePointerLine.Add(new RuleData
                    {
                        Pen = red_pen,
                        FirstPoint = new Point(15, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * (_mineConfig.KletConfig.FirstLevelHight.Value + _mineConfig.KletConfig.SecondLevelHight.Value))),
                        SecondPoint = new Point(x2_long, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * (_mineConfig.KletConfig.FirstLevelHight.Value + _mineConfig.KletConfig.SecondLevelHight.Value)))
                    });
                    RulePointer.Add(new Pointer
                    {
                        Pen = red_pen,
                        Triangle = new Point[3]
                            {
                                new Point(40, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * (_mineConfig.KletConfig.FirstLevelHight.Value + _mineConfig.KletConfig.SecondLevelHight.Value))),
                                new Point(15, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * (_mineConfig.KletConfig.FirstLevelHight.Value + _mineConfig.KletConfig.SecondLevelHight.Value)) - 10),
                                new Point(15, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * (_mineConfig.KletConfig.FirstLevelHight.Value + _mineConfig.KletConfig.SecondLevelHight.Value)) + 10)
                            }
                    });
                    RuleFillPointer.Add(new FillPointer
                    {
                        Brush = red,
                        Triangle = new Point[3]
                            {
                                new Point(40, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * (_mineConfig.KletConfig.FirstLevelHight.Value + _mineConfig.KletConfig.SecondLevelHight.Value))),
                                new Point(15, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * (_mineConfig.KletConfig.FirstLevelHight.Value + _mineConfig.KletConfig.SecondLevelHight.Value)) - 10),
                                new Point(15, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * (_mineConfig.KletConfig.FirstLevelHight.Value + _mineConfig.KletConfig.SecondLevelHight.Value)) + 10)
                            }
                    });
                    LevelsInscriptions.Add(new RuleInscription
                    {
                        Text = "3",
                        Font = levelsFont,
                        Brush = white,
                        Position = new Point(15, Convert.ToInt32(panelHeight / 2 - pixel_pro_ten_santimeter * (_mineConfig.KletConfig.FirstLevelHight.Value + _mineConfig.KletConfig.SecondLevelHight.Value)) - 8)
                    });
                }
            }
            return RulePointerLine;
        }

        public List<Pointer> GetDopRulePointer()
        {
            return RulePointer;
        }

        public List<FillPointer> GetDopRuleFillPointer()
        {
            return RuleFillPointer;
        }
        public List<RuleInscription> GetDopRuleLevelsInscription()
        {
            return LevelsInscriptions;
        }
        public List<BorderLine> GetDopRulePanelBorderLine()
        {
            if (_parameters.v <= _mineConfig.MainViewConfig.MaxDopRuleSpeed.Value)
            {
                PanelBorderLine.Add(new BorderLine
                    {
                        Pen = pen,
                        LeftTopX = 0,
                        LeftTopY = 0,
                        Width = panelWidth - 1,
                        Height = panelHeight - 1
                    });
            }
            return PanelBorderLine;
        }

        public void DisposeDrawingAttributes()
        {
            pen.Dispose();
            red_pen.Dispose();
            drawFont_two.Dispose();
            levelsFont.Dispose();
            black.Dispose();
            red.Dispose();
            white.Dispose();
        }

        public List<RuleData> RuleDatas { get; private set; }
        public List<RuleInscription> RuleInscriptions { get; private set; }
        public List<RuleData> RulePointerLine { get; private set; }
        public List<Pointer> RulePointer { get; private set; }
        public List<FillPointer> RuleFillPointer { get; private set; }
        public List<BorderLine> PanelBorderLine { get; private set; }
        public List<RuleInscription> LevelsInscriptions { get; private set; }

        private Parameters _parameters;
        private MineConfig _mineConfig;
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
        private Pen red_pen;
        private Font drawFont_two;
        private Font levelsFont;
        private SolidBrush black;
        private SolidBrush red;
        private SolidBrush white;
    }
}
