namespace Domain.Entities;

public class Spot : EntityBase<int>
{
    public string Name { get; set; } = string.Empty;
    public int PackageId { get; set; }
    public Package Package { get; set; }
}
