﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities
{
    public class SettingEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
