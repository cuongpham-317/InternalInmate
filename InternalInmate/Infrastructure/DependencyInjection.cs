using InternalInmate.Application.Interfaces;
using InternalInmate.Application.Services;
using InternalInmate.Domain.Interfaces;
using InternalInmate.Infrastructure.Configurations;
using InternalInmate.Infrastructure.Data;
using InternalInmate.Infrastructure.FileStorage;
using InternalInmate.Infrastructure.Maps;
using InternalInmate.Infrastructure.Repositories;
using System.IO;

namespace InternalInmate.Infrastructure
{
    public class AppContainer
    {
        private static AppContainer _instance;
        public static AppContainer Current => _instance ?? (_instance = new AppContainer());

        public AppSettings Settings { get; private set; }
        public IClock Clock { get; private set; }
        public IProfileRepository ProfileRepository { get; private set; }
        public IProfileCategoryRepository ProfileCategoryRepository { get; private set; }
        public IProfileDocumentRepository ProfileDocumentRepository { get; private set; }
        public IAuditLogRepository AuditLogRepository { get; private set; }
        public IFileStorageService FileStorageService { get; private set; }
        public IMapService MapService { get; private set; }
        public IBackupService BackupService { get; private set; }
        public ProfileService ProfileService { get; private set; }
        public DocumentService DocumentService { get; private set; }

        public void Initialize(AppSettings settings = null)
        {
            Settings = settings ?? new AppSettings();
            Settings.EnsureDirectories();

            Clock = new SystemClock();
            FileStorageService = new LocalFileStorageService(Settings);
            MapService = new NominatimMapService();

            ProfileRepository = new ProfileRepository(Settings);
            ProfileCategoryRepository = new ProfileCategoryRepository(Settings);
            ProfileDocumentRepository = new ProfileDocumentRepository(Settings);
            AuditLogRepository = new AuditLogRepository(Settings);

            BackupService = new SqlBackupService(Settings, FileStorageService, AuditLogRepository, Clock);

            ProfileService = new ProfileService(ProfileRepository, ProfileCategoryRepository, AuditLogRepository, Clock);
            DocumentService = new DocumentService(ProfileRepository, ProfileDocumentRepository, FileStorageService, AuditLogRepository, Clock);
        }
    }
}
