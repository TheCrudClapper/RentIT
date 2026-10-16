namespace RentIT.BlazorFrontend.ServiceContracts;

public interface ILoaderService
{
    public bool IsLoading { get; }
    public event Action<bool>? LoadingChanged;
    Task ShowAsync();
    Task HideAsync();
    Task ExecuteAsync(Func<Task> opearation);
    Task<T> ExecuteAsync<T>(Func<Task<T>> operation);
}
