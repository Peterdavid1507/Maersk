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

            string createCustomerUser = "INSERT INTO users (user_name, user_contact, user_email, user_password, role_id) VALUES (@name,@contact,@email,@password,@roleID);";
            SqlCommand sql = new SqlCommand(createCustomerUser, conn);
            sql.Parameters.Add("@name", SqlDbType.VarChar);
            sql.Parameters["@name"].Value = CompanyName.Text;
            sql.Parameters.Add("@contact", SqlDbType.VarChar);
            sql.Parameters["@contact"].Value = Contact.Text;
            sql.Parameters.Add("@email", SqlDbType.VarChar);
            sql.Parameters["@email"].Value = Email.Text;
            sql.Parameters.Add("@password", SqlDbType.VarChar);
            sql.Parameters["@password"].Value = Password.Text;
            sql.Parameters.Add("@roleID", SqlDbType.VarChar);
            sql.Parameters["@roleID"].Value = "CUS";

            conn.Open();
            int success = sql.ExecuteNonQuery();
            conn.Close();

            //not success
            if (success == 0)
            {
            }
            //success
            else
            {
                Response.Redirect("/Account/Login", false);
            }
        }
    }
}