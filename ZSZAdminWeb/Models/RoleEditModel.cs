using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZSZAdminWeb.Models
{
    public class RoleEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int[] PermissionIds { get; set; }
    }
}