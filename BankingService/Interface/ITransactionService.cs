using BankingService.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingService.Interface
{
    public interface ITransactionService
    {
        Task<ICollection<TransactionBaseResponse>> GetBalance(int accountNumber);
        Task<TransactionBaseResponse> Deposit(TransactionBaseRequest request);
        Task<TransactionBaseResponse> Withdraw(TransactionBaseRequest request);
        void InsertTransaction(TransactionBaseRequest request, string message);
        Task InsertTransaction(TransactionBaseResponse model);
    }
}
