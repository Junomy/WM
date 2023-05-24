using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WM.Core.Domain.Entities;

namespace WM.Core.Infrastructure.Configurations;

public class OrderStatusConfig : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.HasData(new OrderStatus[]
        {
            new OrderStatus { 
                Id = 1,
                Name = "New"
            },
            new OrderStatus {
                Id = 2,
                Name = "Accepted"
            },
            new OrderStatus {
                Id = 3,
                Name = "Closed"
            },
        });
    }
}
