﻿namespace WM.Core.Domain.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<User> Users { get; set; }
    public List<MenuRole> MenuRoles { get; set; }
}
