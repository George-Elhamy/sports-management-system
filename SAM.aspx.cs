using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Database_Project
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginbutton_Click(object sender, EventArgs e)
        {
            if (username.Text == "" || password.Text == "" || name.Text == "")
            {
                Response.Write("<script>alert('PLEASE ENTER NAME,USERNAME AND PASSWORD!')</script>");

            }
            else
            {
                String connStr = WebConfigurationManager.ConnectionStrings["Project"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                String n = name.Text;
                String user = username.Text;
                String pass = password.Text;

                SqlCommand addsam = new SqlCommand("addAssociationManager", conn);
                addsam.CommandType = CommandType.StoredProcedure;
                addsam.Parameters.Add(new SqlParameter("@name", n));
                addsam.Parameters.Add(new SqlParameter("@username", user));
                addsam.Parameters.Add(new SqlParameter("@password", pass));
                Session["user"] = user;
                Session["pass"] = pass;

                conn.Open();
                try
                {

                    addsam.ExecuteNonQuery();
                    Response.Redirect("sports_association_manger.aspx");

                }
                catch (Exception)
                {
                    Response.Write("<script>alert('USERNAME ALREADY TAKEN')</script>");
                }
                conn.Close();
            }
        }
    }
}