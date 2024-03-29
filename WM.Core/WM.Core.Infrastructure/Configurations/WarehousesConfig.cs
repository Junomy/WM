﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WM.Core.Domain.Entities;

namespace WM.Core.Infrastructure.Configurations;

public class WarehousesConfig : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.HasOne(p => p.Facility)
            .WithMany(x => x.Warehouses)
            .HasForeignKey(x => x.FacilityId);

        builder.HasData(new Warehouse[]
        {
            new Warehouse
            {
                Id = 1,
                Name = "W-A1",
                Description = "This is a warehouse A1",
                FacilityId = 1,
            }
        });
    }
}
