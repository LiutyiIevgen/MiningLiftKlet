using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using ML.ConfigSettings.Services;
using ML.DataExchange.Model;
using VisualizationSystem.Model;
using VisualizationSystem.Services;
using VisualizationSystem.View.UserControls.Setting;
using GeneralLogEventType = ML.DataExchange.Model.GeneralLogEventType;

namespace VisualizationSystem.ViewModel.MainViewModel
{
    class DataBoxVm
    {
        public DataBoxVm()
        {
            DataBoxes = new List<string>();
            _mineConfig = IoC.Resolve<MineConfig>();
            _canStateService = IoC.Resolve<CanStateService>();
        }

        public void SolveDataBoxes(Parameters parameters)
        {
            DataBoxes.Clear();
            if (parameters.v > _mineConfig.MainViewConfig.MaxDopRuleSpeed.Value)
            {
                DataBoxes.Add(Convert.ToString(Math.Round(-parameters.s, 0), CultureInfo.GetCultureInfo("en-US")));
                DataBoxes.Add(Convert.ToString(Math.Round(-(parameters.s_two), 0), CultureInfo.GetCultureInfo("en-US")));
                DataBoxes.Add(Convert.ToString(Math.Round(parameters.v, 2), CultureInfo.GetCultureInfo("en-US")));
                DataBoxes.Add(Convert.ToString(Math.Round(parameters.tok_anchor, 2), CultureInfo.GetCultureInfo("en-US")));
                DataBoxes.Add(Convert.ToString(Math.Round(parameters.tok_excitation, 2), CultureInfo.GetCultureInfo("en-US")));
            }
            else
            {
                DataBoxes.Add(Convert.ToString(Math.Round(-parameters.s, 2), CultureInfo.GetCultureInfo("en-US")));
                DataBoxes.Add(Convert.ToString(Math.Round(-(parameters.s_two), 2), CultureInfo.GetCultureInfo("en-US")));
                DataBoxes.Add(Convert.ToString(Math.Round(parameters.v, 2), CultureInfo.GetCultureInfo("en-US")));
                DataBoxes.Add(Convert.ToString(Math.Round(parameters.tok_anchor, 2), CultureInfo.GetCultureInfo("en-US")));
                DataBoxes.Add(Convert.ToString(Math.Round(parameters.tok_excitation, 2), CultureInfo.GetCultureInfo("en-US")));
            }

            
        }

        public List<string> GetDataBoxes()
        {
            return DataBoxes;
        }

        public List<string> DataBoxes { get; private set; }
        public Color CanStateColor {
            get {
                return _canStateService.IsConnected ? Color.LimeGreen : Color.Red;
            }
        }
        private MineConfig _mineConfig;
        private CanStateService _canStateService;
    }
}
