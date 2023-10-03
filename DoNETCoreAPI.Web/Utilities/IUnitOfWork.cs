namespace DoNETCoreAPI.Web.Utilities
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
