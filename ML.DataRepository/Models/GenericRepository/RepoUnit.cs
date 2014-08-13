using System;
using System.Data.Entity.Validation;
using ML.DataRepository.DataAccess;

namespace ML.DataRepository.Models.GenericRepository
{
    public class RepoUnit : IDisposable
    {
        private MineContext _context;
        private IDataRepository<BlockLog> _BlockLog;
        private IDataRepository<AnalogSignal> _analogSignal;
        private IDataRepository<GeneralLog> _GeneralLog;

        private MineContext getContext()
        {
            return _context ?? (_context = new MineContext());
        }


        public IDataRepository<BlockLog> BlockLog 
        {
            get { return _BlockLog ?? (_BlockLog = getRepository<BlockLog>()); }
        }

        public IDataRepository<AnalogSignal> AnalogSignal
        {
            get { return _analogSignal ?? (_analogSignal = getRepository<AnalogSignal>()); }
        }

        public IDataRepository<GeneralLog> GeneralLog
        {
            get { return _GeneralLog ?? (_GeneralLog = getRepository<GeneralLog>()); }
        }

        public void Commit() {
            try {
                _context.SaveChanges();
            } catch (DbEntityValidationException dbEx) {
                foreach (var validationErrors in dbEx.EntityValidationErrors) {
                    foreach (var validationError in validationErrors.ValidationErrors) {
                        Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine("Save failed", ex);
            }
        }

        public void Dispose() {
            if (_context != null)
                _context.Dispose();
            
            //GC.SuppressFinalize(this);
        }

        private IDataRepository<T> getRepository<T>()
            where T : class, IEntityId
        {
            return new DataRepository<T>(getContext());
        }
    }
}
