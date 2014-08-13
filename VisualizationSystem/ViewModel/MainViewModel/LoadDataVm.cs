using System.Drawing;
using System.IO;
using System.Threading;
using ML.ConfigSettings.Model;
using ML.ConfigSettings.Services;
using ML.DataExchange.Model;
using VisualizationSystem.Model;
using VisualizationSystem.Model.RichTextBoxData;

namespace VisualizationSystem.ViewModel.MainViewModel
{
    class LoadDataVm
    {
        public LoadDataVm()
        {
            _mineConfig = IoC.Resolve<MineConfig>();
            colors = new Color[] { Color.Red, Color.Red, Color.Green, Color.Green, Color.Gray };
            colors2 = new Color[] { Color.Gray, Color.Red, Color.Green, Color.Gray, Color.Gray };
            textFirst = new string[]{ "ЗАТВОР \r\nОТКРЫТ", "СКИП \r\nРАЗГРУЖАЕТСЯ", "СКИП \r\nРАЗГРУЗИЛСЯ", "ЗАТВОР \r\nЗАКРЫТ", "" };
            //textSecod = new string[]{ "ДОЗАТОР \r\nОТКРЫТ", "СКИП \r\nЗАГРУЖАЕТСЯ", "СКИП \r\nЗАГРУЖЕН", "ДОЗАТОР \r\nЗАКРЫТ", "" };
            textSecod = new string[] { "", "СКИП \r\nЗАГРУЖАЕТСЯ", "СКИП \r\nЗАГРУЖЕН", "", "" };
        }

        public void SolveLoadData(Parameters parameters)
        {
            LoadData = new RichTextBoxData[4];
            for (int i = 0; i < 4; i++)
                LoadData[i] = new RichTextBoxData();
            // сообщения разгрузки (вверху)
            if (parameters.f_ostanov == 1 && parameters.s < (_mineConfig.MainViewConfig.BorderZero.Value + 0.5) && parameters.s > (_mineConfig.MainViewConfig.BorderZero.Value - 0.5))
            {
                for (int i = 1; i <= 5; i++)
                {
                    if (parameters.unload_state == i)
                    {
                        int i1 = i - 1;
                        if (_mineConfig.MainViewConfig.LeftSosud == SosudType.Skip)
                        {
                            LoadData[0].BackColor = colors[i1];
                            LoadData[0].Text = textFirst[i1];
                        }
                    }
                    if (parameters.load_state == i)
                    {
                        int i1 = i - 1;
                        if (_mineConfig.MainViewConfig.RightSosud == SosudType.Skip && RightPanelVm._firstTime == 1)
                        {
                            LoadData[3].BackColor = colors2[i1];
                            LoadData[3].Text = textSecod[i1];
                        }
                    }
                }
            }
            else
            {
                if (_mineConfig.MainViewConfig.LeftSosud == SosudType.Skip)
                {
                    LoadData[0].BackColor = Color.Gray;
                    LoadData[0].Text = "";
                }
                if (_mineConfig.MainViewConfig.RightSosud == SosudType.Skip && RightPanelVm._firstTime == 1)
                {
                    LoadData[3].BackColor = Color.Gray;
                    LoadData[3].Text = "";
                }
            }
            // сообщения загрузки (внизу)
            if (parameters.f_ostanov == 1 && parameters.s > (_mineConfig.MainViewConfig.Border.Value - 0.5) && parameters.s < (_mineConfig.MainViewConfig.Border.Value + 0.5))
            {
                for (int i = 1; i <= 5; i++)
                {
                    if (parameters.load_state == i)
                    {
                        int i1 = i - 1;
                        if (_mineConfig.MainViewConfig.LeftSosud == SosudType.Skip)
                        {
                            LoadData[1].BackColor = colors2[i1];
                            LoadData[1].Text = textSecod[i1];
                        }
                    }
                    if (parameters.unload_state == i)
                    {
                        int i1 = i - 1;
                        if (_mineConfig.MainViewConfig.RightSosud == SosudType.Skip && RightPanelVm._firstTime == 1)
                        {
                            LoadData[2].BackColor = colors[i1];
                            LoadData[2].Text = textFirst[i1];
                        }
                    }
                }
            }
            else
            {
                if (_mineConfig.MainViewConfig.LeftSosud == SosudType.Skip)
                {
                    LoadData[1].BackColor = Color.Gray;
                    LoadData[1].Text = "";
                }
                if (_mineConfig.MainViewConfig.RightSosud == SosudType.Skip && RightPanelVm._firstTime == 1)
                {
                    LoadData[2].BackColor = Color.Gray;
                    LoadData[2].Text = "";
                }
            }
        }

        public RichTextBoxData[] GetLoadData()
        {
            return LoadData;
        }

        public RichTextBoxData[] LoadData { get; private set; }
        private MineConfig _mineConfig;
        private Color[] colors;
        private Color[] colors2;
        private string[] textFirst;
        private string[] textSecod;
    }
}
