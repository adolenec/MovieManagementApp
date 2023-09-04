﻿using System;
using api.Domain.Entities;
using api.Dtos;
using AutoMapper;

namespace api.Mappings
{
	public class MappingConfiguration : Profile
	{
        public MappingConfiguration()
        {
            #region Movies

            CreateMap<Movie, MovieDto>()
                .ForCtorParam(ctorParamName: "Category", opt => opt.MapFrom(src => src.Category.Name))
                .ForCtorParam(ctorParamName: "Director", opt =>
                        opt.MapFrom(src => src.Director.FirstName + " " + src.Director.LastName));

            CreateMap<Movie, MovieDetailsDto>()
                .ForCtorParam(ctorParamName: "Category", opt => opt.MapFrom(src => src.Category.Name))
                .ForCtorParam(ctorParamName: "Director", opt =>
                        opt.MapFrom(src => src.Director.FirstName + " " + src.Director.LastName));

            #endregion

        }
    }
}

