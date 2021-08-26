using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocCatalog.Api.Auth.Models
{
    public class ArchiveDocument
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string SenderCode { get; set; }
        public string RecipientCode { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Version { get; set; }
        public string FileType { get; set; }
        public string FileSize { get; set; }
        public DateTime FileCreationDate { get; set; }
    }
}
