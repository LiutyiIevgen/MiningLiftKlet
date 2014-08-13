using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ML.DataExchange.Model;
using VisualizationSystem.Model;
using VisualizationSystem.Model.Settings;
using VisualizationSystem.View.Forms;
using VisualizationSystem.View.Forms.Setting;

namespace VisualizationSystem.View.UserControls.Setting
{
    public partial class KalibrovkaSettings : UserControl
    {
        public KalibrovkaSettings()
        {
            InitializeComponent();
            IoC.Resolve<DataListener>().SetParameterReceive(ParameterReceive);
            unloadThread = new Thread(UnloadAllParameters) { IsBackground = true };
            checkBoxEdit.CheckState = CheckState.Unchecked;
            textBoxCounter1_1.ReadOnly = true;
            textBoxCounter1_2.ReadOnly = true;
            textBoxCounter1_3.ReadOnly = true;
            textBoxCounter2_1.ReadOnly = true;
            textBoxCounter2_2.ReadOnly = true;
            textBoxCounter2_3.ReadOnly = true;
            textBoxMS1.ReadOnly = true;
            textBoxMS2.ReadOnly = true;
            textBoxMS3.ReadOnly = true;
            textBoxMV1.ReadOnly = true;
            textBoxMV2.ReadOnly = true;
            textBoxMV3.ReadOnly = true;
            textBoxMA1.ReadOnly = true;
            textBoxMA2.ReadOnly = true;
            textBoxMA3.ReadOnly = true;
        }

        private void KalibrovkaSettings_Load(object sender, EventArgs e)
        {

        }

        private void UnloadParameter(ushort controllerId, int index)//выгрузка
        {
            _kalibrovkaOperation = 1;
            IoC.Resolve<DataListener>().GetParameter((ushort)controllerId, (ushort)index, (byte)CanSubindexes.Value);
        }

        private void LoadParameter(ushort controllerId, int index, int subindex, string value) //загрузка
        {
            List<byte> data = new List<byte>();
            try
            {
                if (subindex == 3 || subindex == 4 || subindex == 5)
                    {
                        var nfi = new NumberFormatInfo();
                        nfi.NumberDecimalSeparator = ".";
                        string sf = value;
                        float f = float.Parse(sf, nfi);
                        data.AddRange(BitConverter.GetBytes(f));
                    }
                else if (subindex == 1 || subindex == 2)
                    {
                        string ssh = value;
                        int sh = int.Parse(ssh);
                        var listData = BitConverter.GetBytes(sh).ToList();
                        listData.RemoveAt(listData.Count - 1);
                        data.AddRange(listData.ToArray());
                    }
                IoC.Resolve<DataListener>().SetParameter((ushort)controllerId, (ushort)index, (byte)CanSubindexes.Value, data.ToArray());
            }
            catch (Exception)
            {

            }
        }

        private void ParameterReceive(List<CanParameter> parametersList)
        {
            if (_kalibrovkaOperation == 1)
            {
                foreach (var canParameter in parametersList)
                {
                    if (canParameter.Data == null) //parameter was seted
                    {
                        _isLoaded.Set();
                        continue;
                    }
                    switch (canParameter.ParameterSubIndex)
                    {
                        case (byte) CanSubindexes.Value:
                            _isUnloaded.Set();
                            ValueParser(canParameter);
                            break;
                    }
                }
                _kalibrovkaOperation = 0;
            }
        }

        private void ValueParser(CanParameter canParameter)
        {
            if (canParameter.Data.Count() == 4)//real 32
            {
                float myFloat;
                myFloat = System.BitConverter.ToSingle(canParameter.Data, 0);
                var nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";
                string strData = myFloat.ToString(nfi);
                WriteDataToTextBox(canParameter.ParameterId, canParameter.ControllerId, strData);
            }
            else if (canParameter.Data.Count() == 3) //sint24
            {
                int myShort;
                myShort = (canParameter.Data[0] << 8) + (canParameter.Data[1] << 16) + (canParameter.Data[2] << 24);
                myShort /= 256;
                WriteDataToTextBox(canParameter.ParameterId, canParameter.ControllerId, myShort.ToString());
            }
        }

        private void WriteDataToTextBox(int index, ushort ControllerId, string value)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            this.Invoke((MethodInvoker) delegate
            {
                if (index == startIndex + 1)
                {
                    switch (ControllerId)
                    {
                        case 1:
                            textBoxCounter1_1.Text = value;
                            break;
                        case 2:
                            textBoxCounter1_2.Text = value;
                            break;
                        case 3:
                            textBoxCounter1_3.Text = value;
                            break;
                    }
                }
                else if (index == startIndex + 3)
                {
                    switch (ControllerId)
                    {
                        case 1:
                            textBoxMS1.Text = value;
                            textBoxCounter2_1.Text = (((Convert.ToDouble(textBoxCounter1_1.Text, nfi) / 4) * Convert.ToDouble(value, nfi)) / 1000).ToString(nfi);
                            break;
                        case 2:
                            textBoxMS2.Text = value;
                            textBoxCounter2_2.Text = (((Convert.ToDouble(textBoxCounter1_2.Text, nfi) / 4) * Convert.ToDouble(value, nfi)) / 1000).ToString(nfi);
                            break;
                        case 3:
                            textBoxMS3.Text = value;
                            textBoxCounter2_3.Text = (((Convert.ToDouble(textBoxCounter1_3.Text, nfi) / 4) * Convert.ToDouble(value, nfi)) / 1000).ToString(nfi);
                            break;
                    }
                }
                else if (index == startIndex + 0x0f)
                {
                    switch (ControllerId)
                    {
                        case 1:
                            textBoxMV1.Text = value;
                            break;
                        case 2:
                            textBoxMV2.Text = value;
                            break;
                        case 3:
                            textBoxMV3.Text = value;
                            break;
                    }
                }
                else if (index == startIndex + 0x10)
                {
                    switch (ControllerId)
                    {
                        case 1:
                            textBoxMA1.Text = value;
                            break;
                        case 2:
                            textBoxMA2.Text = value;
                            break;
                        case 3:
                            textBoxMA3.Text = value;
                            break;
                    }
                }
            });
        }

        private void buttonReadAll_Click(object sender, EventArgs e)
        {
            if (unloadThread.IsAlive)
                return;
            unloadThread = new Thread(UnloadAllParameters) { IsBackground = true };
            unloadThread.Start();
        }

        private void UnloadAllParameters()
        {
                int j = 1;
                while (j < 4)
                {
                    Thread.Sleep(200);
                    UnloadParameter((ushort)j, startIndex + 1);
                    //if (!_isUnloaded.WaitOne(TimeSpan.FromMilliseconds(10000)))
                        //return;
                    j++;
                }
                j = 1;
                while (j < 4)
                {
                    Thread.Sleep(200);
                    UnloadParameter((ushort)j, startIndex + 3);
                    //if (!_isUnloaded.WaitOne(TimeSpan.FromMilliseconds(10000)))
                        //return;
                    j++;
                }
                j = 1;
                while (j < 4)
                {
                    Thread.Sleep(200);
                    UnloadParameter((ushort)j, startIndex + 0x0f);
                    //if (!_isUnloaded.WaitOne(TimeSpan.FromMilliseconds(10000)))
                        //return;
                    j++;
                }
                j = 1;
                while (j < 4)
                {
                    Thread.Sleep(200);
                    UnloadParameter((ushort)j, startIndex + 0x10);
                    //if (!_isUnloaded.WaitOne(TimeSpan.FromMilliseconds(10000)))
                        //return;
                    j++;
                }
        }

        private int startIndex = 0x2000;
        private Thread unloadThread;
        EventWaitHandle _isUnloaded = new AutoResetEvent(false);
        EventWaitHandle _isLoaded = new AutoResetEvent(false);
        private int _kalibrovkaOperation = 0; // flag

        private void textBoxCounter1_1_Leave(object sender, EventArgs e)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            if (textBoxCounter1_1.Text == "" || textBoxCounter1_1.Text == ".")
                textBoxCounter1_1.Text = "0";
            textBoxCounter2_1.Text = (((Convert.ToDouble(textBoxCounter1_1.Text, nfi) / 4) * Convert.ToDouble(textBoxMS1.Text, nfi)) / 1000).ToString(nfi);
            if(checkBoxEdit.CheckState == CheckState.Checked)
                Task.Run(() => LoadParameter(1, startIndex + 1, 1, textBoxCounter1_1.Text));
        }

        private void textBoxCounter1_2_Leave(object sender, EventArgs e)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            if (textBoxCounter1_2.Text == "" || textBoxCounter1_2.Text == ".")
                textBoxCounter1_2.Text = "0";
            textBoxCounter2_2.Text = (((Convert.ToDouble(textBoxCounter1_2.Text, nfi) / 4) * Convert.ToDouble(textBoxMS2.Text, nfi)) / 1000).ToString(nfi);
            if (checkBoxEdit.CheckState == CheckState.Checked)
                Task.Run(() => LoadParameter(2, startIndex + 1, 1, textBoxCounter1_2.Text));
        }

        private void textBoxCounter1_3_Leave(object sender, EventArgs e)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            if (textBoxCounter1_3.Text == "" || textBoxCounter1_3.Text == ".")
                textBoxCounter1_3.Text = "0";
            textBoxCounter2_3.Text = (((Convert.ToDouble(textBoxCounter1_3.Text, nfi) / 4) * Convert.ToDouble(textBoxMS3.Text, nfi)) / 1000).ToString(nfi);
            if (checkBoxEdit.CheckState == CheckState.Checked)
                Task.Run(() => LoadParameter(3, startIndex + 1, 1, textBoxCounter1_3.Text));
        }

        private void textBoxCounter2_1_Leave(object sender, EventArgs e)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            if (textBoxCounter2_1.Text == "" || textBoxCounter2_1.Text == ".")
                textBoxCounter2_1.Text = "0";
            var value = ((Convert.ToDouble(textBoxCounter2_1.Text, nfi)*4)/Convert.ToDouble(textBoxMS1.Text, nfi))*1000;
            textBoxCounter1_1.Text = (Math.Round(value)).ToString(nfi);
            if (checkBoxEdit.CheckState == CheckState.Checked)
                Task.Run(() => LoadParameter(1, startIndex + 1, 2, textBoxCounter1_1.Text));
        }

        private void textBoxCounter2_2_Leave(object sender, EventArgs e)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            if (textBoxCounter2_2.Text == "" || textBoxCounter2_2.Text == ".")
                textBoxCounter2_2.Text = "0";
            var value = ((Convert.ToDouble(textBoxCounter2_2.Text, nfi) * 4) / Convert.ToDouble(textBoxMS2.Text, nfi)) * 1000;
            textBoxCounter1_2.Text = (Math.Round(value)).ToString(nfi);
            if (checkBoxEdit.CheckState == CheckState.Checked)
                Task.Run(() => LoadParameter(2, startIndex + 1, 2, textBoxCounter1_2.Text));
        }

        private void textBoxCounter2_3_Leave(object sender, EventArgs e)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            if (textBoxCounter2_3.Text == "" || textBoxCounter2_3.Text == ".")
                textBoxCounter2_3.Text = "0";
            var value = ((Convert.ToDouble(textBoxCounter2_3.Text, nfi) * 4) / Convert.ToDouble(textBoxMS3.Text, nfi)) * 1000;
            textBoxCounter1_3.Text = (Math.Round(value)).ToString(nfi);
            if (checkBoxEdit.CheckState == CheckState.Checked)
                Task.Run(() => LoadParameter(3, startIndex + 1, 2, textBoxCounter1_3.Text));
        }

        private void textBoxMS1_Leave(object sender, EventArgs e)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            if (textBoxMS1.Text == "" || textBoxMS1.Text == ".")
                textBoxMS1.Text = "0.5";
            textBoxCounter2_1.Text = (((Convert.ToDouble(textBoxCounter1_1.Text, nfi) / 4) * Convert.ToDouble(textBoxMS1.Text, nfi)) / 1000).ToString(nfi);
            if (checkBoxEdit.CheckState == CheckState.Checked)
                Task.Run(() => LoadParameter(1, startIndex + 3, 3, textBoxMS1.Text));
        }

        private void textBoxMS2_Leave(object sender, EventArgs e)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            if (textBoxMS2.Text == "" || textBoxMS2.Text == ".")
                textBoxMS2.Text = "0.5";
            textBoxCounter2_2.Text = (((Convert.ToDouble(textBoxCounter1_2.Text, nfi) / 4) * Convert.ToDouble(textBoxMS2.Text, nfi)) / 1000).ToString(nfi);
            if (checkBoxEdit.CheckState == CheckState.Checked)
                Task.Run(() => LoadParameter(2, startIndex + 3, 3, textBoxMS2.Text));
        }

        private void textBoxMS3_Leave(object sender, EventArgs e)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            if (textBoxMS3.Text == "" || textBoxMS3.Text == ".")
                textBoxMS3.Text = "0.5";
            textBoxCounter2_3.Text = (((Convert.ToDouble(textBoxCounter1_3.Text, nfi) / 4) * Convert.ToDouble(textBoxMS3.Text, nfi)) / 1000).ToString(nfi);
            if (checkBoxEdit.CheckState == CheckState.Checked)
                Task.Run(() => LoadParameter(3, startIndex + 3, 3, textBoxMS3.Text));
        }


        private void TextBoxInt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            e.Handled = !(char.IsDigit(c) || c == '\b' || c == '-');
        }
        private void TextBoxReal_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            e.Handled = !(char.IsDigit(c) || c == '.' || c == '\b' || c == '-');
        }

        private void checkBoxEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEdit.CheckState == CheckState.Unchecked)
            {
                textBoxCounter1_1.ReadOnly = true;
                textBoxCounter1_2.ReadOnly = true;
                textBoxCounter1_3.ReadOnly = true;
                textBoxCounter2_1.ReadOnly = true;
                textBoxCounter2_2.ReadOnly = true;
                textBoxCounter2_3.ReadOnly = true;
                textBoxMS1.ReadOnly = true;
                textBoxMS2.ReadOnly = true;
                textBoxMS3.ReadOnly = true;
                textBoxMV1.ReadOnly = true;
                textBoxMV2.ReadOnly = true;
                textBoxMV3.ReadOnly = true;
                textBoxMA1.ReadOnly = true;
                textBoxMA2.ReadOnly = true;
                textBoxMA3.ReadOnly = true;
            }
            else if (checkBoxEdit.CheckState == CheckState.Checked)
            {
                textBoxCounter1_1.ReadOnly = false;
                textBoxCounter1_2.ReadOnly = false;
                textBoxCounter1_3.ReadOnly = false;
                textBoxCounter2_1.ReadOnly = false;
                textBoxCounter2_2.ReadOnly = false;
                textBoxCounter2_3.ReadOnly = false;
                textBoxMS1.ReadOnly = false;
                textBoxMS2.ReadOnly = false;
                textBoxMS3.ReadOnly = false;
                textBoxMV1.ReadOnly = false;
                textBoxMV2.ReadOnly = false;
                textBoxMV3.ReadOnly = false;
                textBoxMA1.ReadOnly = false;
                textBoxMA2.ReadOnly = false;
                textBoxMA3.ReadOnly = false;
            }
        }

        private void textBoxMV1_Leave(object sender, EventArgs e)
        {
            if (textBoxMV1.Text == "" || textBoxMV1.Text == ".")
                textBoxMV1.Text = "1";
            if (checkBoxEdit.CheckState == CheckState.Checked)
                Task.Run(() => LoadParameter(1, startIndex + 0x0f, 4, textBoxMV1.Text));
        }

        private void textBoxMV2_Leave(object sender, EventArgs e)
        {
            if (textBoxMV2.Text == "" || textBoxMV2.Text == ".")
                textBoxMV2.Text = "1";
            if (checkBoxEdit.CheckState == CheckState.Checked)
                Task.Run(() => LoadParameter(2, startIndex + 0x0f, 4, textBoxMV2.Text));
        }

        private void textBoxMV3_Leave(object sender, EventArgs e)
        {
            if (textBoxMV3.Text == "" || textBoxMV3.Text == ".")
                textBoxMV3.Text = "1";
            if (checkBoxEdit.CheckState == CheckState.Checked)
                Task.Run(() => LoadParameter(3, startIndex + 0x0f, 4, textBoxMV3.Text));
        }

        private void textBoxMA1_Leave(object sender, EventArgs e)
        {
            if (textBoxMA1.Text == "" || textBoxMA1.Text == ".")
                textBoxMA1.Text = "1";
            if (checkBoxEdit.CheckState == CheckState.Checked)
                Task.Run(() => LoadParameter(1, startIndex + 0x10, 5, textBoxMA1.Text));
        }

        private void textBoxMA2_Leave(object sender, EventArgs e)
        {
            if (textBoxMA2.Text == "" || textBoxMA2.Text == ".")
                textBoxMA2.Text = "1";
            if (checkBoxEdit.CheckState == CheckState.Checked)
                Task.Run(() => LoadParameter(2, startIndex + 0x10, 5, textBoxMA2.Text));
        }

        private void textBoxMA3_Leave(object sender, EventArgs e)
        {
            if (textBoxMA3.Text == "" || textBoxMA3.Text == ".")
                textBoxMA3.Text = "1";
            if (checkBoxEdit.CheckState == CheckState.Checked)
                Task.Run(() => LoadParameter(3, startIndex + 0x10, 5, textBoxMA3.Text));
        }


    }
}
