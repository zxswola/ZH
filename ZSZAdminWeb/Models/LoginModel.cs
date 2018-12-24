using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZSZAdminWeb.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(16,MinimumLength = 6)]
        public string UserName { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 6)]
        public string Password { get; set; }
        //[Required]
        //public string VerifyCode { get; set; }
    }
}