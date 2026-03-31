namespace suprimmil.Services;

public class ToastService
{
    public event Action? OnToastChanged;

    public bool ShowToast { get; private set; } = false;
    public string Message { get; private set; } = string.Empty;
    public bool IsSuccess { get; private set; } = false;

    public async Task ShowToastAsync(string message, bool isSuccess)
    {
        Message = message;
        IsSuccess = isSuccess;
        ShowToast = true;
        OnToastChanged?.Invoke();

        await Task.Delay(2000);

        ShowToast = false;
        OnToastChanged?.Invoke();
    }
}
