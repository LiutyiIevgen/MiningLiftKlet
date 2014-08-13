using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ML.ConfigSettings.Services;
using ML.DataExchange.Model;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using VisualizationSystem.Model;

namespace VisualizationSystem.View.UserControls.GeneralView
{
    public partial class CycleUC : UserControl
    {
        public CycleUC()
        {
            InitializeComponent();
            plotCycle = new OxyPlot.WindowsForms.Plot { Model = new PlotModel(), Dock = DockStyle.Fill };
            this.panel1.Controls.Add(plotCycle);
            plotCycle.Model.PlotType = PlotType.XY;
            //Axis
            plotCycle.Model.Axes.Add(xAxis);
            plotCycle.Model.Axes.Add(yAxis);
            //
            for (int i = 0; i < checkedListBoxGraphic.Items.Count; i++)
                checkedListBoxGraphic.SetItemChecked(i, true);
            _mineConfig = IoC.Resolve<MineConfig>();
            _wasOstanov = 0;
            plotCycle.Model.Series.Add(s1);
            plotCycle.Model.Series.Add(s2);
            plotCycle.Model.Series.Add(s3);
            plotCycle.Model.Series.Add(s4);
            
            new Thread(RefreshThreadHandler) {IsBackground = true, Priority = ThreadPriority.Lowest}.Start();
        }

        public void SetGraphicInterval()
        {
            this.Invoke((MethodInvoker)delegate
            {
                //Axis
                xAxis.Maximum = -_mineConfig.MainViewConfig.BorderZero.Value;
                xAxis.Minimum = -_mineConfig.MainViewConfig.Border.Value;
                //plotCycle.RefreshPlot(true);
            });
        }

        private void RefreshThreadHandler()
        {
            var lineSerie1 = plotCycle.Model.Series[0] as LineSeries;
            var lineSerie2 = plotCycle.Model.Series[1] as LineSeries;
            var lineSerie3 = plotCycle.Model.Series[2] as LineSeries;
            var lineSerie4 = plotCycle.Model.Series[3] as LineSeries;
            while (true)
            {
                if (_parameters == null)
                {
                    Thread.Sleep(100);
                    continue;
                }
                SetGraphicInterval();
                var param = _parameters as Parameters;
                if (param.f_start == 1 || param.f_back == 1)
                {
                    //var defenceDiagramVm = new DefenceDiagramVm(param);
                    if ((_fStartPrev != param.f_start) || (_fBackPrev != param.f_back))
                    {
                        this.Invoke((MethodInvoker) delegate
                        {
                            lineSerie1.Points.Clear();
                            lineSerie2.Points.Clear();
                            lineSerie3.Points.Clear();
                            lineSerie4.Points.Clear();
                            //s1.Points.Clear();
                            //s2.Points.Clear();
                            //s3.Points.Clear();
                            //s4.Points.Clear();
                        });
                        _wasOstanov = 0;
                        _fStartPrev = param.f_start;
                        _fBackPrev = param.f_back;
                     
                    }
                    this.Invoke((MethodInvoker) delegate
                    {
                        lineSerie1.Points.Add(new DataPoint(-param.s,
                            param.v/(_mineConfig.MainViewConfig.MaxSpeed.Value/100)));
                        lineSerie2.Points.Add(new DataPoint(-param.s,
                            param.tok_anchor/(_mineConfig.MainViewConfig.MaxTokAnchor.Value/100)));
                        lineSerie3.Points.Add(new DataPoint(-param.s,
                            param.tok_excitation/(_mineConfig.MainViewConfig.MaxTokExcitation.Value/100)));
                        lineSerie4.Points.Add(new DataPoint(-param.s,
                            param.defence_diagram/(_mineConfig.MainViewConfig.MaxSpeed.Value/100)));
                       // plotCycle.RefreshPlot(true);
                        plotCycle.InvalidatePlot(true);

                        int j = 0;
                        foreach (object item in checkedListBoxGraphic.Items)
                        {
                            plotCycle.Model.Series[j].IsVisible = checkedListBoxGraphic.CheckedItems.Contains(item);
                            j++;
                        }
                    });
                }
                /*if (param.f_ostanov == 1)
                {
                    _wasOstanov = 1;
                } */
                if(param.v == 0)
                {
                    _wasOstanov = 1;
                }
                Thread.Sleep(180);
            }
        }
        public void Refresh(Parameters parameters)
        {
            _parameters = parameters;
        }

        private MineConfig _mineConfig;
        private OxyPlot.WindowsForms.Plot plotCycle;
        private LineSeries s1 = new LineSeries { StrokeThickness = 1, Color = OxyColors.LimeGreen };
        private LineSeries s2 = new LineSeries { StrokeThickness = 1, Color = OxyColors.DarkOrange };
        private LineSeries s3 = new LineSeries { StrokeThickness = 1, Color = OxyColors.Yellow };
        private LineSeries s4 = new LineSeries { StrokeThickness = 1, Color = OxyColors.Red };
        private LinearAxis xAxis = new LinearAxis(AxisPosition.Bottom, 0)
        {
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot,
            Title = "м"
        };
        private LinearAxis yAxis = new LinearAxis(AxisPosition.Left, 0)
        {
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot,
            Title = "%",
            Minimum = -100,
            Maximum = 120
        };
        private int _wasOstanov;
        private int _fStartPrev = 1;
        private int _fBackPrev = 0;
        private int _prevDirection = 0;
        private Parameters _parameters;


    }
}
