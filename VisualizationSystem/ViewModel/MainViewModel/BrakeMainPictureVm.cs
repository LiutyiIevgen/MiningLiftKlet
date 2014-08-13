using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualizationSystem.Model.BrakeData;

namespace VisualizationSystem.ViewModel.MainViewModel
{
    class BrakeMainPictureVm
    {
        public BrakeMainPictureVm(int panelWidth, int panelHeight)
        {
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;

            MainCylinder = new List<FillRectData>();
            MainCylinderContur = new List<RectData>();
            MainCylinderTriangles = new List<FillTriangleData>();
            SideCylinder = new List<FillRectData>();
            SideCylinderContur = new List<RectData>();
            SideCylinderBackground = new List<FillRectData>();
            SideCylinderConturBackground = new List<RectData>();
            SmallCylinder = new List<FillRectData>();
            SmallCylinderConturBackground = new List<RectData>();
            SmallCylinderContur2Background = new List<RectData>();
            SmallCylinderPipe = new List<FillRectData>();
            SmallCylinderCirkleBackground = new List<FillCircleData>();
            SmallCylinderConturCirkleBackground = new List<CircleData>();
            SmallCylinderRectBackground = new List<FillRectData>();
            SmallCylinderConturLineBackground = new List<LineData>();

            _x = panelWidth / 6;
            _y = panelHeight / 10;
            _width = panelWidth / 5;
            _height = (panelHeight / 6) * 2;
            _color = 0;
            _kol = (_height / 2) / 5;
            _yinc = ((_height / 2) / _kol);
            _hcolor = (200 * 200) / _kol;
            _y += _height/4 + 5;
        }

        public void InitVm()
        {
            MainCylinder.Clear();
            MainCylinderContur.Clear();
            MainCylinderTriangles.Clear();
            SideCylinder.Clear();
            SideCylinderContur.Clear();
            SideCylinderBackground.Clear();
            SideCylinderConturBackground.Clear();
            SmallCylinder.Clear();
            SmallCylinderConturBackground.Clear();
            SmallCylinderContur2Background.Clear();
            SmallCylinderPipe.Clear();
            SmallCylinderCirkleBackground.Clear();
            SmallCylinderConturCirkleBackground.Clear();
            SmallCylinderRectBackground.Clear();
            SmallCylinderConturLineBackground.Clear();
        }

        public List<FillRectData> GetMainCylinderDatas()
        {
            //центральный
            int x = _x;
            int y = _y;
            int width = _width;
            int height = _height;
            int color = _color;
            int kol = _kol;
            int yinc = _yinc;
            int hcolor = _hcolor;
            for (int i = 0; i < kol; i++)
            {
                MainCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color+30, color+30, color+30))
                });
                y += yinc;
                height -= yinc*2;
                color = Convert.ToInt32(Math.Sqrt((i+1)*hcolor));
            }
            //боковые части 1
            x = _x - _width / 5;
            y = _y + _height / 10;
            width = _width / 5;
            height = _height - 2 * (_height / 10);
            color = 0;
            kol = (height / 2) / 5;
            yinc = ((height / 2) / kol);
            hcolor = (200 * 200) / kol;
            MainCylinderContur.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color + 30, color + 30, color + 30))
            });
            for (int i = 0; i < kol; i++)
            {
                MainCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 30, color + 30, color + 30))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            x = _x + _width;
            y = _y + _height / 10;
            width = _width / 5;
            height = _height - 2 * (_height / 10);
            color = 0;
            kol = (height / 2) / 5;
            yinc = ((height / 2) / kol);
            hcolor = (200 * 200) / kol;
            MainCylinderContur.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color + 30, color + 30, color + 30))
            });
            for (int i = 0; i < kol; i++)
            {
                MainCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 30, color + 30, color + 30))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            //2
            x = _x - _width / 5 - _width / 10;
            y = _y + _height/2 - _height / 10;
            width = _width / 10;
            height = 2 * (_height / 10);
            color = 0;
            kol = (height / 2) / 2;
            yinc = ((height / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                MainCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 30, color + 30, color + 30))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            x = _x + _width + _width / 5 +1;
            y = _y + _height / 2 - _height / 10;
            width = _width / 10;
            height = 2 * (_height / 10);
            color = 0;
            kol = (height / 2) / 2;
            yinc = ((height / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                MainCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 30, color + 30, color + 30))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            //3
            x = _x - _width / 5 - _width / 10 - _width / 3;
            y = _y + _height / 2 - (_height / 10)*2;
            width = _width / 3;
            height = 4 * (_height / 10);
            color = 30;
            kol = (height / 2) / 3;
            yinc = ((height / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                MainCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 30, color + 30, color + 30))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            x = _x + _width + _width / 5 + 1 + _width / 10;
            y = _y + _height / 2 - (_height / 10)*2;
            width = _width / 3;
            height = 4 * (_height / 10);
            color = 30;
            kol = (height / 2) / 3;
            yinc = ((height / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                MainCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 30, color + 30, color + 30))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            //triangles
            color = _color;
            MainCylinderTriangles.Add(new FillTriangleData
            {
                Brush = new SolidBrush(Color.FromArgb(255, color + 60, color + 60, color + 60)),
                Triangle = new Point[3]
                { 
                    new Point(_x - _width / 5 - _width / 10 - _width / 3, _y + _height / 2 - (_height / 10)*2),
                    new Point(_x - _width / 5 - _width / 10, _y + _height / 2 - (_height / 10)*2),
                    new Point(_x - _width / 5 - _width / 10 - _width / 6, _y + _height / 2 - (_height / 10)*2 - (_height / 25))
                }
            });
            MainCylinderTriangles.Add(new FillTriangleData
            {
                Brush = new SolidBrush(Color.FromArgb(255, color + 60, color + 60, color + 60)),
                Triangle = new Point[3]
                { 
                    new Point(_x - _width / 5 - _width / 10 - _width / 3, _y + _height / 2 - (_height / 10)*2 + 4 * (_height / 10)),
                    new Point(_x - _width / 5 - _width / 10, _y + _height / 2 - (_height / 10)*2 + 4 * (_height / 10)),
                    new Point(_x - _width / 5 - _width / 10 - _width / 6, _y + _height / 2 - (_height / 10)*2 + 4 * (_height / 10) + (_height / 25))
                }
            });
            MainCylinderTriangles.Add(new FillTriangleData
            {
                Brush = new SolidBrush(Color.FromArgb(255, color + 60, color + 60, color + 60)),
                Triangle = new Point[3]
                { 
                    new Point(_x + _width + _width / 5 + 1 + _width / 10, _y + _height / 2 - (_height / 10)*2),
                    new Point(_x + _width + _width / 5 + 1 + _width / 10 + _width / 3, _y + _height / 2 - (_height / 10)*2),
                    new Point(_x + _width + _width / 5 + 1 + _width / 10 + _width / 6, _y + _height / 2 - (_height / 10)*2 - (_height / 25))
                }
            });
            MainCylinderTriangles.Add(new FillTriangleData
            {
                Brush = new SolidBrush(Color.FromArgb(255, color + 60, color + 60, color + 60)),
                Triangle = new Point[3]
                { 
                    new Point(_x + _width + _width / 5 + 1 + _width / 10, _y + _height / 2 - (_height / 10)*2 + 4 * (_height / 10)),
                    new Point(_x + _width + _width / 5 + 1 + _width / 10 + _width / 3, _y + _height / 2 - (_height / 10)*2 + 4 * (_height / 10)),
                    new Point(_x + _width + _width / 5 + 1 + _width / 10 + _width / 6, _y + _height / 2 - (_height / 10)*2 + 4 * (_height / 10) + (_height / 25))
                }
            });
            //contur
            MainCylinderContur.Add(new RectData
            {
                LeftTopX = _x - _width / 5 - _width / 10 - _width / 6 - _width / 30 - 1,
                LeftTopY = _y + _height / 2 - (_height / 10) * 2 - (_height / 25),
                Height = 4 * (_height / 10) + 2 * (_height / 25),
                Width = _width / 15,
                Pen = new Pen(Color.FromArgb(255, color + 30, color + 30, color + 30))
            });
            x = _x - _width / 5 - _width / 10 - _width / 6 - _width / 30 - 1;
            y = _y + _height / 2 - (_height / 10) * 2 - (_height / 25);
            width = _width / 15;
            height = 4 * (_height / 10) + 2 * (_height / 25);
            color = 30;
            kol = (height / 2) / 3;
            yinc = ((height / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                MainCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 30, color + 30, color + 30))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            color = _color;
            MainCylinderContur.Add(new RectData
            {
                LeftTopX = _x + _width + _width / 5 + 1 + _width / 10 + _width / 6 - _width / 30,
                LeftTopY = _y + _height / 2 - (_height / 10) * 2 - (_height / 25),
                Height = 4 * (_height / 10) + 2 * (_height / 25),
                Width = _width / 15,
                Pen = new Pen(Color.FromArgb(255, color + 30, color + 30, color + 30))
            });
            x = _x + _width + _width / 5 + 1 + _width / 10 + _width / 6 - _width / 30;
            y = _y + _height / 2 - (_height / 10) * 2 - (_height / 25);
            width = _width / 15;
            height = 4 * (_height / 10) + 2 * (_height / 25);
            color = 30;
            kol = (height / 2) / 3;
            yinc = ((height / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                MainCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 30, color + 30, color + 30))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            return MainCylinder;
        }

        public List<RectData> GetMainCylinderConturDatas()
        {
            return MainCylinderContur;
        }

        public List<FillTriangleData> GetMainCylinderTrianglesDatas()
        {
            return MainCylinderTriangles;
        }

        public List<FillRectData> GetSideCylinderBackgroundDatas()
        {
            //left rects
            int x = _x - _width / 5 + 2;
            int y = _y + _height / 10 - 10;
            int width = _width / 5 - 4;
            int height = (_height - 2 * (_height / 10))/3 + 20;
            int color = 180;
            SideCylinderBackground.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            color = 30;
            SideCylinderConturBackground.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x = _x - _width / 5 + 2;
            y = _y + _height / 10 + ((_height - 2 * (_height / 10))/3)*2 - 10;
            width = _width / 5 - 4;
            height = (_height - 2 * (_height / 10)) / 3 + 20;
            color = 180;
            SideCylinderBackground.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            color = 30;
            SideCylinderConturBackground.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x = _x - _width / 5 + 5;
            y = _y + _height / 10 - 10 + (_height - 2 * (_height / 10)) / 3 + 21;
            _middleSideRectY = _y + _height / 10 - 10 + (_height - 2 * (_height / 10)) / 3 + 21;
            width = _width / 5 - 10;
            height = (_height - 2 * (_height / 10)) / 3 + 15;
            color = 240;
            SideCylinder.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            color = 30;
            SideCylinderContur.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x = _x - _width / 5 + 7;
            y = _y + _height / 10 - 10 + (_height - 2 * (_height / 10)) / 3 + 23;
            width = _width / 5 - 14;
            height = (_height - 2 * (_height / 10)) / 3 + 11;
            color = 180;
            SideCylinder.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            color = 30;
            SideCylinderContur.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            //right rects
            x = _x + _width + 2;
            y = _y + _height / 10 - 10;
            width = _width / 5 - 4;
            height = (_height - 2 * (_height / 10)) / 3 + 20;
            color = 180;
            SideCylinderBackground.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            color = 30;
            SideCylinderConturBackground.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x = _x + _width + 2;
            y = _y + _height / 10 + ((_height - 2 * (_height / 10)) / 3) * 2 - 10;
            width = _width / 5 - 4;
            height = (_height - 2 * (_height / 10)) / 3 + 20;
            color = 180;
            SideCylinderBackground.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            color = 30;
            SideCylinderConturBackground.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x = _x + _width + 5;
            y = _y + _height / 10 - 10 + (_height - 2 * (_height / 10)) / 3 + 21;
            width = _width / 5 - 10;
            height = (_height - 2 * (_height / 10)) / 3 + 15;
            color = 240;
            SideCylinder.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            color = 30;
            SideCylinderContur.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x = _x + _width + 7;
            y = _y + _height / 10 - 10 + (_height - 2 * (_height / 10)) / 3 + 23;
            width = _width / 5 - 14;
            height = (_height - 2 * (_height / 10)) / 3 + 11;
            color = 180;
            SideCylinder.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            color = 30;
            SideCylinderContur.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            return SideCylinderBackground;
        }

        public List<RectData> GetSideCylinderConturBackgroundDatas()
        {
            return SideCylinderConturBackground;
        }

        public List<RectData> GetSideCylinderConturDatas()
        {
            return SideCylinderContur;
        }

        public List<FillRectData> GetSideCylinderDatas()
        {
            return SideCylinder;
        }

        public List<RectData> GetSmallCylinderConturBackgroundDatas()
        {
            //left
            int x = _x - _width / 5 + 4;
            int y = _y + _height / 10 + 5;
            int width = _width / 5 - 8;
            int height = (_height - 2 * (_height / 10)) / 3/2;
            int color = 30;
            SmallCylinderConturBackground.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x = _x - _width / 5 + 4 + (_width / 5 - 8) / 2 - (_width / 5 - 8)/4;
            y = _y + _height / 10 + 5 + (_height - 2 * (_height / 10)) / 3 / 4;
            width = (_width / 5 - 12)/2 + 2;
            height = (_height - 2 * (_height / 10)) / 3 / 4;
            color = 30;
            SmallCylinderContur2Background.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x = _x - _width / 5 + 6;
            y = _y + _height - _height / 10 - (_height - 2 * (_height / 10)) / 3 / 3;
            width = _width / 5 - 12;
            height = (_height - 2 * (_height / 10)) / 3 /3 + 4;
            _Y1 = y + height;
            color = 30;
            SmallCylinderConturBackground.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x = _x - _width / 5 + 6;
            y = _y + _height + _height/10;
            _Y2 = y;
            width = _width / 5 - 12;
            //height = (_height - 2 * (_height / 10)) / 3 / 3 + 4;
            _Y3 = y + height;
            color = 30;
            SmallCylinderConturBackground.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x = _x - _width / 5 + 6;
            y = _y + _height + height + 3*(_height / 10);
            _Y4 = y;
            width = _width / 5 - 12;
            //height = (_height - 2 * (_height / 10)) / 3 / 3 + 4;
            color = 30;
            SmallCylinderConturBackground.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            //right
            x = _x + _width + 4;
            y = _y + _height / 10 + 5;
            width = _width / 5 - 8;
            height = (_height - 2 * (_height / 10)) / 3 / 2;
            color = 30;
            SmallCylinderConturBackground.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x = _x + _width + 4 + (_width / 5 - 8) / 2 - (_width / 5 - 8) / 4;
            y = _y + _height / 10 + 5 + (_height - 2 * (_height / 10)) / 3 / 4;
            width = (_width / 5 - 12) / 2 + 2;
            height = (_height - 2 * (_height / 10)) / 3 / 4;
            color = 30;
            SmallCylinderContur2Background.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x = _x + _width + 6;
            y = _y + _height - _height / 10 - (_height - 2 * (_height / 10)) / 3 / 3;
            width = _width / 5 - 12;
            height = (_height - 2 * (_height / 10)) / 3 / 3 + 4;
            _Y1 = y + height;
            color = 30;
            SmallCylinderConturBackground.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x = _x + _width + 6;
            y = _y + _height + _height / 10;
            _Y2 = y;
            width = _width / 5 - 12;
            //height = (_height - 2 * (_height / 10)) / 3 / 3 + 4;
            _Y3 = y + height;
            color = 30;
            SmallCylinderConturBackground.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x = _x + _width + 6;
            y = _y + _height + height + 3 * (_height / 10);
            _Y4 = y;
            width = _width / 5 - 12;
            //height = (_height - 2 * (_height / 10)) / 3 / 3 + 4;
            color = 30;
            SmallCylinderConturBackground.Add(new RectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            return SmallCylinderConturBackground;
        }

        public List<RectData> GetSmallCylinderContur2BackgroundDatas()
        {
            return SmallCylinderContur2Background;
        }
        public List<FillRectData> GetSmallCylinderDatas()
        {
            //left
            int x = _x - _width / 5 + 6 + (_width / 5 - 8) / 2 - (_width / 5 - 8) / 4;
            int y = _y + _height / 10 + 5 + (_height - 2 * (_height / 10)) / 3 / 4;
            int width = (_width / 5 - 14) / 2;
            int height = (_height - 2 * (_height / 10)) / 3 / 4;
            int color = 0;
            int kol = (height / 2) / 1;
            int yinc = ((height / 2) / kol);
            int hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                SmallCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 20, color + 20, color + 20))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            x = _x - _width / 5 + 10;
            y = _y + _height - _height / 10 - (_height - 2 * (_height / 10)) / 3 / 3 + 4;
            width = _width / 5 - 19;
            height = (_height - 2 * (_height / 10)) / 3 / 3 - 3;
            color = 0;
            kol = (height / 2) / 1;
            yinc = ((height / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                SmallCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 20, color + 20, color + 20))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            x = _x - _width / 5 + 10;
            y = _y + _height + _height / 10 + 4;
            width = _width / 5 - 19;
            height = (_height - 2 * (_height / 10)) / 3 / 3 - 3;
            color = 0;
            kol = (height / 2) / 1;
            yinc = ((height / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                SmallCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 20, color + 20, color + 20))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            x = _x - _width / 5 + 10;
            y = _y + _height + (_height - 2 * (_height / 10)) / 3 / 3 + 4 + 3 * (_height / 10) + 4;
            width = _width / 5 - 19;
            height = (_height - 2 * (_height / 10)) / 3 / 3 - 3;
            color = 0;
            kol = (height / 2) / 1;
            yinc = ((height / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                SmallCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 20, color + 20, color + 20))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            //right
            x = _x + _width + 6 + (_width / 5 - 8) / 2 - (_width / 5 - 8) / 4;
            y = _y + _height / 10 + 5 + (_height - 2 * (_height / 10)) / 3 / 4;
            width = (_width / 5 - 14) / 2;
            height = (_height - 2 * (_height / 10)) / 3 / 4;
            color = 0;
            kol = (height / 2) / 1;
            yinc = ((height / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                SmallCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 20, color + 20, color + 20))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            x = _x + _width + 10;
            y = _y + _height - _height / 10 - (_height - 2 * (_height / 10)) / 3 / 3 + 4;
            width = _width / 5 - 19;
            height = (_height - 2 * (_height / 10)) / 3 / 3 - 3;
            color = 0;
            kol = (height / 2) / 1;
            yinc = ((height / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                SmallCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 20, color + 20, color + 20))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            x = _x + _width + 10;
            y = _y + _height + _height / 10 + 4;
            width = _width / 5 - 19;
            height = (_height - 2 * (_height / 10)) / 3 / 3 - 3;
            color = 0;
            kol = (height / 2) / 1;
            yinc = ((height / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                SmallCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 20, color + 20, color + 20))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            x = _x + _width + 10;
            y = _y + _height + (_height - 2 * (_height / 10)) / 3 / 3 + 4 + 3 * (_height / 10) + 4;
            width = _width / 5 - 19;
            height = (_height - 2 * (_height / 10)) / 3 / 3 - 3;
            color = 0;
            kol = (height / 2) / 1;
            yinc = ((height / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                SmallCylinder.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 20, color + 20, color + 20))
                });
                y += yinc;
                height -= yinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            return SmallCylinder;
        }

        public List<FillRectData> GetSmallCylinderPipeDatas()
        {
            //left
            int x = _x - _width / 5 + 6 + (_width / 5 - 8) / 2 - (_width / 5 - 8) / 4;
            int y = _y + _height / 10 + 5 + (_height - 2 * (_height / 10)) / 3 / 2;
            int width = (_width / 5 - 14) / 2;
            int height = _middleSideRectY - y + 1;
            int color = 30;
            int kol = (width / 2) / 1;
            int xinc = ((width / 2) / kol);
            int hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                SmallCylinderPipe.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 30, color + 30, color + 30))
                });
                x += xinc;
                width -= xinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            x = _x - _width / 5 + 6 + (_width / 5 - 8) / 2 - (_width / 5 - 8) / 4;
            y = _middleSideRectY + 3;
            width = (_width / 5 - 14) / 2;
            height = _y + _height - _height / 10 - (_height - 2 * (_height / 10)) / 3 / 3 - y;
            color = 30;
            kol = (width / 2) / 1;
            xinc = ((width / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                SmallCylinderPipe.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 30, color + 30, color + 30))
                });
                x += xinc;
                width -= xinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            //right
            x = _x + _width + 6 + (_width / 5 - 8) / 2 - (_width / 5 - 8) / 4;
            y = _y + _height / 10 + 5 + (_height - 2 * (_height / 10)) / 3 / 2;
            width = (_width / 5 - 14) / 2;
            height = _middleSideRectY - y + 1;
            color = 30;
            kol = (width / 2) / 1;
            xinc = ((width / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                SmallCylinderPipe.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 30, color + 30, color + 30))
                });
                x += xinc;
                width -= xinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            x = _x + _width + 6 + (_width / 5 - 8) / 2 - (_width / 5 - 8) / 4;
            y = _middleSideRectY + 3;
            width = (_width / 5 - 14) / 2;
            height = _y + _height - _height / 10 - (_height - 2 * (_height / 10)) / 3 / 3 - y;
            color = 30;
            kol = (width / 2) / 1;
            xinc = ((width / 2) / kol);
            hcolor = (200 * 200) / kol;
            for (int i = 0; i < kol; i++)
            {
                SmallCylinderPipe.Add(new FillRectData
                {
                    LeftTopX = x,
                    LeftTopY = y,
                    Height = height,
                    Width = width,
                    Brush = new SolidBrush(Color.FromArgb(255, color + 30, color + 30, color + 30))
                });
                x += xinc;
                width -= xinc * 2;
                color = Convert.ToInt32(Math.Sqrt((i + 1) * hcolor));
            }
            return SmallCylinderPipe;
        }

        public List<FillCircleData> GetSmallCylinderCirkleBackgroundDatas()
        {
            //left
            int x = _x - _width / 5 - 11;
            int y = _y + _height + _height / 10 - 14;
            int width = _width / 5 + 22;
            int height = width;
            int color = 30;
            SmallCylinderConturCirkleBackground.Add(new CircleData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            color = 180;
            SmallCylinderCirkleBackground.Add(new FillCircleData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            x = _x - _width / 5 - 5;
            y = _y + _height + (_height - 2 * (_height / 10)) / 3 / 3 + 4 + 3 * (_height / 10) - 8;
            width = _width / 5 + 10;
            height = width;
            color = 30;
            SmallCylinderConturCirkleBackground.Add(new CircleData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            color = 180;
            SmallCylinderCirkleBackground.Add(new FillCircleData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            //right
            x = _x + _width - 11;
            y = _y + _height + _height / 10 - 14;
            width = _width / 5 + 22;
            height = width;
            color = 30;
            SmallCylinderConturCirkleBackground.Add(new CircleData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            color = 180;
            SmallCylinderCirkleBackground.Add(new FillCircleData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            x = _x + _width - 5;
            y = _y + _height + (_height - 2 * (_height / 10)) / 3 / 3 + 4 + 3 * (_height / 10) - 8;
            width = _width / 5 + 10;
            height = width;
            color = 30;
            SmallCylinderConturCirkleBackground.Add(new CircleData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            color = 180;
            SmallCylinderCirkleBackground.Add(new FillCircleData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            return SmallCylinderCirkleBackground;
        }

        public List<CircleData> GetSmallCylinderConturCirkleBackgroundDatas()
        {
            return SmallCylinderConturCirkleBackground;
        }

        public List<FillRectData> GetSmallCylinderRectBackgroundDatas()
        {
            //left
            int x = _x - _width / 5 + 10;
            int y = _Y1;
            int width = _width / 5 - 19;
            int height = _Y2 - _Y1;
            int color = 180;
            SmallCylinderRectBackground.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            x = _x - _width / 5 + 10;
            y = _Y3;
            width = _width / 5 - 19;
            height = _Y4 - _Y3 + 1;
            color = 180;
            SmallCylinderRectBackground.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            //right
            x = _x + _width + 10;
            y = _Y1;
            width = _width / 5 - 19;
            height = _Y2 - _Y1;
            color = 180;
            SmallCylinderRectBackground.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            x = _x + _width + 10;
            y = _Y3;
            width = _width / 5 - 19;
            height = _Y4 - _Y3 + 1;
            color = 180;
            SmallCylinderRectBackground.Add(new FillRectData
            {
                LeftTopX = x,
                LeftTopY = y,
                Height = height,
                Width = width,
                Brush = new SolidBrush(Color.FromArgb(255, color, color, color))
            });
            return SmallCylinderRectBackground;
        }

        public List<LineData> GetSmallCylinderConturLineBackgroundDatas()
        {
            //left
            int x1 = _x - _width / 5 + 10;
            int y1 = _Y1;
            int x2 = x1;
            int y2 = _Y2;
            int color = 0;
            SmallCylinderConturLineBackground.Add(new LineData
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x1 = _x - 9;
            y1 = _Y1;
            x2 = x1;
            y2 = _Y2;
            color = 0;
            SmallCylinderConturLineBackground.Add(new LineData
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x1 = _x - _width / 5 + 10;
            y1 = _Y3;
            x2 = x1;
            y2 = _Y4;
            color = 0;
            SmallCylinderConturLineBackground.Add(new LineData
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x1 = _x - 9;
            y1 = _Y3;
            x2 = x1;
            y2 = _Y4;
            color = 0;
            SmallCylinderConturLineBackground.Add(new LineData
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            //right
            x1 = _x + _width + 10;
            y1 = _Y1;
            x2 = x1;
            y2 = _Y2;
            color = 0;
            SmallCylinderConturLineBackground.Add(new LineData
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x1 = _x + _width + _width / 5 - 9;
            y1 = _Y1;
            x2 = x1;
            y2 = _Y2;
            color = 0;
            SmallCylinderConturLineBackground.Add(new LineData
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x1 = _x + _width + 10;
            y1 = _Y3;
            x2 = x1;
            y2 = _Y4;
            color = 0;
            SmallCylinderConturLineBackground.Add(new LineData
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            x1 = _x + _width + _width / 5 - 9;
            y1 = _Y3;
            x2 = x1;
            y2 = _Y4;
            color = 0;
            SmallCylinderConturLineBackground.Add(new LineData
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Pen = new Pen(Color.FromArgb(255, color, color, color))
            });
            return SmallCylinderConturLineBackground;
        }


        public List<FillRectData> MainCylinder{ get; private set; }
        public List<RectData> MainCylinderContur { get; private set; }
        public List<FillTriangleData> MainCylinderTriangles { get; private set; }
        public List<FillRectData> SideCylinderBackground { get; private set; }
        public List<RectData> SideCylinderConturBackground { get; private set; }
        public List<FillRectData> SideCylinder { get; private set; }
        public List<RectData> SideCylinderContur { get; private set; }
        public List<RectData> SmallCylinderConturBackground { get; private set; }
        public List<RectData> SmallCylinderContur2Background { get; private set; }
        public List<FillRectData> SmallCylinder { get; private set; }
        public List<FillRectData> SmallCylinderPipe { get; private set; }
        public List<CircleData> SmallCylinderConturCirkleBackground { get; private set; }
        public List<FillCircleData> SmallCylinderCirkleBackground { get; private set; }
        public List<LineData> SmallCylinderConturLineBackground { get; private set; }
        public List<FillRectData> SmallCylinderRectBackground { get; private set; }
        private int panelWidth;
        private int panelHeight;
        private int _middleSideRectY;
        private int _Y1;
        private int _Y2;
        private int _Y3;
        private int _Y4;
        //
        private int _x;
        private int _y;
        private int _width;
        private int _height;
        private int _color;
        private int _kol;
        private int _yinc;
        private int _hcolor;
        //
    }
}
