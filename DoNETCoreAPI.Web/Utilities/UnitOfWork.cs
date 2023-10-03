using DoNETCoreAPI.Web.Context;

namespace DoNETCoreAPI.Web.Utilities
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDatabaseContext _dbContext;
        public UnitOfWork(IDatabaseContext context) 
        { 
            _dbContext = context;
        }
        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}
