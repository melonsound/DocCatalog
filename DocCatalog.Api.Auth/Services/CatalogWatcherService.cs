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
        private readonly ILogger _logger;
        private FileSystemWatcher _fileSystemWatcher;

        public CatalogWatcherService(ILogger<CatalogWatcherService> logger)
        {
            _logger = logger;
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
            if (e.Name.StartsWith("AM"))
            {
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                double len = new FileInfo(e.FullPath).Length;
                int order = 0;
                while (len >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    len = len / 1024;
                }

                string size = String.Format("{0:0.##} {1}", len, sizes[order]);
            }
            
        }
    }
}
