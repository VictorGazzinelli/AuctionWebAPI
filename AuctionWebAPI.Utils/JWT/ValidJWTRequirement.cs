using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionWebAPI.Utils
{
    public class ValidJWTRequirement : IAuthorizationRequirement
    {
        public ValidJWTRequirement()
        {
        }
    }
}
