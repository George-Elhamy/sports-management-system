using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Database_Project
{
    public partial class system_admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (name1.Text == "" || location1.Text=="")
            {
                Response.Write("<script>alert('PLEASE ENTER NAME AND LOCATION!')</script>");
                return;
            }
            String connStr = WebConfigurationManager.ConnectionStrings["Project"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String n = name1.Text;
            String l = location1.Text;
            SqlCommand addc = new SqlCommand("addClub", conn);
            addc.CommandType = CommandType.StoredProcedure;
            addc.Parameters.Add(new SqlParameter("@name", n));
            addc.Parameters.Add(new SqlParameter("@location", l));
            conn.Open();
            try
            {
                addc.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Response.Write("<script>alert('THIS CLUB NAME ALREADY EXISTS!')</script>");

            }
            conn.Close();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (name2.Text == "" )
            {
                Response.Write("<script>alert('PLEASE ENTER NAME!')</script>");
                return;
            }
            String connStr = WebConfigurationManager.ConnectionStrings["Project"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String n = name2.Text;
            SqlCommand addc = new SqlCommand("deleteClub", conn);
            addc.CommandType = CommandType.StoredProcedure;
            addc.Parameters.Add(new SqlParameter("@name", n));
            conn.Open();

            String conn2 = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(conn2);
            sqlconn.Open();

            SqlCommand cmd = new SqlCommand("select * from [club]", sqlconn);
            SqlDataReader reader = cmd.ExecuteReader();

            bool flag2 = false;
            while (reader.Read())
            {
                if (reader[2].ToString() == name2.Text)
                {
                    flag2 = true;

                }

            }
            if (!flag2)
            {
                Response.Write("<script>alert('THIS CLUB DOES NOT EXIST!')</script>");

            }
            sqlconn.Close();
            addc.ExecuteNonQuery();
            conn.Close();
           
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (name3.Text == "" || location2.Text=="" || capacity.Text=="")
            {
                Response.Write("<script>alert('PLEASE ENTER STADIUM NAME , LOCATION AND CAPACITY!')</script>");
                return;
            }
            String connStr = WebConfigurationManager.ConnectionStrings["Project"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String n = name3.Text;
            String l = location2.Text;
            Int64 c = Int64.Parse(capacity.Text);
            SqlCommand addc = new SqlCommand("addStadium", conn);
            addc.CommandType = CommandType.StoredProcedure;
            addc.Parameters.Add(new SqlParameter("@name", n));
            addc.Parameters.Add(new SqlParameter("@location", l));
            addc.Parameters.Add(new SqlParameter("@capacity", c));
            conn.Open();

            try
            {
                addc.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                Response.Write("<script>alert('THIS STADIUM ALREADY EXISTS')</script>");

            }
            conn.Close();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (name4.Text == "")
            {
                Response.Write("<script>alert('PLEASE ENTER STADIUM NAME!')</script>");
                return;
            }
            String connStr = WebConfigurationManager.ConnectionStrings["Project"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String n = name4.Text;
            SqlCommand addc = new SqlCommand("deleteStadium", conn);
            addc.CommandType = CommandType.StoredProcedure;
            addc.Parameters.Add(new SqlParameter("@name", n));
            conn.Open();

            String conn2 = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(conn2);
            sqlconn.Open();

            SqlCommand cmd = new SqlCommand("select * from [stadium]", sqlconn);
            SqlDataReader reader = cmd.ExecuteReader();

            bool flag3 = false;
            while (reader.Read())
            {
                if (reader[2].ToString() == name4.Text)
                {
                    flag3 = true;

                }

            }
            if (!flag3)
            {
                Response.Write("<script>alert('THIS STADIUM DOES NOT EXIST!')</script>");

            }
            sqlconn.Close();

            addc.ExecuteNonQuery();
            conn.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (national.Text == "")
            {
                Response.Write("<script>alert('PLEASE ENTER NATIONAL ID!')</script>");
                return;
            }
            String connStr = WebConfigurationManager.ConnectionStrings["Project"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String n = national.Text;
            SqlCommand addc = new SqlCommand("blockFan", conn);
            addc.CommandType = CommandType.StoredProcedure;
            addc.Parameters.Add(new SqlParameter("@national_id", n));
            conn.Open();

            String conn2 = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(conn2);
            sqlconn.Open();


            SqlCommand cmd = new SqlCommand("select * from [fan]", sqlconn);
            SqlDataReader reader = cmd.ExecuteReader();

            bool flag4 = false;
            while (reader.Read())
            {
                if (reader[0].ToString() == national.Text)
                {
                    flag4 = true;

                }

            }
            if (!flag4)
            {
                Response.Write("<script>alert('THIS NATIONAL ID DOES NOT EXIST!')</script>");

            }
            sqlconn.Close();

            addc.ExecuteNonQuery();
            conn.Close();
        }
    }
}