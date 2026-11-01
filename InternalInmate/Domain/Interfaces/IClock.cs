using System;

namespace InternalInmate.Domain.Interfaces
{
    public interface IClock
    {
        DateTime UtcNow { get; }
    }
}


