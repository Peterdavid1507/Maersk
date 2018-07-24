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
    public partial class AddContainer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sql = "INSERT INTO container (container_name, container_size, container_price) VALUES (@name, @size, @price);";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.Parameters.Add("@name", SqlDbType.VarChar);
            sqlcmd.Parameters["@name"].Value = Name.Text;
            sqlcmd.Parameters.Add("@size", SqlDbType.VarChar);
            sqlcmd.Parameters["@size"].Value = Size.Text;
            sqlcmd.Parameters.Add("@price", SqlDbType.VarChar);
            sqlcmd.Parameters["@price"].Value = Price.Text;


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
                    String cstext = "alert('Something went wrong. Please contact Administrator for assistance');window.open('/ViewContainers.aspx','_self');";
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
                    String cstext = "alert('Successfully Registered.');window.open('/ViewContainers.aspx','_self');";
                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                }

            }
        }
    }
}