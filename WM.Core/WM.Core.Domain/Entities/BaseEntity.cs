﻿namespace WM.Core.Domain.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime ModifiedAt { get; set; }
    public int ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}
