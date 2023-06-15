namespace WM.Core.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Position { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }

    public int? FacilityId { get; set; }
    public Facility? Facility { get; set; }
}
