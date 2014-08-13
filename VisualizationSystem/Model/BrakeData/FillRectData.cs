using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizationSystem.Model.BrakeData
{
    public class FillRectData
    {
        public SolidBrush Brush { get; set; }
        public int LeftTopX { get; set; }
        public int LeftTopY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
