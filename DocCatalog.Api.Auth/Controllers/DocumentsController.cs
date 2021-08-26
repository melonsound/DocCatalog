using DocCatalog.Api.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocCatalog.Api.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private ArchiveDocumentContext _archiveDocumentContext;

        public DocumentsController(ArchiveDocumentContext archiveDocumentContext)
        {
            _archiveDocumentContext = archiveDocumentContext;
        }

        [HttpGet("all")]
        [Authorize]
        public IActionResult GetAllDocuments()
        {
            if (User.IsInRole(Role.User))
            {
                return Ok(_archiveDocumentContext.ArchiveDocuments.Where(x => x.FileType == "P"));
            }
            if (User.IsInRole(Role.Admin))
            {
                return Ok(_archiveDocumentContext.ArchiveDocuments.ToList());
            }
            return Forbid();
        }
    }
}
