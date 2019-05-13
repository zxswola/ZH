﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO;

namespace ZSZAdminWeb.Models
{
    public class HouseAddViewModel
    {
        public RegionDTO[] Regions { get; set; }
        public IdNameDTO[] RoomTypes { get; set; }
        public IdNameDTO[] Statuses { get; set; }
        public IdNameDTO[] DecorateStatuses { get; set; }
        public IdNameDTO[] Types { get; set; }
        public AttachmentDTO[] Attachments { get; set; }
    }
}