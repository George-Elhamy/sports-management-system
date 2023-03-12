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

namespace Database_Project
{
    public partial class stadium_manager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                String conn = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(conn);

                String stadium_name = Session["stadiumname"].ToString();
                String sqlquery = "select * from Stadium s where s.name= @stadium ";
                SqlCommand cmd = new SqlCommand(sqlquery, sqlconn);
                cmd.Parameters.AddWithValue("@stadium", stadium_name);

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

        protected void reuest_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["Project"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String username = Session["user"].ToString();
            
            SqlCommand cmd = new SqlCommand(" select * from allPendingRequests(@stadium_manager_username)", conn);
            cmd.Parameters.AddWithValue("@stadium_manager_username", username);

            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            StringBuilder sb = new StringBuilder();
            sb.Append("<center>");
            sb.Append("<h1>PENDING REQUESTS</h1>");


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

        protected void accept_Click(object sender, EventArgs e)
        {
            if (host.Text == "" || guest.Text == "" || start.Text == "")
            {
                Response.Write("<script>alert('PLEASE ENTER HOST,GUEST AND STARTING TIME!')</script>");
                return;
            }
            else if (host.Text.ToString() == guest.Text.ToString())
            {
                Response.Write("<script>alert('THE SAME CLUB CAN NOT PLAY AGAINIST ITSELF!')</script>");
                return;
            }

            try
            {
                String connStr = WebConfigurationManager.ConnectionStrings["Project"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                String h = host.Text;
                String g = guest.Text;
                DateTime s = Convert.ToDateTime(start.Text);

                SqlCommand cmd = new SqlCommand("select * from [club]", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                bool flag1 = false;
                bool flag2 = false;
                while (reader.Read())
                {
                    if (reader[2].ToString() == host.Text)
                    {
                        flag1 = true;

                    }

                }
                conn.Close();
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[2].ToString() == guest.Text)
                    {
                        flag2 = true;
                    }
                }
                if (!(flag1 && flag2))
                {
                    Response.Write("<script>alert('CLUBS DO NOT EXIST!')</script>");

                }
                conn.Close();

                String username = Session["user"].ToString();
                SqlCommand cmd2 = new SqlCommand(" select * from allPendingRequests(@stadium_manager_username)", conn);
                cmd2.Parameters.AddWithValue("@stadium_manager_username", username);
                conn.Open();
                reader = cmd2.ExecuteReader();
                bool flag3 = false;
                while (reader.Read())
                {
                    if (reader[1].ToString() == host.Text && reader[2].ToString()==guest.Text && reader[3].Equals(s))
                    {
                        flag3 = true;
                    }
                }
                if (!flag3)
                {
                    Response.Write("<script>alert('THIS REQUEST IS NOT IN YOUR PENDING LIST , PLEASE CHECK YOUR LIST!')</script>");

                }
                conn.Close();

                SqlCommand cmd4 = new SqlCommand(" select * from allUnassignedMatches(@club_name)", conn);
                cmd4.Parameters.AddWithValue("@club_name", h);
                bool flag4 = false;
                conn.Open();
                reader = cmd4.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[1].Equals(s))
                    {
                        flag4 = true;

                    }

                }

                if (!flag4)
                {
                    Response.Write("<script>alert('THIS MATCH HAS BEEN ALREADY ASSIGNED TO ANOTHER STADIUM , REJECT THE REQUEST TO DELETE IT!')</script>");
                    return;
                }
                conn.Close();

                SqlCommand cmd5 = new SqlCommand(" select * from viewAvailableStadiumsOn(@date)", conn);
                cmd5.Parameters.AddWithValue("@date", s);
                bool flag5 = false;
                conn.Open();
                reader = cmd5.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[0].ToString()==Session["stadiumname"].ToString())
                    {
                        flag5 = true;

                    }

                }

                if (!flag5)
                {
                    Response.Write("<script>alert('YOUR STADIUM HAS ACCEPTED A REQUEST AND IS HOSTING A MATCH AT THAT TIME,PLEASE REJECT THE REQUEST TO DELETE IT!')</script>");
                    return;
                }
                conn.Close();

                SqlCommand cmd3 = new SqlCommand("acceptRequest", conn);
                cmd3.CommandType = CommandType.StoredProcedure;

                cmd3.Parameters.AddWithValue("@stadium_manager_username", Session["user"]);
                cmd3.Parameters.AddWithValue("@host_name", h);
                cmd3.Parameters.AddWithValue("@guest_name", g);
                cmd3.Parameters.AddWithValue("@start", s);
                conn.Open();
                cmd3.ExecuteNonQuery();
                conn.Close();
            }
            catch (System.FormatException) 
            {
                Response.Write("<script>alert('PLEASE ENTER HOST,GUEST AND STARTING TIME!')</script>");
                return;

            }



        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (host.Text == "" || guest.Text == "" || start.Text == "")
            {
                Response.Write("<script>alert('PLEASE ENTER HOST,GUEST AND STARTING TIME!')</script>");
                return;
            }
            else if (host.Text.ToString() == guest.Text.ToString())
            {
                Response.Write("<script>alert('THE SAME CLUB CAN NOT PLAY AGAINIST ITSELF!')</script>");
                return;
            }

            try
            {
                String connStr = WebConfigurationManager.ConnectionStrings["Project"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                String h = host.Text;
                String g = guest.Text;
                DateTime s = Convert.ToDateTime(start.Text);

                SqlCommand cmd = new SqlCommand("select * from [club]", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                bool flag1 = false;
                bool flag2 = false;
                while (reader.Read())
                {
                    if (reader[2].ToString() == host.Text)
                    {
                        flag1 = true;

                    }

                }
                conn.Close();
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[2].ToString() == guest.Text)
                    {
                        flag2 = true;
                    }
                }
                if (!(flag1 && flag2))
                {
                    Response.Write("<script>alert('CLUBS DO NOT EXIST!')</script>");

                }
                conn.Close();

                String username = Session["user"].ToString();
                SqlCommand cmd2 = new SqlCommand(" select * from allPendingRequests(@stadium_manager_username)", conn);
                cmd2.Parameters.AddWithValue("@stadium_manager_username", username);
                conn.Open();
                reader = cmd2.ExecuteReader();
                bool flag3 = false;
                while (reader.Read())
                {
                    if (reader[1].ToString() == host.Text && reader[2].ToString() == guest.Text && reader[3].Equals(s))
                    {
                        flag3 = true;
                    }
                }
                if (!flag3)
                {
                    Response.Write("<script>alert('THIS REQUEST IS NOT IN YOUR PENDING LIST , PLEASE CHECK YOUR LIST!')</script>");

                }
                conn.Close();



                SqlCommand cmd3 = new SqlCommand("rejectRequest", conn);
                cmd3.CommandType = CommandType.StoredProcedure;

                cmd3.Parameters.AddWithValue("@stadium_manager_username", Session["user"]);
                cmd3.Parameters.AddWithValue("@host_name", h);
                cmd3.Parameters.AddWithValue("@guest_name", g);
                cmd3.Parameters.AddWithValue("@start", s);
                conn.Open();
                cmd3.ExecuteNonQuery();
                conn.Close();
            }
            catch (System.FormatException)
            {
                Response.Write("<script>alert('PLEASE ENTER HOST,GUEST AND STARTING TIME!')</script>");
                return;

            }
        }
    }
}