using AutoMapper;
using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.CoverTypeDtos;
using LibraryManagementSystem.Business.Exceptions;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;

namespace LibraryManagementSystem.Business.Services.Implementations;

public class CoverTypeService : ICoverTypeService
{
    private readonly ICoverTypeRepository _repository;
    private readonly IMapper _mapper;

    public CoverTypeService(ICoverTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<CoverTypeGetDto>> GetAll()
    {
        var coverTypes = _repository.GetAll();

        var coverTypeDtos = _mapper.Map<List<CoverTypeGetDto>>(coverTypes);

        return coverTypeDtos;
    }

    public async Task<CoverTypeGetDto> GetByIdAsync(int id)
    {
        var coverType = await _repository.GetByIdAsync(id);

        if (coverType is null)
            throw new GenericNotFoundException("Cover Type tapılmadı");

        var coverTypeDto = _mapper.Map<CoverTypeGetDto>(coverType);

        return coverTypeDto;
    }

    public async Task<GenericResponseDto> CreateAsync(CoverTypePostDto coverTypePostDto)
    {
        if (coverTypePostDto is null) 
            throw new Exception();

        var nameIsExist = await _repository.IsExistAsync(x => x.Name == coverTypePostDto.Name);
        if (nameIsExist)
            throw new Exception();

        var newCoverType = _mapper.Map<CoverType>(coverTypePostDto);

        await _repository.CreateAsync(newCoverType);
        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Cover Type successfully created");
    }

    public async Task<GenericResponseDto> UpdateAsync(CoverTypePutDto coverTypePutDto)
    {
        var existingCoverType = await _repository.GetByIdAsync(coverTypePutDto.Id);
        if (existingCoverType is null)
            throw new Exception();

        var nameIsExist = await _repository.IsExistAsync(x => x.Id != coverTypePutDto.Id && x.Name ==  coverTypePutDto.Name);
        if(nameIsExist)
            throw new Exception();

        existingCoverType.Name = coverTypePutDto.Name;

        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Cover Type successfully updated");
    }

    public async Task<GenericResponseDto> DeleteAsync(int id)
    {
        var coverType = await _repository.GetByIdAsync(id);
        if (coverType is null)
            throw new Exception();

        coverType.IsDeleted = true;

        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Cover Type successfully deleted");
    }
}
