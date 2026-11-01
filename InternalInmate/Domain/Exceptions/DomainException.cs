using System;

namespace InternalInmate.Domain.Exceptions
{

    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}

