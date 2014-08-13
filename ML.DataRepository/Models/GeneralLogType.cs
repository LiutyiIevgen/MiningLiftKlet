using ML.DataRepository.Models.GenericRepository;

namespace ML.DataRepository.Models
{
    public class GeneralLogType : IEntityId 
    {
        public int Id { get; set; }

        public string Type { get; set; }
    }
}
