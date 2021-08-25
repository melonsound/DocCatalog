using DocCatalog.Api.Auth.Helpers;
using DocCatalog.Api.Auth.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DocCatalog.Api.Auth.Services
{
    public interface IAccountService
    {
        Account Authenticate(string username, string password);
        IEnumerable<Account> GetAll();
        Account GetById(int id);
    }

    public class AccountService : IAccountService
    {
        private AccountContext _accountContext = null;
        private readonly IOptions<AuthOptions> _authOptions;

        public AccountService(AccountContext accountContext, IOptions<AuthOptions> authOptions)
        {
            _accountContext = accountContext;
            _authOptions = authOptions;
        }


        public Account Authenticate(string username, string password)
        {
            var account = _accountContext.Accounts.SingleOrDefault(x => x.Username == username);

            if (!account.Password.Equals(password))
                return null;

            // return null if user not found
            if (account == null)
                return null;

            var authParams = _authOptions.Value;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = authParams.GetSymmetricSecurityKey();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.Id.ToString()),
                    new Claim(ClaimTypes.Role, account.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            account.Token = tokenHandler.WriteToken(token);

            return account;
        }

        public IEnumerable<Account> GetAll()
        {
            throw new NotImplementedException();
        }

        public Account GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
