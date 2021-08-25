using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocCatalog.Api.Auth.Models
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; } //BCrypt Hash

        public string Role { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}
