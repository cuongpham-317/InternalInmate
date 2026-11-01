using System.Collections.Generic;

namespace InternalInmate.Application.Interfaces
{
    public class StoredFileInfo
    {
        public string StoredFileName { get; set; } = string.Empty;
        public string RelativePath { get; set; } = string.Empty;
        public string ContentType { get; set; }
        public long SizeBytes { get; set; }
    }

    public interface IFileStorageService
    {
        IReadOnlyCollection<string> AllowedExtensions { get; }
        long MaxFileSizeBytes { get; }
        string DocumentsRoot { get; }
        StoredFileInfo Save(string sourceFilePath, int profileId);
        string GetAbsolutePath(string relativePath);
        bool Exists(string relativePath);
        void Delete(string relativePath);
        void CopyTo(string relativePath, string destinationFilePath);
        void CopyAllTo(string destinationDirectory);
        void ReplaceAllFrom(string sourceDirectory);
        string SaveAvatar(string sourceFilePath);
    }
}

