using LibraryManagementSystem.DataAccess.Repositories.Implementations;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Business.Extensions;

public static class AddRepositoriesExtension
{
    public static IServiceCollection AddRepositoriesService(this IServiceCollection services)
    {
        services.AddScoped<ICoverTypeRepository, CoverTypeRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookGenreRepository, BookGenreRepository>();

        return services;
    }
}
