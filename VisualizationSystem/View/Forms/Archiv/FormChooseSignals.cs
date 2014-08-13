using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualizationSystem.View.Forms.Archiv
{
    public partial class FormChooseSignals : Form
    {
        public FormChooseSignals()
        {
            InitializeComponent();
        }

        public FormChooseSignals(List<string> IOnames, List<int> checkedIOInexes)
        {
            InitializeComponent();
            _checkedIOIndexes = checkedIOInexes;
            _IONames = IOnames;
            SetIONamesList();
        }

        /* ~FormChooseSignals()
        {
            GetCheckedIOIndexes();
        }*/

        private void FormChooseSignals_Load(object sender, EventArgs e)
        {

        }

        private void SetIONamesList()
        {
            foreach (var name in _IONames)
            {
                listViewIOSignals.Items.Add(name);
            }
            foreach (var index in _checkedIOIndexes)
            {
                listViewIOSignals.Items[index].Checked = true;
            }
        }

        private void GetCheckedIOIndexes()
        {
            _checkedIOIndexes.Clear();
            for (int i = 0; i < _IONames.Count; i++)
            {
                if (listViewIOSignals.Items[i].Checked && _checkedIOIndexes.Count < _maxCount)
                    _checkedIOIndexes.Add(i);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            GetCheckedIOIndexes();
            MessageBox.Show("Входные и выходные сигналы выбраны успешно!", "Выбор сигналов", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void listViewIOSignals_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (listViewIOSignals.CheckedItems.Count > _maxCount)
            {
                e.Item.Checked = false;
                MessageBox.Show("Входных и выходных сигналов может быть выбрано не более " + _maxCount.ToString(), "Выбор сигналов", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private List<int> _checkedIOIndexes;
        private List<string> _IONames;
        private int _maxCount = 10;

    }
}
