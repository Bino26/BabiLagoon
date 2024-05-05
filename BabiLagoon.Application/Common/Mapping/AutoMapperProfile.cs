using AutoMapper;
using BabiLagoon.Application.Common.DTOs;
using BabiLagoon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabiLagoon.Application.Common.Mapping
{
    public class AutoMapperProfile : Profile
    {
       public AutoMapperProfile()
        {
            CreateMap<Villa, VillaDto>().ReverseMap();
            CreateMap<CreateVillaDto, Villa>().ReverseMap();
            CreateMap<UpdateVillaDto, Villa>().ReverseMap();

            CreateMap<Amenity, AmenityDto>().ReverseMap();
            CreateMap<CreateAmenityDto, Amenity>().ReverseMap();
            CreateMap<UpdateAmenityDto, Amenity>().ReverseMap();

            CreateMap<LoginRequestDto, UserDto>().ReverseMap();
        }
    }
}
