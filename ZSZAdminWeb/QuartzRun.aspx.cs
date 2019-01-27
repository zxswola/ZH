using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZSZAdminWeb
{
    public partial class QuartzRun : System.Web.UI.Page
    {
        public string timerStr = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            timerStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}