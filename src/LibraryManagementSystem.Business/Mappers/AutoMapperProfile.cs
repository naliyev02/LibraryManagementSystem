using AutoMapper;
using LibraryManagementSystem.Business.DTOs.AuthorDtos;
using LibraryManagementSystem.Business.DTOs.BookAuthorDtos;
using LibraryManagementSystem.Business.DTOs.BookDtos;
using LibraryManagementSystem.Business.DTOs.BookGenreDtos;
using LibraryManagementSystem.Business.DTOs.CategoryDtos;
using LibraryManagementSystem.Business.DTOs.CoverTypeDtos;
using LibraryManagementSystem.Business.DTOs.GenreDtos;
using LibraryManagementSystem.Business.DTOs.LanguageDtos;
using LibraryManagementSystem.Business.DTOs.PublisherDtos;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Business.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<DateTime, DateOnly>().ReverseMap();

        CreateMap<CoverType, CoverTypeGetDto>().ReverseMap();
        CreateMap<CoverType, CoverTypePostDto>().ReverseMap();
        CreateMap<CoverType, CoverTypePutDto>().ReverseMap();

        CreateMap<Language, LanguageGetDto>().ReverseMap();
        CreateMap<Language, LanguagePostDto>().ReverseMap();
        CreateMap<Language, LanguagePutDto>().ReverseMap();

        CreateMap<Publisher, PublisherGetDto>().ReverseMap();
        CreateMap<Publisher, PublisherGetByIdDto>().ReverseMap();
        CreateMap<Publisher, PublisherPostDto>().ReverseMap();
        CreateMap<Publisher, PublisherPutDto>().ReverseMap();

        CreateMap<Book, BookGetDto>().ReverseMap();
        CreateMap<Book, BookGetByIdDto>()
            .ForMember(x => x.Authors, y => y.MapFrom(x => x.BookAuthors))
            .ForMember(x => x.Genres, y => y.MapFrom(x => x.BookGenres))
            .ReverseMap();
        CreateMap<Book, BookPostDto>()
            .ForMember(x => x.Authors, y => y.MapFrom(x => x.BookAuthors))
            .ForMember(x => x.Genres, y => y.MapFrom(x => x.BookGenres))
            .ReverseMap();
        CreateMap<Book, BookPutDto>().ReverseMap();

        CreateMap<BookAuthor, AuthorBookGetDto>().ReverseMap();
        CreateMap<BookAuthor, AuthorBookPostDto>().ReverseMap();
        CreateMap<BookAuthor, BookAuthorGetDto>().ReverseMap();
        CreateMap<BookAuthor, BookAuthorPostDto>().ReverseMap();

        CreateMap<Author, AuthorGetDto>().ReverseMap();
        CreateMap<Author, AuthorGetByIdDto>()
            .ForMember(x => x.Books, y => y.MapFrom(x => x.BookAuthors))
            .ReverseMap();
        CreateMap<Author, AuthorPostDto>().ReverseMap();

        CreateMap<Category, CategoryGetDto>().ReverseMap();
        CreateMap<Category, CategoryPostDto>().ReverseMap();
        CreateMap<Category, CategoryPutDto>().ReverseMap();

        CreateMap<Genre, GenreGetDto>().ReverseMap();
        CreateMap<Genre, GenreGetByIdDto>()
            .ForMember(x => x.Books, y => y.MapFrom(x => x.BookGenres))
            .ReverseMap();
        CreateMap<Genre, GenrePostDto>().ReverseMap();

        CreateMap<BookGenre, GenreBookGetDto>().ReverseMap();
        CreateMap<BookGenre, BookGenreGetDto>().ReverseMap();
        CreateMap<BookGenre, BookGenrePostDto>().ReverseMap();

    }
}
