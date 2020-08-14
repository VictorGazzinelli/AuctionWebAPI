using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionWebAPI.Utils.Exceptions
{
    public class UnauthorizedException : HttpResponseException
    {
        public override string ExceptionName { get; set; } = "UnauthorizedException";
        public override int Status { get; set; } = StatusCodes.Status401Unauthorized;

        public UnauthorizedException() : base() { }

        public UnauthorizedException(string message) : base(message) { }
    }
}
