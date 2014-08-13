using System;
using System.Collections.Generic;
using System.Drawing;
using ML.ConfigSettings.Services;
using ML.DataExchange.Model;
using VisualizationSystem.Model;
using VisualizationSystem.Model.PanelData;

namespace VisualizationSystem.ViewModel.MainViewModel
{
    class TokAnchorPanelVm
    {
        public TokAnchorPanelVm(int panelWidth, int panelHeight)
        {
            
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;
            _mineConfig = IoC.Resolve<MineConfig>();
            pen = new Pen(Color.Black, 2);
            orange_pen = new Pen(Color.Orange, 1);
            drawFont_two = new Font("Arial", 16);
            black = new SolidBrush(Color.Black);
            orange = new SolidBrush(Color.Orange);
            p_orange = new SolidBrush(Color.FromArgb(150, 255, 165, 0));

            RuleDatas = new List<RuleData>();
            RuleInscriptions = new List<RuleInscription>();
            RulePointerLine = new List<RuleData>();
            RulePointer = new List<Pointer>();
            RuleFillPointer = new List<FillPointer>();
            TokAnchorMeaningZone = new List<CageAndRuleZone>();
        }

        public void InitVm(Parameters parameters)
        {
            RuleDatas.Clear();
            RuleInscriptions.Clear();
            RulePointerLine.Clear();
            RulePointer.Clear();
            RuleFillPointer.Clear();
            TokAnchorMeaningZone.Clear();

            _parameters = parameters;
            SetLength();
            SetPointsValue();
            SolveTokAnchor();
        }
        private void SetLength()
        {
            long_desh_width = panelHeight / 3;
            middle_desh_width = panelHeight / 5;
            small_desh_width = panelHeight / 8;
            rule_hight = panelWidth - 20;
            pixel_pro_meter = rule_hight / _mineConfig.MainViewConfig.MaxTokAnchor.Value;
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

        private void SolveTokAnchor()
        {
            _parameters.tok_anchor = _parameters.a + 1;
            if (_parameters.f_ostanov == 1)
            {
                _parameters.tok_anchor = 0;
            }
        }

        public List<RuleData> GetTokAnchorRuleDatas()
        {
            RuleDatas.Add(new RuleData
            {
                Pen = pen,
                FirstPoint = new Point(0, panelHeight / 2),
                SecondPoint = new Point(panelWidth, panelHeight / 2)
            });
            for (int i = 0; i <= _mineConfig.MainViewConfig.MaxTokAnchor.Value * 10; i++)
            {
                if (i % 10 == 0)
                {
                    RuleDatas.Add(new RuleData
                    {
                        Pen = pen,
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
                        Pen = pen,
                        FirstPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * i / 10)), x1_middle),
                        SecondPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * i / 10)), x2_middle)
                    });
                }
                else if (i % 1 == 0)
                {
                    RuleDatas.Add(new RuleData
                    {
                        Pen = pen,
                        FirstPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * i / 10)), x1_small),
                        SecondPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * i / 10)), x2_small)
                    });
                }
            }
            return RuleDatas;
        }

        public List<RuleInscription> GetTokAnchorRuleInscription()
        {
            return RuleInscriptions;
        }

        public List<RuleData> GetTokAnchorRulePointerLine()
        {
                RulePointerLine.Add(new RuleData
                {
                    Pen = orange_pen,
                    FirstPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * _parameters.tok_anchor)), x2_long),
                    SecondPoint = new Point((10 + Convert.ToInt32(pixel_pro_meter * _parameters.tok_anchor)), 3)
                });
                RulePointer.Add(new Pointer
                {
                    Pen = orange_pen,
                    Triangle = new Point[3]
                            {
                                new Point((10 + Convert.ToInt32(pixel_pro_meter * _parameters.tok_anchor)), x1_long - 2),
                                new Point((5 + Convert.ToInt32(pixel_pro_meter * _parameters.tok_anchor)), 3),
                                new Point((15 + Convert.ToInt32(pixel_pro_meter * _parameters.tok_anchor)), 3)
                            }
                });
                RuleFillPointer.Add(new FillPointer
                {
                    Brush = orange,
                    Triangle = new Point[3]
                            {
                                new Point((10 + Convert.ToInt32(pixel_pro_meter * _parameters.tok_anchor)), x1_long - 2),
                                new Point((5 + Convert.ToInt32(pixel_pro_meter * _parameters.tok_anchor)), 3),
                                new Point((15 + Convert.ToInt32(pixel_pro_meter * _parameters.tok_anchor)), 3)
                            }
                });
            return RulePointerLine;
        }

        public List<Pointer> GetTokAnchorRulePointer()
        {
            return RulePointer;
        }

        public List<FillPointer> GetTokAnchorRuleFillPointer()
        {
            return RuleFillPointer;
        }

        public List<CageAndRuleZone> GetTokAnchorRuleTokAnchorMeaningZone()
        {
            TokAnchorMeaningZone.Add(new CageAndRuleZone
                {
                    Brush = p_orange,
                    LeftTopX = 10,
                    LeftTopY = x1_middle,
                    Width = Convert.ToInt32(pixel_pro_meter * _parameters.tok_anchor),
                    Height = Convert.ToInt32(middle_desh_width)
                });
                return TokAnchorMeaningZone;
        }

        public void DisposeDrawingAttributes()
        {
            pen.Dispose();
            orange_pen.Dispose();
            drawFont_two.Dispose();
            black.Dispose();
            orange.Dispose();
            p_orange.Dispose();
        }

        public List<RuleData> RuleDatas { get; private set; }
        public List<RuleInscription> RuleInscriptions { get; private set; }
        public List<RuleData> RulePointerLine { get; private set; }
        public List<Pointer> RulePointer { get; private set; }
        public List<FillPointer> RuleFillPointer { get; private set; }
        public List<CageAndRuleZone> TokAnchorMeaningZone { get; private set; }
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
        private Pen orange_pen;
        private Font drawFont_two;
        private SolidBrush black;
        private SolidBrush orange;
        SolidBrush p_orange;
    }
}
