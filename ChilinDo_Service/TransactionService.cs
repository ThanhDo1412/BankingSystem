using System;
using System.Threading.Tasks;
using Chilindo_Database.ViewModel;
using ChilinDo_Service.Interface;

namespace ChilinDo_Service
{
    public class TransactionService : ITransactionService
    {
        public async Task<TransactionBaseResponse> GetBalance(int accountNumber)
        {
            return new TransactionBaseResponse();
        }

        public async Task<TransactionBaseResponse> Deposit(TransactionBaseRequest request)
        {
            return new TransactionBaseResponse();
        }

        public async Task<TransactionBaseResponse> Withdraw(TransactionBaseRequest request)
        {
            return new TransactionBaseResponse();
        }
    }
}
