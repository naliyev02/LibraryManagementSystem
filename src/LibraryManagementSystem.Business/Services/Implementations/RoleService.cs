using AutoMapper;
using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.RoleDtos;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.DataAccess.Contexts;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net;

namespace LibraryManagementSystem.Business.Services.Implementations;

public class RoleService : IRoleService
{
    private readonly AppDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;


    public RoleService(AppDbContext context, IMapper mapper, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public async Task<GenericResponseDto> CreateRoleAsync(RolePostDto rolePostDto)
    {
        var result = await _roleManager.CreateAsync(new IdentityRole(rolePostDto.Name));

        return new((int)HttpStatusCode.Created, "Role successfully created");
    }

    //public async Task<List<RoleGetResponseDto>> GetAllRolesAsync()
    //{
    //    var roles = await _context.Roles.ToListAsync();

    //    var roleGetResponseDtos = _mapper.Map<List<RoleGetResponseDto>>(roles);
    //    return roleGetResponseDtos;
    //}
}
