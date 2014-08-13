using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ML.ConfigSettings.Services;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using VisualizationSystem.Model;
using VisualizationSystem.Model.Settings;
using VisualizationSystem.View.UserControls.Setting;
using VisualizationSystem.ViewModel;

namespace VisualizationSystem.View.Forms.Setting
{
    public partial class FormCodtDomainSettings : Form, IDisposable
    {
        private int _index;
        private int startIndex = 0x2001;
        private OxyPlot.WindowsForms.Plot plotDefenceDiagram;
        private const int EndOfArray = 2147483647;
        private List<ParametersSettingsData> _parametersSettings;
        private string SolvedDefDiagramPointsFilePath = "../../Files/solvedddpoints.prm";
        private int _points = 20;
        private int _diagramCount = 4;
        private List<List<string>> _solvedHz;
        private List<List<string>> _solvedV;
        private List<string> _diagramName;
        public FormCodtDomainSettings()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
            SaveDataToList();
        }
        public FormCodtDomainSettings(int index, List<ParametersSettingsData> parametersSettings )
        {
            InitializeComponent();
            _parametersSettings = parametersSettings;
            this.Text = "0x" + Convert.ToString(index, 16) + "  " + parametersSettings[index - startIndex].Name;
            _index = index;
            LoadDataFromList(parametersSettings[index - startIndex].CodtDomainArray);
            plotDefenceDiagram = new OxyPlot.WindowsForms.Plot { Model = new PlotModel(), Dock = DockStyle.Fill };
            this.splitContainer2.Panel2.Controls.Add(plotDefenceDiagram);
            MakeGraphic();
            _solvedHz = new List<List<string>>();
            _solvedV = new List<List<string>>();
            _diagramName = new List<string>();
            InitSlovedHzAndVLists();
        }

        private void FormCodtDomainSettings_Load(object sender, EventArgs e)
        {
            
        }

        private void InitSlovedHzAndVLists()
        {
            for (int i = 0; i < _diagramCount; i++)
            {
                _solvedHz.Add(new List<string>());
                _solvedV.Add(new List<string>());
            }
        }

        private void LoadDataFromList(CodtDomainData[] codtDomainDatas )
        {
            dataGridView1.RowCount = codtDomainDatas.Count();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1[0, i].Value = i;
                dataGridView1[1, i].Value = codtDomainDatas[i].Coordinate;
                dataGridView1[2, i].Value = codtDomainDatas[i].Speed;

            }
        }

        private void SaveDataToList()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                _parametersSettings[_index - startIndex].CodtDomainArray[i].Coordinate = 
                    int.Parse(dataGridView1[1, i].Value.ToString());
                _parametersSettings[_index - startIndex].CodtDomainArray[i].Speed = 
                    int.Parse(dataGridView1[2, i].Value.ToString());
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = (TextBox)e.Control;
            tb.KeyPress -= tb_KeyPress;
            tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
        }

        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            string vlCell = ((TextBox)sender).Text;
            //проверка ввода
            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].IsInEditMode == true)
                if (!Char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '\b') && (e.KeyChar != '-'))
                    e.Handled = true;
            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].IsInEditMode == true)
                if (!Char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '\b') && (e.KeyChar != '-'))
                    e.Handled = true;
        }

        private void MakeGraphic()
        {
            //Legend
            plotDefenceDiagram.Model.PlotType = PlotType.XY;
            var valueList = new List<int>();
            foreach (DataGridViewRow row in  dataGridView1.Rows)
            {
                int value = int.Parse(row.Cells[1].Value.ToString());
                if (value != EndOfArray)
	                valueList.Add(value/1000);
            }
            //Axis
            var xAxis = new LinearAxis(AxisPosition.Bottom, 0)
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = "Путь (м)",
                Maximum = -IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value,
                Minimum = valueList.Min()+1
            };
            plotDefenceDiagram.Model.Axes.Add(xAxis);
            var yAxis = new LinearAxis(AxisPosition.Left, 0)
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = "Скорость (м/с)",
            };
            plotDefenceDiagram.Model.Axes.Add(yAxis);
            // Create Line series
            var s1 = new LineSeries {StrokeThickness = 1, Color = OxyColors.Blue};
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToInt32(dataGridView1[1, i].Value) == EndOfArray)
                {
                    s1.Points.Add(
                        new DataPoint(
                            -IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value,
                            Convert.ToDouble(dataGridView1[2, i].Value)/1000));
                    break;
                }
                s1.Points.Add(
                    new DataPoint(
                        Convert.ToDouble(dataGridView1[1, i].Value)/1000,
                        Convert.ToDouble(dataGridView1[2, i].Value)/1000));
            }
            // add Series and Axis to plot model
            plotDefenceDiagram.Model.Series.Add(s1);
            //plotDefenceDiagram.RefreshPlot(true);
            plotDefenceDiagram.InvalidatePlot(true);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1[1, i].Value == null || dataGridView1[1, i].Value.ToString() == ".")
                        dataGridView1[1, i].Value = 0;
                }
            }
            if (e.ColumnIndex == 2)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1[2, i].Value == null || dataGridView1[2, i].Value.ToString() == ".")
                        dataGridView1[2, i].Value = 0;
                }
            }
            //
            plotDefenceDiagram.Dispose();
            plotDefenceDiagram = new OxyPlot.WindowsForms.Plot { Model = new PlotModel(), Dock = DockStyle.Fill };
            this.splitContainer2.Panel2.Controls.Add(plotDefenceDiagram);
            MakeGraphic();
        }

        private void toolStripButtonLoad_Click(object sender, EventArgs e) //load table
        {
            var dialog = new FormCanId { StartPosition = FormStartPosition.CenterScreen };
            dialog.ShowDialog();
            ushort controllerId;
            ushort.TryParse(dialog.textBoxAddress.Text, out controllerId);
            var data = new List<byte>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string firstParamStr = dataGridView1[1, i].Value.ToString();
                int firstParam;
                if (!int.TryParse(firstParamStr, out firstParam))
                {
                    MessageBox.Show("Неверный формат параметра", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                byte[] firstBytes= BitConverter.GetBytes(firstParam);
                data.AddRange(firstBytes);

                string secondParamStr = dataGridView1[2, i].Value.ToString();
                short secondParam;
                if (!short.TryParse(secondParamStr, out secondParam))
                {
                    MessageBox.Show("Неверный формат параметра", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                byte[] secondBytes = BitConverter.GetBytes(secondParam);
                data.AddRange(secondBytes);
            }
            ParametersSettings._paramSettingsOperation = 1;
            IoC.Resolve<DataListener>()
                .SetParameter(controllerId,(ushort)_index, 0x02, data.ToArray());
        }

        private void toolStripButtonLoadSolvedDefDiagram_Click(object sender, EventArgs e)
        {
            ReadSolvedDiagramFromFile(SolvedDefDiagramPointsFilePath);
            var formChooseDefenceDiagram = new FormChooseDefenceDiagramSettings(_diagramName) { StartPosition = FormStartPosition.CenterParent };
            formChooseDefenceDiagram.ShowDialog();
            if (formChooseDefenceDiagram.SelectedIndex >= 0)
            {
                WriteSolvedDiagramToTable(formChooseDefenceDiagram.SelectedIndex);
                plotDefenceDiagram.Dispose();
                plotDefenceDiagram = new OxyPlot.WindowsForms.Plot { Model = new PlotModel(), Dock = DockStyle.Fill };
                this.splitContainer2.Panel2.Controls.Add(plotDefenceDiagram);
                MakeGraphic();
            }
            formChooseDefenceDiagram.Dispose();
        }

        private void ReadSolvedDiagramFromFile(string path)
        {
            for (int i = 0; i < _diagramCount; i++)
            {
                _solvedHz[i].Clear();
                _solvedV[i].Clear();
            }
            _diagramName.Clear();
            for (int i = 0; i < _diagramCount; i++)
            {
                _solvedHz[i] = new List<string>();
                _solvedV[i] = new List<string>();
            }
            _diagramName = new List<string>();
            string Line;
            string[] strArr;
            int k = 0;
            int diagNum = 0;
            string index;
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            while (sr.EndOfStream != true)
            {
                Line = sr.ReadLine();
                k++;
                //if(Line=="")
                //break;
                Line = Line.TrimEnd(' ');
                if ((k == 1) || (k == _points + 2) || (k == _points*2 + 3) || (k == _points*3 + 4))
                {
                    _diagramName.Add(Line.Trim());
                    diagNum++;
                }
                else
                {
                    string[] separator = new string[] { "[$]" };
                    strArr = Line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < strArr.Length; i++)
                    {
                        if (i == 0)
                        {
                            _solvedHz[diagNum-1].Add(strArr[i].Trim());
                        }
                        else if (i == 1)
                        {
                            _solvedV[diagNum-1].Add(strArr[i].Trim());
                        }
                    }
                }
            }
            sr.Close();
        }

        private void WriteSolvedDiagramToTable(int diagNum)
        {
            dataGridView1.RowCount = _solvedHz[diagNum].Count();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (i == dataGridView1.RowCount - 1)
                {
                    dataGridView1[0, i].Value = i;
                    dataGridView1[1, i].Value = Convert.ToDouble(_solvedHz[diagNum][i], CultureInfo.GetCultureInfo("en-US"));
                    dataGridView1[2, i].Value = Convert.ToDouble(_solvedV[diagNum][i], CultureInfo.GetCultureInfo("en-US")) * 1000;
                }
                else
                {
                    dataGridView1[0, i].Value = i;
                    dataGridView1[1, i].Value = Convert.ToDouble(_solvedHz[diagNum][i], CultureInfo.GetCultureInfo("en-US")) * 1000;
                    dataGridView1[2, i].Value = Convert.ToDouble(_solvedV[diagNum][i], CultureInfo.GetCultureInfo("en-US")) * 1000;
                }
            }
        }

    }
}
