using InternalInmate.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InternalInmate.Domain.Interfaces
{
    public interface IProfileCategoryRepository
    {
        Task<IReadOnlyList<ProfileCategory>> GetActiveAsync(CancellationToken cancellationToken = default);
        Task<ProfileCategory> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
