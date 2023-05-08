namespace WM.Core.Domain.Entities;

public class MenuItem 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
    public bool Admin { get; set; }
    public bool Manager { get; set; }
    public bool Worker { get; set; }
}
