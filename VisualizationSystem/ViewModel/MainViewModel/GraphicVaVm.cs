using ML.ConfigSettings.Services;
using ML.DataExchange.Model;
using VisualizationSystem.Model;
using VisualizationSystem.Model.GraphicData;

namespace VisualizationSystem.ViewModel.MainViewModel
{
    class GraphicVaVm
    {
        public GraphicVaVm(Parameters parameter)
        {
            _parameters = parameter;
            Graphic = new GraphicData[3];
            for (int i = 0; i < 3; i++)
                Graphic[i] = new GraphicData();

            SolveNewGraphicPoint();
        }

        private void SolveNewGraphicPoint()
        {
            if (_parameters.f_ostanov == 1)
                was_ostanov = 1;
            if (_parameters.f_start == 1 || _parameters.f_back == 1)
            {
                Graphic[0].X = _parameters.s * (-1);
                Graphic[0].Y = _parameters.v/(IoC.Resolve<MineConfig>().MainViewConfig.MaxSpeed.Value/100);
                Graphic[1].X = _parameters.s * (-1);
                Graphic[1].Y = _parameters.tok_anchor / (IoC.Resolve<MineConfig>().MainViewConfig.MaxTokAnchor.Value / 100);
                Graphic[2].X = _parameters.s * (-1);
                Graphic[2].Y = _parameters.tok_excitation / (IoC.Resolve<MineConfig>().MainViewConfig.MaxTokExcitation.Value / 100);
            }
        }

        public GraphicData[] Graphic { get; private set; }
        private Parameters _parameters;
        public int was_ostanov { get; set; }
    }
}
