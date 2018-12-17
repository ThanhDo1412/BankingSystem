using Chilindo_Data.Data;
using Chilindo_Data.Midleware;
using Chilindo_Data.UnitOfWork;
using Chilindo_Database.Entity;
using Chilindo_Database.ViewModel;
using ChilinDo_Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ChilinDo_Service
{
    public class TransactionService : BaseService, ITransactionService
    {
        private readonly IUnitOfWork _uow;

        public TransactionService(IUnitOfWork uow) : base(uow)
        {
            _uow = uow;
        }

        public async Task<ICollection<TransactionBaseResponse>> GetBalance(int accountNumber)
        {
            return (await _uow.AccountDetailRepo.FindByConditionAync(x => x.AcountInfoId == accountNumber))
                .Select(x => new TransactionBaseResponse
                {
                    AccountNumber = accountNumber,
                    Balance = x.Balance,
                    Currency = x.Currency,
                    Successful = true,
                    Message = Contanst.Message.TransactionSucceeded
                }).ToList();
        }

        public async Task<TransactionBaseResponse> Deposit(TransactionBaseRequest request)
        {
            var accountDetail = (await _uow.AccountDetailRepo.FindByConditionAync(x => x.AcountInfoId == request.AccountNumber && x.Currency == request.Currency)).FirstOrDefault();

            //Check account existed with this currency
            if (accountDetail != null)
            {
                accountDetail =  new AccountDetail
                {
                    AcountInfoId = request.AccountNumber,
                    Balance = request.Amount,
                    Currency = request.Currency,
                    IsDeleted = false
                };
                _uow.AccountDetailRepo.Create(accountDetail);
            }
            else
            {
                accountDetail.Balance += request.Amount;
                _uow.AccountDetailRepo.Update(accountDetail);
            }

            InsertTransaction(request);
            await _uow.SaveAsyn();

            return new TransactionBaseResponse
            {
                AccountNumber = request.AccountNumber,
                Balance = accountDetail.Balance,
                Currency = accountDetail.Currency,
                Successful = true,
                Message = Contanst.Message.TransactionSucceeded
            };
        }

        public async Task<TransactionBaseResponse> Withdraw(TransactionBaseRequest request)
        {
            var accountDetail = (await _uow.AccountDetailRepo.FindByConditionAync(x => x.AcountInfoId == request.AccountNumber && x.Currency == request.Currency)).FirstOrDefault();

            if (request.Amount > accountDetail.Balance)
            {
                throw new CustomException(ErrorCode.E1, request.AccountNumber);
            }

            //Check account existed with this currency
            if (accountDetail != null)
            {
                accountDetail = new AccountDetail
                {
                    AcountInfoId = request.AccountNumber,
                    Balance = request.Amount,
                    Currency = request.Currency,
                    IsDeleted = false
                };
                _uow.AccountDetailRepo.Create(accountDetail);
            }
            else
            {
                accountDetail.Balance -= request.Amount;
                _uow.AccountDetailRepo.Update(accountDetail);
            }

            InsertTransaction(request);
            await _uow.SaveAsyn();

            return new TransactionBaseResponse
            {
                AccountNumber = request.AccountNumber,
                Balance = accountDetail.Balance,
                Currency = accountDetail.Currency,
                Successful = true,
                Message = Contanst.Message.TransactionSucceeded
            };
        }

        public void InsertTransaction(TransactionBaseRequest request)
        {
            var transaction = new TransactionHistory
            {
                AccountId = request.AccountNumber,
                Amount = request.Amount,
                Currency = request.Currency,
                IsSuccess = true
            };
            _uow.TransactionHistoryRepo.Create(transaction);
        }
    }
}
