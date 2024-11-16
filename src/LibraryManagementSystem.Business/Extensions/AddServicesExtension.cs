using LibraryManagementSystem.Business.Services.Implementations.Identity;
using LibraryManagementSystem.Business.Services.Implementations;
using LibraryManagementSystem.Business.Services.Interfaces.Identity;
using LibraryManagementSystem.Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using LibraryManagementSystem.Business.Services.Implementations.Notification;
using LibraryManagementSystem.Business.Services.Interfaces.Notification;

namespace LibraryManagementSystem.Business.Extensions;

public static class AddServicesExtension
{
    public static IServiceCollection AddServicesService(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<ICoverTypeService, CoverTypeService>();
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IBookService, BookService>();

        services.AddTransient<EmailNotificationService>();
        services.AddTransient<SmsNotificationService>();
        services.AddTransient<PushNotificationService>();
        services.AddSingleton<INotificationServiceFactory, NotificationServiceFactory>();

        return services;
    }
}
