namespace LibraryManagementSystem.Business.Services.Interfaces.Notification;

public interface INotificationServiceFactory
{
    INotificationService GetNotificationService(string type);
}
