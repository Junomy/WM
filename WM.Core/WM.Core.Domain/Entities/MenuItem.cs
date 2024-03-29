﻿namespace WM.Core.Domain.Entities;

public class MenuItem 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
    public List<MenuRole> MenuRoles { get; set; }
}
