using Autofac;
using Autofac.Extensions.DependencyInjection;
using DoNETCoreAPI.Web.Utilities.Modules;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
string connectionString = string.Empty;
var connectionSection = builder.Configuration.GetSection("ConnectionStrings");
connectionString = connectionSection["DefaultConnection"]; 

//Autofac module registration
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(b =>b.RegisterModule(new ServiceModule(connectionString) ));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new EFModule(connectionString)));
// Add services to the container.
var assembly= typeof(Program).Assembly.GetExportedTypes()
               .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterTypes(assembly).PropertiesAutowired());

//Configure Controller with Json Response
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 

}

app.UseHttpsRedirection();
app.UseCors(builder =>
{
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
    builder.AllowCredentials();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
