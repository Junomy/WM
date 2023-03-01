using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WM.Core.Application.Common.Interfaces;

namespace WM.Core.Infrastructure.Persistence;

public class ApplicationContext : DbContext, IApplicationContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
