namespace Application.Features.Companies.Models;

public class CompanyBaseModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string LisenceLink { get; set; }
    public bool IsActive { get; set; }
    public int TravelAgencyId { get; set; }
}

public class CompanyDetailModel : CompanyBaseModel
{
}

public class CompanyListModel : CompanyBaseModel
{
}