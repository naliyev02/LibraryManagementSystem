using AutoMapper;
using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.RoleDtos;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.Core.Entities.Identity;
using LibraryManagementSystem.DataAccess.Contexts;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net;

namespace LibraryManagementSystem.Business.Services.Implementations;

public class RoleService : IRoleService
{
    private readonly AppDbContext _context;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;


    public RoleService(AppDbContext context, IMapper mapper, RoleManager<AppRole> roleManager)
    {
        _context = context;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<RoleGetDto>> GetAllRolesAsync()
    {
        var roles = _roleManager.Roles;

        var roleDtos = _mapper.Map<IEnumerable<RoleGetDto>>(roles);
        return roleDtos;
    }

    public async Task<GenericResponseDto> CreateRoleAsync(RolePostDto rolePostDto)
    {
        var result = await _roleManager.CreateAsync(new AppRole(rolePostDto.Name));

        return new((int)HttpStatusCode.Created, "Role successfully created");
    }

    

    public async Task<GenericResponseDto> DeleteRoleAsync(string name)
    {
        var role = await _roleManager.FindByNameAsync(name);

        if (role == null)
        {
            return new((int)HttpStatusCode.NotFound, "Role not found");
        }

        var result = await _roleManager.DeleteAsync(role);

        if (result.Succeeded)
        {
            return new((int)HttpStatusCode.OK, "Role successfully deleted");
        }

        return new((int)HttpStatusCode.InternalServerError, "Failed to delete role");
    }
}
