namespace Webshop.Payment.Api.Services
{
    public interface IThrottleService
    {
        bool CanExecute();
    }
}
