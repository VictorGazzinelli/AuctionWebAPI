using AuctionWebAPI.Boundaries.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionWebAPI.Models.Account
{
    public class LoginAccountOutput
    {
        public AccountDTO Account { get; set; }
        public string AuthToken { get; set; }
    }
}
