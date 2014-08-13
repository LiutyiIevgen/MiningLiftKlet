using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VisualizationSystem.Model.RichTextBoxData
{
    public class RichTextBoxData
    {
        public RichTextBoxData()
        {
            BackColor = Color.Gray;
            Text = "";
        }
        public Color BackColor { get; set; }
        public string Text { get; set; }
    }
}
