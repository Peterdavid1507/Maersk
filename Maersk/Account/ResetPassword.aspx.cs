using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using Maersk.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Maersk.Account
{
    public partial class ResetPassword : Page
    {
        protected string StatusMessage
        {
            get;
            private set;
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sql = "UPDATE users SET user_password = @password WHERE user_id = @id";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.Parameters.Add("@password", SqlDbType.VarChar);
            sqlcmd.Parameters["@password"].Value = Password.Text;
            sqlcmd.Parameters.Add("@id", SqlDbType.VarChar);
            sqlcmd.Parameters["@id"].Value = int.Parse(Session["id"].ToString());

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
            Response.Redirect("~/Account/ResetPasswordConfirmation");
        }
    }
}