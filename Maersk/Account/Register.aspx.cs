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
            SqlCommand cmdCreateCustomerUser = new SqlCommand(createCustomerUser, conn);
            cmdCreateCustomerUser.Parameters.Add("@name", SqlDbType.VarChar);
            cmdCreateCustomerUser.Parameters["@name"].Value = CompanyName.Text;
            cmdCreateCustomerUser.Parameters.Add("@contact", SqlDbType.VarChar);
            cmdCreateCustomerUser.Parameters["@contact"].Value = Contact.Text;
            cmdCreateCustomerUser.Parameters.Add("@email", SqlDbType.VarChar);
            cmdCreateCustomerUser.Parameters["@email"].Value = Email.Text;
            cmdCreateCustomerUser.Parameters.Add("@password", SqlDbType.VarChar);
            cmdCreateCustomerUser.Parameters["@password"].Value = Password.Text;
            cmdCreateCustomerUser.Parameters.Add("@roleID", SqlDbType.VarChar);
            cmdCreateCustomerUser.Parameters["@roleID"].Value = "CUS";

            conn.Open();
            int success = cmdCreateCustomerUser.ExecuteNonQuery();
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