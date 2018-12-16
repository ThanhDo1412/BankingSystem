using System;
using System.Threading.Tasks;
using Chilindo_Data.Data;
using Chilindo_Data.UnitOfWork;
using Chilindo_Database.ViewModel;
using ChilinDo_Service.Interface;

namespace ChilinDo_Service
{
    public class TransactionService : BaseService, ITransactionService
    {
        private readonly IUnitOfWork _uow;

        public TransactionService(IUnitOfWork uow) : base(uow)
        {
            _uow = uow;
        }

        public async Task<TransactionBaseResponse> GetBalance(int accountNumber)
        {
            var accountDetail = await _uow.AccountDetailRepo.FindByIdAync(accountNumber);
            return new TransactionBaseResponse
            {
                AccountNumber = accountNumber,
                Balance = accountDetail.Balance,
                Currency = accountDetail.Currency,
                Successful = true,
                Message = Contanst.Message.TransactionSucceeded
            };
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
