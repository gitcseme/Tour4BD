namespace Domain.Entities;

public class Rating : EntityBase<int>
{
    public int Star { get; set; }
    public int? CompanyId { get; set; }
    public int? PackageId { get; set; }
    public int VisitorId { get; set; }

    public Company Company { get; set; }
    public Package Package { get; set; }
    public Visitor Visitor { get; set; }
}
