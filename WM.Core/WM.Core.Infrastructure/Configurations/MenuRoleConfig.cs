using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WM.Core.Domain.Entities;

namespace WM.Core.Infrastructure.Configurations;

public class MenuRoleConfig : IEntityTypeConfiguration<MenuRole>
{
    public void Configure(EntityTypeBuilder<MenuRole> builder)
    {
        builder.HasOne(x => x.Role)
            .WithMany(x => x.MenuRoles)
            .HasForeignKey(x => x.RoleId);
        builder.HasOne(x => x.MenuItem)
            .WithMany(x => x.MenuRoles)
            .HasForeignKey(x => x.MenuId);
    }
}
