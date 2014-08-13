using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ML.DataExchange.Model;
using VisualizationSystem.ViewModel.MainViewModel;

namespace VisualizationSystem.View.UserControls.GeneralView
{
    public partial class BrakeUC : UserControl
    {
        public BrakeUC()
        {
            InitializeComponent();
            DoubleBuffered(panel1, true);
            //view models creation
            _brakeMainPictureVm = new BrakeMainPictureVm(panel1.Width, panel1.Height);
            _brakeBarsPictureVm = new BrakeBarsPictureVm(panel1.Width, panel1.Height);

            _barsFont = new Font("Microsoft Sans Serif", 16, FontStyle.Bold);
            _barsNameFont = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
        }

        private void BrakeUC_Load(object sender, EventArgs e)
        {
            DrawMainPicture();
        }

        private void DrawMainPicture()
        {
            btBack = new Bitmap(panel1.Width, panel1.Height);
            Graphics g = Graphics.FromImage(btBack);
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            _brakeMainPictureVm.InitVm();
            _brakeMainPictureVm.GetMainCylinderDatas().ForEach(c => g.FillRectangle(c.Brush, c.LeftTopX, c.LeftTopY, c.Width, c.Height));
            _brakeMainPictureVm.GetMainCylinderTrianglesDatas().ForEach(t => g.FillPolygon(t.Brush, t.Triangle));
            _brakeMainPictureVm.GetMainCylinderConturDatas().ForEach(c => g.DrawRectangle(c.Pen, c.LeftTopX, c.LeftTopY, c.Width, c.Height));
            _brakeMainPictureVm.GetSideCylinderBackgroundDatas().ForEach(c => g.FillRectangle(c.Brush, c.LeftTopX, c.LeftTopY, c.Width, c.Height));
            _brakeMainPictureVm.GetSideCylinderConturBackgroundDatas().ForEach(c => g.DrawRectangle(c.Pen, c.LeftTopX, c.LeftTopY, c.Width, c.Height));
            _brakeMainPictureVm.GetSideCylinderDatas().ForEach(c => g.FillRectangle(c.Brush, c.LeftTopX, c.LeftTopY, c.Width, c.Height));
            _brakeMainPictureVm.GetSideCylinderConturDatas().ForEach(c => g.DrawRectangle(c.Pen, c.LeftTopX, c.LeftTopY, c.Width, c.Height));
            _brakeMainPictureVm.GetSmallCylinderCirkleBackgroundDatas().ForEach(c => g.FillEllipse(c.Brush, c.LeftTopX, c.LeftTopY, c.Width, c.Height));
            _brakeMainPictureVm.GetSmallCylinderConturCirkleBackgroundDatas().ForEach(c => g.DrawEllipse(c.Pen, c.LeftTopX, c.LeftTopY, c.Width, c.Height));
            _brakeMainPictureVm.GetSmallCylinderConturBackgroundDatas().ForEach(c => DrawRoundRect(g, c.Pen, c.LeftTopX, c.LeftTopY, c.Width, c.Height, 3));
            _brakeMainPictureVm.GetSmallCylinderContur2BackgroundDatas().ForEach(c => g.DrawRectangle(c.Pen, c.LeftTopX, c.LeftTopY, c.Width, c.Height));
            _brakeMainPictureVm.GetSmallCylinderDatas().ForEach(c => g.FillRectangle(c.Brush, c.LeftTopX, c.LeftTopY, c.Width, c.Height));
            _brakeMainPictureVm.GetSmallCylinderPipeDatas().ForEach(c => g.FillRectangle(c.Brush, c.LeftTopX, c.LeftTopY, c.Width, c.Height));
            _brakeMainPictureVm.GetSmallCylinderRectBackgroundDatas().ForEach(c => g.FillRectangle(c.Brush, c.LeftTopX, c.LeftTopY, c.Width, c.Height));
            _brakeMainPictureVm.GetSmallCylinderConturLineBackgroundDatas().ForEach(c => g.DrawLine(c.Pen, c.X1, c.Y1, c.X2, c.Y2));
            g.Dispose();
            //panel1.Invalidate();
            panel1.BackgroundImage = btBack;
        }

        public void DrawBars(Parameters parameters)
        {
            btForward = new Bitmap(panel1.Width, panel1.Height);
            Graphics g = Graphics.FromImage(btForward);
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            _brakeBarsPictureVm.InitVm(parameters);
            _brakeBarsPictureVm.GetBarsBackgroundDatas().ForEach(b => g.FillRectangle(b.Brush, b.LeftTopX, b.LeftTopY, b.Width, b.Height));
            _brakeBarsPictureVm.GetBarsConturDatas().ForEach(c => g.DrawRectangle(c.Pen, c.LeftTopX, c.LeftTopY, c.Width, c.Height));
            _brakeBarsPictureVm.GetBarsContentDatas().ForEach(c => g.FillRectangle(c.Brush, c.LeftTopX, c.LeftTopY, c.Width, c.Height));
            _brakeBarsPictureVm.GetBarsValueDatas().ForEach(v => g.DrawString(v.Text, _barsFont, Brushes.WhiteSmoke, v.LeftTopX, v.LeftTopY));
            _brakeBarsPictureVm.GetRuleDatas().ForEach(r => g.DrawLine(r.Pen, r.FirstPoint, r.SecondPoint));
            _brakeBarsPictureVm.GetRuleInscriptions().ForEach(r => g.DrawString(r.Text, r.Font, r.Brush, r.Position));
            _brakeBarsPictureVm.GetBarsFrameDatas().ForEach(f => DrawRoundRect(g, f.Pen, f.LeftTopX, f.LeftTopY, f.Width, f.Height, 8));
            _brakeBarsPictureVm.GetBarsNameDatas().ForEach(n => g.DrawString(n.Text, _barsNameFont, Brushes.WhiteSmoke, n.LeftTopX, n.LeftTopY));
            _brakeBarsPictureVm.GetBindingLineDatas().ForEach(l => g.DrawLine(l.Pen, l.X1, l.Y1, l.X2, l.Y2));
            g.Dispose();
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (btForward != null)
                    e.Graphics.DrawImage(btForward, 0, 0);
            }
            catch (Exception)
            {

            }    
        }

        private void DrawRoundRect(Graphics g, Pen p, float x, float y, float width, float height, float radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(x + radius, y, x + width - (radius * 2), y); // Line
            gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90); // Corner
            gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2)); // Line
            gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90); // Corner
            gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height); // Line
            gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90); // Corner
            gp.AddLine(x, y + height - (radius * 2), x, y + radius); // Line
            gp.AddArc(x, y, radius * 2, radius * 2, 180, 90); // Corner
            gp.CloseFigure();
            g.DrawPath(p, gp);
            gp.Dispose();
        }
        private void DoubleBuffered(Panel dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }

        private Bitmap btBack;
        private Bitmap btForward;
        private BrakeMainPictureVm _brakeMainPictureVm;
        private BrakeBarsPictureVm _brakeBarsPictureVm;
        private Font _barsFont;
        private Font _barsNameFont;
    }
}
