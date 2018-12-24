using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZSZAdminWeb.Models
{
    public class RoleAddModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int[] PermissionIds { get; set; }
    }
}