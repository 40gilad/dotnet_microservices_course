﻿using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Profiles
{
	public class PlatformProfile : Profile
	{
        public PlatformProfile()
        {
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformReadDto, PlatformPublishedDto>();
            CreateMap<Platform, GrpcPlatformModel>()
                .ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher));


        }
    }
}
