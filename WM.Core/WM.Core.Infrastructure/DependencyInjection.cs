using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Infrastructure.Persistence;

namespace WM.Core.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IApplicationContext, ApplicationContext>(o =>
            o.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
