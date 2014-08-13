using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML.ConfigSettings.Services;
using ML.DataExchange.Model;
using VisualizationSystem.Model;
using VisualizationSystem.Model.BrakeData;

namespace VisualizationSystem.ViewModel.MainViewModel
{
    class BrakeBarsPictureVm
    {
        public BrakeBarsPictureVm(int panelWidth, int panelHeight)
        {
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;
            _mineConfig = IoC.Resolve<MineConfig>();
            BarsBackground = new List<FillRectData>();
            BarsContur = new List<RectData>();
            BarsContent = new List<FillRectData>();
            BarsValue = new List<TextData>();
            RuleDatas = new List<RuleData>();
            RuleInscriptions = new List<RuleInscription>();
            BarsName = new List<TextData>();
            BarsFrame = new List<RectData>();
            BindingLine = new List<LineData>();

            InitData();
        }

        private void InitData()
        {
            _barHight = 25;
            _barWidth = 150;
            _indent = 50;
            _maxPressure = 10;

            _x = panelWidth / 6;
            _y = panelHeight / 10;
            _width = panelWidth / 5;
            _height = (panelHeight / 6) * 2;
            _color = 180;
        }

        public void InitVm(Parameters parameters)
        {
            BarsBackground.Clear();
            BarsContur.Clear();
            BarsContent.Clear();
            BarsValue.Clear();
            RuleDatas.Clear();
            RuleInscriptions.Clear();
            BarsName.Clear();
            BarsFrame.Clear();
            BindingLine.Clear();

            _parameters = parameters;
            _greenZoneRabCyl1 = _mineConfig.BrakeSystemConfig.GreenZoneRabCyl1.Value;
            _greenZoneRabCyl2 = _mineConfig.BrakeSystemConfig.GreenZoneRabCyl2.Value;
            _greenZonePredCyl1 = _mineConfig.BrakeSystemConfig.GreenZonePredCyl1.Value;
            _greenZonePredCyl2 = _mineConfig.BrakeSystemConfig.GreenZonePredCyl2.Value;
            _rabCyl1Pressure = (_parameters.BrakeRabCyl1Pressure - _mineConfig.BrakeSystemConfig.AdcZero.Value) * _mineConfig.BrakeSystemConfig.AdcValueToBarrKoef.Value;
            _rabCyl2Pressure = (_parameters.BrakeRabCyl2Pressure - _mineConfig.BrakeSystemConfig.AdcZero.Value) * _mineConfig.BrakeSystemConfig.AdcValueToBarrKoef.Value;
            _predCyl1Pressure = (_parameters.BrakePredCyl1Pressure - _mineConfig.BrakeSystemConfig.AdcZero.Value) * _mineConfig.BrakeSystemConfig.AdcValueToBarrKoef.Value;
            _predCyl2Pressure = (_parameters.BrakePredCyl2Pressure - _mineConfig.BrakeSystemConfig.AdcZero.Value) * _mineConfig.BrakeSystemConfig.AdcValueToBarrKoef.Value;
        }

        public List<FillRectData> GetBarsBackgroundDatas()
        {
            //RabCyl1
            int x = _x - 70;
            int y = _y + _height + _height + _height / 2;
            int width = _barWidth;
            int height = _barHight;
            int color = 170;
            Color col;
            BarsBackground.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            color = 150;
            BarsContur.Add(new RectData
            {
                LeftTopX = x - 1,
                LeftTopY = y - 1,
                Height = height + 2,
                Width = width + 2,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 2)
            });
            width = Convert.ToInt32((_barWidth / _maxPressure) * _rabCyl1Pressure);
            if (_rabCyl1Pressure < _greenZoneRabCyl1 || _parameters.BrakeRabCyl1Pressure > _rabCyl1Pressure)
                col = Color.IndianRed;
            else
                col = Color.Green;
            BarsContent.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(col)
            });
            BarsValue.Add(new TextData
            {
                LeftTopX = x + _barWidth/2 - 15,
                LeftTopY = y,
                Text = Convert.ToString(Math.Round(_rabCyl1Pressure, 2), CultureInfo.GetCultureInfo("en-US"))
            });
            //PredCyl1
            x = _x - 70;
            y = _y + _height + _height + _height / 2 - (_barHight + 75);
            width = _barWidth;
            height = _barHight;
            color = 170;
            BarsBackground.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            color = 150;
            BarsContur.Add(new RectData
            {
                LeftTopX = x - 1,
                LeftTopY = y - 1,
                Height = height + 2,
                Width = width + 2,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 2)
            });
            width = Convert.ToInt32((_barWidth / _maxPressure) * _predCyl1Pressure);
            if (_predCyl1Pressure < _greenZonePredCyl1 || _predCyl1Pressure > _greenZonePredCyl2)
                col = Color.IndianRed;
            else
                col = Color.Green;
            BarsContent.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(col)
            });
            BarsValue.Add(new TextData
            {
                LeftTopX = x + _barWidth / 2 - 15,
                LeftTopY = y,
                Text = Convert.ToString(Math.Round(_predCyl1Pressure, 2), CultureInfo.GetCultureInfo("en-US"))
            });
            //RabCyl2
            x = _x - 70 + _barWidth + 50;
            y = _y + _height + _height + _height / 2;
            width = _barWidth;
            height = _barHight;
            color = 170;
            BarsBackground.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            color = 150;
            BarsContur.Add(new RectData
            {
                LeftTopX = x - 1,
                LeftTopY = y - 1,
                Height = height + 2,
                Width = width + 2,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 2)
            });
            width = Convert.ToInt32((_barWidth / _maxPressure) * _rabCyl2Pressure);
            if (_rabCyl2Pressure < _greenZoneRabCyl1 || _rabCyl2Pressure > _greenZoneRabCyl2)
                col = Color.IndianRed;
            else
                col = Color.Green;
            BarsContent.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(col)
            });
            BarsValue.Add(new TextData
            {
                LeftTopX = x + _barWidth / 2 - 15,
                LeftTopY = y,
                Text = Convert.ToString(Math.Round(_rabCyl2Pressure, 2), CultureInfo.GetCultureInfo("en-US"))
            });
            //PredCyl2
            x = _x - 70 + _barWidth + 50;
            y = _y + _height + _height + _height / 2 - (_barHight + 75);
            width = _barWidth;
            height = _barHight;
            color = 170;
            BarsBackground.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            color = 150;
            BarsContur.Add(new RectData
            {
                LeftTopX = x - 1,
                LeftTopY = y - 1,
                Height = height + 2,
                Width = width + 2,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 2)
            });
            width = Convert.ToInt32((_barWidth / _maxPressure) * _predCyl2Pressure);
            if (_predCyl2Pressure < _greenZonePredCyl1 || _predCyl2Pressure > _greenZonePredCyl2)
                col = Color.IndianRed;
            else
                col = Color.Green;
            BarsContent.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(col)
            });
            BarsValue.Add(new TextData
            {
                LeftTopX = x + _barWidth / 2 - 15,
                LeftTopY = y,
                Text = Convert.ToString(Math.Round(_predCyl2Pressure, 2), CultureInfo.GetCultureInfo("en-US"))
            });
            return BarsBackground;
        }

        public List<RectData> GetBarsConturDatas()
        {
            return BarsContur;
        }

        public List<FillRectData> GetBarsContentDatas()
        {
            return BarsContent;
        }

        public List<TextData> GetBarsValueDatas()
        {
            return BarsValue;
        }

        public List<RuleData> GetRuleDatas()
        {
            //RabCyl1
            int x = _x - 70;
            int y = _y + _height + _height + _height / 2 + _barHight;
            int color = 150;
            int shag = Convert.ToInt32(_barWidth / _maxPressure);
            Pen pen = new Pen(Color.FromArgb(255, color, color, color), 3);
            color = 200;
            SolidBrush brush = new SolidBrush(Color.FromArgb(255, color, color, color));
            Font font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
            int h = 0;
            for (int i = 0; i <= _maxPressure; i++)
            {
                if (i%2 == 0)
                {
                    RuleDatas.Add(new RuleData
                    {
                        FirstPoint = new Point(x + i*shag, y),
                        SecondPoint = new Point(x + i*shag, y + 10),
                        Pen = pen
                    });
                    if (i < 10)
                        h = 8;
                    else
                        h = 14;
                    RuleInscriptions.Add(new RuleInscription
                    {
                        Text = i.ToString(CultureInfo.GetCultureInfo("en-US")),
                        Brush = brush,
                        Font = font,
                        Position = new Point(x + i*shag - h, y + 11)
                    });
                }
            }
            //PredCyl1
            x = _x - 70;
            y = _y + _height + _height + _height / 2 + _barHight - (_barHight + 75);
            shag = Convert.ToInt32(_barWidth / _maxPressure);
            h = 0;
            for (int i = 0; i <= _maxPressure; i++)
            {
                if (i%2 == 0)
                {
                    RuleDatas.Add(new RuleData
                    {
                        FirstPoint = new Point(x + i*shag, y),
                        SecondPoint = new Point(x + i*shag, y + 10),
                        Pen = pen
                    });
                    if (i < 10)
                        h = 8;
                    else
                        h = 14;
                    RuleInscriptions.Add(new RuleInscription
                    {
                        Text = i.ToString(CultureInfo.GetCultureInfo("en-US")),
                        Brush = brush,
                        Font = font,
                        Position = new Point(x + i*shag - h, y + 11)
                    });
                }
            }
            //RabCyl2
            x = _x - 70 + _barWidth + 50;
            y = _y + _height + _height + _height / 2 + _barHight;
            shag = Convert.ToInt32(_barWidth / _maxPressure);
            h = 0;
            for (int i = 0; i <= _maxPressure; i++)
            {
                if (i % 2 == 0)
                {
                    RuleDatas.Add(new RuleData
                    {
                        FirstPoint = new Point(x + i * shag, y),
                        SecondPoint = new Point(x + i * shag, y + 10),
                        Pen = pen
                    });
                    if (i < 10)
                        h = 8;
                    else
                        h = 14;
                    RuleInscriptions.Add(new RuleInscription
                    {
                        Text = i.ToString(CultureInfo.GetCultureInfo("en-US")),
                        Brush = brush,
                        Font = font,
                        Position = new Point(x + i * shag - h, y + 11)
                    });
                }
            }
            //PredCyl2
            x = _x - 70 + _barWidth + 50;
            y = _y + _height + _height + _height / 2 + _barHight - (_barHight + 75);
            shag = Convert.ToInt32(_barWidth / _maxPressure);
            h = 0;
            for (int i = 0; i <= _maxPressure; i++)
            {
                if (i % 2 == 0)
                {
                    RuleDatas.Add(new RuleData
                    {
                        FirstPoint = new Point(x + i * shag, y),
                        SecondPoint = new Point(x + i * shag, y + 10),
                        Pen = pen
                    });
                    if (i < 10)
                        h = 8;
                    else
                        h = 14;
                    RuleInscriptions.Add(new RuleInscription
                    {
                        Text = i.ToString(CultureInfo.GetCultureInfo("en-US")),
                        Brush = brush,
                        Font = font,
                        Position = new Point(x + i * shag - h, y + 11)
                    });
                }
            }
            return RuleDatas;
        }

        public List<RuleInscription> GetRuleInscriptions()
        {
            return RuleInscriptions;
        }

        public List<RectData> GetBarsFrameDatas()
        {
            //RabCyl1
            int x = _x - 90;
            int y = _y + _height + _height + _height/2 - 35;
            int width = _barWidth + 40;
            int height = _barHight + 70;
            int color = 180;
            BarsFrame.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 1)
            });
            //PredCyl1
            x = _x - 90;
            y = _y + _height + _height + _height / 2 - 35 - (_barHight + 75);
            width = _barWidth + 40;
            height = _barHight + 70;
            color = 180;
            BarsFrame.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 1)
            });
            //RabCyl2
            x = _x - 90 + _barWidth + 50;
            y = _y + _height + _height + _height / 2 - 35;
            width = _barWidth + 40;
            height = _barHight + 70;
            color = 180;
            BarsFrame.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 1)
            });
            //PredCyl2
            x = _x - 90 + _barWidth + 50;
            y = _y + _height + _height + _height / 2 - 35 - (_barHight + 75);
            width = _barWidth + 40;
            height = _barHight + 70;
            color = 180;
            BarsFrame.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 1)
            });
            return BarsFrame;
        }

        public List<TextData> GetBarsNameDatas()
        {
            //RabCyl1
            int x = _x - 75;
            int y = _y + _height + _height + _height / 2 - 30;
            BarsName.Add(new TextData
            {
                LeftTopX = x,
                LeftTopY = y,
                Text = "РЦ левый, бары"
            });
            //PredCyl1
            x = _x - 75;
            y = _y + _height + _height + _height / 2 - 30 - (_barHight + 75);
            BarsName.Add(new TextData
            {
                LeftTopX = x,
                LeftTopY = y,
                Text = "ПЦ левый, бары"
            });
            //RabCyl2
            x = _x - 75 + _barWidth + 50;
            y = _y + _height + _height + _height / 2 - 30;
            BarsName.Add(new TextData
            {
                LeftTopX = x,
                LeftTopY = y,
                Text = "РЦ правый, бары"
            });
            //PredCyl2
            x = _x - 75 + _barWidth + 50;
            y = _y + _height + _height + _height / 2 - 30 - (_barHight + 75);
            BarsName.Add(new TextData
            {
                LeftTopX = x,
                LeftTopY = y,
                Text = "ПЦ правый, бары"
            });
            return BarsName;
        }

        public List<LineData> GetBindingLineDatas()
        {
            //PredCyl1
            int x1 = _x - _width / 5 - 4;
            int y1 = _y + _height + (_height - 2 * (_height / 10)) / 3 / 3 + 4 + 3 * (_height / 10) - 8 + _height / 4 + 5 + (_width / 5 + 10)/2;
            int x2 = _x - 54;
            int y2 = _y + _height + _height + _height / 2 - 35 - (_barHight + 75);
            int color = 170;
            BindingLine.Add(new LineData
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 2)
            });
            //PredCyl2
            x1 = _x + _width + _width / 5 + 4;
            y1 = _y + _height + (_height - 2 * (_height / 10)) / 3 / 3 + 4 + 3 * (_height / 10) - 8 + _height / 4 + 5 + (_width / 5 + 10) / 2;
            x2 = _x + _barWidth + _barWidth - 70;
            y2 = _y + _height + _height + _height / 2 - 35 - (_barHight + 75);
            color = 170;
            BindingLine.Add(new LineData
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 2)
            });
            //RabCyl1
            x1 = _x - _width / 5 - 10;
            y1 = _y + _height + _height / 10 - 9 + _height / 4 + (_width / 5 + 22) / 2;
            x2 = _x - 90;
            y2 = _y + _height + _height + _height / 2 + _barHight/2;
            color = 170;
            BindingLine.Add(new LineData
            {
                X1 = x1,
                Y1 = y1,
                X2 = x1 - 60,
                Y2 = y1 + 60,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 2)
            });
            BindingLine.Add(new LineData
            {
                X1 = x1 - 60,
                Y1 = y1 + 60,
                X2 = x1 - 60,
                Y2 = y2,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 2)
            });
            BindingLine.Add(new LineData
            {
                X1 = x1 - 60,
                Y1 = y2,
                X2 = x2,
                Y2 = y2,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 2)
            });
            //RabCyl2
            x1 = _x + _width + _width / 5 + 9;
            y1 = _y + _height + _height / 10 - 10 + _height / 4 + (_width / 5 + 22) / 2;
            x2 = _x + 2*_barWidth;
            y2 = _y + _height + _height + _height / 2 + _barHight / 2;
            color = 170;
            BindingLine.Add(new LineData
            {
                X1 = x1,
                Y1 = y1,
                X2 = x1 + 135,
                Y2 = y1 + 60,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 2)
            });
            BindingLine.Add(new LineData
            {
                X1 = x1 + 135,
                Y1 = y1 + 60,
                X2 = x1 + 135,
                Y2 = y2,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 2)
            });
            BindingLine.Add(new LineData
            {
                X1 = x1 + 135,
                Y1 = y2,
                X2 = x2,
                Y2 = y2,
                Pen = new Pen(Color.FromArgb(255, color, color, color), 2)
            });
            return BindingLine;
        }

        public List<FillRectData> BarsBackground { get; private set; }
        public List<RectData> BarsContur { get; private set; }
        public List<FillRectData> BarsContent { get; private set; }
        public List<TextData> BarsValue { get; private set; }
        public List<RuleData> RuleDatas { get; private set; }
        public List<RuleInscription> RuleInscriptions { get; private set; }
        public List<TextData> BarsName { get; private set; }
        public List<RectData> BarsFrame { get; private set; }
        public List<LineData> BindingLine { get; private set; }
        private int panelWidth;
        private int panelHeight;
        private MineConfig _mineConfig;
        private Parameters _parameters;
        private double _greenZoneRabCyl1 = 0; // допустимые границы рабочего цилиндра
        private double _greenZoneRabCyl2 = 0;
        private double _greenZonePredCyl1 = 0; // допустимые границы предохранительного цилиндра
        private double _greenZonePredCyl2 = 0;
        private double _rabCyl1Pressure = 0;
        private double _rabCyl2Pressure = 0;
        private double _predCyl1Pressure = 0;
        private double _predCyl2Pressure = 0;
        private int _barWidth;
        private int _barHight;
        private int _indent;
        private double _maxPressure;
        //
        private int _x;
        private int _y;
        private int _width;
        private int _height;
        private int _color;
        //

    }
}
