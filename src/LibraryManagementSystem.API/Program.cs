using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.DataAccess.Contexts;
using LibraryManagementSystem.Business.Mappers;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;
using LibraryManagementSystem.DataAccess.Repositories.Implementations;
using LibraryManagementSystem.Business.Services.Implementations;
using LibraryManagementSystem.Business.Services.Interfaces;

namespace LibraryManagementSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            builder.Services.AddScoped<ICoverTypeRepository, CoverTypeRepository>();
            builder.Services.AddScoped<ICoverTypeService, CoverTypeService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}