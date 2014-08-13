using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualizationSystem.View.Forms.Setting
{
    public partial class FormChooseDefenceDiagramSettings : Form
    {
        public int SelectedIndex = -1;

        public FormChooseDefenceDiagramSettings()
        {
            InitializeComponent();
        }

        public FormChooseDefenceDiagramSettings(List<string> diagramNames)
        {
            InitializeComponent();
            LoadDataToComboBox(diagramNames);
        }

        private void FormChooseDefenceDiagramSettings_Load(object sender, EventArgs e)
        {
            ChooseDiagramComboBox.SelectedIndex = 0;
        }

        private void LoadDataToComboBox(List<string> names)
        {
            for (int i = 0; i < names.Count; i++)
            {
                ChooseDiagramComboBox.Items.Add(names[i]);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SelectedIndex = ChooseDiagramComboBox.SelectedIndex;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
