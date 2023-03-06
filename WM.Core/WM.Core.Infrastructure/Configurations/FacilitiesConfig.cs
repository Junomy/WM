using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WM.Core.Domain.Entities;

namespace WM.Core.Infrastructure.Configurations;

public class FacilitiesConfig : IEntityTypeConfiguration<Facility>
{
    public void Configure(EntityTypeBuilder<Facility> builder)
    {
        
    }
}
