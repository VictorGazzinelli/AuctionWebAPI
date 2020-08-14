using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionWebAPI.Models.Account
{
    public class LoginAccountInput
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
