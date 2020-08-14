using AuctionWebAPI.Boundaries.Repositories;
using AuctionWebAPI.Boundaries.Services.Account;
using AuctionWebAPI.Utils.Boundary;
using AuctionWebAPI.Utils.Exceptions;
using AuctionWebAPI.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using AccountEntity = AuctionWebAPI.Entities.Account.Account;

namespace AuctionWebAPI.Services.Account
{
    public class DeleteAccountService : IService<DeleteAccountRequest, DeleteAccountResponse>
    {
        #region Repositories
        private readonly IAccountRepository accountRepository;
        #endregion

        #region Constructors
        public DeleteAccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        #endregion

        public DeleteAccountResponse Run(DeleteAccountRequest request)
        {
            ValidateRequest(request);

            if (!accountRepository.Delete(request.AccountId))
                throw new BadRequestException($"Não foi encontrado uma conta com AccountId : {request.AccountId} ");

            return new DeleteAccountResponse()
            {
                AccountId = request.AccountId
            };
        }

        private void ValidateRequest(DeleteAccountRequest request)
        {
            AccountEntity accountEntity = accountRepository.Retrieve(request.AccountId);

            if (accountEntity == null)
                throw new InvalidParamException("AccountId", request.AccountId);
        }
    }
}
