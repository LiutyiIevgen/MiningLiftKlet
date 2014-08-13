using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ML.ConfigSettings.Model;
using ML.ConfigSettings.Model.Settings;

namespace ML.ConfigSettings.Services
{
    public class MineConfig : AbstractConfig<MineConfig>
    {
        public string CanName { get; set; }
        public int CanSpeed { get; set; }
        public int LeadingController { get; set; }
        public int ReceiveDataDelay { get; set; } // receive delay in data exchange
        public int MaxDopMismatch { get; set; } // max correct mismatch in majorization
        public MainViewConfigSection MainViewConfig { get; set; }
        public AuziDSignalsConfigSection AuziDSignalsConfig { get; set; }
        public ParametersConfigSection ParametersConfig { get; set; }
        public BrakeSystemConfigSection BrakeSystemConfig { get; set; }

        protected override MineConfig GetObject()
        {
            return this;
        }

        protected override void GetDefault()
        {

            CanName = "CAN1";
            CanSpeed = 250;
            LeadingController = 1;
            ReceiveDataDelay = 14;
            MaxDopMismatch = 10;
            MainViewConfig = new MainViewConfigSection()
            {
                MaxSpeed = new SimpleParameter() {Value = 12},
                MaxDopRuleSpeed = new SimpleParameter() {Value = 2},
                MaxTokAnchor = new SimpleParameter() {Value = 4},
                MaxTokExcitation = new SimpleParameter() {Value = 200},
                Distance = new SimpleParameter() {Value = 300},
                BorderRed = new SimpleParameter() {Value = 0.5},
                UpZeroZone = new SimpleParameter() {Value = 10},
                BorderZero = new SimpleParameter() {Value = -50},
                Border = new SimpleParameter() {Value = 250},
                LeftSosud = SosudType.Skip,
                RightSosud = SosudType.Skip,
                ArchiveState = ArchiveState.Inactive
            };
            AuziDSignalsConfig = new AuziDSignalsConfigSection()
            {
                AddedSignals =
                    new string[]
                    {
                        "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15",
                        "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "30", "31", "29", "72", "73",
                        "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87"
                    },
                SignalsNames =
                    new string[]
                    {
                        "РСВ", "РСН", "ВККВ", "ВККН", "ВЗВ", "ВЗН", "-", "Prepare", "1РЗ", "1ТП", "1СКАР", "2СКАР", "3СКАР",
                        "РРМ", "-", "КнПуск", "ПОВ", "ПОН", "-", "РЗП", "-", "-", "-", "-", "Ревизия", "Автомат",
                        "Ручной", "РО", "РОл", "-", "-", "-", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41",
                        "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57",
                        "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "РТР", "РС2",
                        "НЗ", "SQ19", "SQ38", "РС3", "Готовн.", "РС1", "SQ2", "SQ8", "SQ9", "SQ7", "SQ11',SQ12", "SQ18",
                        "-", "SQ21", "SQ29", "SQ30", "SQ34", "SQ32", "SQ33", "SQ25", "-", "РЗВ", "-", "-", "-", "-", "-",
                        "-", "-", "РМЗ", "РПС", "РСД", "РПН", "РПВ", "РГЗ", "РТД", "РВД", "112", "113", "114", "115",
                        "116", "117", "118", "119", "120", "121", "122", "123", "124", "125", "126", "127", "128", "129",
                        "130", "131", "132", "133", "134", "135", "136", "137", "138", "139", "140", "141", "142", "143",
                        "144"
                    }
            };
            ParametersConfig = new ParametersConfigSection()
            {
                ParametersFileName = "c:\\Users\\Женя\\Documents\\Work\\MiningLift\\ParametersFiles"
            };
            BrakeSystemConfig = new BrakeSystemConfigSection()
            {
                GreenZoneRabCyl1 = new SimpleParameter() {Value = 4},
                GreenZoneRabCyl2 = new SimpleParameter() {Value = 6},
                GreenZonePredCyl1 = new SimpleParameter() {Value = 4},
                GreenZonePredCyl2 = new SimpleParameter() {Value = 6},
                AdcZero = new SimpleParameter() {Value = 0},
                AdcMaximum = new SimpleParameter() {Value = 5},
                AdcValueToBarrKoef = new SimpleParameter() {Value = 1}
            };
        }

        protected override void CopyObject(MineConfig config)
        {
            CanName = config.CanName;
            CanSpeed = config.CanSpeed;
            LeadingController = config.LeadingController;
            ReceiveDataDelay = config.ReceiveDataDelay;
            MaxDopMismatch = config.MaxDopMismatch;
            MainViewConfig = config.MainViewConfig;
            AuziDSignalsConfig = config.AuziDSignalsConfig;
            ParametersConfig = config.ParametersConfig;
            BrakeSystemConfig = config.BrakeSystemConfig;
        }

    }
}
