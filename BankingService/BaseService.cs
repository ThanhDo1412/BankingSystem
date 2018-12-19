using BankingData.UnitOfWork;

namespace BankingService
{
    public class BaseService
    {
        protected readonly IUnitOfWork _uow;

        protected BaseService(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
