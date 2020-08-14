using System;
using System.Collections.Generic;
using System.Text;
using AccountEntity = AuctionWebAPI.Entities.Account.Account;

namespace AuctionWebAPI.Boundaries.Dtos
{
    public class AccountDTO
    {
        public string Username { get; set; }
        public int AccountId { get; set; }

        public AccountDTO(AccountEntity accountEntity)
        {
            if (accountEntity == null)
                return;

            this.AccountId = accountEntity.AccountId;
            this.Username = accountEntity.Username;
        }
    }
}
