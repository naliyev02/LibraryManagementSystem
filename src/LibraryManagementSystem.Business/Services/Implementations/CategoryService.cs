using AutoMapper;
using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.CategoryDtos;
using LibraryManagementSystem.Business.Exceptions;
using LibraryManagementSystem.Business.Exceptionsı;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;

namespace LibraryManagementSystem.Business.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<CategoryGetDto>> GetAll()
    {
        var categories = _repository.GetAll();

        var categoryDtos = _mapper.Map<List<CategoryGetDto>>(categories);

        return categoryDtos;
    }

    public async Task<CategoryGetDto> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category is null)
            throw new GenericNotFoundException("Kategoriya tapılmadı");

        var categoryDto = _mapper.Map<CategoryGetDto>(category);

        return categoryDto;
    }

    public async Task<GenericResponseDto> CreateAsync(CategoryPostDto categoryPostDto)
    {
        if (categoryPostDto is null)
            throw new GenericNotFoundException("Data boş göndərilib");

        var nameIsExist = await _repository.IsExistAsync(x => x.Name == categoryPostDto.Name);
        if (nameIsExist)
            throw new GenericIsExistException($"Eyni adlı kategoriya mövcuddur: {categoryPostDto.Name}");

        var newCategory = _mapper.Map<Category>(categoryPostDto);

        await _repository.CreateAsync(newCategory);
        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Kategoriya uğurla əlavə edildi");
    }

    public async Task<GenericResponseDto> UpdateAsync(CategoryPutDto categoryPutDto)
    {
        var existingCategory = await _repository.GetByIdAsync(categoryPutDto.Id);
        if (existingCategory is null)
            throw new GenericBadRequestException($"Bu id-li data mövcud deyil: {categoryPutDto.Id} ");

        var nameIsExist = await _repository.IsExistAsync(x => x.Id != categoryPutDto.Id && x.Name == categoryPutDto.Name);
        if (nameIsExist)
            throw new GenericIsExistException($"Eyni adlı kategoriya mövcuddur: {categoryPutDto.Name}");

        existingCategory.Name = categoryPutDto.Name;

        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Kategoriya uğurla yeniləndi");
    }

    public async Task<GenericResponseDto> DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category is null)
            throw new Exception();

        category.IsDeleted = true;

        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Kategoriya uğurla silindi");
    }
}
