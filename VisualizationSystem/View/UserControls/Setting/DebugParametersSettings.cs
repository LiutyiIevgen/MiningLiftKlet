using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ML.ConfigSettings.Services;
using VisualizationSystem.Model;

namespace VisualizationSystem.View.UserControls.Setting
{
    public partial class DebugParametersSettings : UserControl
    {
        public DebugParametersSettings()
        {
            InitializeComponent();
            _mineConfig = IoC.Resolve<MineConfig>();
        }

        private void DebugParametersSettings_Load(object sender, EventArgs e)
        {
            textBoxLeadController.Text = IoC.Resolve<MineConfig>().LeadingController.ToString();
            textBoxMaxDopMismatch.Text = IoC.Resolve<MineConfig>().MaxDopMismatch.ToString();
            textBoxReceiveDataDelay.Text = _mineConfig.ReceiveDataDelay.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _mineConfig.LeadingController = Convert.ToInt32(textBoxLeadController.Text);
            _mineConfig.MaxDopMismatch = Convert.ToInt32(textBoxMaxDopMismatch.Text);
            _mineConfig.ReceiveDataDelay = Convert.ToInt32(textBoxReceiveDataDelay.Text);
        }

        private MineConfig _mineConfig;
    }
}
