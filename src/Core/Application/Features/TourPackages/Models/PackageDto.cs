using AutoMapper;
using Domain.Entities;

namespace Application.Features.TourPackages.Models;

public class PackageBaseModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }

    public int CompanyId { get; set; }
}

public class PackageDetailModel : PackageBaseModel
{
}

public class PackageListModel : PackageBaseModel
{
}

public class PackageMappingProfile : Profile
{
    public PackageMappingProfile()
    {
        CreateMap<Package, PackageBaseModel>();
        CreateMap<Package, PackageDetailModel>();
        CreateMap<Package, PackageListModel>()
            .ForAllMembers(opt => opt.ExplicitExpansion());
    }
}