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
    public partial class EditShipping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Departure.Items[0].Attributes.Add("disabled", "disabled");
            Arrival.Items[0].Attributes.Add("disabled", "disabled");
            Container.Items[0].Attributes.Add("disabled", "disabled");

            int set = int.Parse(Session["setEditShipping"].ToString());

            if (Request["status"].Equals("Shipping") || Request["status"].Equals("Delivered"))
            {
                Response.Redirect("/ViewShipping", false);
            }
            else
            {
                if (set.Equals(0))
                {
                    FirstTimeLoading();
                }
            }
        }

        private void FirstTimeLoading ()
        {
            int id = int.Parse(Request["id"]);
            lblShippingID.Text = id.ToString();

            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();

                SqlCommand sql = new SqlCommand("SELECT * FROM shipping_details WHERE shipping_id = @id", conn);
                sql.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader dr = sql.ExecuteReader();

                if (dr.Read())
                {
                    var departurePort = dr["shipping_departure_port"].ToString();
                    var arrivalPort = dr["shipping_arrival_port"].ToString();
                    var weight = dr["shipping_weight"].ToString();
                    var remarks = dr["shipping_remarks"].ToString();
                    var container = dr["container_id"].ToString();
                    var cost = dr["shipping_cost"].ToString();
                    conn.Close();
                    dr.Close();

                    Departure.SelectedValue = departurePort.ToString();
                    Arrival.SelectedValue = arrivalPort.ToString();
                    Weight.Text = weight.ToString();
                    Remarks.Text = remarks.ToString();
                    Container.SelectedValue = container.ToString();
                    txtPrice.Text = cost.ToString();
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

            Session["setEditShipping"] = 1;
        }

        private void UpdatePrice()
        {
            double currentPrice = 0.00;



            if (!Container.SelectedIndex.Equals(0))
            {
                SqlConnection conn = new SqlConnection();
                try
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    conn.Open();

                    var idTS = Container.SelectedValue.ToString();
                    int id = int.Parse(idTS);

                    SqlCommand sql = new SqlCommand("SELECT * FROM container WHERE container_id = @id", conn);
                    sql.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader dr = sql.ExecuteReader();

                    if (dr.Read())
                    {
                        var priceTS = dr["container_price"].ToString();
                        double price = double.Parse(priceTS);
                        conn.Close();
                        dr.Close();

                        currentPrice = currentPrice + price;

                    }
                    else
                    {
                        //error message
                        Type cstype = this.GetType();
                        ClientScriptManager cs = Page.ClientScript;
                        if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                        {
                            String cstext = "alert('Something went wrong. Please contact adminstrator for assistance.');";
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

            if (!Weight.Text.Equals(""))
            {

                var weightTS = Weight.Text;
                double weight = double.Parse(weightTS);

                try
                {
                    var idTS = Container.SelectedValue.ToString();
                    int id = int.Parse(idTS);

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
                            var sizeTS = dr["container_size"].ToString();
                            double size = double.Parse(sizeTS);
                            conn.Close();
                            dr.Close();

                            if (size >= weight)
                            {
                                weight = weight * 0.5;
                                currentPrice = currentPrice + weight;
                            }
                            else
                            {
                                //error message
                                Type cstype = this.GetType();
                                ClientScriptManager cs = Page.ClientScript;
                                if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                                {
                                    String cstext = "alert('The selected container is not suitable for the shipping weight. Please select new container.');";
                                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                                }
                                Container.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            //error message
                            Type cstype = this.GetType();
                            ClientScriptManager cs = Page.ClientScript;
                            if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                            {
                                String cstext = "alert('Something went wrong. Please contact adminstrator for assistance.');";
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
                catch (Exception ex)
                {
                    weight = weight * 0.5;
                    currentPrice = currentPrice + weight;
                }


            }

            txtPrice.Text = currentPrice.ToString();
        }

        protected void Weight_TextChanged(object sender, EventArgs e)
        {
            UpdatePrice();
        }

        protected void Container_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePrice();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            String sql = "UPDATE shipping SET shipping_departure_port = @departure, shipping_arrival_port = @arrival, shipping_weight = @weight, shipping_cost = @cost, shipping_remarks = @remarks, container_id = @container WHERE shipping_id = @id";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.Parameters.Add("@departure", SqlDbType.Int);
            sqlcmd.Parameters["@departure"].Value = int.Parse(Departure.SelectedValue.ToString());
            sqlcmd.Parameters.Add("@arrival", SqlDbType.Int);
            sqlcmd.Parameters["@arrival"].Value = int.Parse(Arrival.SelectedValue.ToString());
            sqlcmd.Parameters.Add("@weight", SqlDbType.Float);
            sqlcmd.Parameters["@weight"].Value = double.Parse(Weight.Text);
            sqlcmd.Parameters.Add("@cost", SqlDbType.Float);
            sqlcmd.Parameters["@cost"].Value = double.Parse(txtPrice.Text);
            sqlcmd.Parameters.Add("@remarks", SqlDbType.VarChar);
            sqlcmd.Parameters["@remarks"].Value = Remarks.Text;
            sqlcmd.Parameters.Add("@container", SqlDbType.Int);
            sqlcmd.Parameters["@container"].Value = int.Parse(Container.SelectedValue.ToString());
            sqlcmd.Parameters.Add("@id", SqlDbType.Int);
            sqlcmd.Parameters["@id"].Value = int.Parse(Request["id"]);

            conn.Open();
            int success = sqlcmd.ExecuteNonQuery();
            conn.Close();

            Session["setEditShipping"] = 0;

            //fail
            if (success == 0)
            {
                //Error message
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                {
                    String cstext = "alert('Something went wrong. Please contact Administrator for assistance');window.open('/ViewShipping.aspx','_self');";
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
                    String cstext = "alert('Successfully Updated.');window.open('/ViewShipping.aspx','_self');";
                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                }
            }
            Response.Redirect("/ViewShipping.aspx?", false);
        }

        protected void lbtnHere_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request["id"]);
            Session["setEditShipping"] = 0;
            Response.Redirect("/EditShipping.aspx?id=" + id, false);
        }
    }
}