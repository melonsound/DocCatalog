using DocCatalog.Api.Auth.Models;
using DocCatalog.Api.Auth.Services;
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
    public class AccountController : ControllerBase
    {
        private AccountContext _accountContext = null;
        private IAccountService _acccountService = null;

        public AccountController(AccountContext accountContext, IAccountService accountService)
        {
            _accountContext = accountContext;
            _acccountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody]Account account)
        {
            var accountResult = _acccountService.Authenticate(account.Username, account.Password);

            if (accountResult == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(accountResult);
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
