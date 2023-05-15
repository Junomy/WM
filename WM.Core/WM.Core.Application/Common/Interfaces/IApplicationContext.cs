using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WM.Core.Domain.Entities;

namespace WM.Core.Application.Common.Interfaces;

public interface IApplicationContext
{
    DbSet<Facility> Facilities { get; set; }
    DbSet<Warehouse> Warehouses { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Inventory> Inventories { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<MenuRole> MenuRoles { get; set; }
    ChangeTracker ChangeTracker { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}
