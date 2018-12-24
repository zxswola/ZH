using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZSZAdminWeb.Models
{
    public class AdminUserEditModel
    {
        public int Id { get; set; }

        [Required]
        [Phone]
        public string PhoneNum { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string Password2 { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public int[] RoleIds { get; set; }
    }
}