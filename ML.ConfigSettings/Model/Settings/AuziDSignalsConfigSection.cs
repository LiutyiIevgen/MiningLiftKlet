using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ML.ConfigSettings.Model.Settings
{
    public class AuziDSignalsConfigSection
    {
        public string[] AddedSignals { get; set; }

        public string[] SignalsNames { get; set; }
    }
}
