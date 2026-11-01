using InternalInmate.Domain.Entities;
using InternalInmate.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InternalInmate.Domain.Interfaces
{
    public class ProfileSearchCriteria
    {
        public string Keyword { get; set; }
        public int? CategoryId { get; set; }
        public ProfileStatus? Status { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
    }

    public interface IProfileRepository
    {
        Task<Profile> GetByIdAsync(int id, bool includeRelated = true, CancellationToken cancellationToken = default);
        Task<Profile> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Profile>> SearchAsync(ProfileSearchCriteria criteria, CancellationToken cancellationToken = default);
        Task AddAsync(Profile profile, CancellationToken cancellationToken = default);
        void Update(Profile profile);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}



