using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ML.ConfigSettings.Services;
using ML.DataExchange.Model;
using VisualizationSystem.Model;
using VisualizationSystem.ViewModel.MainViewModel;

namespace VisualizationSystem.View.UserControls.GeneralView
{
    public partial class AuziDUC : UserControl
    {
        public AuziDUC()
        {
            InitializeComponent();
            DoubleBuffered(dataGridViewControllerParameters, true);
            DoubleBuffered(dataGridViewInSignals, true);
            DoubleBuffered(dataGridViewOutSignals, true);
            //CreateAuziDIOSignalsMassiv();
            _mineConfig = IoC.Resolve<MineConfig>();
            _auziDInOutSignalsVm = new AuziDInOutSignalsVm();
            _auziDControllerParametersVm = new AuziDControllerParametersVm();
        }

        private void AuziDUC_Load(object sender, EventArgs e)
        {
        }

        public void DoubleBuffered(DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }

        public void Refresh(List<Parameters> listParameters)
        {
            var parameters = listParameters[_mineConfig.LeadingController - 1];
            _auziDInOutSignalsVm.UpDateSignals(parameters);
            int rowCount = _auziDInOutSignalsVm.InputNames.Count / 2;
            int fChet = 0;
            if (_auziDInOutSignalsVm.InputNames.Count % 2 != 0)
            {
                fChet = 1;
                rowCount++;
            }
            this.Invoke((MethodInvoker)delegate
            {
                dataGridViewInSignals.RowCount = rowCount;
                for (int i = 0; i < dataGridViewInSignals.RowCount; i++)
                {
                    dataGridViewInSignals.Rows[i].Cells[1].Style.BackColor = _auziDInOutSignalsVm.InputMeanings[i];
                    dataGridViewInSignals.Rows[i].Cells[2].Value = _auziDInOutSignalsVm.InputNames[i];
                }
                for (int i = 0; i < dataGridViewInSignals.RowCount - fChet; i++)
                {
                    dataGridViewInSignals.Rows[i].Cells[3].Style.BackColor = _auziDInOutSignalsVm.InputMeanings[dataGridViewInSignals.RowCount + i];
                    dataGridViewInSignals.Rows[i].Cells[4].Value = _auziDInOutSignalsVm.InputNames[dataGridViewInSignals.RowCount + i];
                }
                if (fChet == 1)
                {
                    dataGridViewInSignals.Rows[dataGridViewInSignals.RowCount - 1].Cells[3].Style.BackColor = Color.Gray;
                    dataGridViewInSignals.Rows[dataGridViewInSignals.RowCount - 1].Cells[4].Value = "";
                }
            });
            this.Invoke((MethodInvoker) delegate
            {
                dataGridViewOutSignals.RowCount = _auziDInOutSignalsVm.OutputNames.Count;
                for (int i = 0; i < dataGridViewOutSignals.RowCount; i++)
                {
                    dataGridViewOutSignals.Rows[i].Cells[1].Style.BackColor = _auziDInOutSignalsVm.OutputMeanings[i];
                    dataGridViewOutSignals.Rows[i].Cells[2].Value = _auziDInOutSignalsVm.OutputNames[i];
                }
            });
            //dataGridViewOutSignals.FirstDisplayedCell = dataGridViewOutSignals[0, dataGridViewOutSignals.RowCount - 1];
        }

        public void UpdateAuziDControllerParameters(List<Parameters> parameters)
        {
            var dataList = _auziDControllerParametersVm.GetDataList(parameters);
            this.Invoke((MethodInvoker)delegate
            {
                dataGridViewControllerParameters.RowCount = dataList[0].Count();
                for (int i = 0; i < dataGridViewControllerParameters.RowCount; i++)
                {
                    dataGridViewControllerParameters[0, i].Value = i + 1;
                    for (int j = 1; j < dataGridViewControllerParameters.ColumnCount; j++)
                    {
                        dataGridViewControllerParameters[j, i].Value = dataList[j-1][i];
                    }
                }
                for (int i = 0; i < dataGridViewControllerParameters.RowCount; i++)
                    dataGridViewControllerParameters.Rows[i].DefaultCellStyle.BackColor = Color.White;

                dataGridViewControllerParameters.Rows[_mineConfig.LeadingController - 1].DefaultCellStyle.BackColor = Color.LightGreen;
            });
        }
        /*private void CreateAuziDIOSignalsMassiv()
        {
            masInTextBox = new TextBox[] { textBox6, textBox7, textBox8, textBox9, 
                textBox10, textBox11, textBox12, textBox13, textBox14, textBox15,
                textBox16, textBox17, textBox18, textBox19, textBox20, textBox21,
                textBox22, textBox23, textBox24, textBox25, textBox26, textBox27,
                textBox28, textBox29, textBox30, textBox31, textBox32, textBox33,
                textBox34, textBox35, textBox36, textBox37 };

            masOutTextBox = new TextBox[] { textBox38, textBox39, textBox40, textBox41, 
                textBox42, textBox43, textBox44, textBox45, textBox46, textBox47, textBox48,
                textBox49, textBox50, textBox51, textBox52, textBox53 };


            masInLabel = new Label[] { label4, label5, label11, label12, label13, label14, label15,
                label16, label17, label18, label19, label20, label21, label22, label23, label24,
                label25, label26, label27, label28, label29, label30, label31, label32, label33,
                label34, label35, label36, label37, label38, label39, label40};

            masOutLabel = new Label[] { label41, label42, label43, label44, label45, label46,
                label47, label48, label49, label50, label51, label52, label53, label54, label55, label56 };
        } */
        private AuziDInOutSignalsVm _auziDInOutSignalsVm;
        private AuziDControllerParametersVm _auziDControllerParametersVm;

        private TextBox[] masInTextBox;//массив текстбоксов для вывода входных сигналов АУЗИ-Д
        private Label[] masInLabel;//массив лейблов для вывода входных сигналов АУЗИ-Д
        private TextBox[] masOutTextBox;//массив текстбоксов для вывода выходных сигналов АУЗИ-Д
        private Label[] masOutLabel;//массив лейблов для вывода выходных сигналов АУЗИ-Д

        private MineConfig _mineConfig;
    }
}
