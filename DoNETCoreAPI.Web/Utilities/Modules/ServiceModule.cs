using Autofac;

namespace DoNETCoreAPI.Web.Utilities.Modules
{
    public class ServiceModule: Autofac.Module
    {
        public string ConnectionString = "";
        public ServiceModule(string connectionString) 
        {
            ConnectionString = connectionString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new EFModule(ConnectionString));
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterAssemblyTypes()
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
