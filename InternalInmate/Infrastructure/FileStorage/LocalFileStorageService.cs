using InternalInmate.Application.Interfaces;
using InternalInmate.Domain.Exceptions;
using InternalInmate.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InternalInmate.Infrastructure.FileStorage
{
    public class LocalFileStorageService : IFileStorageService
    {
        private static readonly HashSet<string> Extensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".pdf",
            ".doc",
            ".docx",
            ".xls",
            ".xlsx",
            ".jpg",
            ".jpeg",
            ".png",
            ".gif",
            ".txt",
            ".tif",
            ".tiff"
        };

        private readonly AppSettings _settings;

        public LocalFileStorageService(AppSettings settings)
        {
            _settings = settings;
            Directory.CreateDirectory(DocumentsRoot);
        }

        public IReadOnlyCollection<string> AllowedExtensions => Extensions.ToList();
        public long MaxFileSizeBytes => 20 * 1024 * 1024; // 20 MB
        public string DocumentsRoot => _settings.DocumentsDirectory;

        public StoredFileInfo Save(string sourceFilePath, int profileId)
        {
            if (!File.Exists(sourceFilePath))
            {
                throw new DomainException("Không tìm thấy tệp nguồn.");
            }

            var extension = Path.GetExtension(sourceFilePath);
            if (!Extensions.Contains(extension))
            {
                throw new DomainException($"Định dạng '{extension}' không được phép. Cho phép: {string.Join(", ", Extensions)}");
            }

            var fileInfo = new FileInfo(sourceFilePath);
            if (fileInfo.Length <= 0)
            {
                throw new DomainException("Tệp rỗng không được đính kèm.");
            }

            if (fileInfo.Length > MaxFileSizeBytes)
            {
                throw new DomainException("Dung lượng tệp vượt quá 20 MB.");
            }

            var storedName = $"{Guid.NewGuid():N}{extension.ToLowerInvariant()}";
            var relativePath = Path.Combine(profileId.ToString(), storedName);
            var destination = GetAbsolutePath(relativePath);
            var dir = Path.GetDirectoryName(destination);
            if (!string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.Copy(sourceFilePath, destination, false);

            return new StoredFileInfo
            {
                StoredFileName = storedName,
                RelativePath = relativePath,
                ContentType = GuessContentType(extension),
                SizeBytes = fileInfo.Length
            };
        }

        public string SaveAvatar(string sourceFilePath)
        {
            if (!File.Exists(sourceFilePath))
            {
                throw new DomainException("Không tìm thấy tệp nguồn.");
            }

            var extension = Path.GetExtension(sourceFilePath);
            var imageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".tiff", ".webp"
            };

            if (!imageExtensions.Contains(extension))
            {
                throw new DomainException($"Định dạng ảnh '{extension}' không hợp lệ.");
            }

            var fileInfo = new FileInfo(sourceFilePath);
            if (fileInfo.Length <= 0)
            {
                throw new DomainException("Tệp ảnh rỗng không hợp lệ.");
            }

            if (fileInfo.Length > MaxFileSizeBytes)
            {
                throw new DomainException("Dung lượng ảnh vượt quá giới hạn.");
            }

            var storedName = $"{Guid.NewGuid():N}{extension.ToLowerInvariant()}";
            var relativePath = Path.Combine("avatars", storedName);
            var destination = GetAbsolutePath(relativePath);
            var dir = Path.GetDirectoryName(destination);
            if (!string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.Copy(sourceFilePath, destination, true);

            return relativePath;
        }

        public string GetAbsolutePath(string relativePath)
            => Path.GetFullPath(Path.Combine(DocumentsRoot, relativePath));

        public bool Exists(string relativePath)
            => File.Exists(GetAbsolutePath(relativePath));

        public void Delete(string relativePath)
        {
            var path = GetAbsolutePath(relativePath);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void CopyTo(string relativePath, string destinationFilePath)
        {
            var source = GetAbsolutePath(relativePath);
            var dir = Path.GetDirectoryName(destinationFilePath);
            if (!string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.Copy(source, destinationFilePath, true);
        }

        public void CopyAllTo(string destinationDirectory)
        {
            Directory.CreateDirectory(destinationDirectory);
            if (!Directory.Exists(DocumentsRoot))
            {
                return;
            }

            foreach (var file in Directory.GetFiles(DocumentsRoot, "*", SearchOption.AllDirectories))
            {
                var relative = GetRelativePath(DocumentsRoot, file);
                var dest = Path.Combine(destinationDirectory, relative);
                var dir = Path.GetDirectoryName(dest);
                if (!string.IsNullOrEmpty(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                File.Copy(file, dest, true);
            }
        }

        public void ReplaceAllFrom(string sourceDirectory)
        {
            if (Directory.Exists(DocumentsRoot))
            {
                Directory.Delete(DocumentsRoot, true);
            }

            Directory.CreateDirectory(DocumentsRoot);
            if (!Directory.Exists(sourceDirectory))
            {
                return;
            }

            foreach (var file in Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories))
            {
                var relative = GetRelativePath(sourceDirectory, file);
                var dest = Path.Combine(DocumentsRoot, relative);
                var dir = Path.GetDirectoryName(dest);
                if (!string.IsNullOrEmpty(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                File.Copy(file, dest, true);
            }
        }

        public static string GetRelativePath(string fromPath, string toPath)
        {
            if (string.IsNullOrEmpty(fromPath)) return toPath;
            if (!fromPath.EndsWith("\\") && !fromPath.EndsWith("/"))
            {
                fromPath += "\\";
            }
            var fromUri = new Uri(fromPath);
            var toUri = new Uri(toPath);
            var relativeUri = fromUri.MakeRelativeUri(toUri);
            return Uri.UnescapeDataString(relativeUri.ToString()).Replace('/', '\\');
        }

        private static string GuessContentType(string extension)
        {
            if (string.IsNullOrEmpty(extension)) return "application/octet-stream";
            switch (extension.ToLowerInvariant())
            {
                case ".pdf": return "application/pdf";
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                case ".png": return "image/png";
                case ".gif": return "image/gif";
                case ".txt": return "text/plain";
                case ".doc": return "application/msword";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xls": return "application/vnd.ms-excel";
                case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                default: return "application/octet-stream";
            }
        }
    }
}
