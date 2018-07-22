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
    public partial class EditPortShipping : System.Web.UI.Page
    {
        public object Departure { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Status.Items[0].Attributes.Add("disabled", "disabled");
            lblShippingID.Text = Request["id"];
            lblStatus.Text = Request["status"];

            if (Request["status"].Equals("Delivered") || Request["status"].Equals("Shipping"))
            {
                Response.Redirect("/ViewPortShipping", false);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            String sql = "UPDATE shipping SET shipping_status = @status WHERE shipping_id = @id";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.Parameters.Add("@status", SqlDbType.VarChar);
            sqlcmd.Parameters["@status"].Value = Status.SelectedValue.ToString();
            sqlcmd.Parameters.Add("@id", SqlDbType.Int);
            sqlcmd.Parameters["@id"].Value = int.Parse(Request["id"]);

            conn.Open();
            int success = sqlcmd.ExecuteNonQuery();
            conn.Close();

            //fail
            if (success == 0)
            {
                //Error message
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                {
                    String cstext = "alert('Something went wrong. Please contact Administrator for assistance');";
                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                }
            }
            //success
            else
            {
                //Successful message
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                {
                    String cstext = "alert('Successfully Updated.');";
                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                }
            }
            Response.Redirect("/ViewPortShipping.aspx?", false);
        }
    }
}