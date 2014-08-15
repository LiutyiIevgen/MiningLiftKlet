using System;
using System.Globalization;
using System.Windows.Forms;
using ML.ConfigSettings.Model;
using ML.ConfigSettings.Services;
using VisualizationSystem.Model;

namespace VisualizationSystem.View.UserControls.Setting
{
    public partial class MainViewSettings : UserControl
    {
        public MainViewSettings()
        {
            InitializeComponent();
        }

        private void MainViewSettings_Load(object sender, EventArgs e)
        {
            maxSpeedTextBox.Text = Convert.ToString(IoC.Resolve<MineConfig>().MainViewConfig.MaxSpeed.Value, CultureInfo.GetCultureInfo("en-US"));
            maxDopRuleSpeedTextBox.Text = Convert.ToString(IoC.Resolve<MineConfig>().MainViewConfig.MaxDopRuleSpeed.Value, CultureInfo.GetCultureInfo("en-US"));
            maxTokAnchorTextBox.Text = Convert.ToString(IoC.Resolve<MineConfig>().MainViewConfig.MaxTokAnchor.Value, CultureInfo.GetCultureInfo("en-US"));
            maxTokExcitationTextBox.Text = Convert.ToString(IoC.Resolve<MineConfig>().MainViewConfig.MaxTokExcitation.Value, CultureInfo.GetCultureInfo("en-US"));
           // distanceTextBox.Text = Convert.ToString(IoC.Resolve<MineConfig>().MainViewConfig.Distance.Value, CultureInfo.GetCultureInfo("en-US"));
            borderTextBox.Text = (-IoC.Resolve<MineConfig>().MainViewConfig.Border.Value).ToString(CultureInfo.GetCultureInfo("en-US"));
            borderZeroTextBox.Text = (-IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value).ToString(CultureInfo.GetCultureInfo("en-US"));
            borderRedTextBox.Text = Convert.ToString(IoC.Resolve<MineConfig>().MainViewConfig.BorderRed.Value, CultureInfo.GetCultureInfo("en-US"));
            upZeroZoneTextBox.Text = Convert.ToString(IoC.Resolve<MineConfig>().MainViewConfig.UpZeroZone.Value, CultureInfo.GetCultureInfo("en-US"));

            leftSosudСomboBox.SelectedIndex = Convert.ToInt32(IoC.Resolve<MineConfig>().MainViewConfig.LeftSosud);
            rightSosudComboBox.SelectedIndex = Convert.ToInt32(IoC.Resolve<MineConfig>().MainViewConfig.RightSosud);
            ArchiveStateComboBox.SelectedIndex = Convert.ToInt32(IoC.Resolve<MineConfig>().MainViewConfig.ArchiveState);

            textBoxGreenZoneRabCyl1.Text = Convert.ToString(IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZoneRabCyl1.Value, CultureInfo.GetCultureInfo("en-US"));
            textBoxGreenZoneRabCyl2.Text = Convert.ToString(IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZoneRabCyl2.Value, CultureInfo.GetCultureInfo("en-US"));
            textBoxGreenZonePredCyl1.Text = Convert.ToString(IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZonePredCyl1.Value, CultureInfo.GetCultureInfo("en-US"));
            textBoxGreenZonePredCyl2.Text = Convert.ToString(IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZonePredCyl2.Value, CultureInfo.GetCultureInfo("en-US"));
            textBoxAdcZero.Text = Convert.ToString(IoC.Resolve<MineConfig>().BrakeSystemConfig.AdcZero.Value, CultureInfo.GetCultureInfo("en-US"));
            textBoxAdcMaximum.Text = Convert.ToString(IoC.Resolve<MineConfig>().BrakeSystemConfig.AdcMaximum.Value, CultureInfo.GetCultureInfo("en-US"));
            textBoxAdcValueToBarrKoef.Text = Convert.ToString(IoC.Resolve<MineConfig>().BrakeSystemConfig.AdcValueToBarrKoef.Value, CultureInfo.GetCultureInfo("en-US"));

            comboBoxKletLevelsCount.SelectedIndex = Convert.ToInt32(IoC.Resolve<MineConfig>().KletConfig.KletLevelsCount.Value) - 1;
            textBox1to2distance.Text = Convert.ToString(IoC.Resolve<MineConfig>().KletConfig.FirstLevelHight.Value, CultureInfo.GetCultureInfo("en-US"));
            textBox2to3distance.Text = Convert.ToString(IoC.Resolve<MineConfig>().KletConfig.SecondLevelHight.Value, CultureInfo.GetCultureInfo("en-US"));
            CheckKLetLevels();
        }

        private void maxSpeedTextBox_TextChanged(object sender, EventArgs e)
        {
            if (maxSpeedTextBox.Text == "" || maxSpeedTextBox.Text == ".")
                IoC.Resolve<MineConfig>().MainViewConfig.MaxSpeed.Value = 0;
            else if (Convert.ToDouble(maxSpeedTextBox.Text, CultureInfo.GetCultureInfo("en-US")) > 16)
            {
                IoC.Resolve<MineConfig>().MainViewConfig.MaxSpeed.Value = 16;
                maxSpeedTextBox.Text = Convert.ToString(16, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Масштаб скорости не может быть больше 16 м/с", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
                IoC.Resolve<MineConfig>().MainViewConfig.MaxSpeed.Value = Convert.ToDouble(maxSpeedTextBox.Text, CultureInfo.GetCultureInfo("en-US"));
        }

        private void maxDopRuleSpeedTextBox_TextChanged(object sender, EventArgs e)
        {
            if (maxDopRuleSpeedTextBox.Text == "" || maxDopRuleSpeedTextBox.Text == ".")
                IoC.Resolve<MineConfig>().MainViewConfig.MaxDopRuleSpeed.Value = 0;
            else if (Convert.ToDouble(maxDopRuleSpeedTextBox.Text, CultureInfo.GetCultureInfo("en-US")) > 16)
            {
                IoC.Resolve<MineConfig>().MainViewConfig.MaxDopRuleSpeed.Value = 16;
                maxDopRuleSpeedTextBox.Text = Convert.ToString(16, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Макс. скорость доп. шкалы не может быть больше 16 м/с", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
                IoC.Resolve<MineConfig>().MainViewConfig.MaxDopRuleSpeed.Value = Convert.ToDouble(maxDopRuleSpeedTextBox.Text, CultureInfo.GetCultureInfo("en-US"));
        }

        private void maxTokAnchorTextBox_TextChanged(object sender, EventArgs e)
        {
            if (maxTokAnchorTextBox.Text == "" || maxTokAnchorTextBox.Text == ".")
                IoC.Resolve<MineConfig>().MainViewConfig.MaxTokAnchor.Value = 0;
            else if (Convert.ToDouble(maxTokAnchorTextBox.Text, CultureInfo.GetCultureInfo("en-US")) > 8)
            {
                IoC.Resolve<MineConfig>().MainViewConfig.MaxTokAnchor.Value = 8;
                maxTokAnchorTextBox.Text = Convert.ToString(8, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Масштаб тока якоря не может быть больше 8 кА", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
                IoC.Resolve<MineConfig>().MainViewConfig.MaxTokAnchor.Value = Convert.ToDouble(maxTokAnchorTextBox.Text, CultureInfo.GetCultureInfo("en-US"));
        }

        private void maxTokExcitationTextBox_TextChanged(object sender, EventArgs e)
        {
            if (maxTokExcitationTextBox.Text == "" || maxTokExcitationTextBox.Text == ".")
                IoC.Resolve<MineConfig>().MainViewConfig.MaxTokExcitation.Value = 0;
            else if (Convert.ToDouble(maxTokExcitationTextBox.Text, CultureInfo.GetCultureInfo("en-US")) > 400)
            {
                IoC.Resolve<MineConfig>().MainViewConfig.MaxTokExcitation.Value = 400;
                maxTokExcitationTextBox.Text = Convert.ToString(400, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Масштаб тока возбуждения не может быть больше 400 А", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
                IoC.Resolve<MineConfig>().MainViewConfig.MaxTokExcitation.Value = Convert.ToDouble(maxTokExcitationTextBox.Text, CultureInfo.GetCultureInfo("en-US"));
        }

      /*  private void distanceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (distanceTextBox.Text == "" || distanceTextBox.Text == ".")
                IoC.Resolve<MineConfig>().MainViewConfig.Distance.Value = 0;
            else if (Convert.ToDouble(distanceTextBox.Text, CultureInfo.GetCultureInfo("en-US")) > 1500)
            {
                IoC.Resolve<MineConfig>().MainViewConfig.Distance.Value = 1500;
                distanceTextBox.Text = Convert.ToString(1500, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Глубина не может быть больше 1500 м", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
                IoC.Resolve<MineConfig>().MainViewConfig.Distance.Value = Convert.ToDouble(distanceTextBox.Text, CultureInfo.GetCultureInfo("en-US"));
        } */

        private void borderTextBox_TextChanged(object sender, EventArgs e)
        {
            if (borderTextBox.Text == "" || borderTextBox.Text == "." || borderZeroTextBox.Text == "-")
                IoC.Resolve<MineConfig>().MainViewConfig.Border.Value = 0;
                /* else if (Convert.ToDouble(borderTextBox.Text, CultureInfo.GetCultureInfo("en-US")) > IoC.Resolve<MineConfig>().MainViewConfig.Distance.Value)
            {
                IoC.Resolve<MineConfig>().MainViewConfig.Border.Value = IoC.Resolve<MineConfig>().MainViewConfig.Distance.Value;
                borderTextBox.Text = Convert.ToString(IoC.Resolve<MineConfig>().MainViewConfig.Distance.Value, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Точка стопорения внизу не может быть больше глубины", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            } */
            else
            {
                IoC.Resolve<MineConfig>().MainViewConfig.Border.Value = -Convert.ToDouble(borderTextBox.Text, CultureInfo.GetCultureInfo("en-US"));
                IoC.Resolve<MineConfig>().MainViewConfig.Distance.Value = Math.Abs(IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value) + Math.Abs(IoC.Resolve<MineConfig>().MainViewConfig.Border.Value);
            }
        }

        private void borderZeroTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (borderZeroTextBox.Text == "" || borderZeroTextBox.Text == ".")
                    IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value = 0;
                else
                {
                    IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value = -Convert.ToDouble(borderZeroTextBox.Text, CultureInfo.GetCultureInfo("en-US"));
                    IoC.Resolve<MineConfig>().MainViewConfig.Distance.Value = Math.Abs(IoC.Resolve<MineConfig>().MainViewConfig.BorderZero.Value) + Math.Abs(IoC.Resolve<MineConfig>().MainViewConfig.Border.Value);
                }
            }
            catch (Exception)
            {
                borderZeroTextBox.Text = "";
            }    
        }

        private void borderRedTextBox_TextChanged(object sender, EventArgs e)
        {
            if (borderRedTextBox.Text == "" || borderRedTextBox.Text == ".")
                IoC.Resolve<MineConfig>().MainViewConfig.BorderRed.Value = 0;
            else if (Convert.ToDouble(borderRedTextBox.Text, CultureInfo.GetCultureInfo("en-US")) > 1)
            {
                IoC.Resolve<MineConfig>().MainViewConfig.BorderRed.Value = 1;
                borderRedTextBox.Text = Convert.ToString(1, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Переподъём не может быть больше 1 м", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
                IoC.Resolve<MineConfig>().MainViewConfig.BorderRed.Value = Convert.ToDouble(borderRedTextBox.Text, CultureInfo.GetCultureInfo("en-US"));
        }

        private void upZeroZoneTextBox_TextChanged(object sender, EventArgs e)
        {
            if (upZeroZoneTextBox.Text == "" || upZeroZoneTextBox.Text == ".")
                IoC.Resolve<MineConfig>().MainViewConfig.UpZeroZone.Value = 0;
            else if (Convert.ToDouble(upZeroZoneTextBox.Text, CultureInfo.GetCultureInfo("en-US")) > 25)
            {
                IoC.Resolve<MineConfig>().MainViewConfig.UpZeroZone.Value = 25;
                upZeroZoneTextBox.Text = Convert.ToString(25, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Зона переподъёма не может быть больше 25 м", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
                IoC.Resolve<MineConfig>().MainViewConfig.UpZeroZone.Value = Convert.ToDouble(upZeroZoneTextBox.Text, CultureInfo.GetCultureInfo("en-US"));
        }

        //ограничения на ввод букв и символов
        private void maxSpeedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            e.Handled = !(char.IsDigit(c) || c == '.' || c == '\b');
        }

        private void borderZeroTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            e.Handled = !(char.IsDigit(c) || c == '.' || c == '\b' || c == '-');
        }

        //
        private void leftSosudСomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (rightSosudComboBox.SelectedIndex == 1 && leftSosudСomboBox.SelectedIndex == 1)
            {
                leftSosudСomboBox.SelectedIndex = 0;
                MessageBox.Show("Оба сосуда не могут быть противовесом", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            } */
            IoC.Resolve<MineConfig>().MainViewConfig.LeftSosud = (SosudType)leftSosudСomboBox.SelectedIndex;
        }

        private void rightSosudComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (rightSosudComboBox.SelectedIndex == 1 && leftSosudСomboBox.SelectedIndex == 1)
            {
                rightSosudComboBox.SelectedIndex = 0;
                MessageBox.Show("Оба сосуда не могут быть противовесом", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }*/
            IoC.Resolve<MineConfig>().MainViewConfig.RightSosud = (SosudType)rightSosudComboBox.SelectedIndex;
        }

        private void ArchiveStateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            IoC.Resolve<MineConfig>().MainViewConfig.ArchiveState = (ArchiveState)ArchiveStateComboBox.SelectedIndex;
        }

        //BrakeSystem
        private void textBoxGreenZoneRabCyl1_TextChanged(object sender, EventArgs e)
        {
            if (textBoxGreenZoneRabCyl1.Text == "" || textBoxGreenZoneRabCyl1.Text == ".")
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZoneRabCyl1.Value = 1;
            else if (Convert.ToDouble(textBoxGreenZoneRabCyl1.Text, CultureInfo.GetCultureInfo("en-US")) <= 0)
            {
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZoneRabCyl1.Value = 1;
                textBoxGreenZoneRabCyl1.Text = Convert.ToString(1, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Минимальное давление должно быть больше 0 бар", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            /*else if (Convert.ToDouble(textBoxGreenZoneRabCyl1.Text, CultureInfo.GetCultureInfo("en-US")) >= IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZoneRabCyl2.Value)
            {
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZoneRabCyl1.Value = 1;
                textBoxGreenZoneRabCyl1.Text = Convert.ToString(1, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Минимальное давление должно быть меньше максимального", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            } */
            else
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZoneRabCyl1.Value = Convert.ToDouble(textBoxGreenZoneRabCyl1.Text, CultureInfo.GetCultureInfo("en-US"));
        }

        private void textBoxGreenZoneRabCyl2_TextChanged(object sender, EventArgs e)
        {
            if (textBoxGreenZoneRabCyl2.Text == "" || textBoxGreenZoneRabCyl2.Text == ".")
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZoneRabCyl2.Value = 6;
            /*else if (Convert.ToDouble(textBoxGreenZoneRabCyl2.Text, CultureInfo.GetCultureInfo("en-US")) <= IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZoneRabCyl1.Value)
            {
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZoneRabCyl2.Value = 6;
                textBoxGreenZoneRabCyl2.Text = Convert.ToString(6, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Максимальное давление должно быть больше минимального", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            } */
            else if (Convert.ToDouble(textBoxGreenZoneRabCyl2.Text, CultureInfo.GetCultureInfo("en-US")) > 10)
            {
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZoneRabCyl2.Value = 6;
                textBoxGreenZoneRabCyl2.Text = Convert.ToString(6, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Максимальное давление не должно превышать 10 бар", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZoneRabCyl2.Value = Convert.ToDouble(textBoxGreenZoneRabCyl2.Text, CultureInfo.GetCultureInfo("en-US"));
        }

        private void textBoxGreenZonePredCyl1_TextChanged(object sender, EventArgs e)
        {
            if (textBoxGreenZonePredCyl1.Text == "" || textBoxGreenZonePredCyl1.Text == ".")
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZonePredCyl1.Value = 1;
            else if (Convert.ToDouble(textBoxGreenZonePredCyl1.Text, CultureInfo.GetCultureInfo("en-US")) <= 0)
            {
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZonePredCyl1.Value = 1;
                textBoxGreenZonePredCyl1.Text = Convert.ToString(1, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Минимальное давление должно быть больше 0 бар", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
           /* else if (Convert.ToDouble(textBoxGreenZonePredCyl1.Text, CultureInfo.GetCultureInfo("en-US")) >= IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZonePredCyl2.Value)
            {
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZonePredCyl1.Value = 1;
                textBoxGreenZonePredCyl1.Text = Convert.ToString(1, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Минимальное давление должно быть меньше максимального", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            } */
            else
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZonePredCyl1.Value = Convert.ToDouble(textBoxGreenZonePredCyl1.Text, CultureInfo.GetCultureInfo("en-US"));
        }

        private void textBoxGreenZonePredCyl2_TextChanged(object sender, EventArgs e)
        {
            if (textBoxGreenZonePredCyl2.Text == "" || textBoxGreenZonePredCyl2.Text == ".")
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZonePredCyl2.Value = 6;
            /*else if (Convert.ToDouble(textBoxGreenZonePredCyl2.Text, CultureInfo.GetCultureInfo("en-US")) <= IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZonePredCyl1.Value)
            {
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZonePredCyl2.Value = 6;
                textBoxGreenZonePredCyl2.Text = Convert.ToString(6, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Максимальное давление должно быть больше минимального", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            } */
            else if (Convert.ToDouble(textBoxGreenZonePredCyl2.Text, CultureInfo.GetCultureInfo("en-US")) > 10)
            {
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZonePredCyl2.Value = 6;
                textBoxGreenZonePredCyl2.Text = Convert.ToString(6, CultureInfo.GetCultureInfo("en-US"));
                MessageBox.Show("Максимальное давление не должно превышать 10 бар", "Ошибка ввода параметров", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
                IoC.Resolve<MineConfig>().BrakeSystemConfig.GreenZonePredCyl2.Value = Convert.ToDouble(textBoxGreenZonePredCyl2.Text, CultureInfo.GetCultureInfo("en-US"));
        }

        private void textBoxAdcZero_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAdcZero.Text == "" || textBoxAdcZero.Text == ".")
                IoC.Resolve<MineConfig>().BrakeSystemConfig.AdcZero.Value = 0;
            else
                IoC.Resolve<MineConfig>().BrakeSystemConfig.AdcZero.Value = Convert.ToDouble(textBoxAdcZero.Text, CultureInfo.GetCultureInfo("en-US"));
        }

        private void textBoxAdcMaximum_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAdcMaximum.Text == "" || textBoxAdcMaximum.Text == ".")
                IoC.Resolve<MineConfig>().BrakeSystemConfig.AdcMaximum.Value = 0;
            else
                IoC.Resolve<MineConfig>().BrakeSystemConfig.AdcMaximum.Value = Convert.ToDouble(textBoxAdcMaximum.Text, CultureInfo.GetCultureInfo("en-US"));
        }

        private void textBoxAdcValueToBarrKoef_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAdcValueToBarrKoef.Text == "" || textBoxAdcValueToBarrKoef.Text == ".")
                IoC.Resolve<MineConfig>().BrakeSystemConfig.AdcValueToBarrKoef.Value = 0;
            else
                IoC.Resolve<MineConfig>().BrakeSystemConfig.AdcValueToBarrKoef.Value = Convert.ToDouble(textBoxAdcValueToBarrKoef.Text, CultureInfo.GetCultureInfo("en-US"));
        }
        
        // Klet
        private void comboBoxKletLevelsCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            IoC.Resolve<MineConfig>().KletConfig.KletLevelsCount.Value = comboBoxKletLevelsCount.SelectedIndex + 1;
            CheckKLetLevels();
        }

        private void textBox1to2distance_TextChanged(object sender, EventArgs e)
        {
            if (textBox1to2distance.Text == "" || textBox1to2distance.Text == ".")
                IoC.Resolve<MineConfig>().KletConfig.FirstLevelHight.Value = 0;
            else
                IoC.Resolve<MineConfig>().KletConfig.FirstLevelHight.Value = Convert.ToDouble(textBox1to2distance.Text, CultureInfo.GetCultureInfo("en-US"));
        }

        private void textBox2to3distance_TextChanged(object sender, EventArgs e)
        {
            if (textBox2to3distance.Text == "" || textBox2to3distance.Text == ".")
                IoC.Resolve<MineConfig>().KletConfig.SecondLevelHight.Value = 0;
            else
                IoC.Resolve<MineConfig>().KletConfig.SecondLevelHight.Value = Convert.ToDouble(textBox2to3distance.Text, CultureInfo.GetCultureInfo("en-US"));
        }

        private void CheckKLetLevels()
        {
            if (comboBoxKletLevelsCount.SelectedIndex + 1 == 1)
            {
                IoC.Resolve<MineConfig>().KletConfig.FirstLevelHight.Value = 0;
                textBox1to2distance.Text = "0";
                textBox1to2distance.ReadOnly = true;
                IoC.Resolve<MineConfig>().KletConfig.SecondLevelHight.Value = 0;
                textBox2to3distance.Text = "0";
                textBox2to3distance.ReadOnly = true;
            }
            else if (comboBoxKletLevelsCount.SelectedIndex + 1 == 2)
            {
                IoC.Resolve<MineConfig>().KletConfig.SecondLevelHight.Value = 0;
                textBox2to3distance.Text = "0";
                textBox2to3distance.ReadOnly = true;
                textBox1to2distance.ReadOnly = false;
                textBox1to2distance.Text = Convert.ToString(IoC.Resolve<MineConfig>().KletConfig.FirstLevelHight.Value, CultureInfo.GetCultureInfo("en-US"));
            }
            else if (comboBoxKletLevelsCount.SelectedIndex + 1 == 3)
            {
                textBox1to2distance.ReadOnly = false;
                textBox1to2distance.Text = Convert.ToString(IoC.Resolve<MineConfig>().KletConfig.FirstLevelHight.Value, CultureInfo.GetCultureInfo("en-US"));
                textBox2to3distance.ReadOnly = false;
                textBox2to3distance.Text = Convert.ToString(IoC.Resolve<MineConfig>().KletConfig.SecondLevelHight.Value, CultureInfo.GetCultureInfo("en-US"));
            }
        }
        //



    }
}
