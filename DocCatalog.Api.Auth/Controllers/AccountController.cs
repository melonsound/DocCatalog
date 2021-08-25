using DocCatalog.Api.Auth.Models;
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
    public class AccountController : ControllerBase
    {
        private AccountContext _accountContext = null;

        public AccountController(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        [HttpGet]
        public ActionResult GetAccount([FromBody]Account account)
        {
            return Ok(_accountContext.Accounts.SingleOrDefault(x => x.Username == account.Username));
        }

        [HttpPost]
        public ActionResult PostAccount([FromBody]Account account)
        {
            _accountContext.Accounts.Add(account);
            return Ok();
        }
    }
}
