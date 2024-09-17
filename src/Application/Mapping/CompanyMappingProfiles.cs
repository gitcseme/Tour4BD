using Application.Features.Companies.Commands;

using Domain.Entities;

namespace Application.Mapping;

public partial class MappingProfile
{
    public void AddCompanyMapping()
    {
        CreateMap<CreateCompanyCommand, Company>();
    }
}