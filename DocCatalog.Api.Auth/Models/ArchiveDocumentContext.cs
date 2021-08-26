using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocCatalog.Api.Auth.Models
{
    public class ArchiveDocumentContext : DbContext
    {
        public ArchiveDocumentContext(DbContextOptions<ArchiveDocumentContext> options) : base(options)
        {

        }

        public DbSet<ArchiveDocument> ArchiveDocuments { get; set; }
    }
}
