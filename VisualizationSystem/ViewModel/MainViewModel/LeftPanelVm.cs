using System;
using System.Collections.Generic;
using System.Drawing;
using ML.ConfigSettings.Services;
using ML.DataExchange.Model;
using VisualizationSystem.Model;
using VisualizationSystem.Model.PanelData;
using VisualizationSystem.Services;

namespace VisualizationSystem.ViewModel.MainViewModel
{
    class LeftPanelVm
    {
        
        public LeftPanelVm(int panelWidth, int panelHeight)
        {
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;

            _mineConfig = IoC.Resolve<MineConfig>();
            
            pen = new Pen(Color.Black, 2);
            pen_zero = new Pen(Color.Black, 1);
            green_pen = new Pen(Color.FromArgb(255, 100, 200, 50), 1);
            red_pen = new Pen(Color.Red, 1);
            skyblue_pen = new Pen(Color.DeepSkyBlue, 1);
            lightgray_pen = new Pen(Color.GhostWhite, 1);
            lightgray_pen_two = new Pen(Color.GhostWhite, 2);
            green_pen_two = new Pen(Color.FromArgb(255, 100, 200, 50), 2);
            red_pen_two = new Pen(Color.Red, 2);
            skyblue_pen_two = new Pen(Color.DeepSkyBlue, 2);
            drawFont = new Font("Arial", 17);
            black = new SolidBrush(Color.Black);
            red = new SolidBrush(Color.Red);
            green = new SolidBrush(Color.FromArgb(255, 100, 200, 50));
            skyblue = new SolidBrush(Color.DeepSkyBlue);
            lightgray = new SolidBrush(Color.GhostWhite);
            p_yellow = new SolidBrush(Color.FromArgb(125, 255, 255, 0));
            p_orange = new SolidBrush(Color.FromArgb(150, 255, 165, 0));

            RuleDatas = new List<RuleData>();
            RuleInscriptions = new List<RuleInscription>();
            Zones = new List<CageAndRuleZone>();
            RulePointerLine = new List<RuleData>();
            RulePointer = new List<Pointer>();
            RuleFillPointer = new List<FillPointer>();
            Cage = new List<CageAndRuleZone>();
            DirectionPointer = new List<Pointer>();
            DirectionFillPointer = new List<FillPointer>();
            LoadCage = new CageAndRuleZone();
        }

        static LeftPanelVm()
        {
            slowdown_zone = 0;
            dot_zone = 0;
            slowdown_zone_back = 0;
            dot_zone_back = 0;
        }

        public void InitVm(Parameters parameters)
        {
            RuleDatas.Clear();
            RuleInscriptions.Clear();
            Zones.Clear();
            RulePointerLine.Clear();
            RulePointer.Clear();
            RuleFillPointer.Clear();
            Cage.Clear();
            DirectionPointer.Clear();
            DirectionFillPointer.Clear();

            _parameters = parameters;
            SetLength();
            SetPointsValue();
            SolveZones();
            SolveDirection();
        }

        private void SetLength()
        {
            long_desh_width = panelWidth / 3 / 2;
            middle_desh_width = panelWidth / 3 / 4;
            small_desh_width = panelWidth / 3 / 6;
            rule_hight = panelHeight - 20;
            pixel_pro_meter = rule_hight / (_mineConfig.MainViewConfig.Distance.Value + 2 * Settings.UpZeroZone);
            pixel_pro_ten_santimeter = rule_hight / 20;
        }
        private void SetPointsValue()
        {
            x1_long = Convert.ToInt32(panelWidth / 2 - long_desh_width / 2);
            x2_long = Convert.ToInt32(panelWidth / 2 + long_desh_width / 2);
            x1_middle = Convert.ToInt32(panelWidth / 2 - middle_desh_width / 2);
            x2_middle = Convert.ToInt32(panelWidth / 2 + middle_desh_width / 2);
            x1_small = Convert.ToInt32(panelWidth / 2 - small_desh_width / 2);
            x2_small = Convert.ToInt32(panelWidth / 2 + small_desh_width / 2);
        }

        private void SolveZones()
        {
            if ((_parameters.f_slowdown_zone == 1) && (slowdown_zone == 0))
                slowdown_zone = _parameters.s;
            if (_parameters.f_slowdown_zone == 0)
                slowdown_zone = 0;
            if ((_parameters.f_dot_zone == 1) && (dot_zone == 0))
                dot_zone = _parameters.s;
            if (_parameters.f_dot_zone == 0)
                dot_zone = 0;
            if ((_parameters.f_slowdown_zone_back == 1) && (slowdown_zone_back == 0))
                slowdown_zone_back = _parameters.s;
            if (_parameters.f_slowdown_zone_back == 0)
                slowdown_zone_back = 0;
            if ((_parameters.f_dot_zone_back == 1) && (dot_zone_back == 0))
                dot_zone_back = _parameters.s;
            if (_parameters.f_dot_zone_back == 0)
                dot_zone_back = 0;
        } 

        private void SolveDirection()
        {
            if (_parameters.f_start == 1)
            {
                direction = 0;
            }
            if (_parameters.f_back == 1)
            {
                direction = 1;
            }
            if (_parameters.f_ostanov == 1)
            {
                direction = 2;
            }
        }

        public List<RuleData> GetMainRuleDatas()
        {
            RuleDatas.Add(new RuleData
            {
                Pen = pen,
                FirstPoint = new Point(panelWidth / 2, 0),
                SecondPoint = new Point(panelWidth / 2, panelHeight)
            });
            for (int i = Convert.ToInt32(-Settings.UpZeroZone + _mineConfig.MainViewConfig.BorderZero.Value); i <= (_mineConfig.MainViewConfig.Border.Value + Settings.UpZeroZone); i++)
            {
                if (i % 50 == 0)
                {
                    RuleDatas.Add(new RuleData
                    {Pen = pen,
                     FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * i))),
                     SecondPoint = new Point(x2_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * i)))
                    });
                    
                    if (i >= 1000)
                        RuleInscriptions.Add(new RuleInscription
                            {
                                Text = Convert.ToString(i * (-1)),
                                Font = drawFont,
                                Brush = black,
                                Position = new Point(x1_long - 65, Convert.ToInt32(pixel_pro_meter * i) + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) - 5)
                            });
                    else if (i >= 100 && i < 1000)
                        RuleInscriptions.Add(new RuleInscription
                            {
                                Text = Convert.ToString(i * (-1)),
                                Font = drawFont,
                                Brush = black,
                                Position = new Point(x1_long - 55, Convert.ToInt32(pixel_pro_meter * i) + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) - 5)
                            });
                    else if (i >= 10 && i < 100)
                        RuleInscriptions.Add(new RuleInscription
                            {
                                Text = Convert.ToString(i * (-1)),
                                Font = drawFont,
                                Brush = black,
                                Position = new Point(x1_long - 50, Convert.ToInt32(pixel_pro_meter * i) + Convert.ToInt32(pixel_pro_meter *(-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) - 5)
                            });
                    else
                        RuleInscriptions.Add(new RuleInscription
                        {
                            Text = Convert.ToString(i * (-1)),
                            Font = drawFont,
                            Brush = black,
                            Position = new Point(x1_long - 35, Convert.ToInt32(pixel_pro_meter * i) + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) - 5)
                        });
                }
                else if (i % 10 == 0)
                {
                    RuleDatas.Add(new RuleData
                    {
                        Pen = pen,
                        FirstPoint = new Point(x1_small, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * i))),
                        SecondPoint = new Point(x2_small, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * i)))
                    });
                }
            }
            return RuleDatas;
        }

        public List<RuleInscription> GetMainRuleInscription()
        {
            return RuleInscriptions;
        }

        public List<CageAndRuleZone> GetMainRuleZones()
        {
            if (_parameters.f_slowdown_zone == 1 && _parameters.s >= slowdown_zone)//slowdown_zone
            {
                Zones.Add(new CageAndRuleZone
                {
                    Brush = p_yellow,
                    LeftTopX = panelWidth / 2,
                    LeftTopY = (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * slowdown_zone)),
                    Width = 30,
                    Height = (Convert.ToInt32(pixel_pro_meter * (_parameters.s - slowdown_zone)))
                });
            }
            if (_parameters.f_dot_zone == 1 && _parameters.s >= dot_zone)//dot_zone
            {
                Zones.Add(new CageAndRuleZone
                {
                    Brush = p_orange,
                    LeftTopX = panelWidth / 2,
                    LeftTopY = (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * dot_zone)),
                    Width = 30,
                    Height = (Convert.ToInt32(pixel_pro_meter * (_parameters.s - dot_zone)))
                });
            }
            if (_parameters.f_slowdown_zone_back == 1 && _parameters.s <= slowdown_zone_back)//slowdown_zone_back
            {
                Zones.Add(new CageAndRuleZone
                {
                    Brush = p_yellow,
                    LeftTopX = panelWidth / 2,
                    LeftTopY = (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                    Width = 30,
                    Height = (Convert.ToInt32(pixel_pro_meter * (slowdown_zone_back - _parameters.s)))
                });
            }
            if (_parameters.f_dot_zone_back == 1 && _parameters.s <= dot_zone_back)//dot_zone_back
            {
                Zones.Add(new CageAndRuleZone
                {
                    Brush = p_orange,
                    LeftTopX = panelWidth / 2,
                    LeftTopY = (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                    Width = 30,
                    Height = (Convert.ToInt32(pixel_pro_meter * (dot_zone_back - _parameters.s)))
                });
            }
            return Zones;
        }

        public List<RuleData> GetMainRulePointerLineSkip()
        {
            if (_parameters.f_ostanov == 1 && LoadCageFlag == 0 && UnLoadCageFlag == 0)
            {
                RulePointerLine.Add(new RuleData
                {
                    Pen = green_pen_two,
                    FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                    SecondPoint = new Point(panelWidth / 2 + panelWidth / 6, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                });//отрисовка текущего значения пути
                RulePointer.Add(new Pointer
                {
                    Pen = green_pen,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                RuleFillPointer.Add(new FillPointer
                {
                    Brush = green,
                    Triangle = new Point[3]
                    { 
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                //клеть
                Cage.Add(new CageAndRuleZone
                {
                    Brush = green,
                    LeftTopX = panelWidth / 2 + panelWidth / 6,
                    LeftTopY = (Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                    Width = panelWidth / 2 - panelWidth / 6,
                    Height = 20
                });
            }
            if (_parameters.f_ostanov == 1 && _parameters.s < (_mineConfig.MainViewConfig.BorderZero.Value + 0.5) && _parameters.unload_state > 1 && _parameters.unload_state < 4)
            {
                RulePointerLine.Add(new RuleData
                {
                    Pen = lightgray_pen_two,
                    FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                    SecondPoint = new Point(panelWidth / 2 + panelWidth / 6, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                });//отрисовка текущего значения пути
                RulePointer.Add(new Pointer
                {
                    Pen = lightgray_pen,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                RuleFillPointer.Add(new FillPointer
                {
                    Brush = lightgray,
                    Triangle = new Point[3]
                    { 
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                UnLoadCageWidth += Convert.ToDouble(Convert.ToDouble(panelWidth / 2 - panelWidth / 6) / (6 * 20)); // (panelWidth / 2 - panelWidth / 6) / (9 * 20);
                Cage.Add(new CageAndRuleZone
                {
                    Brush = lightgray,
                    LeftTopX = panelWidth / 2 + panelWidth / 6,
                    LeftTopY = (Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                    Width = Convert.ToInt32(UnLoadCageWidth),
                    Height = 20
                });
                if (_parameters.unload_state == 3)
                    LoadUnLoadState = 1;
            }
            if (_parameters.f_ostanov == 1 && _parameters.s < (_mineConfig.MainViewConfig.BorderZero.Value + 0.5) && _parameters.unload_state >= 4)
            {
                RulePointerLine.Add(new RuleData
                {
                    Pen = lightgray_pen_two,
                    FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                    SecondPoint = new Point(panelWidth / 2 + panelWidth / 6, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                });//отрисовка текущего значения пути
                RulePointer.Add(new Pointer
                {
                    Pen = lightgray_pen,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                RuleFillPointer.Add(new FillPointer
                {
                    Brush = lightgray,
                    Triangle = new Point[3]
                    { 
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                UnLoadCageWidth = 0;
                UnLoadCageFlag = 1;
                Cage.Add(new CageAndRuleZone
                {
                    Brush = lightgray,
                    LeftTopX = panelWidth / 2 + panelWidth / 6,
                    LeftTopY = (Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                    Width = panelWidth / 2 - panelWidth / 6,
                    Height = 20
                });
            }
            if (_parameters.f_ostanov == 1 && _parameters.s > (_mineConfig.MainViewConfig.Border.Value - 0.5) && _parameters.load_state > 1 && _parameters.load_state < 4)
            {
                RulePointerLine.Add(new RuleData
                {
                    Pen = pen,
                    FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                    SecondPoint = new Point(panelWidth / 2 + panelWidth / 6, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                });//отрисовка текущего значения пути
                RulePointer.Add(new Pointer
                {
                    Pen = pen,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                RuleFillPointer.Add(new FillPointer
                {
                    Brush = black,
                    Triangle = new Point[3]
                    { 
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                LoadCageWidth += Convert.ToDouble(Convert.ToDouble(panelWidth / 2 - panelWidth / 6) / (6 * 20)); // (panelWidth / 2 - panelWidth / 6) / (9 * 20);
                Cage.Add(new CageAndRuleZone
                {
                    Brush = black,
                    LeftTopX = panelWidth / 2 + panelWidth / 6,
                    LeftTopY = (Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                    Width = Convert.ToInt32(LoadCageWidth),
                    Height = 20
                });
                if (_parameters.load_state == 3)
                    LoadUnLoadState = 2;
            }
            else if (_parameters.f_ostanov == 1 && _parameters.s > (_mineConfig.MainViewConfig.Border.Value - 0.5) && _parameters.load_state >= 4)
            {
                RulePointerLine.Add(new RuleData
                {
                    Pen = pen,
                    FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                    SecondPoint = new Point(panelWidth / 2 + panelWidth / 6, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                });//отрисовка текущего значения пути
                RulePointer.Add(new Pointer
                {
                    Pen = pen_zero,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                RuleFillPointer.Add(new FillPointer
                {
                    Brush = black,
                    Triangle = new Point[3]
                    { 
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                LoadCageWidth = 0;
                LoadCageFlag = 1;
                Cage.Add(new CageAndRuleZone
                {
                    Brush = black,
                    LeftTopX = panelWidth / 2 + panelWidth / 6,
                    LeftTopY = (Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                    Width = panelWidth / 2 - panelWidth / 6,
                    Height = 20
                });
            } 
            else if ((_parameters.s <= _mineConfig.MainViewConfig.BorderZero.Value - _mineConfig.MainViewConfig.BorderRed.Value) || (_parameters.s >= _mineConfig.MainViewConfig.Border.Value + _mineConfig.MainViewConfig.BorderRed.Value))
            {
                RulePointerLine.Add(new RuleData
                {
                    Pen = red_pen_two,
                    FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                    SecondPoint = new Point(panelWidth / 2 + panelWidth / 6, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                });//отрисовка текущего значения пути
                RulePointer.Add(new Pointer
                {
                    Pen = red_pen,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                RuleFillPointer.Add(new FillPointer
                {
                    Brush = red,
                    Triangle = new Point[3]
                    { 
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                //клеть
                Cage.Add(new CageAndRuleZone
                {
                    Brush = red,
                    LeftTopX = panelWidth / 2 + panelWidth / 6,
                    LeftTopY = (Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                    Width = panelWidth / 2 - panelWidth / 6,
                    Height = 20
                });
            }
            else if (direction == 0) //move down
            {
                if (LoadUnLoadState == 1 || LoadUnLoadState == 0)
                {
                    LoadCageFlag = 0;
                    UnLoadCageFlag = 0;
                    RulePointerLine.Add(new RuleData
                    {
                        Pen = lightgray_pen_two,
                        FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        SecondPoint = new Point(panelWidth / 2 + panelWidth / 6, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    });//отрисовка текущего значения пути
                    RulePointer.Add(new Pointer
                    {
                        Pen = lightgray_pen,
                        Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                    });
                    RuleFillPointer.Add(new FillPointer
                    {
                        Brush = lightgray,
                        Triangle = new Point[3]
                    { 
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                    });
                    //клеть
                    Cage.Add(new CageAndRuleZone
                    {
                        Brush = lightgray,
                        LeftTopX = panelWidth / 2 + panelWidth / 6,
                        LeftTopY = (Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                        Width = panelWidth / 2 - panelWidth / 6,
                        Height = 20
                    });
                }
                else if (LoadUnLoadState == 2)
                {
                    LoadCageFlag = 0;
                    UnLoadCageFlag = 0;
                    RulePointerLine.Add(new RuleData
                    {
                        Pen = pen,
                        FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        SecondPoint = new Point(panelWidth / 2 + panelWidth / 6, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    });//отрисовка текущего значения пути
                    RulePointer.Add(new Pointer
                    {
                        Pen = pen_zero,
                        Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                    });
                    RuleFillPointer.Add(new FillPointer
                    {
                        Brush = black,
                        Triangle = new Point[3]
                    { 
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                    });
                    //клеть
                    Cage.Add(new CageAndRuleZone
                    {
                        Brush = black,
                        LeftTopX = panelWidth / 2 + panelWidth / 6,
                        LeftTopY = (Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                        Width = panelWidth / 2 - panelWidth / 6,
                        Height = 20
                    });
                }
            }
            else if (direction == 1)//move up
            {
                if (LoadUnLoadState == 1 || LoadUnLoadState == 0)
                {
                    LoadCageFlag = 0;
                    UnLoadCageFlag = 0;
                    RulePointerLine.Add(new RuleData
                    {
                        Pen = lightgray_pen_two,
                        FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        SecondPoint = new Point(panelWidth / 2 + panelWidth / 6, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    });//отрисовка текущего значения пути
                    RulePointer.Add(new Pointer
                    {
                        Pen = lightgray_pen,
                        Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                    });
                    RuleFillPointer.Add(new FillPointer
                    {
                        Brush = lightgray,
                        Triangle = new Point[3]
                    { 
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                    });
                    //клеть
                    Cage.Add(new CageAndRuleZone
                    {
                        Brush = lightgray,
                        LeftTopX = panelWidth / 2 + panelWidth / 6,
                        LeftTopY = (Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                        Width = panelWidth / 2 - panelWidth / 6,
                        Height = 20
                    });
                }
                if (LoadUnLoadState == 2)
                {
                    LoadCageFlag = 0;
                    UnLoadCageFlag = 0;
                    RulePointerLine.Add(new RuleData
                    {
                        Pen = pen,
                        FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        SecondPoint = new Point(panelWidth / 2 + panelWidth / 6, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    });//отрисовка текущего значения пути
                    RulePointer.Add(new Pointer
                    {
                        Pen = pen_zero,
                        Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                    });
                    RuleFillPointer.Add(new FillPointer
                    {
                        Brush = black,
                        Triangle = new Point[3]
                    { 
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                    });
                    //клеть
                    Cage.Add(new CageAndRuleZone
                    {
                        Brush = black,
                        LeftTopX = panelWidth / 2 + panelWidth / 6,
                        LeftTopY = (Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                        Width = panelWidth / 2 - panelWidth / 6,
                        Height = 20
                    });
                }
            }
            else if (LoadCageFlag == 1)
            {
                RulePointerLine.Add(new RuleData
                {
                    Pen = pen,
                    FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                    SecondPoint = new Point(panelWidth / 2 + panelWidth / 6, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                });//отрисовка текущего значения пути
                RulePointer.Add(new Pointer
                {
                    Pen = pen_zero,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                RuleFillPointer.Add(new FillPointer
                {
                    Brush = black,
                    Triangle = new Point[3]
                    { 
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                //клеть
                Cage.Add(new CageAndRuleZone
                {
                    Brush = black,
                    LeftTopX = panelWidth / 2 + panelWidth / 6,
                    LeftTopY = (Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                    Width = panelWidth / 2 - panelWidth / 6,
                    Height = 20
                });
            }
            else if (UnLoadCageFlag == 1)
            {
                RulePointerLine.Add(new RuleData
                {
                    Pen = lightgray_pen_two,
                    FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                    SecondPoint = new Point(panelWidth / 2 + panelWidth / 6, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                });//отрисовка текущего значения пути
                RulePointer.Add(new Pointer
                {
                    Pen = lightgray_pen,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                RuleFillPointer.Add(new FillPointer
                {
                    Brush = lightgray,
                    Triangle = new Point[3]
                    { 
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                //клеть
                Cage.Add(new CageAndRuleZone
                {
                    Brush = lightgray,
                    LeftTopX = panelWidth / 2 + panelWidth / 6,
                    LeftTopY = (Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                    Width = panelWidth / 2 - panelWidth / 6,
                    Height = 20
                });
            }
            return RulePointerLine;
        }

        public List<RuleData> GetMainRulePointerLineBackBalance()
        {
            if (_parameters.f_ostanov == 1)
            {
                RulePointerLine.Add(new RuleData
                {
                    Pen = green_pen_two,
                    FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                    SecondPoint = new Point(panelWidth / 2 + panelWidth / 6, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                });//отрисовка текущего значения пути
                RulePointer.Add(new Pointer
                {
                    Pen = green_pen,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                RuleFillPointer.Add(new FillPointer
                {
                    Brush = green,
                    Triangle = new Point[3]
                    { 
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                //клеть
                Cage.Add(new CageAndRuleZone
                {
                    Brush = green,
                    LeftTopX = panelWidth / 2 + panelWidth / 6,
                    LeftTopY = (Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                    Width = panelWidth / 2 - panelWidth / 6,
                    Height = 20
                });
            }
            else if ((_parameters.s <= _mineConfig.MainViewConfig.BorderZero.Value - _mineConfig.MainViewConfig.BorderRed.Value) || (_parameters.s >= _mineConfig.MainViewConfig.Border.Value + _mineConfig.MainViewConfig.BorderRed.Value))
            {
                RulePointerLine.Add(new RuleData
                {
                    Pen = red_pen_two,
                    FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                    SecondPoint = new Point(panelWidth / 2 + panelWidth / 6, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                });//отрисовка текущего значения пути
                RulePointer.Add(new Pointer
                {
                    Pen = red_pen,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                RuleFillPointer.Add(new FillPointer
                {
                    Brush = red,
                    Triangle = new Point[3]
                    { 
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                //клеть
                Cage.Add(new CageAndRuleZone
                {
                    Brush = red,
                    LeftTopX = panelWidth / 2 + panelWidth / 6,
                    LeftTopY = (Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                    Width = panelWidth / 2 - panelWidth / 6,
                    Height = 20
                });
            }
            else if (_parameters.f_start == 1 || _parameters.f_back == 1)
            {
                RulePointerLine.Add(new RuleData
                {
                    Pen = skyblue_pen_two,
                    FirstPoint = new Point(x1_long, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                    SecondPoint = new Point(panelWidth / 2 + panelWidth / 6, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                });//отрисовка текущего значения пути
                RulePointer.Add(new Pointer
                {
                    Pen = skyblue_pen,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                RuleFillPointer.Add(new FillPointer
                {
                    Brush = skyblue,
                    Triangle = new Point[3]
                    { 
                        new Point(panelWidth / 2 + panelWidth / 6 - 10, (10 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (5 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6, (15 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                //клеть
                Cage.Add(new CageAndRuleZone
                {
                    Brush = skyblue,
                    LeftTopX = panelWidth / 2 + panelWidth / 6,
                    LeftTopY = (Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)),
                    Width = panelWidth / 2 - panelWidth / 6,
                    Height = 20
                });
            }
            return RulePointerLine;
        }

        public List<Pointer> GetMainRulePointer()
        {
            return RulePointer;
        }

        public List<FillPointer> GetMainRuleFillPointer()
        {
            return RuleFillPointer;
        }

        public List<CageAndRuleZone> GetMainRuleCage()
        {
            return Cage;
        }

        public List<Pointer> GetMainRuleDirectionPointerSkip()
        {
            if (direction == 0)
            {
                DirectionPointer.Add(new Pointer
                {
                    Pen = green_pen,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2, (18 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 - 9, (2 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 + 9, (2 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                DirectionFillPointer.Add(new FillPointer
                {
                    Brush = green,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2, (18 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 - 9, (2 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 + 9, (2 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
            }
            if (direction == 1)
            {
                DirectionPointer.Add(new Pointer
                {
                    Pen = green_pen,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2, (2 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 - 9, (18 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 + 9, (18 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                DirectionFillPointer.Add(new FillPointer
                {
                    Brush = green,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2, (2 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 - 9, (18 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 + 9, (18 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
            }
            return DirectionPointer;
        }

        public List<Pointer> GetMainRuleDirectionPointerBackBalance()
        {
            if (direction == 0)
            {
                DirectionPointer.Add(new Pointer
                {
                    Pen = lightgray_pen,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2, (18 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 - 9, (2 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 + 9, (2 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                DirectionFillPointer.Add(new FillPointer
                {
                    Brush = lightgray,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2, (18 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 - 9, (2 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 + 9, (2 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
            }
            if (direction == 1)
            {
                DirectionPointer.Add(new Pointer
                {
                    Pen = lightgray_pen,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2, (2 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 - 9, (18 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 + 9, (18 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
                DirectionFillPointer.Add(new FillPointer
                {
                    Brush = lightgray,
                    Triangle = new Point[3]
                    {
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2, (2 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 - 9, (18 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s))),
                        new Point(panelWidth / 2 + panelWidth / 6 + (panelWidth / 2 - panelWidth / 6) / 2 + 9, (18 + Convert.ToInt32(pixel_pro_meter * (-_mineConfig.MainViewConfig.BorderZero.Value + Settings.UpZeroZone)) + Convert.ToInt32(pixel_pro_meter * _parameters.s)))
                    }
                });
            }
            return DirectionPointer;
        }

        public List<FillPointer> GetMainRuleDirectionFillPointer()
        {
            return DirectionFillPointer;
        }

        public void DisposeDrawingAttributes()
        {
            pen.Dispose();
            pen_zero.Dispose();
            green_pen.Dispose();
            red_pen.Dispose();
            skyblue_pen.Dispose();
            lightgray_pen.Dispose();
            lightgray_pen_two.Dispose();
            green_pen_two.Dispose();
            red_pen_two.Dispose();
            skyblue_pen_two.Dispose();
            drawFont.Dispose();
            black.Dispose();
            red.Dispose();
            green.Dispose();
            skyblue.Dispose();
            lightgray.Dispose();
            p_yellow.Dispose();
            p_orange.Dispose();
        }

        public List<RuleData> RuleDatas { get; private set; }
        public List<RuleInscription> RuleInscriptions { get; private set; }
        public List<CageAndRuleZone> Zones { get; private set; }
        public List<RuleData> RulePointerLine { get; private set; }
        public List<Pointer> RulePointer { get; private set; }
        public List<FillPointer> RuleFillPointer { get; private set; }
        public List<CageAndRuleZone> Cage { get; private set; }
        public List<Pointer> DirectionPointer { get; private set; }
        public List<FillPointer> DirectionFillPointer { get; private set; }
        public CageAndRuleZone LoadCage { get; private set; }

        private Parameters _parameters;
        private MineConfig _mineConfig;
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
        private Pen pen_zero;
        private Pen green_pen;
        private Pen red_pen;
        private Pen skyblue_pen;
        private Pen lightgray_pen;
        private Pen lightgray_pen_two;
        private Pen green_pen_two;
        private Pen red_pen_two;
        private Pen skyblue_pen_two;
        private Font drawFont;
        private SolidBrush black;
        private SolidBrush red;
        private SolidBrush green;
        private SolidBrush skyblue;
        private SolidBrush lightgray;
        private SolidBrush p_yellow;
        private SolidBrush p_orange;
        private int panelWidth;
        private int panelHeight;
        private static double slowdown_zone;
        private static double dot_zone;
        private static double slowdown_zone_back;
        private static double dot_zone_back;
        private int direction;
        private static double LoadCageWidth;
        private static int LoadCageFlag;
        private static double UnLoadCageWidth;
        private static int UnLoadCageFlag;
        private static int LoadUnLoadState;
    }
}
