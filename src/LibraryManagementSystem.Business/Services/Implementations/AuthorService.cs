using AutoMapper;
using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.AuthorDtos;
using LibraryManagementSystem.Business.Exceptions;
using LibraryManagementSystem.Business.Exceptionsı;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Business.Services.Implementations;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly IBookAuthorRepository _bookAuthorRepository;
    private readonly IMapper _mapper;

    public AuthorService(IAuthorRepository repository, IBookAuthorRepository bookAuthorRepository, IMapper mapper)
    {
        _repository = repository;
        _bookAuthorRepository = bookAuthorRepository;
        _mapper = mapper;
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
            throw new GenericNotFoundException("Müəllif taplmadı");

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
        var existingAuthor = await _repository.GetByIdAsync(authorPutDto.Id, a =>
        a.Include(ba => ba.BookAuthors).ThenInclude(b => b.Book));
        if (existingAuthor is null)
            throw new GenericBadRequestException($"Bu id-li data mövcud deyil: {authorPutDto.Id} ");

        existingAuthor.FirstName = authorPutDto.FirstName;
        existingAuthor.LastName = authorPutDto.LastName;
        existingAuthor.DateOfBirth = authorPutDto.DateOfBirth;
        existingAuthor.Nationality = authorPutDto.Nationality;

        foreach (var BookAuthor in existingAuthor.BookAuthors)
        {
            _bookAuthorRepository.Delete(BookAuthor);
        }

        foreach (var BookAuthor in authorPutDto.BookAuthors)
        {
            await _bookAuthorRepository.CreateAsync(new BookAuthor { AuthorId = existingAuthor.Id, BookId = BookAuthor.BookId });
        }

        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Nəşriyyatçı uğurla yeniləndi");
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
