using Autofac;

namespace DoNETCoreAPI.Web.Utilities.Modules
{
    public class RepositoryModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes()
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
