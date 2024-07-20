﻿using AutoMapper;
using LibraryManagementSystem.Business.DTOs.CoverTypeDtos;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Business.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CoverType, CoverTypeGetDto>().ReverseMap();
        CreateMap<CoverType, CoverTypePostDto>().ReverseMap();
        CreateMap<CoverType, CoverTypePutDto>().ReverseMap();
    }
}
