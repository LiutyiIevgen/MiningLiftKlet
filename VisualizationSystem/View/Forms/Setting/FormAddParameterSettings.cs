using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ML.ConfigSettings.Services;
using VisualizationSystem.Model;
using VisualizationSystem.Model.Settings;

namespace VisualizationSystem.View.Forms.Setting
{
    public partial class FormAddParameterSettings : Form
    {
        public FormAddParameterSettings(List<ParametersSettingsData> parametersSettingsDatas)
        {
            InitializeComponent();
            _parametersSettingsDatas = parametersSettingsDatas;
        }

        private void FormAddParameterSettings_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            var parameterSettingsData = new ParametersSettingsData
            {
                Id = _parametersSettingsDatas[_parametersSettingsDatas.Count - 1].Id + 1,
                Name = textBox1.Text,
                Type = comboBox1.Text,
                Value = textBox2.Text
            };
            if (parameterSettingsData.Type == "codtDomain")
            {
                parameterSettingsData.CodtDomainArray = new CodtDomainData[20];
                for (int i = 0; i < parameterSettingsData.CodtDomainArray.Count(); i++)
                {
                    parameterSettingsData.CodtDomainArray[i] = new CodtDomainData();
                }
            }
            _parametersSettingsDatas.Add(parameterSettingsData);

            MessageBox.Show("Новый параметр добавлен успешно!", "Добавление параметра", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "codtDomain")
            {
                textBox2.Text = "Двоичные данные";
                textBox2.ReadOnly = true;
            }
            else
            {
                textBox2.ReadOnly = false;
            }
        }

        private List<ParametersSettingsData> _parametersSettingsDatas;
    }
}
