using BankingDatabase.Entity;
using System.Threading.Tasks;

namespace BankingData.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<AccountInfo> AccountInfoRepo { get; }
        IRepository<AccountDetail> AccountDetailRepo { get; }
        IRepository<TransactionHistory> TransactionHistoryRepo { get; }
        Task SaveAsyn();
    }
}
