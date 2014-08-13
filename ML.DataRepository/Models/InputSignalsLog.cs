using ML.DataRepository.Models.GenericRepository;

namespace ML.DataRepository.Models
{
    public class InputSignalsLog : IEntityId
    {
        public int Id { get; set; }
        public byte? Vio0 { get; set; }
        public byte? Vio1 { get; set; }
        public byte? Vio2 { get; set; }
        public byte? Vio3 { get; set; }
        public int BlockLogId { get; set; }
        public virtual BlockLog BlockLog { get; set; }
    }
}
