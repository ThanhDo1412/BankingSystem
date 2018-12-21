using System;
using BankingData;
using BankingData.Entity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BankingData.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChilinDoContext _chilinDoContext;
        private IRepository<AccountInfo> _accountInfoRepo;
        private IRepository<AccountDetail> _accountDetailRepo;
        private IRepository<TransactionHistory> _transactionHistoryRepo;

        public UnitOfWork(ChilinDoContext chilinDoContext)
        {
            _chilinDoContext = chilinDoContext;
        }

        public IRepository<AccountInfo> AccountInfoRepo
        {
            get
            {
                return _accountInfoRepo = _accountInfoRepo ?? new Repository<AccountInfo>(_chilinDoContext);
            }
        }

        public IRepository<AccountDetail> AccountDetailRepo
        {
            get
            {
                return _accountDetailRepo = _accountDetailRepo ?? new Repository<AccountDetail>(_chilinDoContext);
            }
        }

        public IRepository<TransactionHistory> TransactionHistoryRepo
        {
            get
            {
                return _transactionHistoryRepo = _transactionHistoryRepo ?? new Repository<TransactionHistory>(_chilinDoContext);
            }
        }

        public async Task SaveAsyn()
        {
            await _chilinDoContext.SaveChangesAsync();
        }
    }
}
