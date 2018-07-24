using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace Maersk
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var status = Session["status"];
            if (status.Equals("true"))
            {
                var role = Session["role"];
                // port staffs
                if (role.Equals("POR"))
                {
                    nb_viewportshipsDeparture.Visible = true;
                    nb_viewportshipsArrival.Visible = true;
                    nb_login.Visible = false;
                    nb_logout.Visible = true;
                    nb_register.Visible = false;
                }
                // administrator
                else if (role.Equals("ADM"))
                {
                    nb_addcontainer.Visible = true;
                    nb_viewcontainer.Visible = true;
                    nb_login.Visible = false;
                    nb_logout.Visible = true;
                    nb_register.Visible = false;
                }
                // customers / company
                else
                {
                    nb_addship.Visible = true;
                    nb_viewship.Visible = true;
                    nb_login.Visible = false;
                    nb_logout.Visible = true;
                    nb_register.Visible = false;
                }
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {

        }
        
        protected void logout_serverclick (object sender, EventArgs e)
        {
            
        }
    }

}