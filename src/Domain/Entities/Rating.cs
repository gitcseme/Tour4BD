namespace Domain.Entities;

public class Rating : BaseEntity<int>
{
    public int? CompanyId { get; set; }
    public Company Company { get; set; }

    public int? PackageId { get; set; }
    public Package Package { get; set; }

    public int UserId { get; set; }
    public ApplicationUser User { get; set; }
}
