using Chilindo_Data.UnitOfWork;

namespace ChilinDo_Service
{
    public class BaseService
    {
        private readonly IUnitOfWork _uow;

        public BaseService(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
