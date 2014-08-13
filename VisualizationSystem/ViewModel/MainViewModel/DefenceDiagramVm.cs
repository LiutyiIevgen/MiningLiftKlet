using System;
using System.Globalization;
using System.Linq;
using ML.ConfigSettings.Services;
using ML.DataExchange.Model;
using VisualizationSystem.Model;
using VisualizationSystem.Model.GraphicData;

namespace VisualizationSystem.ViewModel.MainViewModel
{
    class DefenceDiagramVm
    {
        //public DefenceDiagramVm(Parameters parameter)
        //{
        //    _parameters = parameter;
        //    variableParametersValueStr = IoC.Resolve<MineConfig>().ParametersConfig.VariableParametersValue;
        //    variableParametersValue = new double[variableParametersValueStr.Count()];
        //    for (int i = 0; i < variableParametersValueStr.Count(); i++)
        //    {
        //        variableParametersValue[i] = Convert.ToDouble(variableParametersValueStr[i], CultureInfo.GetCultureInfo("en-US"));
        //    }
        //    _points = 20;
        //    HzCrossHkDown = 0;
        //    HzCrossHkUp = 0;
        //    vVeight = new double[_points];
        //    vPeople = new double[_points];
        //    vEquipment = new double[_points];
        //    vRevision = new double[_points];
        //    hzVeight = new double[_points];
        //    hzPeople = new double[_points];
        //    hzEquipment = new double[_points];
        //    hzRevision = new double[_points];
        //    hkVeight = new double[_points];
        //    hkPeople = new double[_points];
        //    hkEquipment = new double[_points];
        //    hkRevision = new double[_points];
        //    DiagramVeight = new GraphicData[_points + 2];
        //    for (int i = 0; i < (_points + 2); i++)
        //        DiagramVeight[i] = new GraphicData();
        //    DiagramPeople = new GraphicData[_points + 2];
        //    for (int i = 0; i < (_points + 2); i++)
        //        DiagramPeople[i] = new GraphicData();
        //    DiagramEquipment = new GraphicData[_points + 2];
        //    for (int i = 0; i < (_points + 2); i++)
        //        DiagramEquipment[i] = new GraphicData();
        //    DiagramRevision = new GraphicData[_points + 2];
        //    for (int i = 0; i < (_points + 2); i++)
        //        DiagramRevision[i] = new GraphicData();
        //    CurrentDiagram = new GraphicData[_points + 2];
        //    for (int i = 0; i < (_points + 2); i++)
        //        CurrentDiagram[i] = new GraphicData();

        //    InitV();
        //    SolveHz();
        //    SolveHk();
        //    CheckHz();
        //    SolveDiagramData();
        //    ChooseCurrentDiagram();
        //}

        //private void InitV()
        //{
        //    double shag;
        //    shag = (1.15 * variableParametersValue[0] - variableParametersValue[4]) / _points;
        //    vVeight[0] = variableParametersValue[4];
        //    for (int i = 1; i < _points; i++)
        //    {
        //        vVeight[i] = vVeight[i - 1] + shag;
        //    }
        //    if (vVeight[_points - 1] != 1.15 * variableParametersValue[0])
        //    {
        //        vVeight[_points - 1] = 1.15 * variableParametersValue[0];
        //    }
        //    shag = (1.15 * variableParametersValue[1] - variableParametersValue[5]) / _points;
        //    vPeople[0] = variableParametersValue[5];
        //    for (int i = 1; i < _points; i++)
        //    {
        //        vPeople[i] = vPeople[i - 1] + shag;
        //    }
        //    if (vPeople[_points - 1] != 1.15 * variableParametersValue[1])
        //    {
        //        vPeople[_points - 1] = 1.15 * variableParametersValue[1];
        //    }
        //    shag = (1.15 * variableParametersValue[2] - variableParametersValue[4]) / _points;
        //    vEquipment[0] = variableParametersValue[4];
        //    for (int i = 1; i < _points; i++)
        //    {
        //        vEquipment[i] = vEquipment[i - 1] + shag;
        //    }
        //    if (vEquipment[_points - 1] != 1.15 * variableParametersValue[2])
        //    {
        //        vEquipment[_points - 1] = 1.15 * variableParametersValue[2];
        //    }
        //    for (int i = 0; i < _points; i++)
        //    {
        //        vRevision[i] = variableParametersValue[3];
        //    }
        //}

        //private void SolveHz()
        //{
        //    if (_parameters.f_start == 1)
        //    {
        //        //вниз
        //        for (int i = 0; i < _points; i++)
        //        {
        //            hzVeight[i] = (vVeight[i] * vVeight[i] - variableParametersValue[4] * variableParametersValue[4]) * (variableParametersValue[0] * variableParametersValue[0] - variableParametersValue[6] * variableParametersValue[6]);
        //            hzVeight[i] /= 2 * variableParametersValue[13] * (1.15 * variableParametersValue[0] * 1.15 * variableParametersValue[0] - variableParametersValue[4] * variableParametersValue[4]);
        //            hzVeight[i] += variableParametersValue[16];
        //        }
        //        //вниз
        //        for (int i = 0; i < _points; i++)
        //        {
        //            hzPeople[i] = (vPeople[i] * vPeople[i] - variableParametersValue[5] * variableParametersValue[5]) * (variableParametersValue[1] * variableParametersValue[1] - variableParametersValue[6] * variableParametersValue[6]);
        //            hzPeople[i] /= 2 * variableParametersValue[14] * (1.15 * variableParametersValue[1] * 1.15 * variableParametersValue[1] - variableParametersValue[5] * variableParametersValue[5]);
        //            hzPeople[i] += variableParametersValue[18];
        //        }
        //        //вниз
        //        for (int i = 0; i < _points; i++)
        //        {
        //            hzEquipment[i] = (vEquipment[i] * vEquipment[i] - variableParametersValue[4] * variableParametersValue[4]) * (variableParametersValue[2] * variableParametersValue[2] - variableParametersValue[6] * variableParametersValue[6]);
        //            hzEquipment[i] /= 2 * variableParametersValue[13] * (1.15 * variableParametersValue[2] * 1.15 * variableParametersValue[2] - variableParametersValue[4] * variableParametersValue[4]);
        //            hzEquipment[i] += variableParametersValue[16];
        //        }
        //        //
        //        double shag = (IoC.Resolve<MineConfig>().MainViewConfig.Border.Value - IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value) / _points;
        //        hzRevision[0] = IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value;
        //        for (int i = 1; i < _points; i++)
        //        {
        //            hzRevision[i] = hzRevision[i - 1] + shag;
        //        }
        //        if (hzRevision[_points - 1] != IoC.Resolve<MineConfig>().MainViewConfig.Border.Value)
        //        {
        //            hzRevision[_points - 1] = IoC.Resolve<MineConfig>().MainViewConfig.Border.Value;
        //        }
        //    }
        //    else if (_parameters.f_back == 1)
        //    {
        //        //вверх
        //        for (int i = 0; i < _points; i++)
        //        {
        //            hzVeight[i] = (vVeight[i] * vVeight[i] - variableParametersValue[4] * variableParametersValue[4]) * (variableParametersValue[0] * variableParametersValue[0] - variableParametersValue[6] * variableParametersValue[6]);
        //            hzVeight[i] /= 2 * variableParametersValue[13] * (1.15 * variableParametersValue[0] * 1.15 * variableParametersValue[0] - variableParametersValue[4] * variableParametersValue[4]);
        //            hzVeight[i] += variableParametersValue[17];
        //        }
        //        //вверх
        //        for (int i = 0; i < _points; i++)
        //        {
        //            hzPeople[i] = (vPeople[i] * vPeople[i] - variableParametersValue[5] * variableParametersValue[5]) * (variableParametersValue[1] * variableParametersValue[1] - variableParametersValue[6] * variableParametersValue[6]);
        //            hzPeople[i] /= 2 * variableParametersValue[14] * (1.15 * variableParametersValue[1] * 1.15 * variableParametersValue[1] - variableParametersValue[5] * variableParametersValue[5]);
        //            hzPeople[i] += variableParametersValue[19];
        //        }
        //        //вверх
        //        for (int i = 0; i < _points; i++)
        //        {
        //            hzEquipment[i] = (vEquipment[i] * vEquipment[i] - variableParametersValue[4] * variableParametersValue[4]) * (variableParametersValue[2] * variableParametersValue[2] - variableParametersValue[6] * variableParametersValue[6]);
        //            hzEquipment[i] /= 2 * variableParametersValue[13] * (1.15 * variableParametersValue[2] * 1.15 * variableParametersValue[2] - variableParametersValue[4] * variableParametersValue[4]);
        //            hzEquipment[i] += variableParametersValue[17];
        //        }
        //        //
        //        double shag = (IoC.Resolve<MineConfig>().MainViewConfig.Border.Value - IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value) / _points;
        //        hzRevision[0] = IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value;
        //        for (int i = 1; i < _points; i++)
        //        {
        //            hzRevision[i] = hzRevision[i - 1] + shag;
        //        }
        //        if (hzRevision[_points - 1] != IoC.Resolve<MineConfig>().MainViewConfig.Border.Value)
        //        {
        //            hzRevision[_points - 1] = IoC.Resolve<MineConfig>().MainViewConfig.Border.Value;
        //        }
        //    }
        //}

        //private void SolveHk()
        //{
        //    for (int i = 0; i < _points; i++)
        //    {
        //        hkVeight[i] = (vVeight[i] + variableParametersValue[15]) * (vVeight[i] + variableParametersValue[15]) / (2 * variableParametersValue[11]);
        //        hkVeight[i] += vVeight[i] * (variableParametersValue[7] + variableParametersValue[8]);
        //        hkVeight[i] += variableParametersValue[15] * variableParametersValue[8] / 2;
        //    }
        //    for (int i = 0; i < _points; i++)
        //    {
        //        hkPeople[i] = (vPeople[i] + variableParametersValue[15]) * (vPeople[i] + variableParametersValue[15]) / (2 * variableParametersValue[12]);
        //        hkPeople[i] += vPeople[i] * (variableParametersValue[7] + variableParametersValue[8]);
        //        hkPeople[i] += variableParametersValue[15] * variableParametersValue[8] / 2;
        //    }
        //    for (int i = 0; i < _points; i++)
        //    {
        //        hkEquipment[i] = (vEquipment[i] + variableParametersValue[15]) * (vEquipment[i] + variableParametersValue[15]) / (2 * variableParametersValue[11]);
        //        hkEquipment[i] += vEquipment[i] * (variableParametersValue[7] + variableParametersValue[8]);
        //        hkEquipment[i] += variableParametersValue[15] * variableParametersValue[8] / 2;
        //    }
        //}

        //private void CheckHz()
        //{
        //    for (int i = 0; i < _points; i++)
        //    {
        //        if (_parameters.f_start == 1)
        //        {
        //            if ((IoC.Resolve<MineConfig>().MainViewConfig.Border.Value - hzVeight[i] >= IoC.Resolve<MineConfig>().MainViewConfig.Border.Value - hkVeight[i]) || (IoC.Resolve<MineConfig>().MainViewConfig.Border.Value - hzPeople[i] >= IoC.Resolve<MineConfig>().MainViewConfig.Border.Value - hkPeople[i]) || (IoC.Resolve<MineConfig>().MainViewConfig.Border.Value - hzEquipment[i] >= IoC.Resolve<MineConfig>().MainViewConfig.Border.Value - hkEquipment[i]))
        //            {
        //                HzCrossHkDown = 1;
        //            }   
        //        }
        //        if (_parameters.f_back == 1)
        //        {
        //            if ((hzVeight[i] <= hkVeight[i]) || (hzPeople[i] <= hkPeople[i]) || (hzEquipment[i] <= hkEquipment[i]))
        //            {
        //                HzCrossHkUp = 1;
        //            }
        //        }
        //    }
        //}

        //private void SolveDiagramData()
        //{
        //    if (_parameters.f_start == 1)
        //    {
        //        DiagramVeight[0].X = IoC.Resolve<MineConfig>().MainViewConfig.Border.Value;
        //        DiagramVeight[0].Y = variableParametersValue[4];
        //        for (int i = 1; i < (_points + 1); i++)
        //        {
        //            DiagramVeight[i].X = IoC.Resolve<MineConfig>().MainViewConfig.Border.Value - hzVeight[i - 1];
        //            DiagramVeight[i].Y = vVeight[i - 1];
        //        }
        //        DiagramVeight[_points + 1].X = IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value;
        //        DiagramVeight[_points + 1].Y = 1.15*variableParametersValue[0];
        //        //
        //        DiagramPeople[0].X = IoC.Resolve<MineConfig>().MainViewConfig.Border.Value;
        //        DiagramPeople[0].Y = variableParametersValue[5];
        //        for (int i = 1; i < (_points + 1); i++)
        //        {
        //            DiagramPeople[i].X = IoC.Resolve<MineConfig>().MainViewConfig.Border.Value - hzPeople[i - 1];
        //            DiagramPeople[i].Y = vPeople[i - 1];
        //        }
        //        DiagramPeople[_points + 1].X = IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value;
        //        DiagramPeople[_points + 1].Y = 1.15 * variableParametersValue[1];
        //        //
        //        DiagramEquipment[0].X = IoC.Resolve<MineConfig>().MainViewConfig.Border.Value;
        //        DiagramEquipment[0].Y = variableParametersValue[4];
        //        for (int i = 1; i < (_points + 1); i++)
        //        {
        //            DiagramEquipment[i].X = IoC.Resolve<MineConfig>().MainViewConfig.Border.Value - hzEquipment[i - 1];
        //            DiagramEquipment[i].Y = vEquipment[i - 1];
        //        }
        //        DiagramEquipment[_points + 1].X = IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value;
        //        DiagramEquipment[_points + 1].Y = 1.15 * variableParametersValue[2];
        //        //
        //        DiagramRevision[0].X = hzRevision[0];
        //        DiagramRevision[0].Y = vRevision[0];
        //        for (int i = 1; i < (_points + 1); i++)
        //        {
        //            DiagramRevision[i].X = hzRevision[i - 1];
        //            DiagramRevision[i].Y = vRevision[i - 1];
        //        }
        //        DiagramRevision[_points + 1].X = hzRevision[_points - 1];
        //        DiagramRevision[_points + 1].Y = vRevision[_points - 1];
        //    }
        //    else if (_parameters.f_back == 1)
        //    {
        //        DiagramVeight[0].X = IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value;
        //        DiagramVeight[0].Y = variableParametersValue[4];
        //        for (int i = 1; i < (_points + 1); i++)
        //        {
        //            DiagramVeight[i].X = hzVeight[i - 1];
        //            DiagramVeight[i].Y = vVeight[i - 1];
        //        }
        //        DiagramVeight[_points + 1].X = IoC.Resolve<MineConfig>().MainViewConfig.Border.Value;
        //        DiagramVeight[_points + 1].Y = 1.15 * variableParametersValue[0];
        //        //
        //        DiagramPeople[0].X = IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value;
        //        DiagramPeople[0].Y = variableParametersValue[5];
        //        for (int i = 1; i < (_points + 1); i++)
        //        {
        //            DiagramPeople[i].X = hzPeople[i - 1];
        //            DiagramPeople[i].Y = vPeople[i - 1];
        //        }
        //        DiagramPeople[_points + 1].X = IoC.Resolve<MineConfig>().MainViewConfig.Border.Value;
        //        DiagramPeople[_points + 1].Y = 1.15 * variableParametersValue[1];
        //        //
        //        DiagramEquipment[0].X = IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value;
        //        DiagramEquipment[0].Y = variableParametersValue[4];
        //        for (int i = 1; i < (_points + 1); i++)
        //        {
        //            DiagramEquipment[i].X = hzEquipment[i - 1];
        //            DiagramEquipment[i].Y = vEquipment[i - 1];
        //        }
        //        DiagramEquipment[_points + 1].X = IoC.Resolve<MineConfig>().MainViewConfig.Border.Value;
        //        DiagramEquipment[_points + 1].Y = 1.15 * variableParametersValue[2];
        //        //
        //        DiagramRevision[0].X = hzRevision[0];
        //        DiagramRevision[0].Y = vRevision[0];
        //        for (int i = 1; i < (_points + 1); i++)
        //        {
        //            DiagramRevision[i].X = hzRevision[i - 1];
        //            DiagramRevision[i].Y = vRevision[i - 1];
        //        }
        //        DiagramRevision[_points + 1].X = hzRevision[_points - 1];
        //        DiagramRevision[_points + 1].Y = vRevision[_points - 1];
        //    }
        //}

        //private void ChooseCurrentDiagram()
        //{
        //    switch (_parameters.DefenceDiagramRegime)
        //    {
        //        case 1:
        //            for (int i = 0; i < (_points + 2); i++)
        //            {
        //                CurrentDiagram[i].X = DiagramVeight[i].X;
        //                CurrentDiagram[i].Y = DiagramVeight[i].Y;
        //            }
        //            break;
        //        case 2:
        //            for (int i = 0; i < (_points + 2); i++)
        //            {
        //                CurrentDiagram[i].X = DiagramPeople[i].X;
        //                CurrentDiagram[i].Y = DiagramPeople[i].Y;
        //            }
        //            break;
        //        case 3:
        //            for (int i = 0; i < (_points + 2); i++)
        //            {
        //                CurrentDiagram[i].X = DiagramEquipment[i].X;
        //                CurrentDiagram[i].Y = DiagramEquipment[i].Y;
        //            }
        //            break;
        //        case 4:
        //            for (int i = 0; i < (_points + 2); i++)
        //            {
        //                CurrentDiagram[i].X = DiagramRevision[i].X;
        //                CurrentDiagram[i].Y = DiagramRevision[i].Y;
        //            }
        //            break;
        //    }
        //}

        //public double[] vVeight { get; private set; }
        //public double[] vPeople { get; private set; }
        //public double[] vEquipment { get; private set; }
        //public double[] vRevision { get; private set; }
        //public double[] hzVeight { get; private set; }
        //public double[] hzPeople { get; private set; }
        //public double[] hzEquipment { get; private set; }
        //public double[] hzRevision { get; private set; }
        //public double[] hkVeight { get; private set; }
        //public double[] hkPeople { get; private set; }
        //public double[] hkEquipment { get; private set; }
        //public double[] hkRevision { get; private set; }
        //private GraphicData[] DiagramVeight;
        //private GraphicData[] DiagramPeople;
        //private GraphicData[] DiagramEquipment;
        //private GraphicData[] DiagramRevision;
        //public GraphicData[] CurrentDiagram { get; private set; }
        //private string[] variableParametersValueStr;
        //private double[] variableParametersValue;
        //private Parameters _parameters;
        //private int _points;
        //public int HzCrossHkDown { get; private set; }
        //public int HzCrossHkUp { get; private set; }
    }
}
