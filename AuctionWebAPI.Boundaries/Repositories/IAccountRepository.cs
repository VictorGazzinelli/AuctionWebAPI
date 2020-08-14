using System;
using System.Collections.Generic;
using System.Text;
using AccountEntity = AuctionWebAPI.Entities.Account.Account;

namespace AuctionWebAPI.Boundaries.Repositories
{
    public interface IAccountRepository
    {
        int Create(AccountEntity account);
        bool UsernameIsTaken(string username);
        AccountEntity Retrieve(int accountId);
        AccountEntity RetrieveByUsername(string username);
        bool Delete(int accountId);
    }
}
