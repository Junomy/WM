using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WM.Core.Application.Common.Interfaces;
using WM.Core.Infrastructure.Persistence;

namespace WM.Core.Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddDbContext<IApplicationContext, ApplicationContext>(o => { 
                o.UseInMemoryDatabase($"WMDB-TEST-{Guid.NewGuid()}");
            });
        });
    }
}
