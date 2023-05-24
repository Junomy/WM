using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WM.Core.Domain.Entities;

namespace WM.Core.Infrastructure.Configurations;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(x => x.Facility)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.FacilityId);
    }
}
