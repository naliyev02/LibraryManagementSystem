﻿using AutoMapper;
using LibraryManagementSystem.Business.DTOs.CoverTypeDtos;
using LibraryManagementSystem.Business.DTOs.LanguageDtos;
using LibraryManagementSystem.Business.DTOs.PublisherDtos;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Business.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CoverType, CoverTypeGetDto>().ReverseMap();
        CreateMap<CoverType, CoverTypePostDto>().ReverseMap();
        //CreateMap<CoverType, CoverTypePutDto>().ReverseMap();

        CreateMap<Language, LanguageGetDto>().ReverseMap();
        CreateMap<Language, LanguagePostDto>().ReverseMap();

        CreateMap<Publisher, PublisherGetDto>().ReverseMap();
        CreateMap<Book, PublisherBookGetDtos>().ReverseMap();
        CreateMap<Publisher, PublisherPostDto>().ReverseMap();
    }
}
