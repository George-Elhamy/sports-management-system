using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Globalization;

namespace Database_Project
{
    public partial class FAN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void loginbutton_Click(object sender, EventArgs e)
        {
            if (username.Text == "" || password.Text == "" || name.Text == "" || NatID.Text == "" || phone.Text == "" || birth.Text == "" || address.Text == "")
            {
                Response.Write("<script>alert('PLEASE ENTER NAME,USERNAME AND PASSWORD!')</script>");

            }
            else
            {
                String connStr = WebConfigurationManager.ConnectionStrings["Project"].ToString();
                SqlConnection conn = new SqlConnection(connStr);
                try 
                { 

                String n = name.Text;
                String user = username.Text;
                String pass = password.Text;
                String national = NatID.Text;
                Int64 phoneN = Int64.Parse(phone.Text);        
                DateTime birthdate = Convert.ToDateTime(birth.Text);
                String add = address.Text;

                SqlCommand addsam = new SqlCommand("addFan", conn);
                addsam.CommandType = CommandType.StoredProcedure;
                addsam.Parameters.Add(new SqlParameter("@name", n));
                addsam.Parameters.Add(new SqlParameter("@username", user));
                addsam.Parameters.Add(new SqlParameter("@password", pass));
                addsam.Parameters.Add(new SqlParameter("@national_id", national));
                addsam.Parameters.Add(new SqlParameter("@address", add));
                addsam.Parameters.Add(new SqlParameter("@birthdate", birthdate));
                addsam.Parameters.Add(new SqlParameter("@phone", phoneN));

                Session["user"] = user;
                Session["pass"] = pass;
                Session["national"] = national;

                conn.Open();
               

                    addsam.ExecuteNonQuery();
                    Response.Redirect("Fanpage.aspx");
                }
                catch (System.FormatException)
                {
                    Response.Write("<script>alert('PLEASE ENTER A VALID DATE FORMAT!')</script>");

                }
                catch (System.Data.SqlClient.SqlException)
                {
                    Response.Write("<script>alert('USERNAME OR NATIONAL ID ALREADY EXISTS!')</script>");

                }
                
                conn.Close();

            }
        }
    }
}