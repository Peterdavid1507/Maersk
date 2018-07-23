using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        private void changeSQL()
        {
            String sql = "SELECT shipping_id, departure, arrival, shipping_date, shipping_status, shipping_weight, " +
                "shipping_cost, shipping_remarks, container_name AS ContainerName, container_size AS ContainerSize " +
                "FROM Shipping_Details " +
                "WHERE (shipping_customer = '" + Session["id"].ToString() + "' AND shipping_id = '" + Search.Text + "') " +
                "ORDER BY shipping_id DESC";

            dsCheckShipping.SelectCommand = sql;
        }

        protected void Search_TextChanged(object sender, EventArgs e)
        {
            changeSQL();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ViewShipping",false);
        }
    }
}