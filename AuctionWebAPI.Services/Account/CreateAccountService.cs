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
    public class CreateAccountService : IService<CreateAccountRequest, CreateAccountResponse>
    {
        #region Repositories
        private readonly IAccountRepository accountRepository;
        #endregion

        #region Constructors
        public CreateAccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        #endregion

        public CreateAccountResponse Run(CreateAccountRequest request)
        {
            ValidateRequest(request);

            AccountEntity accountEntity = new AccountEntity()
            {
                Username = request.Username,
                Password = Cryptographer.Encrypt(request.Password),
            };

            accountEntity.AccountId = accountRepository.Create(accountEntity);

            return new CreateAccountResponse()
            {
                Account = new AccountDTO(accountEntity),
                AuthToken = AccountHelper.CreateTokenForAccount(accountEntity.AccountId, accountEntity.Username),
            };
        }

        private void ValidateRequest(CreateAccountRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new InvalidParamException("Username", request.Username);

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new InvalidParamException("Password", request.Password);

            if (accountRepository.UsernameIsTaken(request.Username))
                throw new BadRequestException("Este username está indisponível");
        }
    }
}
