
using PSU_PaymentGateway.Models;
using Webshop.Payment.Api.Models;

namespace Webshop.Payment.Api.Repository
{
    public interface IMemoryRepository
    {
        Result AddTransaction(Transaction transaction);
    }
}
