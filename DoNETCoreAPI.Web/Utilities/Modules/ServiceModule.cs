using Autofac;
using System.Reflection;

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
            builder.RegisterAssemblyTypes(Assembly.Load("DoNETCoreAPI.Web"))
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().PropertiesAutowired()
                .InstancePerLifetimeScope();
        }
    }
}
