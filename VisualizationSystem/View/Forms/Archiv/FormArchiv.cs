using System;
using System.Windows.Forms;
using VisualizationSystem.View.UserControls.Archiv;

namespace VisualizationSystem.View.Forms.Archiv
{
    public partial class FormArchiv : Form
    {
        public FormArchiv()
        {
            InitializeComponent();
        }

        private void FormArchiv_Load(object sender, EventArgs e)
        {
            _archivUC = new ArchivUC { Dock = System.Windows.Forms.DockStyle.Fill };
            _generalLogArchivUC = new GeneralLogArchivUC { Dock = System.Windows.Forms.DockStyle.Fill };
            panel1.Controls.Add(_archivUC);
        }

        private void toolStripMenuItemSignals_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(_archivUC);
        }

        private void toolStripMenuItemLog_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(_generalLogArchivUC);
        }

        private ArchivUC _archivUC;
        private GeneralLogArchivUC _generalLogArchivUC;
    }
}
