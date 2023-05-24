﻿using Microsoft.EntityFrameworkCore;
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

        builder.HasData(new MenuRole[]
        {
            new MenuRole
            {
                Id = 1,
                MenuId = 1,
                RoleId = 1,
            },
            new MenuRole
            {
                Id = 2,
                MenuId = 2,
                RoleId = 1,
            },
            new MenuRole
            {
                Id = 3,
                MenuId = 3,
                RoleId = 1,
            },
            new MenuRole
            {
                Id = 4,
                MenuId = 4,
                RoleId = 1,
            },
            new MenuRole
            {
                Id = 5,
                MenuId = 1,
                RoleId = 2,
            },
            new MenuRole
            {
                Id = 6,
                MenuId = 2,
                RoleId = 2,
            },
            new MenuRole
            {
                Id = 7,
                MenuId = 3,
                RoleId = 2,
            },
            new MenuRole
            {
                Id = 8,
                MenuId = 4,
                RoleId = 2,
            },
            new MenuRole
            {
                Id = 9,
                MenuId = 1,
                RoleId = 3,
            },
            new MenuRole
            {
                Id = 10,
                MenuId = 3,
                RoleId = 3,
            },
        });
    }
}