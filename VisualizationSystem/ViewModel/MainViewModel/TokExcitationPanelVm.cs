using System;
using System.Collections.Generic;
using System.Drawing;
using ML.ConfigSettings.Services;
using ML.DataExchange.Model;
using VisualizationSystem.Model;
using VisualizationSystem.Model.PanelData;

namespace VisualizationSystem.ViewModel.MainViewModel
{
    class TokExcitationPanelVm
    {
        public TokExcitationPanelVm(int panelWidth, int panelHeight)
        {
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;
            _mineConfig = IoC.Resolve<MineConfig>();
            pen = new Pen(Color.Black, 2);
            yellow_pen = new Pen(Color.Yellow, 1);
            drawFont_two = new Font("Arial", 16);
            black = new SolidBrush(Color.Black);
            yellow = new SolidBrush(Color.Yellow);
            p_yellow = new SolidBrush(Color.FromArgb(125, 255, 255, 0));

            RuleDatas = new List<RuleData>();
            RuleInscriptions = new List<RuleInscription>();
            RulePointerLine = new List<RuleData>();
            RulePointer = new List<Pointer>();
            RuleFillPointer = new List<FillPointer>();
            TokExcitationMeaningZone = new List<CageAndRuleZone>();
        }

        public void InitVm(Parameters parameters)
        {
            RuleDatas.Clear();
            RuleInscriptions.Clear();
            RulePointerLine.Clear();
            RulePointer.Clear();
            RuleFillPointer.Clear();
            TokExcitationMeaningZone.Clear();

            _parameters = parameters;
            SetLength();
            SetPointsValue();
            SolveTokExcitation();
        }
        private void SetLength()
        {
            long_desh_width = panelHeight / 3;
            middle_desh_width = panelHeight / 5;
            small_desh_width = panelHeight / 8;
            rule_hight = panelWidth - 20;
            pixel_pro_meter = rule_hight / (_mineConfig.MainViewConfig.MaxTokExcitation.Value * 2);
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

        private void SolveTokExcitation()
        {
            if (_parameters.f_start == 1)
            {
                _parameters.tok_excitation = 100;
            }
            if (_parameters.f_back == 1)
            {
                _parameters.tok_excitation = -100;
            }
            if (_parameters.f_ostanov == 1)
            {
                _parameters.tok_excitation = 0;
            }
        }

        public List<RuleData> GetTokExcitationRuleDatas()
        {
            RuleDatas.Add(new RuleData
            {
                Pen = pen,
                FirstPoint = new Point(0, panelHeight / 2),
                SecondPoint = new Point(panelWidth, panelHeight / 2)
            });
            for (int i = Convert.ToInt32(_mineConfig.MainViewConfig.MaxTokExcitation.Value * (-1)); i <= _mineConfig.MainViewConfig.MaxTokExcitation.Value; i++)
            {
                if (i % 50 == 0)
                {
                    RuleDatas.Add(new RuleData
                    {
                        Pen = pen,
                        FirstPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) + Convert.ToInt32(pixel_pro_meter * i)), x1_long),
                        SecondPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) + Convert.ToInt32(pixel_pro_meter * i)), x2_long)
                    });
                    if (i >= 10 && i < 100)
                        RuleInscriptions.Add(new RuleInscription
                        {
                            Text = Convert.ToString(i),
                            Font = drawFont_two,
                            Brush = black,
                            Position = new Point(Convert.ToInt32(pixel_pro_meter * i) + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) - 5, x2_long)
                        });
                    else if (i >= 100 && i < _mineConfig.MainViewConfig.MaxTokExcitation.Value)
                        RuleInscriptions.Add(new RuleInscription
                        {
                            Text = Convert.ToString(i),
                            Font = drawFont_two,
                            Brush = black,
                            Position = new Point(Convert.ToInt32(pixel_pro_meter * i) + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) - 10, x2_long)
                        });
                    else if (i >= _mineConfig.MainViewConfig.MaxTokExcitation.Value)
                        RuleInscriptions.Add(new RuleInscription
                        {
                            Text = Convert.ToString(i),
                            Font = drawFont_two,
                            Brush = black,
                            Position = new Point(Convert.ToInt32(pixel_pro_meter * i) + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) - 20, x2_long)
                        });
                    else if (i <= -10 && i > -_mineConfig.MainViewConfig.MaxTokExcitation.Value)
                        RuleInscriptions.Add(new RuleInscription
                        {
                            Text = Convert.ToString(i),
                            Font = drawFont_two,
                            Brush = black,
                            Position = new Point(Convert.ToInt32(pixel_pro_meter * i) + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) - 10, x2_long)
                        });
                    else
                        RuleInscriptions.Add(new RuleInscription
                        {
                            Text = Convert.ToString(i),
                            Font = drawFont_two,
                            Brush = black,
                            Position = new Point(Convert.ToInt32(pixel_pro_meter * i) + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value), x2_long)
                        });
                }
                else if (i % 10 == 0)
                {
                    RuleDatas.Add(new RuleData
                    {
                        Pen = pen,
                        FirstPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) + Convert.ToInt32(pixel_pro_meter * i)), x1_middle),
                        SecondPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) + Convert.ToInt32(pixel_pro_meter * i)), x2_middle)
                    });
                }
            }
            return RuleDatas;
        }

        public List<RuleInscription> GetTokExcitationRuleInscription()
        {
            return RuleInscriptions;
        }

        public List<RuleData> GetTokExcitationRulePointerLine()
        {
                RulePointerLine.Add(new RuleData
                {
                    Pen = yellow_pen,
                    FirstPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) + Convert.ToInt32(pixel_pro_meter * _parameters.tok_excitation)), x2_long),
                    SecondPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) + Convert.ToInt32(pixel_pro_meter * _parameters.tok_excitation)), 3)
                });
                RulePointer.Add(new Pointer
                {
                    Pen = yellow_pen,
                    Triangle = new Point[3]
                            {
                                new Point((10 + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) + Convert.ToInt32(pixel_pro_meter * _parameters.tok_excitation)), x1_long - 2),
                                new Point((5 + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) + Convert.ToInt32(pixel_pro_meter * _parameters.tok_excitation)), 3),
                                new Point((15 + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) + Convert.ToInt32(pixel_pro_meter * _parameters.tok_excitation)), 3)
                            }
                });
                RuleFillPointer.Add(new FillPointer
                {
                    Brush = yellow,
                    Triangle = new Point[3]
                            {
                                new Point((10 + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) + Convert.ToInt32(pixel_pro_meter * _parameters.tok_excitation)), x1_long - 2),
                                new Point((5 + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) + Convert.ToInt32(pixel_pro_meter * _parameters.tok_excitation)), 3),
                                new Point((15 + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) + Convert.ToInt32(pixel_pro_meter * _parameters.tok_excitation)), 3)
                            }
                });
            return RulePointerLine;
        }

        public List<Pointer> GetTokExcitationRulePointer()
        {
            return RulePointer;
        }

        public List<FillPointer> GetTokExcitationRuleFillPointer()
        {
            return RuleFillPointer;
        }

        public List<CageAndRuleZone> GetTokExcitationRuleTokAnchorMeaningZone()
        {
            if (_parameters.tok_excitation > 0)
                TokExcitationMeaningZone.Add(new CageAndRuleZone
                {
                    Brush = p_yellow,
                    LeftTopX = 10 + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value),
                    LeftTopY = x1_middle,
                    Width = Convert.ToInt32(pixel_pro_meter * _parameters.tok_excitation),
                    Height = Convert.ToInt32(middle_desh_width)
                });
            if (_parameters.tok_excitation < 0)
                TokExcitationMeaningZone.Add(new CageAndRuleZone
                {
                    Brush = p_yellow,
                    LeftTopX = 10 + Convert.ToInt32(pixel_pro_meter * _mineConfig.MainViewConfig.MaxTokExcitation.Value) + Convert.ToInt32(pixel_pro_meter * _parameters.tok_excitation),
                    LeftTopY = x1_middle,
                    Width = Convert.ToInt32(pixel_pro_meter * _parameters.tok_excitation) * (-1),
                    Height = Convert.ToInt32(middle_desh_width)
                });
            return TokExcitationMeaningZone;
        }

        public void DisposeDrawingAttributes()
        {
            pen.Dispose();
            yellow_pen.Dispose();
            drawFont_two.Dispose();
            black.Dispose();
            yellow.Dispose();
            p_yellow.Dispose();
        }

        public List<RuleData> RuleDatas { get; private set; }
        public List<RuleInscription> RuleInscriptions { get; private set; }
        public List<RuleData> RulePointerLine { get; private set; }
        public List<Pointer> RulePointer { get; private set; }
        public List<FillPointer> RuleFillPointer { get; private set; }
        public List<CageAndRuleZone> TokExcitationMeaningZone { get; private set; }
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
        private Font drawFont_two;
        private SolidBrush black;
        private SolidBrush p_yellow;
        private Pen yellow_pen;
        private SolidBrush yellow;
    }
}
