﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class TokenResponse
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string scope { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}
