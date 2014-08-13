using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ML.ConfigSettings.Services;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using VisualizationSystem.Model;
using VisualizationSystem.Model.Settings;
using VisualizationSystem.ViewModel;

namespace VisualizationSystem.View.UserControls.Setting
{
    public partial class SolveDefenceDiagramSettings : UserControl
    {
        private MineConfig _mineConfig;
        private static int variableParametersCount = 0;
        private static int readonlyParametersCount = 0;
        private List<string> variableParametersName;
        private List<string> variableParametersValue;
        private List<string> readonlyParametersName;
        private List<string> readonlyParametersValue;
        private double calculatedWayVeightAndEquipment = 0;
        private double calculatedWayPeople = 0;
        private double dotWayVeightAndEquipment = 0;
        private double dotWayPeople = 0;
        private string SolveDefDiagramParametersFilePath = "../../Files/solveddparam.prm";
        private string SolvedDefDiagramPointsFilePath = "../../Files/solvedddpoints.prm";
        private OxyPlot.WindowsForms.Plot _plotDefenceDiagram;
        public double[] _selectedV;
        public double[] _selectedHz;
        private int _points = 20;

        public SolveDefenceDiagramSettings()
        {
            InitializeComponent();
            variableParametersName = new List<string>();
            variableParametersValue = new List<string>();
            readonlyParametersName = new List<string>();
            readonlyParametersValue = new List<string>();
            InitReadOnlyParameters();
            _mineConfig = IoC.Resolve<MineConfig>();
            _selectedV = new double[_points];
            _selectedHz = new double[_points];
        }

        private void SolveDefenceDiagramSettings_Load(object sender, EventArgs e)
        {
            InitData();
            DefenceDiagramComboBox.SelectedIndex = 0;
        }

        private void InitReadOnlyParameters()
        {
            for (int i = 0; i < 4; i++)
            {
                readonlyParametersValue.Add("0");
            }
            readonlyParametersName.Add(@"расчётный путь ""гр. и об.""");
            readonlyParametersName.Add(@"расчётный путь ""люди""");
            readonlyParametersName.Add(@"путь дотягивания ""гр. и об.""");
            readonlyParametersName.Add(@"путь дотягивания ""люди""");
        }

        private void InitData()
        {
            ReadParamFromFile(SolveDefDiagramParametersFilePath);
            variableParametersCount = variableParametersName.Count();
            readonlyParametersCount = readonlyParametersName.Count();
            dataGridViewVariableParameters.RowCount = variableParametersCount;
            dataGridViewReadOnlyParameters.RowCount = readonlyParametersCount;
            for (int i = 0; i < dataGridViewVariableParameters.RowCount; i++)
            {
                dataGridViewVariableParameters[0, i].Value = i;
                dataGridViewVariableParameters[1, i].Value = variableParametersName[i];
                dataGridViewVariableParameters[2, i].Value = variableParametersValue[i];
            }
            for (int i = 0; i < dataGridViewReadOnlyParameters.RowCount; i++)
            {
                dataGridViewReadOnlyParameters[0, i].Value = i;
                dataGridViewReadOnlyParameters[1, i].Value = readonlyParametersName[i];
                dataGridViewReadOnlyParameters[2, i].Value = readonlyParametersValue[i];
            }
            CalculateParameters(); //calculate readonly parameters
        }

        private void CalculateParameters()
        {
            calculatedWayVeightAndEquipment = Convert.ToDouble(dataGridViewVariableParameters[2, 4].Value, CultureInfo.GetCultureInfo("en-US")) * (Convert.ToDouble(dataGridViewVariableParameters[2, 7].Value, CultureInfo.GetCultureInfo("en-US")) + Convert.ToDouble(dataGridViewVariableParameters[2, 8].Value, CultureInfo.GetCultureInfo("en-US")));
            calculatedWayVeightAndEquipment += Convert.ToDouble(dataGridViewVariableParameters[2, 15].Value, CultureInfo.GetCultureInfo("en-US")) * Convert.ToDouble(dataGridViewVariableParameters[2, 8].Value, CultureInfo.GetCultureInfo("en-US")) / 2;
            calculatedWayVeightAndEquipment += (Convert.ToDouble(dataGridViewVariableParameters[2, 4].Value, CultureInfo.GetCultureInfo("en-US")) + Convert.ToDouble(dataGridViewVariableParameters[2, 15].Value, CultureInfo.GetCultureInfo("en-US"))) * (Convert.ToDouble(dataGridViewVariableParameters[2, 4].Value, CultureInfo.GetCultureInfo("en-US")) + Convert.ToDouble(dataGridViewVariableParameters[2, 15].Value, CultureInfo.GetCultureInfo("en-US"))) / (2 * Convert.ToDouble(dataGridViewVariableParameters[2, 11].Value, CultureInfo.GetCultureInfo("en-US")));
            dataGridViewReadOnlyParameters[2, 0].Value = Math.Round(calculatedWayVeightAndEquipment, 2);
            calculatedWayPeople = Convert.ToDouble(dataGridViewVariableParameters[2, 5].Value, CultureInfo.GetCultureInfo("en-US")) * (Convert.ToDouble(dataGridViewVariableParameters[2, 7].Value, CultureInfo.GetCultureInfo("en-US")) + Convert.ToDouble(dataGridViewVariableParameters[2, 8].Value, CultureInfo.GetCultureInfo("en-US")));
            calculatedWayPeople += Convert.ToDouble(dataGridViewVariableParameters[2, 15].Value, CultureInfo.GetCultureInfo("en-US")) * Convert.ToDouble(dataGridViewVariableParameters[2, 8].Value, CultureInfo.GetCultureInfo("en-US")) / 2;
            calculatedWayPeople += (Convert.ToDouble(dataGridViewVariableParameters[2, 5].Value, CultureInfo.GetCultureInfo("en-US")) + Convert.ToDouble(dataGridViewVariableParameters[2, 15].Value, CultureInfo.GetCultureInfo("en-US"))) * (Convert.ToDouble(dataGridViewVariableParameters[2, 5].Value, CultureInfo.GetCultureInfo("en-US")) + Convert.ToDouble(dataGridViewVariableParameters[2, 15].Value, CultureInfo.GetCultureInfo("en-US"))) / (2 * Convert.ToDouble(dataGridViewVariableParameters[2, 12].Value, CultureInfo.GetCultureInfo("en-US")));
            dataGridViewReadOnlyParameters[2, 1].Value = Math.Round(calculatedWayPeople, 2);
            dotWayVeightAndEquipment = calculatedWayVeightAndEquipment + Convert.ToDouble(dataGridViewVariableParameters[2, 9].Value, CultureInfo.GetCultureInfo("en-US")) + Convert.ToDouble(dataGridViewVariableParameters[2, 10].Value, CultureInfo.GetCultureInfo("en-US"));
            dataGridViewReadOnlyParameters[2, 2].Value = Math.Round(dotWayVeightAndEquipment, 2);
            dotWayPeople = calculatedWayPeople + Convert.ToDouble(dataGridViewVariableParameters[2, 9].Value, CultureInfo.GetCultureInfo("en-US")) + Convert.ToDouble(dataGridViewVariableParameters[2, 10].Value, CultureInfo.GetCultureInfo("en-US"));
            dataGridViewReadOnlyParameters[2, 3].Value = Math.Round(dotWayPeople, 2);
        }

        private void WriteParamToFile(string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                string Line;
                for(int i = 0; i < variableParametersName.Count; i++)
                {
                    Line = variableParametersName[i] + "[$]" + variableParametersValue[i];
                    sw.WriteLine(Line);
                }
                sw.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void WritePointsToFile(string path)
        {
            var defenceDiagramSettingsVm = new DefenceDiagramSettingsVm(variableParametersValue);
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                string Line;
                Line = @"Тахограмма ""груз""";
                sw.WriteLine(Line);
                for (int i = 0; i < _points; i++)
                {
                    Line = Math.Round(defenceDiagramSettingsVm.hzVeightUp[i], 3).ToString(CultureInfo.GetCultureInfo("en-US")) + "[$]" + Math.Round(defenceDiagramSettingsVm.vVeight[i], 3).ToString(CultureInfo.GetCultureInfo("en-US"));
                    sw.WriteLine(Line);
                }
                Line = @"Тахограмма ""люди""";
                sw.WriteLine(Line);
                for (int i = 0; i < _points; i++)
                {
                    Line = Math.Round(defenceDiagramSettingsVm.hzPeopleUp[i], 3).ToString(CultureInfo.GetCultureInfo("en-US")) + "[$]" + Math.Round(defenceDiagramSettingsVm.vPeople[i], 3).ToString(CultureInfo.GetCultureInfo("en-US"));
                    sw.WriteLine(Line);
                }
                Line = @"Тахограмма ""оборудование""";
                sw.WriteLine(Line);
                for (int i = 0; i < _points; i++)
                {
                    Line = Math.Round(defenceDiagramSettingsVm.hzEquipmentUp[i], 3).ToString(CultureInfo.GetCultureInfo("en-US")) + "[$]" + Math.Round(defenceDiagramSettingsVm.vEquipment[i], 3).ToString(CultureInfo.GetCultureInfo("en-US"));
                    sw.WriteLine(Line);
                }
                Line = @"Тахограмма ""ревизия""";
                sw.WriteLine(Line);
                for (int i = 0; i < _points; i++)
                {
                    Line = Math.Round(defenceDiagramSettingsVm.hzRevision[i], 3).ToString(CultureInfo.GetCultureInfo("en-US")) + "[$]" + Math.Round(defenceDiagramSettingsVm.vRevision[i], 3).ToString(CultureInfo.GetCultureInfo("en-US"));
                    sw.WriteLine(Line);
                }
                sw.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ReadParamFromFile(string path)
        {
            variableParametersName.Clear();
            variableParametersValue.Clear();
            string Line;
            string[] strArr;
            //int k = 0;
            string index;
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            while (sr.EndOfStream != true)
            {
                Line = sr.ReadLine();
                //if(Line=="")
                //break;
                Line = Line.TrimEnd(' ');
                string[] separator = new string[] { "[$]" };
                strArr = Line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArr.Length; i++)
                {
                    if (i == 0)
                    {
                        variableParametersName.Add(strArr[i].Trim());
                    }
                    else if (i == 1)
                    {
                        variableParametersValue.Add(strArr[i].Trim());
                    }
                }
            }
            sr.Close();
        }

        private void buttonSaveParameters_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewVariableParameters.RowCount; i++)
            {
                variableParametersName[i] = dataGridViewVariableParameters[1, i].Value.ToString();
                variableParametersValue[i] = Convert.ToDouble(dataGridViewVariableParameters[2, i].Value, CultureInfo.GetCultureInfo("en-US")).ToString(CultureInfo.GetCultureInfo("en-US"));
            }
            WriteParamToFile(SolveDefDiagramParametersFilePath);
            InitData();
        }

        private void dataGridViewVariableParameters_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = (TextBox)e.Control;
            tb.KeyPress -= tb_KeyPress;
            tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
        }

        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            string vlCell = ((TextBox)sender).Text;
            //bool temp = (vlCell.IndexOf(".") == -1);
            //проверка ввода
            if (dataGridViewVariableParameters.Rows[dataGridViewVariableParameters.CurrentRow.Index].Cells[1].IsInEditMode == true)
                if (e.KeyChar == '/')
                    e.Handled = true;
            if (dataGridViewVariableParameters.Rows[dataGridViewVariableParameters.CurrentRow.Index].Cells[2].IsInEditMode == true)
                if (!Char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '\b'))
                    e.Handled = true;
        }

        private void dataGridViewVariableParameters_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (e.RowIndex == 0)
                {
                    if (dataGridViewVariableParameters[2, 0].Value == null || dataGridViewVariableParameters[2, 0].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 0].Value = variableParametersValue[0];
                    else if (Convert.ToDouble(dataGridViewVariableParameters[2, 0].Value, CultureInfo.GetCultureInfo("en-US")) > 16 || Convert.ToDouble(dataGridViewVariableParameters[2, 0].Value, CultureInfo.GetCultureInfo("en-US")) <= 0)
                    {
                        dataGridViewVariableParameters[2, 0].Value = variableParametersValue[0];
                        MessageBox.Show(@"Vм ""груз"" (0-й параметр) должен быть больше 0 и меньше либо равен 16 м/с", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (e.RowIndex == 1)
                {
                    if (dataGridViewVariableParameters[2, 1].Value == null || dataGridViewVariableParameters[2, 1].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 1].Value = variableParametersValue[1];
                    else if (Convert.ToDouble(dataGridViewVariableParameters[2, 1].Value, CultureInfo.GetCultureInfo("en-US")) > 8 || Convert.ToDouble(dataGridViewVariableParameters[2, 1].Value, CultureInfo.GetCultureInfo("en-US")) <= 0)
                    {
                        dataGridViewVariableParameters[2, 1].Value = variableParametersValue[1];
                        MessageBox.Show(@"Vм ""люди"" (1-й параметр) должен быть больше 0 и меньше либо равен 8 м/с", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (e.RowIndex == 2)
                {
                    if (dataGridViewVariableParameters[2, 2].Value == null || dataGridViewVariableParameters[2, 2].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 2].Value = variableParametersValue[2];
                    else if (Convert.ToDouble(dataGridViewVariableParameters[2, 2].Value, CultureInfo.GetCultureInfo("en-US")) > 16 || Convert.ToDouble(dataGridViewVariableParameters[2, 2].Value, CultureInfo.GetCultureInfo("en-US")) <= 0)
                    {
                        dataGridViewVariableParameters[2, 2].Value = variableParametersValue[2];
                        MessageBox.Show(@"Vм ""оборудование"" (2-й параметр) должен быть больше 0 и меньше либо равен 16 м/с", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (e.RowIndex == 3)
                {
                    if (dataGridViewVariableParameters[2, 3].Value == null || dataGridViewVariableParameters[2, 3].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 3].Value = variableParametersValue[3];
                    else if (Convert.ToDouble(dataGridViewVariableParameters[2, 3].Value, CultureInfo.GetCultureInfo("en-US")) > 3 || Convert.ToDouble(dataGridViewVariableParameters[2, 3].Value, CultureInfo.GetCultureInfo("en-US")) <= 0)
                    {
                        dataGridViewVariableParameters[2, 3].Value = variableParametersValue[3];
                        MessageBox.Show(@"Vм ""ревизия"" (3-й параметр) должен быть больше 0 и меньше либо равен 3 м/с", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (e.RowIndex == 4)
                {
                    if (dataGridViewVariableParameters[2, 4].Value == null || dataGridViewVariableParameters[2, 4].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 4].Value = variableParametersValue[4];
                    else if (Convert.ToDouble(dataGridViewVariableParameters[2, 4].Value, CultureInfo.GetCultureInfo("en-US")) > 1.5 || Convert.ToDouble(dataGridViewVariableParameters[2, 4].Value, CultureInfo.GetCultureInfo("en-US")) <= 0)
                    {
                        dataGridViewVariableParameters[2, 4].Value = variableParametersValue[4];
                        MessageBox.Show(@"Vп ""груз и оборудование"" (4-й параметр) должен быть больше 0 и меньше либо равен 1.5 м/с", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (e.RowIndex == 5)
                {
                    if (dataGridViewVariableParameters[2, 5].Value == null || dataGridViewVariableParameters[2, 5].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 5].Value = variableParametersValue[5];
                    else if (Convert.ToDouble(dataGridViewVariableParameters[2, 5].Value, CultureInfo.GetCultureInfo("en-US")) > 1 || Convert.ToDouble(dataGridViewVariableParameters[2, 5].Value, CultureInfo.GetCultureInfo("en-US")) <= 0)
                    {
                        dataGridViewVariableParameters[2, 5].Value = variableParametersValue[5];
                        MessageBox.Show(@"Vп ""люди"" (5-й параметр) должен быть больше 0 и меньше либо равен 1 м/с", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (e.RowIndex == 6)
                {
                    if (dataGridViewVariableParameters[2, 6].Value == null || dataGridViewVariableParameters[2, 6].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 6].Value = variableParametersValue[6];
                    else if (Convert.ToDouble(dataGridViewVariableParameters[2, 6].Value, CultureInfo.GetCultureInfo("en-US")) > 0.8 || Convert.ToDouble(dataGridViewVariableParameters[2, 6].Value, CultureInfo.GetCultureInfo("en-US")) <= 0)
                    {
                        dataGridViewVariableParameters[2, 6].Value = variableParametersValue[6];
                        MessageBox.Show(@"Vд (6-й параметр) должен быть больше 0 и меньше либо равен 0.8 м/с", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (e.RowIndex == 7)
                {
                    if (dataGridViewVariableParameters[2, 7].Value == null || dataGridViewVariableParameters[2, 7].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 7].Value = variableParametersValue[7];
                    else if (Convert.ToDouble(dataGridViewVariableParameters[2, 7].Value, CultureInfo.GetCultureInfo("en-US")) > 1 || Convert.ToDouble(dataGridViewVariableParameters[2, 7].Value, CultureInfo.GetCultureInfo("en-US")) <= 0)
                    {
                        dataGridViewVariableParameters[2, 7].Value = variableParametersValue[7];
                        MessageBox.Show(@"tос (7-й параметр) должен быть больше 0 и меньше либо равен 1 с", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (e.RowIndex == 8)
                {
                    if (dataGridViewVariableParameters[2, 8].Value == null || dataGridViewVariableParameters[2, 8].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 8].Value = variableParametersValue[8];
                    else if (Convert.ToDouble(dataGridViewVariableParameters[2, 8].Value, CultureInfo.GetCultureInfo("en-US")) > 1 || Convert.ToDouble(dataGridViewVariableParameters[2, 8].Value, CultureInfo.GetCultureInfo("en-US")) <= 0)
                    {
                        dataGridViewVariableParameters[2, 8].Value = variableParametersValue[8];
                        MessageBox.Show(@"tср (8-й параметр) должен быть больше 0 и меньше либо равен 1 с", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (e.RowIndex == 9)
                {
                    if (dataGridViewVariableParameters[2, 9].Value == null || dataGridViewVariableParameters[2, 9].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 9].Value = variableParametersValue[9];
                }
                else if (e.RowIndex == 10)
                {
                    if (dataGridViewVariableParameters[2, 10].Value == null || dataGridViewVariableParameters[2, 10].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 10].Value = variableParametersValue[10];
                }
                else if (e.RowIndex == 11)
                {
                    if (dataGridViewVariableParameters[2, 11].Value == null || dataGridViewVariableParameters[2, 11].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 11].Value = variableParametersValue[11];
                    else if (Convert.ToDouble(dataGridViewVariableParameters[2, 11].Value, CultureInfo.GetCultureInfo("en-US")) > 4 || Convert.ToDouble(dataGridViewVariableParameters[2, 11].Value, CultureInfo.GetCultureInfo("en-US")) < 1.5)
                    {
                        dataGridViewVariableParameters[2, 11].Value = variableParametersValue[11];
                        MessageBox.Show(@"aпт ""груз и оборудование"" (11-й параметр) должен быть больше либо равен 1.5 и меньше либо равен 4 м/с2", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (e.RowIndex == 12)
                {
                    if (dataGridViewVariableParameters[2, 12].Value == null || dataGridViewVariableParameters[2, 12].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 12].Value = variableParametersValue[12];
                    else if (Convert.ToDouble(dataGridViewVariableParameters[2, 12].Value, CultureInfo.GetCultureInfo("en-US")) > 4 || Convert.ToDouble(dataGridViewVariableParameters[2, 12].Value, CultureInfo.GetCultureInfo("en-US")) < 1.5)
                    {
                        dataGridViewVariableParameters[2, 12].Value = variableParametersValue[12];
                        MessageBox.Show(@"aпт ""люди"" (12-й параметр) должен быть больше либо равен 1.5 и меньше либо равен 4 м/с2", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (e.RowIndex == 13)
                {
                    if (dataGridViewVariableParameters[2, 13].Value == null || dataGridViewVariableParameters[2, 13].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 13].Value = variableParametersValue[13];
                    else if (Convert.ToDouble(dataGridViewVariableParameters[2, 13].Value, CultureInfo.GetCultureInfo("en-US")) > 1.5 || Convert.ToDouble(dataGridViewVariableParameters[2, 13].Value, CultureInfo.GetCultureInfo("en-US")) < 0.3)
                    {
                        dataGridViewVariableParameters[2, 13].Value = variableParametersValue[13];
                        MessageBox.Show(@"aр ""груз и оборудование"" (13-й параметр) должен быть больше либо равен 0.3 и меньше либо равен 1.5 м/с2", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (e.RowIndex == 14)
                {
                    if (dataGridViewVariableParameters[2, 14].Value == null || dataGridViewVariableParameters[2, 14].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 14].Value = variableParametersValue[14];
                    else if (Convert.ToDouble(dataGridViewVariableParameters[2, 14].Value, CultureInfo.GetCultureInfo("en-US")) > 1.5 || Convert.ToDouble(dataGridViewVariableParameters[2, 14].Value, CultureInfo.GetCultureInfo("en-US")) < 0.3)
                    {
                        dataGridViewVariableParameters[2, 14].Value = variableParametersValue[14];
                        MessageBox.Show(@"aр ""люди"" (14-й параметр) должен быть больше либо равен 0.3 и меньше либо равен 1.5 м/с2", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (e.RowIndex == 15)
                {
                    if (dataGridViewVariableParameters[2, 15].Value == null || dataGridViewVariableParameters[2, 15].Value.ToString() == ".")
                        dataGridViewVariableParameters[2, 15].Value = variableParametersValue[15];
                    else if (Convert.ToDouble(dataGridViewVariableParameters[2, 15].Value, CultureInfo.GetCultureInfo("en-US")) > 1.5 || Convert.ToDouble(dataGridViewVariableParameters[2, 15].Value, CultureInfo.GetCultureInfo("en-US")) < 0.3)
                    {
                        dataGridViewVariableParameters[2, 15].Value = variableParametersValue[15];
                        MessageBox.Show(@"deltaV (15-й параметр) должен быть больше либо равен 0.3 и меньше либо равен 1.5 м/с", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (e.RowIndex > 15)
                {
                    for (int i = 16; i < 20; i++)
                    {
                        if (dataGridViewVariableParameters[2, i].Value == null || dataGridViewVariableParameters[2, i].Value.ToString() == ".")
                            dataGridViewVariableParameters[2, i].Value = variableParametersValue[i];
                    }
                    for (int i = 20; i < variableParametersCount; i++)
                    {
                        if (dataGridViewVariableParameters[2, i].Value == null || dataGridViewVariableParameters[2, i].Value.ToString() == ".")
                            dataGridViewVariableParameters[2, i].Value = 0;
                    }
                }
            }
            if (e.ColumnIndex == 1)
            {
                for (int i = 0; i < variableParametersCount; i++)
                {
                    if (dataGridViewVariableParameters[2, i].Value == null)
                        dataGridViewVariableParameters[2, i].Value = i;
                }
            }
            if (e.ColumnIndex == 2)
            {
                CalculateParameters();
            }
        }

        private void UpdateDiagramData()
        {
            var defenceDiagramSettingsVm = new DefenceDiagramSettingsVm(variableParametersValue);
            if (defenceDiagramSettingsVm.HzCrossHkDown == 0 && defenceDiagramSettingsVm.HzCrossHkUp == 0)
            {
                dataGridViewDefenceDiagramDown.RowCount = defenceDiagramSettingsVm.vVeight.Count();
                dataGridViewDefenceDiagramUp.RowCount = defenceDiagramSettingsVm.vVeight.Count();
                for (int i = 0; i < defenceDiagramSettingsVm.vVeight.Count(); i++)
                {
                    dataGridViewDefenceDiagramDown[0, i].Value = i;
                    dataGridViewDefenceDiagramDown[1, i].Value = Math.Round(defenceDiagramSettingsVm.vVeight[i], 3);
                    dataGridViewDefenceDiagramDown[2, i].Value = Math.Round(defenceDiagramSettingsVm.hzVeightDown[i], 3);
                    dataGridViewDefenceDiagramDown[3, i].Value = Math.Round(defenceDiagramSettingsVm.vPeople[i], 3);
                    dataGridViewDefenceDiagramDown[4, i].Value = Math.Round(defenceDiagramSettingsVm.hzPeopleDown[i], 3);
                    dataGridViewDefenceDiagramDown[5, i].Value = Math.Round(defenceDiagramSettingsVm.vEquipment[i], 3);
                    dataGridViewDefenceDiagramDown[6, i].Value = Math.Round(defenceDiagramSettingsVm.hzEquipmentDown[i], 3);
                    dataGridViewDefenceDiagramDown[7, i].Value = Math.Round(defenceDiagramSettingsVm.vRevision[i], 3);
                    dataGridViewDefenceDiagramDown[8, i].Value = Math.Round(defenceDiagramSettingsVm.hzRevision[i], 3);
                    dataGridViewDefenceDiagramUp[0, i].Value = i;
                    dataGridViewDefenceDiagramUp[1, i].Value = Math.Round(defenceDiagramSettingsVm.vVeight[i], 3);
                    dataGridViewDefenceDiagramUp[2, i].Value = Math.Round(defenceDiagramSettingsVm.hzVeightUp[i], 3);
                    dataGridViewDefenceDiagramUp[3, i].Value = Math.Round(defenceDiagramSettingsVm.vPeople[i], 3);
                    dataGridViewDefenceDiagramUp[4, i].Value = Math.Round(defenceDiagramSettingsVm.hzPeopleUp[i], 3);
                    dataGridViewDefenceDiagramUp[5, i].Value = Math.Round(defenceDiagramSettingsVm.vEquipment[i], 3);
                    dataGridViewDefenceDiagramUp[6, i].Value = Math.Round(defenceDiagramSettingsVm.hzEquipmentUp[i], 3);
                    dataGridViewDefenceDiagramUp[7, i].Value = Math.Round(defenceDiagramSettingsVm.vRevision[i], 3);
                    dataGridViewDefenceDiagramUp[8, i].Value = Math.Round(defenceDiagramSettingsVm.hzRevision[i], 3);
                }
            }
            else if (defenceDiagramSettingsVm.HzCrossHkDown == 1 || defenceDiagramSettingsVm.HzCrossHkUp == 1)
            {
                MessageBox.Show("Защитная диаграмма пересекается с критической!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void MakeGraphic()
        {
            var defenceDiagramSettingsVm = new DefenceDiagramSettingsVm(variableParametersValue);
            if (defenceDiagramSettingsVm.HzCrossHkDown == 1 || defenceDiagramSettingsVm.HzCrossHkUp == 1)
            {
                MessageBox.Show("Защитная диаграмма пересекается с критической!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                var plotDefenceDiagram = new OxyPlot.WindowsForms.Plot { Model = new PlotModel(), Dock = DockStyle.Fill };
                this.tabControl1.TabPages[3].Controls.Clear();
                this.tabControl1.TabPages[3].Controls.Add(plotDefenceDiagram);
                //Legend
                plotDefenceDiagram.Model.PlotType = PlotType.XY;
                //plotDefenceDiagram.Model.LegendTitle = "Legend";
                plotDefenceDiagram.Model.LegendOrientation = LegendOrientation.Horizontal;
                plotDefenceDiagram.Model.LegendPlacement = LegendPlacement.Outside;
                plotDefenceDiagram.Model.LegendPosition = LegendPosition.TopRight;
                plotDefenceDiagram.Model.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
                plotDefenceDiagram.Model.LegendBorder = OxyColors.Gray;
                //Axis
                var xAxis = new LinearAxis(AxisPosition.Bottom, 0)
                {
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    Title = "Путь (м)",
                    Minimum = -_mineConfig.MainViewConfig.Border.Value,
                    Maximum = -_mineConfig.MainViewConfig.BorderZero.Value
                };
                plotDefenceDiagram.Model.Axes.Add(xAxis);
                var yAxis = new LinearAxis(AxisPosition.Left, 0)
                {
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    Title = "Скорость (м/с)",
                    Minimum = 0,
                    Maximum = 1.2 * Convert.ToDouble(variableParametersValue[0], CultureInfo.GetCultureInfo("en-US"))
                };
                plotDefenceDiagram.Model.Axes.Add(yAxis);
                // Create Line series
                var s1 = new LineSeries { Title = "Груз", StrokeThickness = 1, Color = OxyColors.Blue };
                var s2 = new LineSeries { Title = "Люди", StrokeThickness = 1, Color = OxyColors.Green };
                var s3 = new LineSeries { Title = "Оборудование", StrokeThickness = 1, Color = OxyColors.Red };
                var s4 = new LineSeries { Title = "Ревизия", StrokeThickness = 1, Color = OxyColors.Orange };
                for (int i = 0; i < defenceDiagramSettingsVm.DiagramVeight.Count(); i++)
                {
                    s1.Points.Add(new DataPoint(-defenceDiagramSettingsVm.DiagramVeight[i].X,
                        defenceDiagramSettingsVm.DiagramVeight[i].Y));
                    s2.Points.Add(new DataPoint(-defenceDiagramSettingsVm.DiagramPeople[i].X,
                        defenceDiagramSettingsVm.DiagramPeople[i].Y));
                    s3.Points.Add(new DataPoint(-defenceDiagramSettingsVm.DiagramEquipment[i].X,
                        defenceDiagramSettingsVm.DiagramEquipment[i].Y));
                }
                for (int i = 0; i < defenceDiagramSettingsVm.DiagramRevision.Count(); i++)
                {
                    s4.Points.Add(new DataPoint(-defenceDiagramSettingsVm.DiagramRevision[i].X,
                        defenceDiagramSettingsVm.DiagramRevision[i].Y));
                }
                // add Series and Axis to plot model
                plotDefenceDiagram.Model.Series.Add(s1);
                plotDefenceDiagram.Model.Series.Add(s2);
                plotDefenceDiagram.Model.Series.Add(s3);
                plotDefenceDiagram.Model.Series.Add(s4);
            }
        }

        private void DefenceDiagramComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_plotDefenceDiagram != null)
                _plotDefenceDiagram.Dispose();
            _plotDefenceDiagram = new OxyPlot.WindowsForms.Plot { Model = new PlotModel(), Dock = DockStyle.Fill };
            this.splitContainerDefenceDiagram.Panel2.Controls.Clear();
            this.splitContainerDefenceDiagram.Panel2.Controls.Add(_plotDefenceDiagram);
            MakeSolvedDiagramGraphic();
        }

        private void MakeSolvedDiagramGraphic()
        {
            var selectedIndex = SelectDataToDiagram();
            var defenceDiagramSettingsVm = new DefenceDiagramSettingsVm(variableParametersValue);
            if (defenceDiagramSettingsVm.HzCrossHkDown == 1 || defenceDiagramSettingsVm.HzCrossHkUp == 1)
            {
                MessageBox.Show("Защитная диаграмма пересекается с критической!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                //Legend
                _plotDefenceDiagram.Model.PlotType = PlotType.XY;
                //plotDefenceDiagram.Model.LegendTitle = "Legend";
                _plotDefenceDiagram.Model.LegendOrientation = LegendOrientation.Horizontal;
                _plotDefenceDiagram.Model.LegendPlacement = LegendPlacement.Outside;
                _plotDefenceDiagram.Model.LegendPosition = LegendPosition.TopRight;
                _plotDefenceDiagram.Model.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
                _plotDefenceDiagram.Model.LegendBorder = OxyColors.Gray;
                //Axis
                var xAxis = new LinearAxis(AxisPosition.Bottom, 0)
                {
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    Title = "Путь (м)",
                    Minimum = _selectedHz[0],
                    Maximum = -_mineConfig.MainViewConfig.BorderZero.Value
                };
                if (selectedIndex > 3)
                {
                    xAxis.Minimum = -_mineConfig.MainViewConfig.Border.Value;
                    xAxis.Maximum = _selectedHz[0];
                }
                _plotDefenceDiagram.Model.Axes.Add(xAxis);
                var yAxis = new LinearAxis(AxisPosition.Left, 0)
                {
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    Title = "Скорость (м/с)",
                    Minimum = 0,
                    Maximum = 1.2 * Convert.ToDouble(_selectedV[0], CultureInfo.GetCultureInfo("en-US"))
                };
                _plotDefenceDiagram.Model.Axes.Add(yAxis);
                // Create Line series
                var s1 = new LineSeries { StrokeThickness = 1, Color = OxyColors.Red };
                for (int i = 0; i < _points; i++)
                {
                    s1.Points.Add(new DataPoint(_selectedHz[i], _selectedV[i]));
                }
                // add Series and Axis to plot model
                _plotDefenceDiagram.Model.Series.Add(s1);
            }
        }

        private int SelectDataToDiagram()
        {
            var defenceDiagramSettingsVm = new DefenceDiagramSettingsVm(variableParametersValue);
            int index = DefenceDiagramComboBox.SelectedIndex;
            switch (index)
            {
                case 0:
                    for (int i = 0; i < _points; i++)
                    {
                        _selectedV[i] = defenceDiagramSettingsVm.vVeight[i];
                        _selectedHz[i] = defenceDiagramSettingsVm.hzVeightUp[i];
                    }
                    break;
                case 1:
                    for (int i = 0; i < _points; i++)
                    {
                        _selectedV[i] = defenceDiagramSettingsVm.vPeople[i];
                        _selectedHz[i] = defenceDiagramSettingsVm.hzPeopleUp[i];
                    }
                    break;
                case 2:
                    for (int i = 0; i < _points; i++)
                    {
                        _selectedV[i] = defenceDiagramSettingsVm.vEquipment[i];
                        _selectedHz[i] = defenceDiagramSettingsVm.hzEquipmentUp[i];
                    }
                    break;
                case 3:
                    for (int i = 0; i < _points; i++)
                    {
                        _selectedV[i] = defenceDiagramSettingsVm.vRevision[i];
                        _selectedHz[i] = defenceDiagramSettingsVm.hzRevision[i];
                    }
                    break;
                case 4:
                    for (int i = 0; i < _points; i++)
                    {
                        _selectedV[i] = defenceDiagramSettingsVm.vVeight[i];
                        _selectedHz[i] = defenceDiagramSettingsVm.hzVeightDown[i];
                    }
                    break;
                case 5:
                    for (int i = 0; i < _points; i++)
                    {
                        _selectedV[i] = defenceDiagramSettingsVm.vPeople[i];
                        _selectedHz[i] = defenceDiagramSettingsVm.hzPeopleDown[i];
                    }
                    break;
                case 6:
                    for (int i = 0; i < _points; i++)
                    {
                        _selectedV[i] = defenceDiagramSettingsVm.vEquipment[i];
                        _selectedHz[i] = defenceDiagramSettingsVm.hzEquipmentDown[i];
                    }
                    break;
            }
            return index;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewVariableParameters.RowCount; i++)
            {
                variableParametersName[i] = dataGridViewVariableParameters[1, i].Value.ToString();
                variableParametersValue[i] = Convert.ToDouble(dataGridViewVariableParameters[2, i].Value, CultureInfo.GetCultureInfo("en-US")).ToString(CultureInfo.GetCultureInfo("en-US"));
            }
            int index = tabControl1.SelectedIndex;
            switch (index)
            {
                case 0:
                    break;
                case 1:
                    UpdateDiagramData();
                    break;
                case 2:
                    _plotDefenceDiagram = new OxyPlot.WindowsForms.Plot { Model = new PlotModel(), Dock = DockStyle.Fill };
                    this.splitContainerDefenceDiagram.Panel2.Controls.Add(_plotDefenceDiagram);
                    MakeSolvedDiagramGraphic();
                    break;
                case 3:
                    MakeGraphic();
                    break;
            }
        }

        private void buttonSavePoints_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewVariableParameters.RowCount; i++)
            {
                variableParametersName[i] = dataGridViewVariableParameters[1, i].Value.ToString();
                variableParametersValue[i] = Convert.ToDouble(dataGridViewVariableParameters[2, i].Value, CultureInfo.GetCultureInfo("en-US")).ToString(CultureInfo.GetCultureInfo("en-US"));
            }
            WritePointsToFile(SolvedDefDiagramPointsFilePath);
        }

    }
}
