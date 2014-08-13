using ML.DataRepository.Models.GenericRepository;

namespace ML.DataRepository.Models
{
    public class AnalogSignalLog : IEntityId
    {
        public int Id { get; set; }

        public int BlockLogId { get; set; }
        public int NodeId { get; set; }
        public int SignalTypeId { get; set; }

        public double SignalValue { get; set; }

        public virtual BlockLog BlockLog { get; set; }
        public virtual AnalogSignal SignalType { get; set; }
    }
}
