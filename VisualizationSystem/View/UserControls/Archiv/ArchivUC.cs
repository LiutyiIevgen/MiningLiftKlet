using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
//using System.Windows.Forms.DataVisualization.Charting;
using ML.ConfigSettings.Services;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using VisualizationSystem.Model;
using VisualizationSystem.Model.DataBase;
using VisualizationSystem.Services;
using VisualizationSystem.View.Forms.Archiv;
using DataPoint = OxyPlot.DataPoint;
using SelectionMode = OxyPlot.SelectionMode;

namespace VisualizationSystem.View.UserControls.Archiv
{
    public partial class ArchivUC : UserControl
    {
        public ArchivUC()
        {
            InitializeComponent();
            _mineConfig = IoC.Resolve<MineConfig>();
            _checkedIOIndexes = new List<int>();
        }

        private void ArchivUC_Load(object sender, EventArgs e)
        {
            SetAnalogSignalsNamesList();
            //InitIOLineSeries();
            comboBoxOC.SelectedIndex = 0;
            comboBoxSync.SelectedIndex = 0;
            comboBoxMarkers.SelectedIndex = 0;
        }

        /*private void InitIOLineSeries()
        {
            _ioSeries[0] = new LineSeries { StrokeThickness = 1, Color = OxyColors.Blue, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.Blue, MarkerFill = OxyColors.Blue, MarkerSize = 2 };
            _ioSeries[1] = new LineSeries { StrokeThickness = 1, Color = OxyColors.Magenta, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.Magenta, MarkerFill = OxyColors.Magenta, MarkerSize = 2 };
            _ioSeries[2] = new LineSeries { StrokeThickness = 1, Color = OxyColors.Green, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.Green, MarkerFill = OxyColors.Green, MarkerSize = 2 };
            _ioSeries[3] = new LineSeries { StrokeThickness = 1, Color = OxyColors.IndianRed, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.IndianRed, MarkerFill = OxyColors.IndianRed, MarkerSize = 2 };
            _ioSeries[4] = new LineSeries { StrokeThickness = 1, Color = OxyColors.DarkOrange, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.DarkOrange, MarkerFill = OxyColors.DarkOrange, MarkerSize = 2 };
            _ioSeries[5] = new LineSeries { StrokeThickness = 1, Color = OxyColors.Yellow, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.Yellow, MarkerFill = OxyColors.Yellow, MarkerSize = 2 };
            _ioSeries[6] = new LineSeries { StrokeThickness = 1, Color = OxyColors.Red, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.Red, MarkerFill = OxyColors.Red, MarkerSize = 2 };
            _ioSeries[7] = new LineSeries { StrokeThickness = 1, Color = OxyColors.LimeGreen, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.LimeGreen, MarkerFill = OxyColors.Red, MarkerSize = 2 };
            _ioSeries[8] = new LineSeries { StrokeThickness = 1, Color = OxyColors.DeepSkyBlue, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.DeepSkyBlue, MarkerFill = OxyColors.Red, MarkerSize = 2 };
            _ioSeries[9] = new LineSeries { StrokeThickness = 1, Color = OxyColors.BlueViolet, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.BlueViolet, MarkerFill = OxyColors.Red, MarkerSize = 2 };
        } */

        private void SetAnalogSignalsNamesList()
        {
            var names = _dataBaseService.GetAnalogSignalsNames();
            int j = 0;
            foreach (var name in names)
            {
                listViewAnalogSignals.Items.Add(name);
                listViewAnalogSignals.Items[j].Checked = true;
                j++;
            }
            listViewAnalogSignals.Items[0].ForeColor = Color.Blue;
            listViewAnalogSignals.Items[1].ForeColor = Color.Magenta;
            listViewAnalogSignals.Items[2].ForeColor = Color.Green;
            listViewAnalogSignals.Items[3].ForeColor = Color.IndianRed;
            listViewAnalogSignals.Items[4].ForeColor = Color.DarkOrange;
            listViewAnalogSignals.Items[5].ForeColor = Color.Yellow;
            listViewAnalogSignals.Items[6].ForeColor = Color.Red;
            listViewAnalogSignals.Items[0].SubItems.Add((_mineConfig.MainViewConfig.Distance.Value + 10).ToString());
            listViewAnalogSignals.Items[1].SubItems.Add((_mineConfig.MainViewConfig.Distance.Value + 10).ToString());
            listViewAnalogSignals.Items[2].SubItems.Add(_mineConfig.MainViewConfig.MaxSpeed.Value.ToString());
            listViewAnalogSignals.Items[3].SubItems.Add(_maxSpeedUp.ToString());
            listViewAnalogSignals.Items[4].SubItems.Add(_mineConfig.MainViewConfig.MaxTokAnchor.Value.ToString());
            listViewAnalogSignals.Items[5].SubItems.Add(_mineConfig.MainViewConfig.MaxTokExcitation.Value.ToString());
            listViewAnalogSignals.Items[6].SubItems.Add(_mineConfig.MainViewConfig.MaxSpeed.Value.ToString());
        }

        private void MakeAnalogSignalsGraphic(List<List<List<string>>> analogSignals, List<DateTime> dateTimes)
        {
            this.Invoke((MethodInvoker)delegate
            {
            plotAnalogSignals = new OxyPlot.WindowsForms.Plot { Model = new PlotModel(), Dock = DockStyle.Fill };
            panel1.Controls.Clear(); 
            plotAnalogSignals.Model.PlotType = PlotType.XY;
            int oc = comboBoxOC.SelectedIndex;
            
            //Legend
            //Axis
            plotAnalogSignals.Model.Axes.Clear();
            var xAxis = new DateTimeAxis(AxisPosition.Bottom, dateTimes[0], dateTimes[dateTimes.Count - 1], null, null, DateTimeIntervalType.Auto)
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                //Title = "Дата",
                StringFormat = "dd-MM-yyyy HH:mm:ss",
                FontSize = 10,
                IsZoomEnabled = true,
                MajorStep = 1.0 / 24 / 60 / 2,
                MinorStep = 1.0 / 24 / 60 / 12,
                Minimum = DateTimeAxis.ToDouble(dateTimes[0]),
                Maximum = DateTimeAxis.ToDouble(dateTimes[dateTimes.Count - 1])
            };
            plotAnalogSignals.Model.Axes.Add(xAxis);
            var yAxis = new LinearAxis(AxisPosition.Left, 0)
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = "%",
                Minimum = -100,
                Maximum = 100
            };
            plotAnalogSignals.Model.Axes.Add(yAxis);
            plotAnalogSignals.Model.Axes[0].AxisChanged += new EventHandler<AxisChangedEventArgs>(xAxis_AxisChanged);
            //annotations
            lineAnnotationVertical = new LineAnnotation { Type = LineAnnotationType.Vertical, Color = OxyColors.Brown, Layer = AnnotationLayer.BelowSeries};
            lineAnnotationVertical.X = DateTimeAxis.ToDouble(dateTimes[1]);
            lineAnnotationVertical.MouseDown += (s, e) =>
            {
                if (e.ChangedButton != OxyMouseButton.Left)
                {
                    return;
                }
                if (_isMarkersActive == 1)
                {
                    lineAnnotationVertical.StrokeThickness *= 3;
                    plotAnalogSignals.Model.InvalidatePlot(false);
                    e.Handled = true;
                    if (_plotsSynchronized == 1)
                    {
                        for (int i = 0; i < _checkedIOIndexes.Count; i++)
                        {
                            lineAnnotationsIO[i].StrokeThickness *= 3;
                            plotIOSignals[i].Model.InvalidatePlot(false);
                        }
                    }
                }
            };
            lineAnnotationVertical.MouseMove += (s, e) =>
            {
                if (_isMarkersActive == 1)
                {
                    lineAnnotationVertical.X = lineAnnotationVertical.InverseTransform(e.Position).X;
                    plotAnalogSignals.Model.InvalidatePlot(false);
                    e.Handled = true;
                    if (_plotsSynchronized == 1)
                    {
                        for (int i = 0; i < _checkedIOIndexes.Count; i++)
                        {
                            lineAnnotationsIO[i].X = lineAnnotationVertical.X;
                            plotIOSignals[i].Model.InvalidatePlot(false);
                        }
                    }
                }
            };
            lineAnnotationVertical.MouseUp += (s, e) =>
            {
                if (_isMarkersActive == 1)
                {
                    lineAnnotationVertical.StrokeThickness /= 3;
                    plotAnalogSignals.Model.InvalidatePlot(false);
                    e.Handled = true;
                    if (_plotsSynchronized == 1)
                    {
                        for (int i = 0; i < _checkedIOIndexes.Count; i++)
                        {
                            lineAnnotationsIO[i].StrokeThickness /= 3;
                            plotIOSignals[i].Model.InvalidatePlot(false);
                        }
                    }
                }
            };
            lineAnnotationHorizontal = new LineAnnotation { Type = LineAnnotationType.Horizontal, Color = OxyColors.Brown, Layer = AnnotationLayer.BelowSeries};
            lineAnnotationHorizontal.Y = 90;
            lineAnnotationHorizontal.MouseDown += (s, e) =>
            {
                if (e.ChangedButton != OxyMouseButton.Left)
                {
                    return;
                }
                if (_isMarkersActive == 1)
                {
                    lineAnnotationHorizontal.StrokeThickness *= 3;
                    plotAnalogSignals.Model.InvalidatePlot(false);
                    e.Handled = true;
                }
            };
            lineAnnotationHorizontal.MouseMove += (s, e) =>
            {
                if (_isMarkersActive == 1)
                {
                    lineAnnotationHorizontal.Y = lineAnnotationHorizontal.InverseTransform(e.Position).Y;
                    plotAnalogSignals.Model.InvalidatePlot(false);
                    e.Handled = true;
                }
            };
            lineAnnotationHorizontal.MouseUp += (s, e) =>
            {
                if (_isMarkersActive == 1)
                {
                    lineAnnotationHorizontal.StrokeThickness /= 3;
                    plotAnalogSignals.Model.InvalidatePlot(false);
                    e.Handled = true;
                }
            };
            plotAnalogSignals.Model.Annotations.Add(lineAnnotationVertical);
            plotAnalogSignals.Model.Annotations.Add(lineAnnotationHorizontal);
            // Create Line series
            var s1 = new LineSeries { StrokeThickness = 1, Color = OxyColors.Blue, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.Blue, MarkerFill = OxyColors.Blue, MarkerSize = 2};
            var s2 = new LineSeries { StrokeThickness = 1, Color = OxyColors.Magenta, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.Magenta, MarkerFill = OxyColors.Magenta, MarkerSize = 2 };
            var s3 = new LineSeries { StrokeThickness = 1, Color = OxyColors.Green, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.Green, MarkerFill = OxyColors.Green, MarkerSize = 2 };
            var s4 = new LineSeries { StrokeThickness = 1, Color = OxyColors.IndianRed, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.IndianRed, MarkerFill = OxyColors.IndianRed, MarkerSize = 2 };
            var s5 = new LineSeries { StrokeThickness = 1, Color = OxyColors.DarkOrange, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.DarkOrange, MarkerFill = OxyColors.DarkOrange, MarkerSize = 2 };
            var s6 = new LineSeries { StrokeThickness = 1, Color = OxyColors.Yellow, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.Yellow, MarkerFill = OxyColors.Yellow, MarkerSize = 2 };
            var s7 = new LineSeries { StrokeThickness = 1, Color = OxyColors.Red, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.Red, MarkerFill = OxyColors.Red, MarkerSize = 2 };
            for (int i = 0; i < dateTimes.Count; i++)
            {
                s1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dateTimes[i]), Convert.ToDouble(analogSignals[oc][0][i]) / ((_mineConfig.MainViewConfig.Distance.Value + 10) / 100)));
                s2.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dateTimes[i]), Convert.ToDouble(analogSignals[oc][1][i]) / ((_mineConfig.MainViewConfig.Distance.Value + 10) / 100)));
                s3.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dateTimes[i]), Convert.ToDouble(analogSignals[oc][2][i]) / (_mineConfig.MainViewConfig.MaxSpeed.Value / 100)));
                s4.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dateTimes[i]), Convert.ToDouble(analogSignals[oc][3][i]) / (_maxSpeedUp / 100)));
                s5.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dateTimes[i]), Convert.ToDouble(analogSignals[oc][4][i]) / (_mineConfig.MainViewConfig.MaxTokAnchor.Value / 100)));
                s6.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dateTimes[i]), Convert.ToDouble(analogSignals[oc][5][i]) / (_mineConfig.MainViewConfig.MaxTokExcitation.Value / 100)));
                s7.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dateTimes[i]), Convert.ToDouble(analogSignals[oc][6][i]) / (_mineConfig.MainViewConfig.MaxSpeed.Value / 100)));
            }
            //Series
            plotAnalogSignals.Model.Series.Add(s1);
            plotAnalogSignals.Model.Series.Add(s2);
            plotAnalogSignals.Model.Series.Add(s3);
            plotAnalogSignals.Model.Series.Add(s4);
            plotAnalogSignals.Model.Series.Add(s5);
            plotAnalogSignals.Model.Series.Add(s6);
            plotAnalogSignals.Model.Series.Add(s7); 
            int j = 0;
            foreach (ListViewItem item in listViewAnalogSignals.Items)
            {
                plotAnalogSignals.Model.Series[j].IsVisible = listViewAnalogSignals.CheckedItems.Contains(item);
                j++;
            }
            plotAnalogSignals.InvalidatePlot(true);
            panel1.Controls.Add(plotAnalogSignals);
            });
        } 
         
        private void buttonFind_Click(object sender, EventArgs e)
        {
            var findThread = new Thread(Find) { IsBackground = true, Priority = ThreadPriority.Lowest };
            findThread.Start();
        }

        private void Find()
        {
            var blocksIds = _dataBaseService.GetBlocksIds(dateTimePicker1.Value, dateTimePicker2.Value);
            _blocksDates = _dataBaseService.GetBlocksDateTimes(dateTimePicker1.Value, dateTimePicker2.Value);
            //analog signals
            var names = _dataBaseService.GetAnalogSignalsNames();
            _analogSignals = new List<List<List<string>>>();
            for (int j = 0; j < 3; j++)
            {
                _analogSignals.Add(new List<List<string>>());
            }
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < names.Count; i++)
                {
                    _analogSignals[j].Add(new List<string>());
                }
            }
            for (int j = 0; j < blocksIds.Count; j++)
            {
                var listSignalsList = _dataBaseService.GetAnalogSignalsById(blocksIds[j]);
                for (int i = 0; i < 3; i++)
                {
                    for (int k = 0; k < names.Count; k++)
                    {
                        _analogSignals[i][k].Add(listSignalsList[i][k].Value);
                    }
                }
            }
            if (_blocksDates.Count > 0)
                MakeAnalogSignalsGraphic(_analogSignals, _blocksDates);
            //IOsignals
            var signalsNames = _mineConfig.AuziDSignalsConfig.SignalsNames;
            var ioNames = new List<string>();
            var inputSignalsList = new List<ParameterData>();
            var outputSignalsList = new List<ParameterData>();
            if (blocksIds.Count > 0)
            {
                inputSignalsList = _dataBaseService.GetInputSignalsByIdArchiv(blocksIds[0]);
                outputSignalsList = _dataBaseService.GetOutputSignalsByIdArchiv(blocksIds[0]);
            }
            _IOSignals = new List<List<string>>();
            for (int i = 0; i < inputSignalsList.Count; i++)
            {
                _IOSignals.Add(new List<string>());
                ioNames.Add(signalsNames[i]);
            }
            for (int i = 0; i < outputSignalsList.Count; i++)
            {
                _IOSignals.Add(new List<string>());
                ioNames.Add(signalsNames[72 + i]);
            }
            for (int j = 0; j < blocksIds.Count; j++)
            {
                var iSignalsList = _dataBaseService.GetInputSignalsByIdArchiv(blocksIds[j]);
                var oSignalsList = _dataBaseService.GetOutputSignalsByIdArchiv(blocksIds[j]);
                for (int k = 0; k < iSignalsList.Count; k++)
                {
                    _IOSignals[k].Add(iSignalsList[k].Value);
                }
                for (int k = 0; k < oSignalsList.Count; k++)
                {
                    _IOSignals[iSignalsList.Count + k].Add(oSignalsList[k].Value);
                }
            }
            if (_blocksDates.Count > 0)
                MakeIOSignalsGraphics(_IOSignals, _blocksDates);
            //
        }

        private void listViewAnalogSignals_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (plotAnalogSignals != null)
            {
                int j = 0;
                foreach (ListViewItem item in listViewAnalogSignals.Items)
                {
                    plotAnalogSignals.Model.Series[j].IsVisible = listViewAnalogSignals.CheckedItems.Contains(item);
                    j++;
                }
                //plotAnalogSignals.RefreshPlot(true);
                plotAnalogSignals.InvalidatePlot(true);
            }
        }

        private void comboBoxOC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_blocksDates != null && _blocksDates.Count > 0)
                MakeAnalogSignalsGraphic(_analogSignals, _blocksDates);
        }

        private void comboBoxSync_SelectedIndexChanged(object sender, EventArgs e)
        {
            _plotsSynchronized = comboBoxSync.SelectedIndex;
            if (_plotsSynchronized == 1)
            {
                SynchronizePlotsByTimeAxis();
                for (int i = 0; i < _checkedIOIndexes.Count; i++)
                {
                    lineAnnotationsIO[i].X = lineAnnotationVertical.X;
                    plotIOSignals[i].Model.InvalidatePlot(false);
                }
            }
        }
        
        private void comboBoxMarkers_SelectedIndexChanged(object sender, EventArgs e)
        {
            _isMarkersActive = comboBoxMarkers.SelectedIndex;
        }

        //IOsignals
        private void DrawIOsignals(List<string> names)
        {
            int rowCount = _checkedIOIndexes.Count;
            if (rowCount == 0)
                rowCount = 1;
            //IOsignals names
            var tlpIOnames = new TableLayoutPanel();
            tlpIOnames.Dock = DockStyle.Fill;
            tlpIOnames.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            tlpIOnames.ColumnCount = 1;
            tlpIOnames.RowCount = rowCount;
            for (int i = 0; i < rowCount; i++)
            {
                tlpIOnames.RowStyles.Add(new RowStyle());
            }
            float rowHight2 = 100 / rowCount;
            TableLayoutRowStyleCollection styles2 = tlpIOnames.RowStyles;
            foreach (RowStyle style in styles2)
            {
                style.SizeType = SizeType.Percent;
                style.Height = rowHight2;
            }
            panelIOnames.Controls.Clear();
            panelIOnames.Controls.Add(tlpIOnames);
            for (int i = 0; i < _checkedIOIndexes.Count; i++)
            {
                var nameLabel = new Label {Text = names[_checkedIOIndexes[i]], ForeColor = _colors[i], TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill};
                tlpIOnames.Controls.Add(nameLabel, 0, i);
            }
        }

        private void MakeIOSignalsGraphics(List<List<string>> ioSignals, List<DateTime> dateTimes)
        {
            this.Invoke((MethodInvoker)delegate
            {
                int rowCount = _checkedIOIndexes.Count;
                if (rowCount == 0)
                    rowCount = 1;
                //plots
                var tlpIOplots = new TableLayoutPanel();
                tlpIOplots.Dock = DockStyle.Fill;
                tlpIOplots.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
                tlpIOplots.ColumnCount = 1;
                tlpIOplots.RowCount = rowCount;
                for (int i = 0; i < rowCount; i++)
                {
                    tlpIOplots.RowStyles.Add(new RowStyle());
                }
                float rowHight = 100 / rowCount;
                TableLayoutRowStyleCollection styles = tlpIOplots.RowStyles;
                foreach (RowStyle style in styles)
                {
                    style.SizeType = SizeType.Percent;
                    style.Height = rowHight;
                }
                panelIOplots.Controls.Clear();
                panelIOplots.Controls.Add(tlpIOplots);
                _ioSeries[0] = new LineSeries { StrokeThickness = 1, Color = OxyColors.Blue, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.Blue, MarkerFill = OxyColors.Blue, MarkerSize = 2 };
                _ioSeries[1] = new LineSeries { StrokeThickness = 1, Color = OxyColors.Magenta, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.Magenta, MarkerFill = OxyColors.Magenta, MarkerSize = 2 };
                _ioSeries[2] = new LineSeries { StrokeThickness = 1, Color = OxyColors.Green, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.Green, MarkerFill = OxyColors.Green, MarkerSize = 2 };
                _ioSeries[3] = new LineSeries { StrokeThickness = 1, Color = OxyColors.IndianRed, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.IndianRed, MarkerFill = OxyColors.IndianRed, MarkerSize = 2 };
                _ioSeries[4] = new LineSeries { StrokeThickness = 1, Color = OxyColors.DarkOrange, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.DarkOrange, MarkerFill = OxyColors.DarkOrange, MarkerSize = 2 };
                _ioSeries[5] = new LineSeries { StrokeThickness = 1, Color = OxyColors.Yellow, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.Yellow, MarkerFill = OxyColors.Yellow, MarkerSize = 2 };
                _ioSeries[6] = new LineSeries { StrokeThickness = 1, Color = OxyColors.Red, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.Red, MarkerFill = OxyColors.Red, MarkerSize = 2 };
                _ioSeries[7] = new LineSeries { StrokeThickness = 1, Color = OxyColors.LimeGreen, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.LimeGreen, MarkerFill = OxyColors.LimeGreen, MarkerSize = 2 };
                _ioSeries[8] = new LineSeries { StrokeThickness = 1, Color = OxyColors.DeepSkyBlue, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.DeepSkyBlue, MarkerFill = OxyColors.DeepSkyBlue, MarkerSize = 2 };
                _ioSeries[9] = new LineSeries { StrokeThickness = 1, Color = OxyColors.BlueViolet, MarkerType = MarkerType.Circle, MarkerStroke = OxyColors.BlueViolet, MarkerFill = OxyColors.BlueViolet, MarkerSize = 2 };
                for (int i = 0; i < _checkedIOIndexes.Count; i++)
                {
                    xAxis[i] = new DateTimeAxis(AxisPosition.Bottom, dateTimes[0], dateTimes[dateTimes.Count - 1], null, null, DateTimeIntervalType.Auto)
                    {
                        MajorGridlineStyle = LineStyle.Solid,
                        MinorGridlineStyle = LineStyle.Dot,
                        //Title = "Дата",
                        StringFormat = "dd-MM-yyyy HH:mm:ss",
                        FontSize = 10,
                        IsZoomEnabled = true,
                        MajorStep = 1.0 / 24 / 60 / 2,
                        MinorStep = 1.0 / 24 / 60 / 12,
                        Minimum = DateTimeAxis.ToDouble(dateTimes[0]),
                        Maximum = DateTimeAxis.ToDouble(dateTimes[dateTimes.Count - 1])
                    };
                    yAxis[i] = new LinearAxis(AxisPosition.Left, 0)
                    {
                        MajorGridlineStyle = LineStyle.Solid,
                        MinorGridlineStyle = LineStyle.Dot,
                        Title = "value",
                        StringFormat = "00#",
                        Minimum = -0.2,
                        Maximum = 1.2
                    };
                    plotIOSignals[i] = new OxyPlot.WindowsForms.Plot { Model = new PlotModel(), Dock = DockStyle.Fill };
                    plotIOSignals[i].Model.PlotType = PlotType.XY;
                    plotIOSignals[i].Model.Axes.Clear();
                    plotIOSignals[i].Model.Axes.Add(xAxis[i]);
                    plotIOSignals[i].Model.Axes.Add(yAxis[i]);
                    //annotations
                    lineAnnotationsIO[i] = new LineAnnotation { Type = LineAnnotationType.Vertical, Color = OxyColors.Brown, Layer = AnnotationLayer.BelowSeries };
                    lineAnnotationsIO[i].X = lineAnnotationVertical.X;
                   /* lineAnnotationsIO[i].MouseDown += (s, e) =>
                    {
                        if (e.ChangedButton != OxyMouseButton.Left)
                        {
                            return;
                        }
                        if (_isMarkersActive == 1)
                        {
                            lineAnnotationsIO[i].StrokeThickness *= 3;
                            plotIOSignals[i].Model.InvalidatePlot(false);
                            e.Handled = true;
                        }
                    };
                    lineAnnotationsIO[i].MouseMove += (s, e) =>
                    {
                        if (_isMarkersActive == 1)
                        {
                            lineAnnotationsIO[i].X = lineAnnotationsIO[i].InverseTransform(e.Position).X;
                            plotIOSignals[i].Model.InvalidatePlot(false);
                            e.Handled = true;
                        }
                    };
                    lineAnnotationsIO[i].MouseUp += (s, e) =>
                    {
                        if (_isMarkersActive == 1)
                        {
                            lineAnnotationsIO[i].StrokeThickness /= 3;
                            plotIOSignals[i].Model.InvalidatePlot(false);
                            e.Handled = true;
                        }
                    }; */
                    plotIOSignals[i].Model.Annotations.Add(lineAnnotationsIO[i]);
                }
                for (int i = 0; i < _ioSeries.Count(); i++)
                {
                    _ioSeries[i].Points.Clear();
                }
                for (int i = 0; i < _checkedIOIndexes.Count; i++)
                {
                    plotIOSignals[i].Model.Series.Clear();
                }
                for (int i = 0; i < _checkedIOIndexes.Count; i++)
                {
                    for (int j = 0; j < dateTimes.Count; j++)
                    {
                        if (ioSignals[_checkedIOIndexes[i]][j] != "нет данных")
                            _ioSeries[i].Points.Add(new DataPoint(DateTimeAxis.ToDouble(dateTimes[j]), Convert.ToDouble(ioSignals[_checkedIOIndexes[i]][j])));
                    }
                }
                for (int i = 0; i < _checkedIOIndexes.Count; i++)
                {
                    plotIOSignals[i].Model.Series.Add(_ioSeries[i]);
                    plotIOSignals[i].InvalidatePlot(true);
                    tlpIOplots.Controls.Add(plotIOSignals[i], 0, i);
                }
            });
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)//choose IO signals
        {
            var blocksIds = _dataBaseService.GetBlocksIds(dateTimePicker1.Value, dateTimePicker2.Value);
            var signalsNames = _mineConfig.AuziDSignalsConfig.SignalsNames;
            var ioNames = new List<string>();
            var inputSignalsList = new List<ParameterData>();
            var outputSignalsList = new List<ParameterData>();
            int iCount;
            int oCount;
            if (blocksIds.Count > 0)
            {
                inputSignalsList = _dataBaseService.GetInputSignalsById(blocksIds[0]);
                outputSignalsList = _dataBaseService.GetOutputSignalsById(blocksIds[0]);
                iCount = inputSignalsList.Count;
                oCount = outputSignalsList.Count;
            }
            else
            {
                iCount = 32;
                oCount = 40;
            }
            for (int i = 0; i < iCount; i++)
            {
                ioNames.Add(signalsNames[i]);
            }
            for (int i = 0; i < oCount; i++)
            {
                ioNames.Add(signalsNames[72 + i]);
            }
            FormChooseSignals formChooseSignals = new FormChooseSignals(ioNames, _checkedIOIndexes);
            formChooseSignals.ShowDialog();
            DrawIOsignals(ioNames);
            if (_blocksDates != null && _blocksDates.Count > 0)
                MakeIOSignalsGraphics(_IOSignals, _blocksDates);
        }

        private void xAxis_AxisChanged(object sender, AxisChangedEventArgs args)
        {
            if (_plotsSynchronized == 1)
                SynchronizePlotsByTimeAxis();
        }

        private void SynchronizePlotsByTimeAxis()
        {
            DateTime fistPoint = new DateTime();
            DateTime secondPoint = new DateTime();
            fistPoint = DateTimeAxis.ToDateTime(plotAnalogSignals.Model.Axes[0].ActualMinimum);
            secondPoint = DateTimeAxis.ToDateTime(plotAnalogSignals.Model.Axes[0].ActualMaximum);
            for (int i = 0; i < _checkedIOIndexes.Count; i++)
            {
                plotIOSignals[i].Model.Axes[0].Minimum = DateTimeAxis.ToDouble(fistPoint);
                plotIOSignals[i].Model.Axes[0].Maximum = DateTimeAxis.ToDouble(secondPoint);
                plotIOSignals[i].InvalidatePlot(true);
            }
        }
        //

        private MineConfig _mineConfig;
        private double _maxSpeedUp = 2;
        readonly DataBaseService _dataBaseService = IoC.Resolve<DataBaseService>();
        private OxyPlot.WindowsForms.Plot plotAnalogSignals;
        private List<List<List<string>>> _analogSignals;
        private LineAnnotation lineAnnotationVertical;
        private LineAnnotation lineAnnotationHorizontal;
        private LineAnnotation[] lineAnnotationsIO = new LineAnnotation[10];
        private List<List<string>> _IOSignals;
        private List<int> _checkedIOIndexes; 
        private List<DateTime> _blocksDates;
        private OxyPlot.WindowsForms.Plot[] plotIOSignals = new Plot[10];
        private Color[] _colors = new Color[] { Color.Blue, Color.Magenta, Color.Green, Color.IndianRed, Color.DarkOrange, Color.Yellow, Color.Red, Color.LimeGreen, Color.DeepSkyBlue, Color.BlueViolet };
        private LineSeries[] _ioSeries = new LineSeries[10];
        private DateTimeAxis[] xAxis = new DateTimeAxis[10];
        private LinearAxis[] yAxis = new LinearAxis[10];
        private int _plotsSynchronized = 0;
        private int _isMarkersActive = 0;

    }
}
