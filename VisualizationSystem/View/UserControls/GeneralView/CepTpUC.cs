using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ML.DataExchange.Model;
using VisualizationSystem.ViewModel.MainViewModel;

namespace VisualizationSystem.View.UserControls.GeneralView
{
    public partial class CepTpUC : UserControl
    {
        public CepTpUC()
        {
            InitializeComponent();
            _centralSignalsDataVm = new CentralSignalsDataVm();
            CreateRichTextBoxMassiv();
        }
        private void CreateRichTextBoxMassiv()
        {
            masRichTextBox = new RichTextBox[] { richTextBox5, richTextBox6, richTextBox7, richTextBox8, 
                richTextBox9, richTextBox10, richTextBox11, richTextBox12,
                richTextBox13, richTextBox14, richTextBox15, richTextBox16,
                richTextBox17, richTextBox18, richTextBox19, richTextBox20, richTextBox21,
                richTextBox22, richTextBox23, richTextBox24, richTextBox25, richTextBox26, 
                richTextBox27, richTextBox28};
        }
        public void Refresh(Parameters parameters)
        {
            var centralSignals = _centralSignalsDataVm.GetSignalsData(parameters);
            this.Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < 24; i++)
                {
                    masRichTextBox[i].BackColor = centralSignals[i].BackColor;
                    masRichTextBox[i].Text = centralSignals[i].Text;
                }
            });
            /*if (centralSignals[11].BackColor == Color.Red && DefenceDiagramWorking == 0)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    tabControl1.SelectedIndex = 1;
                });
                DefenceDiagramWorking = 1;
            }
            if (centralSignals[11].BackColor == Color.DarkGray && DefenceDiagramWorking == 1)
            {
                DefenceDiagramWorking = 0;
            }*/
        }
        private RichTextBox[] masRichTextBox;//массив текстбоксов для вывода сигналов цунтральной части экрана
        private CentralSignalsDataVm _centralSignalsDataVm;
    }
}
