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

        builder.HasOne(p => p.Role)
            .WithMany(p => p.Users)
            .HasForeignKey(x => x.RoleId);

        builder.HasData(new User[]
        {
            new User { 
                Id = 1,
                Name = "Maxim",
                Surname = "Babyuk",
                Email = "maxslag74@gmail.com",
                Password = "Pa$$word1234",
                Position = "Head Admin",
                RoleId = 1,
                FacilityId = 3
            },
            new User
            {
                Id = 2,
                Name = "Manager",
                Surname = "Test",
                Email = "manager_test@gmail.com",
                Password = "Pa$$word1234",
                Position = "Manager",
                RoleId = 2,
                FacilityId = 3
            },
            new User
            {
                Id = 3,
                Name = "Worker",
                Surname = "Test",
                Email = "worker_test@gmail.com",
                Password = "Pa$$word1234",
                Position = "Worker",
                RoleId = 3,
                FacilityId = 3
            }
        });
    }
}
