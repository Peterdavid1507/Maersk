using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maersk.Admin
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Port.Items[0].Attributes.Add("disabled", "disabled");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sql = "INSERT INTO users (user_name, user_contact, user_email, user_password, role_id, port_id) VALUES (@name,@contact,@email,@password,@roleID,@portID);";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.Parameters.Add("@name", SqlDbType.VarChar);
            sqlcmd.Parameters["@name"].Value = Name.Text;
            sqlcmd.Parameters.Add("@contact", SqlDbType.VarChar);
            sqlcmd.Parameters["@contact"].Value = Contact.Text;
            sqlcmd.Parameters.Add("@email", SqlDbType.VarChar);
            sqlcmd.Parameters["@email"].Value = Email.Text;
            sqlcmd.Parameters.Add("@password", SqlDbType.VarChar);
            sqlcmd.Parameters["@password"].Value = "P@ssw0rd";
            sqlcmd.Parameters.Add("@roleID", SqlDbType.VarChar);
            sqlcmd.Parameters["@roleID"].Value = "POR";
            sqlcmd.Parameters.Add("@portID", SqlDbType.Int);
            sqlcmd.Parameters["@portID"].Value = Port.SelectedValue;


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
            Response.Redirect("/ViewUsers.aspx", false);
        }
    }
}