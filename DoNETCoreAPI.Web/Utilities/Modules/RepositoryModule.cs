using Autofac;
using System.Reflection;

namespace DoNETCoreAPI.Web.Utilities.Modules
{
    public class RepositoryModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("DoNETCoreAPI.Web"))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();
        }
    }
}
