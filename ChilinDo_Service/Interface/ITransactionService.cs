using Chilindo_Database.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChilinDo_Service.Interface
{
    public interface ITransactionService
    {
        Task<ICollection<TransactionBaseResponse>> GetBalance(int accountNumber);
        Task<TransactionBaseResponse> Deposit(TransactionBaseRequest request);
        Task<TransactionBaseResponse> Withdraw(TransactionBaseRequest request);
        void InsertTransaction(TransactionBaseRequest request);
    }
}
