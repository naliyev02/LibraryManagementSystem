using AutoMapper;
using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.BookDtos;
using LibraryManagementSystem.Business.DTOs.GenericPaginationAndSearchDtos;
using LibraryManagementSystem.Business.Exceptions;
using LibraryManagementSystem.Business.Exceptionsı;
using LibraryManagementSystem.Business.Extensions;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.Business.Utilities.Helpers;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Business.Services.Implementations;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;
    private readonly IBookAuthorRepository _bookAuthorRepository;
    private readonly IBookGenreRepository _bookGenreRepository;

    public BookService(IBookRepository repository, IMapper mapper, IBookAuthorRepository bookAuthorRepository, IBookGenreRepository bookGenreRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _bookAuthorRepository = bookAuthorRepository;
        _bookGenreRepository = bookGenreRepository;
    }

    public async Task<List<BookGetDto>> GetAll()
    {
        var books = _repository.GetAll();

        var bookDtos = _mapper.Map<List<BookGetDto>>(books);

        return bookDtos;
    }

    public async Task<PaginationAndSearchGetDto<BookGetDto>> GetWithPaginationAndSearch(PaginationAndSearchPostDto genericPaginationAndSearchDto)
    {
        var books = _repository.GetAll(c => c.Include(x => x.CoverType));

        books = await books.QueryableSearch(genericPaginationAndSearchDto?.Searchs);
        books = await books.QueryableOrderBy(genericPaginationAndSearchDto?.OrderBy);
        var pagination = await books.Pagination(genericPaginationAndSearchDto?.Pagination);


        var bookDtos = _mapper.Map<PaginationAndSearchGetDto<BookGetDto>>(pagination);

        return bookDtos;
    }

    public async Task<BookGetByIdDto> GetByIdAsync(int id)
    {
        var book = await _repository.GetByIdAsync(id,
            ct => ct.Include(x => x.CoverType),
            l => l.Include(x => x.Language),
            p => p.Include(x => x.Publisher),
            ba => ba.Include(x => x.BookAuthors).ThenInclude(x => x.Author),
            bg => bg.Include(x => x.BookGenres).ThenInclude(x => x.Genre));

        if (book is null)
            throw new GenericNotFoundException("Kitab tapılmadı");

        var bookDto = _mapper.Map<BookGetByIdDto>(book);

        return bookDto;
    }

    public async Task<GenericResponseDto> CreateAsync(BookPostDto bookPostDto)
    {
        if (bookPostDto is null)
            throw new GenericNotFoundException("Data boş göndərilib");

        var ISBNIsExist = await _repository.IsExistAsync(x => x.ISBN == bookPostDto.ISBN);
        if (ISBNIsExist)
            throw new GenericIsExistException($"Eyni ISBN  nömrəli kitab mövcuddur - ISBN nömrəsi: {bookPostDto.ISBN}");

        var newBook = _mapper.Map<Book>(bookPostDto);

        await _repository.CreateAsync(newBook);
        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Kitab uğurla əlavə edildi");
    }

    public async Task<GenericResponseDto> UpdateAsync(BookPutDto bookPutDto)
    {
        var transaction = await _repository.BeginTransactionAsync();

        var existingBook = await _repository.GetByIdAsync(bookPutDto.Id,
            ct => ct.Include(x => x.CoverType),
            l => l.Include(x => x.Language),
            p => p.Include(x => x.Publisher),
            ba => ba.Include(x => x.BookAuthors).ThenInclude(x => x.Author),
            bg => bg.Include(x => x.BookGenres).ThenInclude(x => x.Genre));

        if (existingBook is null)
            throw new GenericBadRequestException($"Bu id-li data mövcud deyil: {bookPutDto.Id} ");

        var ISBNIsExist = await _repository.IsExistAsync(x => x.Id != bookPutDto.Id && x.ISBN == bookPutDto.ISBN);
        if (ISBNIsExist)
            throw new GenericIsExistException($"Eyni ISBN  nömrəli kitab mövcuddur - ISBN nömrəsi: {bookPutDto.ISBN}");

        existingBook.Title = bookPutDto.Title;
        existingBook.ISBN = bookPutDto.ISBN;
        existingBook.CoverTypeId = bookPutDto.CoverTypeId;
        existingBook.LanguageId = bookPutDto.LanguageId;
        existingBook.PageCount = bookPutDto.PageCount;
        existingBook.PublisherId = bookPutDto.PublisherId;
        existingBook.PublishedDate = bookPutDto.PublishedDate;
        existingBook.CopiesAvailable = bookPutDto.CopiesAvailable;

        await RelationshipUpdateHelper.UpdateManyToManyAsync(
            existingBook.BookAuthors,
            bookPutDto.Authors.Select(x => x.AuthorId),
            x => x.AuthorId,
            id => new BookAuthor { BookId = existingBook.Id, AuthorId = id },
            _bookAuthorRepository
        );

        await RelationshipUpdateHelper.UpdateManyToManyAsync(
            existingBook.BookGenres,
            bookPutDto.Genres.Select(x => x.GenreId),
            x => x.GenreId,
            id => new BookGenre { BookId = existingBook.Id, GenreId = id },
            _bookGenreRepository
        );

        await _repository.SaveAsync();
        await transaction.CommitAsync();

        return new GenericResponseDto(200, "Kitab uğurla yeniləndi");
    }

    public async Task<GenericResponseDto> DeleteAsync(int id)
    {
        var book = await _repository.GetByIdAsync(id);
        if (book is null)
            throw new Exception();

        book.IsDeleted = true;

        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Kitab uğurla silindi");
    }
}
