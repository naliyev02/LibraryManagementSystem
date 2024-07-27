using AutoMapper;
using LibraryManagementSystem.Business.DTOs.AuthorDtos;
using LibraryManagementSystem.Business.Exceptions;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Business.Services.Implementations;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;

    public AuthorService(IAuthorRepository repository, IMapper mapper)
    {
        _repository = repository;
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
            throw new GenericNotFoundException("Nəşriyyatçı taplmadı");

        var pauthorDto = _mapper.Map<AuthorGetByIdDto>(author);

        return pauthorDto;
    }
}
