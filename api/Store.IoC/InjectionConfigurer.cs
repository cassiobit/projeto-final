using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Serilog;
using Store.Domain.Services;
using Store.Domain.Services.Interfaces;
using Store.Domain.DataBase;

namespace Store.IoC;
public class InjectionConfigurer
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        var log = Log.ForContext(typeof(InjectionConfigurer));

        log.Information("Injetando dependências");
        services.AddDbContext<AppDbContext>();
        services.AddScoped<ICustomerService, CustomerService>();
    }
}
