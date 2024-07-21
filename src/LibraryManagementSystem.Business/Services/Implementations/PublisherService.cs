using AutoMapper;
using LibraryManagementSystem.Business.DTOs.PublisherDtos;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        var publishers = await _repository.GetAll().Include(x => x.Books).ToListAsync(); //Task 1: include-u repository-də yaz

        var publisherDtos = _mapper.Map<List<PublisherGetDto>>(publishers);

        return publisherDtos;
    }

    
}
