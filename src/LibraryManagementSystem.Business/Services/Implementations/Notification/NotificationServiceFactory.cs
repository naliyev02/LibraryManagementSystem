using LibraryManagementSystem.Business.Services.Interfaces.Notification;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Business.Services.Implementations.Notification;

public class NotificationServiceFactory : INotificationServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public NotificationServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public INotificationService GetNotificationService(string type)
    {
        return type switch
        {
            "Email" => _serviceProvider.GetRequiredService<EmailNotificationService>(),
            "Sms" => _serviceProvider.GetRequiredService<SmsNotificationService>(),
            "Push" => _serviceProvider.GetRequiredService<PushNotificationService>(),
            _ => throw new ArgumentException("Invalid notification type")
        };
    }
}
