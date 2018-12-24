using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO;

namespace ZSZAdminWeb.Models
{
    public class AdminUserEditViewModel
    {
        public AdminUserDTO User { get; set; }
        public CityDTO[] Cities { get; set; }
        public RoleDTO[] Roles { get; set; }
        public long[] RoleIds { get; set; }

    }
}