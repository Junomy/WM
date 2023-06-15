namespace WM.Core.Domain.Entities;

public class MenuRole
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public int MenuId { get; set; }
    public MenuItem MenuItem { get; set; }
}
