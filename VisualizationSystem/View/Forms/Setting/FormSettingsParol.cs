using System;
using System.Windows.Forms;
using VisualizationSystem.Model;

namespace VisualizationSystem.View.Forms.Setting
{
    public partial class FormSettingsParol : Form
    {
        private string _parol = "";
        public FormSettingsParol()
        {
            InitializeComponent();
        }

        private void FormSettingsParol_Load(object sender, EventArgs e)
        {
            textBoxSettingsParol.Clear();
        }

        private void buttonSettingsParol_Click(object sender, EventArgs e)
        {
            if (textBoxSettingsParol.Text == _parol)
            {
                IoC.Resolve<FormSettings>().ShowDialog();
                IoC.Resolve<FormSettingsParol>().Close();
            }
            else
            {
                MessageBox.Show("Введенный пароль - неверный!", "Неверный пароль", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                textBoxSettingsParol.Clear();
            }
        }

        private void textBoxSettingsParol_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            e.Handled = !(char.IsDigit(c) || c == '\b');
            if(c == 0xd)//enter
                buttonSettingsParol_Click(new object(), new EventArgs());
        }

        private void textBoxSettingsParol_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
