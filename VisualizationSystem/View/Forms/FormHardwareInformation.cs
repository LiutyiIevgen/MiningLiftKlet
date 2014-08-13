using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualizationSystem.View.Forms
{
    public partial class FormHardwareInformation : Form
    {
        public FormHardwareInformation(List<string> deviceInformationList)
        {
            InitializeComponent();
            for (int i = 0; i < deviceInformationList.Count; i++)
            {
                richTextBox1.Text += names[i]+": " + deviceInformationList[i] + "\n";
            }
        }
        private string[] names = new string[]{"Количество параметров девайса",
            "Название системы", "Название устройства", "Версия железа", 
            "Версия софта (с датой сборки)", "Версия протокола"};
    }
}
