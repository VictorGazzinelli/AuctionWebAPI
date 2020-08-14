using AuctionWebAPI.Boundaries.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionWebAPI.Boundaries.Services.Account
{
    public class CreateAccountResponse
    {
        public AccountDTO Account { get; set; }
        public string AuthToken { get; set; }
    }
}
