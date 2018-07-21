using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maersk
{
    public partial class ViewContainers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["setEditContainer"] = 0;
        }
    }
}