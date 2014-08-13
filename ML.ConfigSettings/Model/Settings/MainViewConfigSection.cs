using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ML.ConfigSettings.Model.Settings
{
    public class MainViewConfigSection
    {
        public SimpleParameter MaxSpeed { get; set; }

        public SimpleParameter MaxDopRuleSpeed { get; set; }

        public SimpleParameter MaxTokAnchor { get; set; }

        public SimpleParameter MaxTokExcitation { get; set; }

        public SimpleParameter Distance { get; set; }

        public SimpleParameter BorderRed { get; set; }

        public SimpleParameter UpZeroZone { get; set; }

        public SimpleParameter BorderZero { get; set; }

        public SimpleParameter Border { get; set; }

        public SosudType LeftSosud { get; set; }

        public SosudType RightSosud { get; set; }

        public ArchiveState ArchiveState { get; set; }
    }
}
