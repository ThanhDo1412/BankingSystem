using Chilindo_Database.ViewModel;
using System.Threading.Tasks;

namespace ChilinDo_Service.Interface
{
    public interface ICommonService
    {
        Task InsertTransactionHistory(TransactionBaseResponse model);
    }
}
