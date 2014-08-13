using ML.DataRepository.Models.GenericRepository;

namespace ML.DataRepository.Models
{
    public class AnalogSignal : IEntityId 
    {
        public int Id { get; set; }

        public string Type { get; set; }
    }
}
