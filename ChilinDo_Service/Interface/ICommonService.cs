using Chilindo_Database.Entity;
using System.Threading.Tasks;

namespace ChilinDo_Service.Interface
{
    public interface ICommonService
    {
        Task InsertTransactionHistory(TransactionHistory model);
    }
}
