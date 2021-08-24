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

        [Column(TypeName = "nvarchar(24)")]
        public Role[] Roles { get; set; }
    }

    public enum Role
    {
        User,
        Admin
    }
}
