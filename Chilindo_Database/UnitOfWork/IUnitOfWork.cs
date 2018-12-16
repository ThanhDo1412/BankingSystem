using Chilindo_Database.Entity;
using System.Threading.Tasks;

namespace Chilindo_Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<AccountInfo> AccountInfoRepo { get; }
        IRepository<AccountDetail> AccountDetailRepo { get; }
        IRepository<TransactionHistory> TransactionHistoryRepo { get; }
        Task SaveAsyn();
    }
}
