using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Domain.Entities;

namespace WM.Core.Infrastructure.Configurations;

public class RolesConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(new Role[]
        {
            new Role
            {
                Id = 1,
                Name = "Admin"
            },
            new Role
            {
                Id = 2,
                Name = "Manager"
            },
            new Role
            {
                Id = 3,
                Name = "Worker"
            }
        });
    }
}
