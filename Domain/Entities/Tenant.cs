using Domain.Utilities;

namespace Domain.Entities;

public class Tenant : BaseEntity<int>
{
    public required string OrganizationName { get; set; }

    [Encrypted]
    public required string ConnectionString { get; set; }
}
