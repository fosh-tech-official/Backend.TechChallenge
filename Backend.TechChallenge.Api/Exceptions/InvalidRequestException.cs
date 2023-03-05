using System;
using System.Net;

namespace Backend.TechChallenge.Api.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public int StatusCode => (int)HttpStatusCode.BadRequest;

        public InvalidRequestException(string message) : base(message)
        {
        }
    }
}
