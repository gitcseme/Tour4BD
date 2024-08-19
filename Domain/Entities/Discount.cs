using System;

namespace Domain.Entities;

public class Discount : BaseEntity<int>
{
    public int Persentage { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

    public int PackageId { get; set; }
    public Package Package { get; set; }
}
