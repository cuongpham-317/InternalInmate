using InternalInmate.Domain.Interfaces;
using System;

namespace InternalInmate.Infrastructure
{
    public sealed class SystemClock : IClock
    { 
        public DateTime UtcNow => DateTime.UtcNow;
    }
}


