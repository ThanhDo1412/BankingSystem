using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chilindo_Data.UnitOfWork;
using Chilindo_Database.Entity;
using Chilindo_Database.ViewModel;
using ChilinDo_Service.Interface;

namespace ChilinDo_Service
{
    public class CommonService : ICommonService
    {
        private readonly IUnitOfWork _uow;

        public CommonService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task InsertTransactionHistory(TransactionBaseResponse model)
        {
            var transactionHistory = new TransactionHistory
            {
                AccountId = model.AccountNumber,
                Amount = model.Balance,
                Currency = model.Currency,
                IsSuccess = model.Successful,
                Message = model.Message
            };
            _uow.TransactionHistoryRepo.Create(transactionHistory);
            await _uow.SaveAsyn();
        }
    }
}
