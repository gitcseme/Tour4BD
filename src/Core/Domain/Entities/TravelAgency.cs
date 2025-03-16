using System.Collections.Generic;

namespace Domain.Entities;

public class TravelAgency : EntityBase<int>
{
    public string Name { get; set; } = string.Empty;
    public string? City { get; set; }
    public string Country { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public ICollection<Company> Companies { get; set; } = [];
    public ICollection<Employee> Employees { get; set; } = [];
}
