using InternalInmate.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace InternalInmate.Domain.Interfaces
{
    public interface IAuditLogRepository
    {
        Task AddAsync(AuditLog log, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}


