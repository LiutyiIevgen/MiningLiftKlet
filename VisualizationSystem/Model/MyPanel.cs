using System.Windows.Forms;

namespace VisualizationSystem.Model
{
    class MyPanel:Panel
    {
        public MyPanel()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
    }
}
