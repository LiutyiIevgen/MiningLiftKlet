using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML.DataRepository.Models.GenericRepository;

namespace ML.DataRepository.Models
{
    public class GeneralLog : IEntityId
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int GeneralLogTypeId { get; set; }

        public string LogLine { get; set; }
        public string LogShortLine { get; set; }

        public virtual GeneralLogType GeneralLogType { get; set; }
    }
}
