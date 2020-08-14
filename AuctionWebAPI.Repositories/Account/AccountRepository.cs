using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuctionWebAPI.Boundaries.Repositories;
using AccountEntity = AuctionWebAPI.Entities.Account.Account;

namespace AuctionWebAPI.Repositories.Game
{
    public class AccountRepository : IAccountRepository
    {
        static List<AccountEntity> list_Accounts_In_Memory = new List<AccountEntity>();

        public int Create(AccountEntity account)
        {
            int accountId = list_Accounts_In_Memory.Count() + 1;
            account.AccountId = accountId;
            list_Accounts_In_Memory.Add(account);

            return accountId;
        }

        public AccountEntity Retrieve(int accountId)
        {
            return list_Accounts_In_Memory.Where(account => account.AccountId == accountId)
                .FirstOrDefault();
        }

        public AccountEntity RetrieveByUsername(string username)
        {
            return list_Accounts_In_Memory.Where(account => String.Equals(account.Username, username))
                .FirstOrDefault();
        }

        public bool UsernameIsTaken(string username)
        {
            return list_Accounts_In_Memory.Any(account => account.Username.Equals(username));
        }

        public bool Delete(int accountId)
        {
            list_Accounts_In_Memory = list_Accounts_In_Memory.Where(account => account.AccountId != accountId).ToList();
            return !list_Accounts_In_Memory.Any(account => account.AccountId == accountId);
        }
    }
}
