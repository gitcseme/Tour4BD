﻿namespace Domain.Entities;

public class Company : BaseEntity<int>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string LisenceLink { get; set; }

    public string OwnerId { get; set; }
    public ApplicationUser User { get; set; }
}
