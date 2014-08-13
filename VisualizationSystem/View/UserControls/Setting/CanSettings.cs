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
    public partial class CanSettings : UserControl
    {
        public CanSettings()
        {
            InitializeComponent();
            _mineConfig = IoC.Resolve<MineConfig>();
        }

        private void CanSettings_Load(object sender, EventArgs e)
        {
            textBox1.Text = _mineConfig.CanName;
            comboBoxCanSpeed.Text = _mineConfig.CanSpeed.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _mineConfig.CanName = textBox1.Text;
            _mineConfig.CanSpeed = Convert.ToInt32(comboBoxCanSpeed.Text);
            IoC.Resolve<DataListener>().Init(null);
        }

        private MineConfig _mineConfig;
    }
}
