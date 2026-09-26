namespace RentIT.BlazorFrontend.ServiceContracts;

public interface INotificationService
{
    Task ShowSuccess(string title, string message);
    Task ShowWarning(string title, string message);
    Task ShowError(string title, string message);
}

