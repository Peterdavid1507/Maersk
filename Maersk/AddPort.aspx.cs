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
    public partial class AddPort : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["setEditContainer"] = 0;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sql = "INSERT INTO port (port_name, port_country) VALUES (@name,@country);";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.Parameters.Add("@name", SqlDbType.VarChar);
            sqlcmd.Parameters["@name"].Value = Name.Text;
            sqlcmd.Parameters.Add("@country", SqlDbType.VarChar);
            sqlcmd.Parameters["@country"].Value = Country.Text;


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
                    String cstext = "alert('Successfully Registered.');";
                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                }

            }
            Response.Redirect("/ViewPorts.aspx", false);
        }
    }
}