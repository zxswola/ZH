using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO;

namespace ZSZAdminWeb.Models
{
    public class AdminUserAddViewModel
    {
        public CityDTO[] Cities { get; set; }
        public RoleDTO[] Roles { get; set; }

    }
}