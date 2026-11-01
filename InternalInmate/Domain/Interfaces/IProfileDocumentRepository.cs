using InternalInmate.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InternalInmate.Domain.Interfaces
{
    public interface IProfileDocumentRepository
    {
        Task<IReadOnlyList<ProfileDocument>> GetByProfileIdAsync(int profileId, CancellationToken cancellationToken = default);
        Task<ProfileDocument> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(ProfileDocument document, CancellationToken cancellationToken = default);
        void Remove(ProfileDocument document);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}


