using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ML.DataRepository.Models.GenericRepository;

namespace ML.DataRepository.Models
{
    public class BlockLog : IEntityId
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<MainDigitSignalsLog> MainDigitSignalsLogs { get; set; }
        public virtual ICollection<InputSignalsLog> InputSignalsLogs { get; set; }
        public virtual ICollection<OutputSignalsLog> OutputSignalsLogs { get; set; }
        public virtual ICollection<AnalogSignalLog> AnalogSignalLogs { get; set; }
    }
}
