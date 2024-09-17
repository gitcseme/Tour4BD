using AutoMapper;

namespace Application.Mapping;

public partial class MappingProfile : Profile
{
    public MappingProfile()
    {
        AddCompanyMapping();
    }
}
