using Autofac;
using DoNETCoreAPI.Web.Context;
using Microsoft.EntityFrameworkCore;

namespace DoNETCoreAPI.Web.Utilities.Modules
{
    public class EFModule : Autofac.Module
    {
        string connectionString = "DefaultConnection";
        public EFModule(string conString) :base()
        { 
            connectionString=conString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            Autofac.NamedParameter para = new NamedParameter("ConnectionString", connectionString);
            builder.RegisterType(typeof(DatabaseContext)).As(typeof(IDatabaseContext)).WithParameter(para).PropertiesAutowired().InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).PropertiesAutowired().InstancePerLifetimeScope();
        }
    }
}
