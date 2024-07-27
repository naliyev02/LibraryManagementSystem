using AutoMapper;
using LibraryManagementSystem.Business.DTOs.LanguageDtos;
using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.PublisherDtos;
using LibraryManagementSystem.Business.Exceptions;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Business.Exceptionsı;

namespace LibraryManagementSystem.Business.Services.Implementations;

public class PublisherService : IPublisherService
{
    private readonly IPublisherRepository _repository;
    private readonly IMapper _mapper;

    public PublisherService(IPublisherRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<PublisherGetDto>> GetAll()
    {
        var publishers = _repository.GetAll(); 

        var publisherDtos = _mapper.Map<List<PublisherGetDto>>(publishers);

        return publisherDtos;
    }

    public async Task<PublisherGetDto> GetByIdAsync(int id)
    {
        var publisher = await _repository.GetByIdAsync(id,
            p => p.Include(pb => pb.Books));

        if (publisher is null)
            throw new GenericNotFoundException("Nəşriyyatçı taplmadı"); 

        var publiherDto = _mapper.Map<PublisherGetDto>(publisher);

        return publiherDto;
    }

    public async Task<GenericResponseDto> CreateAsync(PublisherPostDto publisherPostDto)
    {
        if (publisherPostDto is null)
            throw new GenericNotFoundException("Data boş göndərilib");

        var nameIsExist = await _repository.IsExistAsync(x => x.Name == publisherPostDto.Name);
        if (nameIsExist)
            throw new GenericIsExistException($"Eyni adlı nəşriyyatçı mövcuddur: {publisherPostDto.Name}");

        var newPublisher = _mapper.Map<Publisher>(publisherPostDto);

        await _repository.CreateAsync(newPublisher);
        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Nəşriyyatçı uğurla əlavə edildi");
    }

    public async Task<GenericResponseDto> UpdateAsync(PublisherPutDto publisherPutDto)
    {
        var existingPublisher = await _repository.GetByIdAsync(publisherPutDto.Id);
        if (existingPublisher is null)
            throw new GenericBadRequestException($"Bu id-li data mövcud deyil: {publisherPutDto.Id} ");

        var nameIsExist = await _repository.IsExistAsync(x => x.Id != publisherPutDto.Id && x.Name == publisherPutDto.Name);
        if (nameIsExist)
            throw new GenericIsExistException($"Eyni adlı nəşriyyatçı mövcuddur: {publisherPutDto.Name}");

        existingPublisher.Name = publisherPutDto.Name;
        existingPublisher.Address = publisherPutDto.Address;
        existingPublisher.ContactNumber = publisherPutDto.ContactNumber;
        //var newPublisher = _mapper.Map<Publisher>(publisherPutDto);
        //_repository.Update(newPublisher);
        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Nəşriyyatçı uğurla yeniləndi");
    }

    public async Task<GenericResponseDto> DeleteAsync(int id)
    {
        var publisher = await _repository.GetByIdAsync(id);
        if (publisher is null)
            throw new Exception();

        publisher.IsDeleted = true;

        await _repository.SaveAsync();

        return new GenericResponseDto(200, "Nəşriyyatçı uğurla silindi");
    }

}
