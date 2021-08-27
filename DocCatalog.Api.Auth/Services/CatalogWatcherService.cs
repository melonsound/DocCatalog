using DocCatalog.Api.Auth.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocCatalog.Api.Auth.Services
{
    public interface ICatalogWatcherService
    {
        //TODO: Интерфейс каталога не забыть описать
    }

    public class CatalogWatcherService : BackgroundService, ICatalogWatcherService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger _logger;
        private FileSystemWatcher _fileSystemWatcher;
        private ArchiveDocument _archiveDocument = null;

        public CatalogWatcherService(ILogger<CatalogWatcherService> logger, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            //_archiveDocumentContext = archiveDocumentContext;
        }

        private void ParseFileName()
        {
            throw new NotImplementedException();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            _fileSystemWatcher = new FileSystemWatcher(@"D:\Archive"); //TODO: Сделать путь через appsettigs.json
            _fileSystemWatcher.Created += Created;
            _fileSystemWatcher.Filter = "*.zip";
            _fileSystemWatcher.EnableRaisingEvents = true;
            return Task.CompletedTask;

        }

        private void Created(object sender, FileSystemEventArgs e)
        {
            if (e.Name.StartsWith("AM") && e.Name.Length == 21)
            {
                _archiveDocument = new ArchiveDocument();

                string senderCode = e.Name.Substring(2, 6);
                string recipientCode = e.Name.Substring(8, 3);
                string year = e.Name.Substring(11, 2);
                string month = e.Name.Substring(13, 2);
                string ver = e.Name.Substring(15, 1);
                string type = e.Name.Substring(16, 1);

                #region Size Calculate
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                double len = new FileInfo(e.FullPath).Length;
                int order = 0;
                while (len >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    len = len / 1024;
                }

                string size = String.Format("{0:0.##} {1}", len, sizes[order]);
                #endregion

                //время создания файла
                DateTime FileCreationTime = File.GetCreationTime(e.FullPath);

                

                _archiveDocument.FileName = e.Name;
                _archiveDocument.FileSize = size;
                _archiveDocument.FileCreationDate = FileCreationTime;
                _archiveDocument.SenderCode = senderCode;
                _archiveDocument.RecipientCode = recipientCode;
                _archiveDocument.Year = int.Parse(year);
                _archiveDocument.Month = int.Parse(month);
                _archiveDocument.Version = int.Parse(ver);
                _archiveDocument.FileType = type;

                using (var scope = _scopeFactory.CreateScope())
                {
                    var _archiveDocumentContext = scope.ServiceProvider.GetRequiredService<ArchiveDocumentContext>();
                    try
                    {
                        _archiveDocumentContext.Add(_archiveDocument);
                        _archiveDocumentContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation(ex.Message);
                    }
                }
            }
        }
    }
}
