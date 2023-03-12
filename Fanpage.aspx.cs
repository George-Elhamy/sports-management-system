using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.CodeDom;
using System.Xml.Linq;
using System.Net;
using System.Security.Policy;

namespace Database_Project
{
    public partial class Try : System.Web.UI.Page
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
            try
            {
                String conn = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(conn);
                DateTime d = Convert.ToDateTime(startdate.Text);


                String sqlquery = "select * from dbo.availableMatchesToAttend(@date)";
                SqlCommand cmd = new SqlCommand(sqlquery, sqlconn);
                cmd.Parameters.AddWithValue("@date", d);

                sqlconn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                StringBuilder sb = new StringBuilder();
                sb.Append("<center>");
                sb.Append("<h1>AVAILABLE MATCHES TO ATTEND</h1>");

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
            catch (System.FormatException)
            {
                Response.Write("<script>alert('PLEASE ENTER THE TIME!')</script>");

            }


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (host.Text == "" || guest.Text == "" || start.Text == "")
            {
                Response.Write("<script>alert('PLEASE ENTER HOST,GUEST AND STARTING TIME!')</script>");
                return;
            }
            try
            {
            String conn = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(conn);

            String host_name = host.Text;
            String guest_name = guest.Text;
            DateTime start_date = Convert.ToDateTime(start.Text);
            




            SqlCommand cmd = new SqlCommand("purchaseTicket", sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@host", host_name));
            cmd.Parameters.Add(new SqlParameter("@guest", guest_name));
            cmd.Parameters.Add(new SqlParameter("@starting_time", start_date));
            cmd.Parameters.Add(new SqlParameter("@national_id", Session["national"]));


            sqlconn.Open();

            SqlCommand cmd2 = new SqlCommand("select * from [club]", sqlconn);
            SqlDataReader reader = cmd2.ExecuteReader();
            bool flag1 = false;
            bool flag2 = false;
            while (reader.Read())
            {
                if (reader[2].ToString() == host.Text)
                {
                    flag1 = true;

                }

            }
            sqlconn.Close();
            sqlconn.Open();
            reader = cmd2.ExecuteReader();
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
            sqlconn.Close();

            sqlconn.Open();
            SqlCommand cmd3 = new SqlCommand("select * from availableMatchesToAttend(@date)", sqlconn);
            cmd3.Parameters.AddWithValue("@date", start_date);


            reader = cmd3.ExecuteReader();
            bool flag3 = false;
            while (reader.Read())
            {
                   
                if (reader[0].ToString() == host.Text && reader[1].ToString() == guest.Text && reader[4].Equals(start_date))
                {
                    flag3 = true;
                    break;
                }

            }
            if (flag3 == false)
            {
                Response.Write("<script>alert('THIS MATCH DOES NOT EXIST OR HAS NO AVAILABLE TICKETS')</script>");

            }
            sqlconn.Close();


            sqlconn.Open();
            cmd.ExecuteNonQuery();
            sqlconn.Close();
        } 
            catch (System.FormatException)
            {
                Response.Write("<script>alert('PLEASE ENTER THE TIME!')</script>");

            }

        }
    }
}