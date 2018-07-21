using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using Maersk.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

namespace Maersk.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["id"] = "";
            Session["name"] = "";
            Session["email"] = "";
            Session["role"] = "";
            Session["status"] = "";

            RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                SqlConnection conn = new SqlConnection();
                try
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    conn.Open();

                    SqlCommand sql = new SqlCommand("SELECT * FROM users WHERE user_email = @email AND user_password = @password", conn);
                    sql.Parameters.Add(new SqlParameter("@email", Email.Text));
                    sql.Parameters.Add(new SqlParameter("@password", Password.Text));
                    SqlDataReader dr = sql.ExecuteReader();

                    if (dr.Read())
                    {
                        var idTS = dr["user_id"].ToString();
                        int id = int.Parse(idTS);
                        var role = dr["role_id"].ToString();
                        var name = dr["user_name"].ToString();
                        conn.Close();
                        dr.Close();

                        Session["id"] = id;
                        Session["name"] = name;
                        Session["email"] = Email.Text;
                        Session["role"] = role;
                        Session["status"] = "true";


                        // Remember Me with encryption
                        if (this.RememberMe != null && this.RememberMe.Checked == true)
                        {
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                            Email.Text,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(120),
                            false,
                            Email.Text,
                            FormsAuthentication.FormsCookiePath);

                            // Encrypt the ticket.
                            string encTicket = FormsAuthentication.Encrypt(ticket);

                            // Create the cookie.
                            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                        }

                        if (role.Equals("ADM"))
                        {
                            Response.Redirect("/Admin/ViewUsers");
                        }
                        else
                        {
                            Response.Redirect("~/ViewShipping");
                        }
                    }
                    else
                    {
                        //error message
                        Type cstype = this.GetType();
                        ClientScriptManager cs = Page.ClientScript;
                        if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                        {
                            String cstext = "alert('Invalid User! Try again with VALID username and password');";
                            cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                        }
                    }
                    if (!dr.IsClosed)
                        dr.Close();

                }
                catch (Exception ex) { }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }
        }
    }
}