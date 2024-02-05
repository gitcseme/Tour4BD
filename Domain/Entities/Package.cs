namespace Domain.Entities;

public class Package : BaseEntity<int>
{
    public string Name { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }

    public int CompanyId { get; set; }
    public Company Comapny { get; set; }
}
