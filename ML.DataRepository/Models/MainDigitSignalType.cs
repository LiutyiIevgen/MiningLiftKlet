using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML.DataRepository.Models.GenericRepository;

namespace ML.DataRepository.Models
{
    public class MainDigitSignalType : IEntityId 
    {
        public int Id { get; set; }

        public string Type { get; set; }
    }
}
