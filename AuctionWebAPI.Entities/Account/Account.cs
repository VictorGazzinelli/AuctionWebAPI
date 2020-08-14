using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionWebAPI.Entities.Account
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
