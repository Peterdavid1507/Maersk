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
    public partial class EditContainer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = int.Parse(Request["id"]);
            lblContainerID.Text = id.ToString();

            int set = int.Parse(Session["setEditContainer"].ToString());

            if (set.Equals(0))
            {
                FirstTimeLoading();
            }

        }

        private void FirstTimeLoading()
        {
            int id = int.Parse(Request["id"]);

            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();

                SqlCommand sql = new SqlCommand("SELECT * FROM container WHERE container_id = @id", conn);
                sql.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader dr = sql.ExecuteReader();

                if (dr.Read())
                {
                    var name = dr["container_name"].ToString();
                    var size = dr["container_size"].ToString();
                    var price = dr["container_price"].ToString();
                    var isStopped = dr["container_isStopped"].ToString();
                    conn.Close();
                    dr.Close();

                    Name.Text = name.ToString();
                    Size.Text = size.ToString();
                    Price.Text = price.ToString();

                    if (isStopped.Equals("YES"))
                    {
                        rbYes.Checked = true;
                        rbNo.Checked = false;
                    }
                    else
                    {
                        rbYes.Checked = false;
                        rbNo.Checked = true;
                    }
                }
                else
                {
                    //error message
                    Type cstype = this.GetType();
                    ClientScriptManager cs = Page.ClientScript;
                    if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                    {
                        String cstext = "alert('Something went wrong. Please contact Adminstrator for assistance.');";
                        cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                    }

                }
                if (!dr.IsClosed)
                {
                    dr.Close();
                }
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

            Session["setEditContainer"] = 1;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            String sql = "";
            if (rbYes.Checked == true && rbNo.Checked == false)
            {
                sql = "UPDATE container SET container_name = @name, container_size = @size, container_price = @price, container_isStopped = 'YES' WHERE container_id = @id";
                SqlCommand sqlcmd = new SqlCommand(sql, conn);
                sqlcmd.Parameters.Add("@name", SqlDbType.VarChar);
                sqlcmd.Parameters["@name"].Value = Name.Text;
                sqlcmd.Parameters.Add("@size", SqlDbType.Float);
                sqlcmd.Parameters["@size"].Value = Size.Text;
                sqlcmd.Parameters.Add("@price", SqlDbType.Float);
                sqlcmd.Parameters["@price"].Value = Price.Text;
                sqlcmd.Parameters.Add("@id", SqlDbType.Int);
                sqlcmd.Parameters["@id"].Value = int.Parse(Request["id"]);

                conn.Open();
                int success = sqlcmd.ExecuteNonQuery();
                conn.Close();

                Session["setEditContainer"] = 0;

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
                Response.Redirect("/ViewContainers.aspx?", false);
            }
            else if (rbYes.Checked == false && rbNo.Checked == true)
            {
                sql = "UPDATE container SET container_name = @name, container_size = @size, container_price = @price, container_isStopped = null WHERE container_id = @id";
                SqlCommand sqlcmd = new SqlCommand(sql, conn);
                sqlcmd.Parameters.Add("@name", SqlDbType.VarChar);
                sqlcmd.Parameters["@name"].Value = Name.Text;
                sqlcmd.Parameters.Add("@size", SqlDbType.Float);
                sqlcmd.Parameters["@size"].Value = Size.Text;
                sqlcmd.Parameters.Add("@price", SqlDbType.Float);
                sqlcmd.Parameters["@price"].Value = Price.Text;
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
                Response.Redirect("/ViewContainers.aspx?", false);
            }

        }
    }
}