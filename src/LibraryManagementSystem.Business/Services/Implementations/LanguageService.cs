using AutoMapper;
using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.LanguageDtos;
using LibraryManagementSystem.Business.Exceptions;
using LibraryManagementSystem.Business.Exceptionsı;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;

namespace LibraryManagementSystem.Business.Services.Implementations;

public class LanguageService : ILanguageService
{
    private readonly ILanguageRepository _repository;
    private readonly IMapper _mapper;

    public LanguageService(ILanguageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<LanguageGetDto>> GetAll()
    {
        var languages = _repository.GetAll();

        var languageDtos = _mapper.Map<List<LanguageGetDto>>(languages);

        return languageDtos;
    }

    public async Task<LanguageGetDto> GetByIdAsync(int id)
    {
        var language = await _repository.GetByIdAsync(id);

        if (language is null)
            throw new GenericNotFoundException("Dil tapılmadı");

        var languageDto = _mapper.Map<LanguageGetDto>(language);

        return languageDto;
    }

    public async Task<GenericResponseDto> CreateAsync(LanguagePostDto languagePostDto)
    {
        if (languagePostDto is null)
            throw new GenericNotFoundException("Data boş göndərilib");

        var nameIsExist = await _repository.IsExistAsync(x => x.Name == languagePostDto.Name);
        if (nameIsExist)
            throw new GenericIsExistException($"Eyni adlı dil mövcuddur: {languagePostDto.Name}");

        var newLanguage = _mapper.Map<Language>(languagePostDto);

        await _repository.CreateAsync(newLanguage);
        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Dil uğurla əlavə edildi");
    }

    public async Task<GenericResponseDto> UpdateAsync(LanguagePutDto languagePutDto)
    {
        var existingLanguage = await _repository.GetByIdAsync(languagePutDto.Id);
        if (existingLanguage is null)
            throw new GenericBadRequestException($"Bu id-li data mövcud deyil: {languagePutDto.Id} ");

        var nameIsExist = await _repository.IsExistAsync(x => x.Id != languagePutDto.Id && x.Name == languagePutDto.Name);
        if (nameIsExist)
            throw new GenericIsExistException($"Eyni adlı dil mövcuddur: {languagePutDto.Name}");

        existingLanguage.Name = languagePutDto.Name;

        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Dil uğurla yeniləndi");
    }

    public async Task<GenericResponseDto> DeleteAsync(int id)
    {
        var language = await _repository.GetByIdAsync(id);
        if (language is null)
            throw new Exception();

        language.IsDeleted = true;

        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Dil uğurla silindi");
    }
}
