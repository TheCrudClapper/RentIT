using RentIT.BlazorFrontend.ServiceContracts;

namespace RentIT.BlazorFrontend.Services;
public class LoaderService : ILoaderService
{
    public bool IsLoading => _pendingOperations > 0;
    private int _pendingOperations { get; set; }
    public event Action<bool>? LoadingChanged;
    public async Task ShowAsync()
    {
        _pendingOperations++;

        if (_pendingOperations == 1)
            LoadingChanged?.Invoke(true);

    }
    public async Task HideAsync()
    {
        if (_pendingOperations == 0)
            return;

        _pendingOperations--;

        if (_pendingOperations == 0)
            LoadingChanged?.Invoke(false);
    }

    public async Task ExecuteAsync(Func<Task> operation)
    {
        await ShowAsync();

        try
        {
            await operation();
        }
        finally
        {
            await HideAsync();
        }
    }

    public async Task<T> ExecuteAsync<T>(Func<Task<T>> operation)
    {
        await ShowAsync();

        try
        {
            return await operation();
        }
        finally
        {
            await HideAsync();
        }
    }
}
