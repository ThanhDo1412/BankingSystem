using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chilindo_Data.UnitOfWork;
using Chilindo_Database.Entity;
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

        public async Task InsertTransactionHistory(TransactionHistory model)
        {
            _uow.TransactionHistoryRepo.Create(model);
            await _uow.SaveAsyn();
        }
    }
}
