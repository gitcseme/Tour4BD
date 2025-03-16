using System;

namespace Domain.Entities;

public class Discount : EntityBase<int>
{
    public string Title { get; set; }
    public int Persentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

    public int PackageId { get; set; }
    public Package Package { get; set; }
}
