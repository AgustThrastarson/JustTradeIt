using System;

namespace JustTradeIt.Software.API.Models.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("UnAuthorized"){}
        public UnauthorizedException(string message) : base(message) {}
        public UnauthorizedException(string message, Exception inner) : base(message,inner) {}
    }
}