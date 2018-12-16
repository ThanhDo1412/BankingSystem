using Chilindo_Database.ViewModel;
using System.Threading.Tasks;

namespace ChilinDo_Service.Interface
{
    public interface ITransactionService
    {
        Task<TransactionBaseResponse> GetBalance(int accountNumber);
        Task<TransactionBaseResponse> Deposit(TransactionBaseRequest request);
        Task<TransactionBaseResponse> Withdraw(TransactionBaseRequest request);
    }
}
