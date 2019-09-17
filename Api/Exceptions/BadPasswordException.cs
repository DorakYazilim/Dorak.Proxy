using System;

namespace Api.Exceptions
{
    public class BadPasswordException
        : Exception
    {
        public BadPasswordException()
            : base("Bad password")
        {
        }
    }
}