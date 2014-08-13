using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualizationSystem.Model.Settings;

namespace VisualizationSystem.ViewModel
{
    class ParametersSettingsVm
    {

        public void ReadFromFile(string path)
        {
            _parametersSettingsDatas = new List<ParametersSettingsData>();
                string Line;
                string[] strArr;
                int k = 0;
                string index;
                FileStream fs = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                while (sr.EndOfStream != true)
                {
                    ParametersSettingsData paramSettingsData = new ParametersSettingsData();
                    Line = sr.ReadLine();
                    //if(Line=="")
                        //break;
                    Line = Line.TrimEnd(' ');
                    string[] separator = new string[]{"[$]"};
                    strArr = Line.Split(separator,StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < strArr.Length; i++)
                    {
                        if (i == 0)
                        {
                            index = strArr[i].Trim();
                            index = index.Substring(2);
                            paramSettingsData.Id = Convert.ToInt32(index, 16);
                        }
                        else if (i == 1)
                        {
                            paramSettingsData.Name = strArr[i].Trim();
                        }
                        else if (i == 2)
                        {
                            paramSettingsData.Type = strArr[i].Trim();
                        }
                        else if (i == 3)
                        {
                            paramSettingsData.Value = strArr[i].Trim();
                        }
                        else if (i == 4)
                        {
                            paramSettingsData.Value = "Двоичные данные";
                            paramSettingsData.CodtDomainArray = new CodtDomainData[20];
                            int j = 0;
                            foreach (var coord in strArr[i].Split('/'))
                            {
                                paramSettingsData.CodtDomainArray[j++] = new CodtDomainData()
                                {
                                    Coordinate = int.Parse(coord)
                                };
                            }
                        }
                        else if (i == 5)
                        {
                            int j = 0;
                            foreach (var speed in strArr[i].Split('/'))
                            {
                                paramSettingsData.CodtDomainArray[j++].Speed = int.Parse(speed);
                            }
                        }
                    }
                    _parametersSettingsDatas.Add(paramSettingsData);
                }
                sr.Close();
        }

        public void WriteToFile(string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                string Line;
                foreach (var paramData in _parametersSettingsDatas)
                {
                    if (paramData.Type == "codtDomain")
                    {
                        Line = "0x" + Convert.ToString(paramData.Id, 16) + "[$]" + paramData.Name + "[$]" + paramData.Type + "[$]" + "Двоичные_данные" + "[$]";
                        paramData.CodtDomainArray.ToList().ForEach(c => Line += c.Coordinate + "/");
                        Line = Line.Substring(0, Line.Length - 1);
                        Line += "[$]";
                        paramData.CodtDomainArray.ToList().ForEach(c => Line += c.Speed + "/");
                        Line = Line.Substring(0, Line.Length - 1);
                    }
                    else
                        Line = "0x" + Convert.ToString(paramData.Id, 16) + "[$]" + paramData.Name + "[$]" + paramData.Type + "[$]" + paramData.Value;
                    sw.WriteLine(Line);
                }
                sw.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public List<ParametersSettingsData> ParametersSettingsDatas
        {
            get
            {
                return _parametersSettingsDatas;
            }
            set
            {
                _parametersSettingsDatas = value;
            }
        }
        private List<ParametersSettingsData> _parametersSettingsDatas;

    }
}
