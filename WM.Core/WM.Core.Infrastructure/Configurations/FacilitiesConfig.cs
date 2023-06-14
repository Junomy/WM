using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WM.Core.Domain.Entities;

namespace WM.Core.Infrastructure.Configurations;

public class FacilitiesConfig : IEntityTypeConfiguration<Facility>
{
    public void Configure(EntityTypeBuilder<Facility> builder)
    {
        builder.HasData(new Facility[]
        {
            new Facility
            {
                Id = 1,
                Name = "Main Facility",
                Description = "This is the main facility.",
                Address = "Earth, Europe, Ukraine",
                IsActive = true,
            }
        });
    }
}
