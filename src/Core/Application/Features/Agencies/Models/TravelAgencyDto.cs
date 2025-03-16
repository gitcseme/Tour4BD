using Application.Features.Agencies.Commands;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Agencies.Models;

public class TravelAgencyBaseModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? City { get; set; }
    public required string Country { get; set; }
    public required string Address { get; set; }
}

public class TravelAgencyDetailModel : TravelAgencyBaseModel
{

}

public class TravelAgencyListModel : TravelAgencyBaseModel
{

}

public class TravelAgencyMappingProfile : Profile
{
    public TravelAgencyMappingProfile()
    {
        CreateMap<AddOrUpdateTravelAgencyCommand, TravelAgency>();
    }
}