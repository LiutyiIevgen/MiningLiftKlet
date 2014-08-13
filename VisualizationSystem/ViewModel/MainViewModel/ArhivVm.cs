using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ML.ConfigSettings.Services;
using VisualizationSystem.Model;
using VisualizationSystem.Services;

namespace VisualizationSystem.ViewModel.MainViewModel
{
    class ArhivVm
    {
        public TreeNode[] GetNodesList(DateTime from, DateTime till)
        {
            _blocksIds = _dataBaseService.GetBlocksIds(from, till);
            if (_blocksIds.Count == 0)
                return new TreeNode[0];
            _currentId = 0;
            return GenereteNodesList();
        }

        public TreeNode[] GetNextNodesList()
        {
            if (_currentId == _blocksIds.Count - 1)
                _currentId = 0;
            else
                _currentId++;
            return GenereteNodesList();
        }

        public TreeNode[] GetPrevNodesList()
        {
            if (_currentId == 0)
                _currentId = _blocksIds.Count - 1;
            else
                _currentId--;
            return GenereteNodesList();
        }

        public int CurrentId
        {
            get { return _currentId + 1; }
        }

        public int RecordsNum
        {
            get { return _blocksIds.Count(); }
        }

        private TreeNode[] GenereteNodesList()
        {
            if(_blocksIds.Count == 0)
                return new TreeNode[0];
            //get analog signals list
            var listSignalsList = _dataBaseService.GetAnalogSignalsById(_blocksIds[_currentId]);
            for (int j = 0; j < 3; j++)
            {
                _analogNodes.Add(new List<TreeNode>());
            }
            for (int j = 0; j < 3; j++)
            {
                _analogNodes[j].Clear();
            }
            for (int i = 0; i < 3; i++)
            {
                listSignalsList[i].ForEach(s => _analogNodes[i].Add(new TreeNode(s.Name + " = " + s.Value.ToString())));
            }

            //get input signals list
            _inputNodes.Clear();
            var signalsList = _dataBaseService.GetInputSignalsById(_blocksIds[_currentId]);
            var signalsNames = IoC.Resolve<MineConfig>().AuziDSignalsConfig.SignalsNames;
            for (int i = 0; i < signalsList.Count; i++)
            {
                signalsList[i].Name = signalsNames[i];
            }
            signalsList.ForEach(s => _inputNodes.Add(new TreeNode(s.Name + " = " + s.Value.ToString())));

            //get output signals list
            _outputNodes.Clear();
            signalsList = _dataBaseService.GetOutputSignalsById(_blocksIds[_currentId]);
            for (int i = 0; i < signalsList.Count; i++)
            {
                signalsList[i].Name = signalsNames[i+72];
            }
            signalsList.ForEach(s => _outputNodes.Add(new TreeNode(s.Name + " = " + s.Value.ToString())));

            var block = _dataBaseService.GetBlockLogById(_blocksIds[_currentId]);
            if (_mainNodes == null)
            {
                _analogMainNodes = new TreeNode[3]
                {
                    new TreeNode("OC1", _analogNodes[0].ToArray()),
                    new TreeNode("OC2", _analogNodes[1].ToArray()),
                    new TreeNode("OC3", _analogNodes[2].ToArray())
                };
                _mainNodes = new TreeNode[4]
                {
                    new TreeNode(block.Date.ToString()),
                    new TreeNode("Аналоговые сигналы", _analogMainNodes),
                    new TreeNode("Входные сигналы", _inputNodes.ToArray()),
                    new TreeNode("Выходные сигналы", _outputNodes.ToArray())
                };
            }
            else
            {
                _mainNodes[0].Text = block.Date.ToString();
                for (int i = 1; i < 4; i++)
                {
                    _mainNodes[i].Nodes.Clear();
                    _analogMainNodes[i-1].Nodes.Clear();
                }
                for (int j = 0; j < 3; j++)
                {
                    _analogMainNodes[j].Nodes.AddRange(_analogNodes[j].ToArray());
                }
                _mainNodes[1].Nodes.AddRange(_analogMainNodes);
                _mainNodes[2].Nodes.AddRange(_inputNodes.ToArray());
                _mainNodes[3].Nodes.AddRange(_outputNodes.ToArray());
            }
            return _mainNodes;
        }


        readonly DataBaseService _dataBaseService = IoC.Resolve<DataBaseService>();
        private List<List<TreeNode>> _analogNodes = new List<List<TreeNode>>();
        private List<TreeNode> _inputNodes = new List<TreeNode>();
        private List<TreeNode> _outputNodes = new List<TreeNode>();
        private TreeNode[] _mainNodes;
        private TreeNode[] _analogMainNodes;
        private List<int> _blocksIds;
        private int _currentId = 0;
    }
}
