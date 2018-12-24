using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO;

namespace ZSZAdminWeb.Models
{
    public class RoleEditGetModel
    {
        public RoleDTO Role { get; set; }
        public PermissionDTO[] RolePers{ get; set; }
        public PermissionDTO[] AllPers { get; set; }
    }
}