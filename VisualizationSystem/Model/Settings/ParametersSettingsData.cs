using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace VisualizationSystem.Model.Settings
{
    public class ParametersSettingsData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public CodtDomainData[] CodtDomainArray;
    }

    public class CodtDomainData
    {
        public int Coordinate { get; set; }
        public int Speed { get; set; }
    }
}
