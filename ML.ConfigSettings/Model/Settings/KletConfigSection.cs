using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.ConfigSettings.Model.Settings
{
    public class KletConfigSection
    {
        public SimpleParameter KletLevelsCount { get; set; }
        public SimpleParameter FirstLevelHight { get; set; }
        public SimpleParameter SecondLevelHight { get; set; }
    }
}
