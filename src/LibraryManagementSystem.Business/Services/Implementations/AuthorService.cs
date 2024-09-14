using AutoMapper;
using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.AuthorDtos;
using LibraryManagementSystem.Business.DTOs.PublisherDtos;
using LibraryManagementSystem.Business.Exceptions;
using LibraryManagementSystem.Business.Exceptionsı;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Entities.Identity;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Business.Services.Implementations;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly IBookAuthorRepository _bookAuthorRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public AuthorService(IAuthorRepository repository, IBookAuthorRepository bookAuthorRepository, IMapper mapper, UserManager<AppUser> userManager)
    {
        _repository = repository;
        _bookAuthorRepository = bookAuthorRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<List<AuthorGetDto>> GetAll()
    {
        var authors = _repository.GetAll();

        var authorDtos = _mapper.Map<List<AuthorGetDto>>(authors);

        return authorDtos;
    }

    public async Task<AuthorGetByIdDto> GetByIdAsync(int id)
    {
        var author = await _repository.GetByIdAsync(id,
            a => a.Include(ba => ba.BookAuthors).ThenInclude(b => b.Book));

        if (author is null)
            throw new GenericNotFoundException("Müəllif tapılmadı");

        var authorDto = _mapper.Map<AuthorGetByIdDto>(author);

        return authorDto;
    }


    public async Task<GenericResponseDto> CreateAsync(AuthorPostDto authorPostDto)
    {
        if (authorPostDto is null)
            throw new GenericNotFoundException("Data boş göndərilib");

        var newAuthor = _mapper.Map<Author>(authorPostDto);

        await _repository.CreateAsync(newAuthor);
        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Müəllif uğurla əlavə edildi");
    }

    public async Task<GenericResponseDto> UpdateAsync(AuthorPutDto authorPutDto)
    {
        var author = await _repository.GetByIdAsync(authorPutDto.Id, a =>
        a.Include(ba => ba.BookAuthors).ThenInclude(b => b.Book));
        if (author is null)
            throw new GenericBadRequestException($"Bu id-li data mövcud deyil: {authorPutDto.Id} ");

        if (authorPutDto.UserId != null)
        {
            var user = await _userManager.FindByIdAsync(authorPutDto.UserId);
            if (user == null)
                throw new GenericNotFoundException("User tapılmadı");
        }

        var existingAuthor = await _repository.IsExistAsync(x => x.Id !=  authorPutDto.Id && x.UserId == authorPutDto.UserId);
        if (existingAuthor)
            throw new GenericIsExistException("2 müəllif eyni istifadəçi nömrəsi ilə əlaqələndirilə bilməz ");


        author.UserId = authorPutDto.UserId;
        author.FirstName = authorPutDto.FirstName;
        author.LastName = authorPutDto.LastName;
        author.DateOfBirth = authorPutDto.DateOfBirth;
        author.Nationality = authorPutDto.Nationality;

        foreach (var BookAuthor in author.BookAuthors)
        {
            _bookAuthorRepository.Delete(BookAuthor);
        }

        foreach (var BookAuthor in authorPutDto.BookAuthors)
        {
            await _bookAuthorRepository.CreateAsync(new BookAuthor { AuthorId = author.Id, BookId = BookAuthor.BookId });
        }

        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Müəllif uğurla yeniləndi");
    }

    public async Task<GenericResponseDto> DeleteAsync(int id)
    {
        var author = await _repository.GetByIdAsync(id);
        if (author is null)
            throw new Exception();

        author.IsDeleted = true;

        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Müəllif uğurla silindi");
    }
}
