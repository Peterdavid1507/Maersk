using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maersk
{
    public partial class ViewShipping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            welcome.Text = Session["name"].ToString();
            Session["setEditShipping"] = 0;
        }
    }
}