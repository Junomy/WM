using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WM.Core.Domain.Entities;

namespace WM.Core.Infrastructure.Configurations;

public class UsersConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(p => p.Facility)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.FacilityId);
    }
}
