using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maersk
{
    public partial class ViewPortShippingDeparture : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            welcome.Text = Session["name"].ToString();
        }

        private void changeSQL()
        {
            String sql = "SELECT * FROM Shipping_Details " +
                "WHERE(shipping_departure_port = (SELECT port_id FROM users WHERE user_id = '" + Session["id"].ToString() + "')) " +
                "AND shipping_id = '" + Search.Text + "' " +
                "ORDER BY shipping_id DESC; ";

            dsCheckShipping.SelectCommand = sql;
        }

        protected void Search_TextChanged(object sender, EventArgs e)
        {
            changeSQL();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ViewPortShippingDeparture", false);
        }
    }
}