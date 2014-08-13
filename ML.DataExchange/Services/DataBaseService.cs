using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML.DataExchange.Model;
using ML.DataRepository.Models;
using ML.DataRepository.Models.GenericRepository;


namespace ML.DataExchange.Services
{
    public class DataBaseService
    {
        public DataBaseService()
        {
        }

        public void FillGeneralLog(string line, string shortLine, GeneralLogEventType type)
        {
            var generalLog = new GeneralLog
            {
                Date = DateTime.Now,
                GeneralLogTypeId = Convert.ToInt32(type),
                LogLine = line,
                LogShortLine = shortLine
            };
            using (var repoUnit = new RepoUnit())
            {
                repoUnit.GeneralLog.Save(generalLog);
            }
        }
    }
}
