﻿using AutoMapper;
using CommandsService.DTOs;
using CommandsService.Models;
using WebApplication1;

namespace CommandsService.Profiles
{
	public class CommandsProfile : Profile
	{
        public CommandsProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<PlatformPublishedDto, Platform>()
                .ForMember(dest => dest.ExternalId,opt => opt.MapFrom(src => src.Id));
			CreateMap<GrpcPlatformModel, Platform>()
			    .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.PlatformId))
				 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				 .ForMember(dest => dest.Commands, opt => opt.Ignore());
		}
	}
}
