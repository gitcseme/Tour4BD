namespace Domain.Entities;

public class Spot : BaseEntity<int>
{
    public string Name { get; set; }
    public int PackageId { get; set; }
    public Package Package { get; set; }
}
