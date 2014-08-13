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
    public partial class FormCanId : Form
    {
        public FormCanId()
        {
            InitializeComponent();
        }

        private void buttonAddress_Click(object sender, EventArgs e)
        {
            Int16 value;
            if(!Int16.TryParse(textBoxAddress.Text, out value))
                MessageBox.Show(@"Неверный формат адреса");
            else
                this.Close();
        }

        private void textBoxSettingsParol_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            e.Handled = !(char.IsDigit(c) || c == '\b');
            if (c == 0xd)//enter
                buttonAddress_Click(new object(), new EventArgs());
        }
    }
}
