using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionWebAPI.Boundaries.Services.Account
{
    public class CreateAccountRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
