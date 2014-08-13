using System;
using System.Drawing;
using System.Windows.Forms;
using VisualizationSystem.View.UserControls.Setting;

namespace VisualizationSystem.View.Forms.Setting
{
    public partial class FormSettings : Form
    {
        MainViewSettings _mainViewSettings = new MainViewSettings();
        AuziDSettings _auziDSettings = new AuziDSettings();
        ParametersSettings _parametersSettings = new ParametersSettings();
        DefenceDiagramSettings _defenceDiagramSettings;
        CanSettings _canSettings = new CanSettings();
        DebugParametersSettings _debugParametersSettings = new DebugParametersSettings();
        KalibrovkaSettings _kalibrovkaSettings = new KalibrovkaSettings();
        public FormSettings()
        {
            InitializeComponent();
            //start
           /* AuziDButton.BackColor = Color.Silver;
            ParametersButton.BackColor = Color.Silver;
            DefenceDiagramButton.BackColor = Color.Silver;
            CanSettingsButton.BackColor = Color.Silver;
            DebugParametersButton.BackColor = Color.Silver;
            KalibrovkaButton.BackColor = Color.Silver;
            MainViewSettingsButton.BackColor = SystemColors.ControlDark; */
            _mainViewSettings.Dock = System.Windows.Forms.DockStyle.Fill; 
            panel1.Controls.Clear();
            panel1.Controls.Add(_mainViewSettings);
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
        }

        private void MainViewSettingsButton_Click(object sender, EventArgs e)
        {
            AuziDButton.BackColor = Color.Silver;
            ParametersButton.BackColor = Color.Silver;
            DefenceDiagramButton.BackColor = Color.Silver;
            CanSettingsButton.BackColor = Color.Silver;
            DebugParametersButton.BackColor = Color.Silver;
            KalibrovkaButton.BackColor = Color.Silver;
            MainViewSettingsButton.BackColor = SystemColors.ControlDark;
            _mainViewSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(_mainViewSettings);
        }

        private void AuziDButton_Click(object sender, EventArgs e)
        {
            MainViewSettingsButton.BackColor = Color.Silver;
            ParametersButton.BackColor = Color.Silver;
            DefenceDiagramButton.BackColor = Color.Silver;
            CanSettingsButton.BackColor = Color.Silver;
            DebugParametersButton.BackColor = Color.Silver;
            KalibrovkaButton.BackColor = Color.Silver;
            AuziDButton.BackColor = SystemColors.ControlDark;
            _auziDSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(_auziDSettings);
        }

        private void ParametersButton_Click(object sender, EventArgs e)
        {
            MainViewSettingsButton.BackColor = Color.Silver;
            AuziDButton.BackColor = Color.Silver;
            DefenceDiagramButton.BackColor = Color.Silver;
            CanSettingsButton.BackColor = Color.Silver;
            DebugParametersButton.BackColor = Color.Silver;
            KalibrovkaButton.BackColor = Color.Silver;
            ParametersButton.BackColor = SystemColors.ControlDark;
            _parametersSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(_parametersSettings);
        }

        private void DefenceDiagramButton_Click(object sender, EventArgs e)
        {
            MainViewSettingsButton.BackColor = Color.Silver;
            AuziDButton.BackColor = Color.Silver;
            ParametersButton.BackColor = Color.Silver;
            CanSettingsButton.BackColor = Color.Silver;
            DebugParametersButton.BackColor = Color.Silver;
            KalibrovkaButton.BackColor = Color.Silver;
            DefenceDiagramButton.BackColor = SystemColors.ControlDark;
            _defenceDiagramSettings = new DefenceDiagramSettings();
            _defenceDiagramSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(_defenceDiagramSettings);
        }

        private void CanSettingsButton_Click(object sender, EventArgs e)
        {
            MainViewSettingsButton.BackColor = Color.Silver;
            AuziDButton.BackColor = Color.Silver;
            ParametersButton.BackColor = Color.Silver;
            DefenceDiagramButton.BackColor = Color.Silver;
            DebugParametersButton.BackColor = Color.Silver;
            KalibrovkaButton.BackColor = Color.Silver;
            CanSettingsButton.BackColor = SystemColors.ControlDark;
            _canSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(_canSettings);
        }

        private void DebugParametersButton_Click(object sender, EventArgs e)
        {
            MainViewSettingsButton.BackColor = Color.Silver;
            AuziDButton.BackColor = Color.Silver;
            ParametersButton.BackColor = Color.Silver;
            DefenceDiagramButton.BackColor = Color.Silver;
            CanSettingsButton.BackColor = Color.Silver;
            KalibrovkaButton.BackColor = Color.Silver;
            DebugParametersButton.BackColor = SystemColors.ControlDark;
            _debugParametersSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(_debugParametersSettings);
        }

        private void KalibrovkaButton_Click(object sender, EventArgs e)
        {
            MainViewSettingsButton.BackColor = Color.Silver;
            AuziDButton.BackColor = Color.Silver;
            ParametersButton.BackColor = Color.Silver;
            DefenceDiagramButton.BackColor = Color.Silver;
            CanSettingsButton.BackColor = Color.Silver;
            DebugParametersButton.BackColor = Color.Silver;
            KalibrovkaButton.BackColor = SystemColors.ControlDark;
            _kalibrovkaSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(_kalibrovkaSettings);
        }


    }
}
