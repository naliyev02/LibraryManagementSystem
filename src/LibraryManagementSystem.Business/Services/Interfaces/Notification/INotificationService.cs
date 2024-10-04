namespace LibraryManagementSystem.Business.Services.Interfaces.Notification;

public interface INotificationService
{
    Task SendMessage(string message);
}
