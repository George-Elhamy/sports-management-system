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
using System.Configuration;

namespace Database_Project
{
    public partial class SM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void loginbutton_Click(object sender, EventArgs e)
        {
            if (username.Text == "" || password.Text == "" || name.Text == "" || stadium.Text == "")
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
                String stadiumname = stadium.Text;

                SqlCommand addsam = new SqlCommand("addStadiumManager", conn);
                addsam.CommandType = CommandType.StoredProcedure;
                addsam.Parameters.Add(new SqlParameter("@name", n));
                addsam.Parameters.Add(new SqlParameter("@username", user));
                addsam.Parameters.Add(new SqlParameter("@password", pass));
                addsam.Parameters.Add(new SqlParameter("@stadium_name", stadiumname));

                Session["user"] = user;
                Session["pass"] = pass;
                Session["stadiumname"] = stadiumname;


                try
                {
                    conn.Open();
                    String conn2 = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;
                    SqlConnection sqlconn2 = new SqlConnection(conn2);

                    sqlconn2.Open();
                    SqlCommand cmd = new SqlCommand("select * from [Stadium]", sqlconn2);
                    SqlDataReader reader = cmd.ExecuteReader();
                    bool flag = false;
                    while (reader.Read())
                    {
                        if (reader[2].ToString() == stadium.Text)
                        {
                            flag = true;

                        }
                    }
                    if (!flag)
                    {
                        Response.Write("<script>alert('THE CHOSEN STADIUM DOES NOT EXIST!')</script>");
                        return;

                    }
                    sqlconn2.Close();
                    addsam.ExecuteNonQuery();
                    Response.Redirect("stadium_manager.aspx");
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    Response.Write("<script>alert('USERNAME ALREADY TAKEN OR THE CHOSEN STADIUM ALREADY HAS A MANAGER')</script>");

                }
                conn.Close();
            }
        }
    }
}