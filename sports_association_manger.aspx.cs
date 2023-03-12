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
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Database_Project
{
    public partial class sports_association_manger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("start.aspx");
            }
        }

     

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (host.Text == "" || guest.Text == "" || start.Text == "" || end.Text=="")
            {
                Response.Write("<script>alert('PLEASE ENTER HOST,GUEST, STARTING TIME AND ENDING TIME!')</script>");
                return;
            }
            else if (host.Text.ToString()==guest.Text.ToString())
            {
                Response.Write("<script>alert('THE SAME CLUB CAN NOT PLAY AGAINIST ITSELF!')</script>");
                return;
            }
            
            String connStr = WebConfigurationManager.ConnectionStrings["Project"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String h = host.Text;
            String g = guest.Text;
            DateTime st = Convert.ToDateTime(start.Text);
            DateTime et = Convert.ToDateTime(end.Text);
            if (st.CompareTo(et) > 0)
            {
                Response.Write("<script>alert('END TIME MUST BE AFTER START TIME!')</script>");
                return;

            }
            else
            {

                SqlCommand addc = new SqlCommand("addNewMatch", conn);
                addc.CommandType = CommandType.StoredProcedure;
                addc.Parameters.Add(new SqlParameter("@host", h));
                addc.Parameters.Add(new SqlParameter("@guest", g));
                addc.Parameters.Add(new SqlParameter("@dateStart", st));
                addc.Parameters.Add(new SqlParameter("@dateEnd", et));


                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from [club]", conn);

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
                SqlDataReader reader2 = cmd.ExecuteReader();
                while (reader2.Read())
                {
                    if (reader2[2].ToString() == guest.Text)
                    {
                        flag2 = true;

                    }

                }

                if (!(flag1 && flag2))
                {
                    Response.Write("<script>alert('CLUBS DO NOT EXIST!')</script>");
                    return;
                }
                conn.Close();
                conn.Open();
                SqlCommand cmd3 = new SqlCommand("select h.name as Host ,g.name , m.start_time , m.end_time from Match m inner join Club h on h.id=m.host_id inner join Club g on g.id=m.guest_id", conn);

                reader = cmd3.ExecuteReader();

                while (reader.Read())
                {

                    if (reader[0].ToString() == host.Text || reader[0].ToString() == guest.Text || reader[1].ToString() == host.Text || reader[1].ToString() == guest.Text)
                    {
                        DateTime s = Convert.ToDateTime(reader[2]);
                        DateTime ee = Convert.ToDateTime(reader[3]);
                        if ((s.CompareTo(et) < 0 && et.CompareTo(ee) <= 0) || (s.CompareTo(st) <= 0 && (st.CompareTo(ee) < 0)))
                        {
                            Response.Write("<script>alert('ONE OF THE CLUBS OR BOTH HAVE A MATCH AT THAT TIME PLEASE CHECK MATCHES SCHEDULE!')</script>");
                            return;
                        }
                    }

                }
                conn.Close();
                conn.Open();
                addc.ExecuteNonQuery();
                conn.Close();
            }
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (host0.Text == "" || guest0.Text == "" || start0.Text == "" || end0.Text == "")
            {
                Response.Write("<script>alert('PLEASE ENTER HOST,GUEST, STARTING TIME AND ENDING TIME!')</script>");
                return;
            }
            String connStr = WebConfigurationManager.ConnectionStrings["Project"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String h = host0.Text;
            String g = guest0.Text;
            DateTime st = Convert.ToDateTime(start0.Text);
            DateTime et = Convert.ToDateTime(end0.Text);


            SqlCommand addc = new SqlCommand("deleteMatch", conn);
            addc.CommandType = CommandType.StoredProcedure;
            addc.Parameters.Add(new SqlParameter("@host", h));
            addc.Parameters.Add(new SqlParameter("@guest", g));


            conn.Open();


            String conn2 = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(conn2);
            sqlconn.Open();


            SqlCommand cmd = new SqlCommand("select * from [club]", sqlconn);

            SqlDataReader reader = cmd.ExecuteReader();

            bool flag1 = false;
            bool flag2 = false;
            while (reader.Read())
            {
                if (reader[2].ToString() == host0.Text)
                {
                    flag1 = true;

                }

            }
            sqlconn.Close();
            sqlconn.Open();
            SqlDataReader reader2 = cmd.ExecuteReader();
            while (reader2.Read())
            {
                if (reader2[2].ToString() == guest0.Text)
                {
                    flag2 = true;

                }

            }
            Response.Write("" + flag1 + flag2 + "\r\n");
            if (!(flag1 && flag2))
            {
                Response.Write("<script>alert('THIS MATCH DOES NOT EXIST!')</script>");
                return;

            }
            sqlconn.Close();
            sqlconn.Open();
            SqlCommand cmd3 = new SqlCommand("select h.name as Host ,g.name , m.start_time , m.end_time from Match m inner join Club h on h.id=m.host_id inner join Club g on g.id=m.guest_id", sqlconn);
            


            reader = cmd3.ExecuteReader();
            bool flag3 = false;
            while (reader.Read())
            {
                
                if (reader[0].ToString() == host0.Text && reader[1].ToString() == guest0.Text && reader[2].Equals(st) && reader[3].Equals(et))
                {
                    flag3 = true;
                    break;
                }

            }
            if (flag3 == false)
            {
                Response.Write("<script>alert('THIS MATCH DOES NOT EXIST ')</script>");
                return;

            }

            sqlconn.Close();

            sqlconn.Open();
            addc.ExecuteNonQuery();
            conn.Close();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            String conn = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(conn);


            String sqlquery = "select * from allMatches where start_time > CURRENT_TIMESTAMP ";
            SqlCommand cmd = new SqlCommand(sqlquery, sqlconn);

            sqlconn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            StringBuilder sb = new StringBuilder();
            sb.Append("<center>");
            sb.Append("<h1>UPCOMING MATCHES </h1>");

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

        protected void Button4_Click(object sender, EventArgs e)
        {
            String conn = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(conn);


            String sqlquery = "select * from allMatches where start_time < CURRENT_TIMESTAMP ";
            SqlCommand cmd = new SqlCommand(sqlquery, sqlconn);

            sqlconn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            StringBuilder sb = new StringBuilder();
            sb.Append("<center>");
            sb.Append("<h1>ALREADY PLAYED MATCH </h1>");

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

        protected void Button5_Click(object sender, EventArgs e)
        {
            String conn = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(conn);


            String sqlquery = "select * from clubsNeverMatched  ";
            SqlCommand cmd = new SqlCommand(sqlquery, sqlconn);

            sqlconn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            StringBuilder sb = new StringBuilder();
            sb.Append("<center>");
            sb.Append("<h1>CLUBS NEVER MATCHED </h1>");

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
    }
}