using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizationSystem.Model.BrakeData
{
    public class RuleInscription
    {
        public string Text { get; set; }
        public Font Font { get; set; }
        public Brush Brush { get; set; }
        public Point Position { get; set; }
    }
}
