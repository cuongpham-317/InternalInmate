using System.Threading;
using System.Threading.Tasks;

namespace InternalInmate.Application.Interfaces
{
    public interface IBackupService
    {
        Task<string> BackupAsync(string destinationFolder, CancellationToken cancellationToken = default);
        Task RestoreAsync(string backupFolder, CancellationToken cancellationToken = default);
    }
}


