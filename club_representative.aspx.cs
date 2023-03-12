using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Xml.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Database_Project
{
    public partial class club_representative : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                String conn = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(conn);

                String club_name = Session["clubname"].ToString();
                String sqlquery = "select * from club c where c.name= @club ";
                SqlCommand cmd = new SqlCommand(sqlquery, sqlconn);
                cmd.Parameters.AddWithValue("@club", club_name);

                sqlconn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                StringBuilder sb = new StringBuilder();
                sb.Append("<center>");

                sb.Append("<table border=1>");
                sb.Append("<tr>");
                foreach (DataColumn dc in dt.Columns)
                {
                    sb.Append("<th>");
                    sb.Append(dc.ColumnName.ToUpper());
                    sb.Append("<th/>");
                }
                sb.Append("<tr/>");

                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<tr>");
                    foreach (DataColumn dc in dt.Columns)
                    {
                        sb.Append("<th>");
                        sb.Append(dr[dc.ColumnName].ToString());
                        sb.Append("<th/>");
                    }
                    sb.Append("<tr/>");
                }
                sb.Append("</table>");
                sb.Append("</center>");
                Panel1.Controls.Add(new Label { Text = sb.ToString() });
                sqlconn.Close();
            }
            else
            {
                Response.Redirect("start.aspx");
            }
        }

        protected void B1_Click(object sender, EventArgs e)
        {
            if (name1.Text=="" || start.Text == "")
            {
                Response.Write("<script>alert('PLEASE ENTER THE STADIUM NAME AND THE STARTING TIME OF THE MATCH!')</script>");
                return;
            }
            try
            {


                String connStr = WebConfigurationManager.ConnectionStrings["Project"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                String n = name1.Text;
                DateTime date = Convert.ToDateTime(start.Text);

                SqlCommand cmd = new SqlCommand("addHostRequest", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@clubname", Session["clubname"]);
                cmd.Parameters.AddWithValue("@stadium_name", n);
                cmd.Parameters.AddWithValue("@starting_time", date);

                SqlCommand cmd2 = new SqlCommand(" select * from viewAvailableStadiumsOn(@date)", conn);
                cmd2.Parameters.AddWithValue("@date", date);
                bool flag = false;
                conn.Open();
                SqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[0].ToString() == name1.Text)
                    {
                        flag = true;

                    }

                }

                if (!flag)
                {
                    Response.Write("<script>alert('THIS STADIUM IS NOT AVAILABLE OR DOES NOT EXIST , PLEASE CHECK THE AVAILABLE STADIUMS SCHEDULE!')</script>");
                    return;
                }
                conn.Close();
                SqlCommand cmd3 = new SqlCommand(" select * from allUnassignedMatches(@club_name)", conn);
                cmd3.Parameters.AddWithValue("@club_name", Session["clubname"]);
                bool flag3 = false;
                conn.Open();
                reader = cmd3.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[1].Equals(date))
                    {
                        flag3 = true;

                    }

                }

                if (!flag3)
                {
                    Response.Write("<script>alert('THIS MATCH DOES NOT EXIST OR IS ALREADY ASSIGNED TO A STADIUM. IT CAN BE THE CASE ALSO THAT YOU ARE NOT THE HOST OF THE MATCH;ONLY HOSTS CAN SEND REQUESTS!')</script>");
                    return;
                }
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            catch (System.FormatException)
            {
                Response.Write("<script>alert('PLEASE ENTER THE STARTING TIME OF THE MATCH!')</script>");
                return;
            }


        }

        protected void B3_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Project"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String club_name = Session["clubname"].ToString();

            SqlCommand cmd = new SqlCommand(" select * from upcomingMatchesOfClub(@club_name)", conn);
            cmd.Parameters.AddWithValue("@club_name", club_name);

            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            StringBuilder sb = new StringBuilder();
            sb.Append("<center>");
            sb.Append("<h1>UPCOMING MATCHES OF MY CLUB</h1>");


            sb.Append("<table border=1>");
            sb.Append("<tr>");
            foreach (DataColumn dc in dt.Columns)
            {
                sb.Append("<th>");
                sb.Append(dc.ColumnName.ToUpper());
                sb.Append("<th/>");
            }
            sb.Append("<tr/>");

            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<tr>");
                foreach (DataColumn dc in dt.Columns)
                {
                    sb.Append("<th>");
                    sb.Append(dr[dc.ColumnName].ToString());
                    sb.Append("<th/>");
                }
                sb.Append("<tr/>");
            }
            sb.Append("</table>");
            sb.Append("</center>");
            Panel2.Controls.Add(new Label { Text = sb.ToString() });
            conn.Close();





        }

        protected void B4_Click(object sender, EventArgs e)
        {
            if (name3.Text == "" )
            {
                Response.Write("<script>alert('PLEASE ENTER THE STARTING TIME OF THE MATCH!')</script>");
                return;
            }
            try
            {


                String connStr = WebConfigurationManager.ConnectionStrings["Project"].ToString();
                SqlConnection conn = new SqlConnection(connStr);


                DateTime d = Convert.ToDateTime(name3.Text);

                SqlCommand cmd = new SqlCommand(" select * from viewAvailableStadiumsOn(@date)", conn);
                cmd.Parameters.AddWithValue("@date", d);


                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                StringBuilder sb = new StringBuilder();
                sb.Append("<center>");
                sb.Append("<h1>AVAILABLE STADIUMS</h1>");


                sb.Append("<table border=1>");
                sb.Append("<tr>");
                foreach (DataColumn dc in dt.Columns)
                {
                    sb.Append("<th>");
                    sb.Append(dc.ColumnName.ToUpper());
                    sb.Append("<th/>");
                }
                sb.Append("<tr/>");

                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<tr>");
                    foreach (DataColumn dc in dt.Columns)
                    {
                        sb.Append("<th>");
                        sb.Append(dr[dc.ColumnName].ToString());
                        sb.Append("<th/>");
                    }
                    sb.Append("<tr/>");
                }
                sb.Append("</table>");
                sb.Append("</center>");
                Panel2.Controls.Add(new Label { Text = sb.ToString() });
                conn.Close();
            }
            catch (System.FormatException)
            {
                Response.Write("<script>alert('PLEASE ENTER THE STARTING TIME OF THE MATCH!')</script>");
                return;
            }
        }
    }
}