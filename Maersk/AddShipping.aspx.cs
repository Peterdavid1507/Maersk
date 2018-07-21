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
    public partial class AddShipping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Departure.Items[0].Attributes.Add("disabled", "disabled");
            Arrival.Items[0].Attributes.Add("disabled", "disabled");
            Container.Items[0].Attributes.Add("disabled", "disabled");
            Session["setEditShipping"] = 0;
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
                            String cstext = "alert('Something went wrong. Please contact Administrator for assistance');";
                            cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                        }
                    }
                    if (!dr.IsClosed)
                        dr.Close();

                }
                catch (Exception ex) {}
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
                                String cstext = "alert('Something went wrong. Please contact Administrator for assistance');";
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
                catch(Exception ex)
                {
                    weight = weight * 0.5;
                    currentPrice = currentPrice + weight;
                }

                
            }

            txtPrice.Text = currentPrice.ToString();
        }

        protected void Container_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePrice();
        }

        protected void Weight_TextChanged(object sender, EventArgs e)
        {
            UpdatePrice();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DateTime currentDateTime = DateTime.Now;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            String sql = "INSERT INTO shipping (shipping_departure_port, shipping_arrival_port, shipping_date, shipping_status, shipping_weight, shipping_cost, shipping_remarks, shipping_customer, container_id) VALUES(@departure,@arrival,@date,@status,@weight,@cost,@remarks,@cust,@container); ";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.Parameters.Add("@departure", SqlDbType.Int);
            sqlcmd.Parameters["@departure"].Value = int.Parse(Departure.SelectedValue.ToString());
            sqlcmd.Parameters.Add("@arrival", SqlDbType.Int);
            sqlcmd.Parameters["@arrival"].Value = int.Parse(Arrival.SelectedValue.ToString());
            sqlcmd.Parameters.Add("@date", SqlDbType.DateTime);
            sqlcmd.Parameters["@date"].Value = currentDateTime;
            sqlcmd.Parameters.Add("@status", SqlDbType.VarChar);
            sqlcmd.Parameters["@status"].Value = "Pending Approval";
            sqlcmd.Parameters.Add("@weight", SqlDbType.Float);
            sqlcmd.Parameters["@weight"].Value = double.Parse(Weight.Text);
            sqlcmd.Parameters.Add("@cost", SqlDbType.Float);
            sqlcmd.Parameters["@cost"].Value = double.Parse(txtPrice.Text);
            sqlcmd.Parameters.Add("@remarks", SqlDbType.VarChar);
            sqlcmd.Parameters["@remarks"].Value = Remarks.Text;
            sqlcmd.Parameters.Add("@cust", SqlDbType.Int);
            sqlcmd.Parameters["@cust"].Value = int.Parse(Session["id"].ToString());
            sqlcmd.Parameters.Add("@container", SqlDbType.Int);
            sqlcmd.Parameters["@container"].Value = int.Parse(Container.SelectedValue.ToString());

            conn.Open();
            int success = sqlcmd.ExecuteNonQuery();
            conn.Close();

            //fail
            if (success == 0)
            {
                //Successful message
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
                    String cstext = "alert('Successfully Added.');";
                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                }              
            }
            Response.Redirect("/ViewShipping.aspx?", false);
        }
    }
}