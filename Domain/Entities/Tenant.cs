namespace Domain.Entities;

public class Tenant : BaseEntity<int>
{
    public required string OrganizationName { get; set; }
    public required string ConnectionString { get; set; }
}
