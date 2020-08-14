using AuctionWebAPI.Boundaries.Dtos;
using AuctionWebAPI.Boundaries.Repositories;
using AuctionWebAPI.Boundaries.Services.Account;
using AuctionWebAPI.Util.Cryptography;
using AuctionWebAPI.Utils.Boundary;
using AuctionWebAPI.Utils.Exceptions;
using AuctionWebAPI.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using AccountEntity = AuctionWebAPI.Entities.Account.Account;

namespace AuctionWebAPI.Services.Account
{
    public class LoginAccountService : IService<LoginAccountRequest, LoginAccountResponse>
    {
        #region Repositories
        private readonly IAccountRepository accountRepository;
        #endregion

        #region Constructors
        public LoginAccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        #endregion

        public LoginAccountResponse Run(LoginAccountRequest request)
        {
            ValidateRequest(request);

            AccountEntity accountEntity = accountRepository.RetrieveByUsername(request.Username);

            if (accountEntity == null || !string.Equals(accountEntity.Password, Cryptographer.Encrypt(request.Password)))
                throw new BadRequestException("Seu usuario e/ou senha estão incorretos");

            return new LoginAccountResponse()
            {
                Account = new AccountDTO(accountEntity),
                AuthToken = AccountHelper.CreateTokenForAccount(accountEntity.AccountId, request.Username),
            };
        }

        private void ValidateRequest(LoginAccountRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                throw new BadRequestException("Seu usuario e/ou senha estão incorretos");
        }
    }
}
