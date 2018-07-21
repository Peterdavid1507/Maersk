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
using System.Web.Configuration;

namespace Maersk.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sql = "INSERT INTO users (user_name, user_contact, user_email, user_password, role_id) VALUES (@name,@contact,@email,@password,@roleID);";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.Parameters.Add("@name", SqlDbType.VarChar);
            sqlcmd.Parameters["@name"].Value = CompanyName.Text;
            sqlcmd.Parameters.Add("@contact", SqlDbType.VarChar);
            sqlcmd.Parameters["@contact"].Value = Contact.Text;
            sqlcmd.Parameters.Add("@email", SqlDbType.VarChar);
            sqlcmd.Parameters["@email"].Value = Email.Text;
            sqlcmd.Parameters.Add("@password", SqlDbType.VarChar);
            sqlcmd.Parameters["@password"].Value = Password.Text;
            sqlcmd.Parameters.Add("@roleID", SqlDbType.VarChar);
            sqlcmd.Parameters["@roleID"].Value = "CUS";

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
                Response.Redirect("/Account/Register.aspx", false);
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
                Response.Redirect("/Account/Login.aspx", false);
            }
        }
    }
}