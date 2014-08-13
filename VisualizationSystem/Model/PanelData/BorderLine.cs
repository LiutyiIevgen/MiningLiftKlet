using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VisualizationSystem.Model.PanelData
{
    public class BorderLine
    {
        public Pen Pen { get; set; }
        public int LeftTopX { get; set; }
        public int LeftTopY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
