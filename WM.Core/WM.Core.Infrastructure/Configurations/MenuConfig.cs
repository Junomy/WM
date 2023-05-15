using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WM.Core.Domain.Entities;

namespace WM.Core.Infrastructure.Configurations;

public class MenuConfig : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.HasData(new MenuItem[]
        {
            new MenuItem {
                Id = 1,
                Name = "Inventory",
                Link = "/inventory",
            },
            new MenuItem { 
                Id = 2,
                Name = "Orders",
                Link = "/orders",
            },
            new MenuItem { 
                Id = 3,
                Name = "Products",
                Link = "/products",
            },
            new MenuItem { 
                Id = 4,
                Name = "Warehouses",
                Link = "/warehouses",
            },
        });;
    }
}
