using System;
using BankingData.Data;
using BankingData.Helper;
using BankingData.UnitOfWork;
using BankingDatabase.Entity;
using BankingDatabase.ViewModel;
using BankingService.Interface;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace BankingService
{
    public class TransactionService : BaseService, ITransactionService
    {
        public TransactionService(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<ICollection<TransactionBaseResponse>> GetBalance(int accountNumber)
        {
            return (await _uow.AccountDetailRepo.FindByConditionAsync(x => x.AcountInfoId == accountNumber && !x.IsDeleted))
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
            //Check account valid
            var account = (await _uow.AccountInfoRepo.FindByConditionAsync(x => x.Id == request.AccountNumber && !x.IsDeleted)).FirstOrDefault();

            if (account == null)
            {
                throw new CustomException(ErrorCode.E3, request.AccountNumber);
            }

            var accountDetail = (await _uow.AccountDetailRepo.FindByConditionAsync(x => x.AcountInfoId == request.AccountNumber && x.Currency == request.Currency && !x.IsDeleted)).FirstOrDefault();

            //Check account existed with this currency
            if (accountDetail != null)
            {
                accountDetail.Balance += request.Amount;
                _uow.AccountDetailRepo.Update(accountDetail);
            }
            else
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
            //Check account valid
            var account = (await _uow.AccountInfoRepo.FindByConditionAsync(x => x.Id == request.AccountNumber && !x.IsDeleted)).FirstOrDefault();

            if (account == null)
            {
                throw new CustomException(ErrorCode.E3, request.AccountNumber);
            }

            var accountDetail = (await _uow.AccountDetailRepo.FindByConditionAsync(x => x.AcountInfoId == request.AccountNumber && x.Currency == request.Currency && !x.IsDeleted)).FirstOrDefault();
            
            //Check account existed with this currency
            if (accountDetail == null)
            {
                throw new CustomException(ErrorCode.E4, request.AccountNumber);
            }

            if (request.Amount > accountDetail.Balance)
            {
                throw new CustomException(ErrorCode.E1, request.AccountNumber);
            }

            accountDetail.Balance -= request.Amount;
            _uow.AccountDetailRepo.Update(accountDetail);

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

        public async Task InsertTransaction(TransactionBaseResponse model)
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
