using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO;

namespace ZSZFrontWeb.Models
{
    [Serializable]
    public class LinkModel
    {
        public LinkDTO[] Links { get; set; }
    }
}