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
using VisualizationSystem.Services;

namespace VisualizationSystem.View.UserControls.Archiv
{
    public partial class GeneralLogArchivUC : UserControl
    {
        public GeneralLogArchivUC()
        {
            InitializeComponent();
            DoubleBuffered(ArhivLog, true);
        }

        private void GeneralLogArchivUC_Load(object sender, EventArgs e)
        {

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
                ArhivLog.Text += text + "\n";
                ArhivLog.SelectionStart = ArhivLog.Text.Length; //Set the current caret position at the end
                ArhivLog.ScrollToCaret(); //Now scroll it automatically
            });
        }

        private void ClearLog()
        {
            this.Invoke((MethodInvoker)delegate
            {
                ArhivLog.Clear();
            });
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            var logUpdateThread = new Thread(FindDataToLogThread) { IsBackground = true, Priority = ThreadPriority.Lowest };
            logUpdateThread.Start();
        }

        private void FindDataToLogThread()
        {
            var dataBaseService = new DataBaseService();
            var ids = dataBaseService.GetGeneralLogIds(dateTimePicker1.Value, dateTimePicker2.Value);
            ClearLog();
            foreach (var id in ids)
            {
                var logData = dataBaseService.GetGeneralLogLineById(id);
                AddLineToLog(logData.Text);
            }
        }

    }
}
