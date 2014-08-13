using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualizationSystem.Model;
using VisualizationSystem.Services;
using VisualizationSystem.ViewModel;

namespace VisualizationSystem.View.UserControls.GeneralView
{
    public partial class LogUC : UserControl
    {
        public LogUC()
        {
            InitializeComponent();
            DoubleBuffered(GeneralLog, true);
            _progStart = 1;
        }

        private void LogUC_Load(object sender, EventArgs e)
        {
            //ClearLog();
        }

        public void DoubleBuffered(RichTextBox rtb, bool setting)
        {
            Type dgvType = rtb.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(rtb, setting, null);
        }

        private void AddLineToLog(string text)
        {
            this.Invoke((MethodInvoker)delegate
            {
            GeneralLog.Text += text + "\n";
            GeneralLog.SelectionStart = GeneralLog.Text.Length; //Set the current caret position at the end
            GeneralLog.ScrollToCaret(); //Now scroll it automatically
            });
        }

        private void ClearLog()
        {
            this.Invoke((MethodInvoker)delegate
            {
            GeneralLog.Clear();
            });
        }

        public GeneralLogData Refresh()
        {
            //var refreshThread = new Thread(RefreshThreadHandler) { IsBackground = true, Priority = ThreadPriority.Lowest };
            //refreshThread.Start();
            RefreshThreadHandler();
            return _logData;
        }

        private void RefreshThreadHandler()
        {
            var dataBaseService = new DataBaseService();
            DateTime till = DateTime.Now;
            DateTime from = DateTime.Now.AddDays(-1);
            var ids = dataBaseService.GetGeneralLogIds(from, till);
            if (ids.Count == 0 || ids.Last() <= _lastId)
            {
                //_fNewLogEvent = Color.Gray;
                _logData.TypeColor = Color.Gray;
                _logData.Text = "";
                _logData.ShortText = "";
                return;
            }
            var newIds = new List<int>();
            foreach (var id in ids)
            {
                if (id > _lastId)
                {
                    var logData = dataBaseService.GetGeneralLogLineById(id);
                    AddLineToLog(logData.Text);
                    //_fNewLogEvent = logData.TypeColor;
                        _logData.TypeColor = logData.TypeColor;
                        _logData.Text = logData.Text;
                        _logData.ShortText = logData.ShortText;
                }
            }
            if (_progStart == 1)
            {
                _logData.TypeColor = Color.Gray;
                _logData.Text = "";
                _logData.ShortText = "";
                _progStart = 0;
            }
            _lastId = ids.Last(); 
        }

        private int _lastId = -1;
        private int _progStart;
        private Color _fNewLogEvent = Color.Gray;
        private GeneralLogData _logData = new GeneralLogData();
    }
}
