using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionWebAPI.Utils.Exceptions
{
    public class InvalidParamException : HttpResponseException
    {
        public const string msg = "Parametro inválido: {0} - {1}";

        public override string ExceptionName { get; set; } = "InvalidParamException";
        public override int Status { get; set; } = StatusCodes.Status400BadRequest;

        public InvalidParamException() : base() { }

        public InvalidParamException(string paramName, object paramValue) : base(string.Format(msg, paramName, paramValue?.ToString() ?? "null")) { }
    }
}
