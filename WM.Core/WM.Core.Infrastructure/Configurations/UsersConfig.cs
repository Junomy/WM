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

        builder.HasData(new User[]
        {
            new User { 
                Id = 1,
                Name = "Maxim",
                Surname = "Babyuk",
                Email = "maxslag74@gmail.com",
                Password = "Pa$$word1234",
                Position = "Head Admin",
                Role = Domain.Enums.Roles.Admin,
                FacilityId = null
            },
            new User
            {
                Id = 2,
                Name = "Manager",
                Surname = "Test",
                Email = "manager_test@gmail.com",
                Password = "Pa$$word1234",
                Position = "Manager",
                Role = Domain.Enums.Roles.Manager,
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
                Role = Domain.Enums.Roles.Worker,
                FacilityId = 3
            }
        });
    }
}
