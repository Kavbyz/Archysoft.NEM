using System.Threading.Tasks;
using io.nem1.sdk.Model.Transactions;

namespace Archysoft.NEM.Domain.Model.Services.Abstract
{
    public interface ITransactionService
    {
        Task<TransactionResponse> Send(object data);
    }
}
