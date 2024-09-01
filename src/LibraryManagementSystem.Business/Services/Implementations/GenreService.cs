using AutoMapper;
using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.AuthorDtos;
using LibraryManagementSystem.Business.DTOs.GenreDtos;
using LibraryManagementSystem.Business.Exceptions;
using LibraryManagementSystem.Business.Exceptionsı;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Repositories.Implementations;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Business.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;
    private readonly IMapper _mapper;

    public GenreService(IGenreRepository repository , IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GenreGetDto>> GetAll()
    {
        var genres = _repository.GetAll();

        var genreDtos = _mapper.Map<List<GenreGetDto>>(genres);

        return genreDtos;
    }

    public async Task<GenreGetByIdDto> GetByIdAsync(int id)
    {
        var genre = await _repository.GetByIdAsync(id,
            a => a.Include(ba => ba.BookGenres).ThenInclude(b => b.Book));

        if (genre is null)
            throw new GenericNotFoundException("Janr tapılmadı");

        var genreDto = _mapper.Map<GenreGetByIdDto>(genre);

        return genreDto;
    }

    public async Task<GenericResponseDto> CreateAsync(GenrePostDto genrePostDto)
    {
        if (genrePostDto is null)
            throw new GenericNotFoundException("Data boş göndərilib");

        var newGenre = _mapper.Map<Genre>(genrePostDto);

        await _repository.CreateAsync(newGenre);
        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Janr uğurla əlavə edildi");
    }

    public async Task<GenericResponseDto> UpdateAsync(GenrePutDto genrePutDto)
    {
        var existingGenre = await _repository.GetByIdAsync(genrePutDto.Id, a =>
        a.Include(ba => ba.BookGenres).ThenInclude(b => b.Book));

        if (existingGenre is null)
            throw new GenericBadRequestException($"Bu id-li data mövcud deyil: {genrePutDto.Id} ");

        existingGenre.Name = genrePutDto.Name;
        existingGenre.CategoryId = genrePutDto.CategoryId;
        
        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Janr uğurla yeniləndi");
    }

    public async Task<GenericResponseDto> DeleteAsync(int id)
    {
        var genre = await _repository.GetByIdAsync(id);
        if (genre is null)
            throw new Exception();

        genre.IsDeleted = true;

        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Janr uğurla silindi");
    }


}
