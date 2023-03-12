using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Database_Project
{
    public partial class Start : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("SAM.aspx");
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("CR.aspx");

        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("FAN.aspx");

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String conn = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(conn);

            sqlconn.Open();
            if (username.Text == "" && password.Text == "")
            {
                Response.Write("<script>alert('Please enter username and password!')</script>");

            }
            else if (username.Text == "" )
            {
                Response.Write("<script>alert('Please enter username!')</script>");
            }
            else if (password.Text == "")
            {
                Response.Write("<script>alert('Please enter password!')</script>");
            }
            else if (username.Text=="admin" && password.Text == "admin")
            {
                Response.Redirect("system_admin.aspx");
            }
            else if (RadioButton1.Checked)
            {
                SqlCommand cmd = new SqlCommand("select * from [Sports_association_manager]", sqlconn);
                SqlDataReader reader = cmd.ExecuteReader();
                bool flag = false;
                while(reader.Read())
                {
                    if (reader[2].ToString() == username.Text)
                    {
                        if (reader[3].ToString() == password.Text)
                        {
                            Session["user"] = username.Text;
                            Session["pass"] = password.Text;
                            flag = true;
                            Response.Redirect("sports_association_manger.aspx"); 
                        }
                       
                    }
                }
                if (!flag)
                {
                    Response.Write("<script>alert('INVALID USERNAME OR PASSWORD!')</script>");

                }
            }
            else if (RadioButton2.Checked)
            {
                SqlCommand cmd = new SqlCommand("select * from [Club_representative]", sqlconn);
                SqlDataReader reader = cmd.ExecuteReader();
                bool flag = false;

                while (reader.Read())
                {
                    if (reader[2].ToString() == username.Text)
                    {
                        if (reader[3].ToString() == password.Text)
                        {
                            String conn2 = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;
                            SqlConnection sqlconn2 = new SqlConnection(conn);
                            Int64 club_id = Int64.Parse(reader[4].ToString());

                            String sqlquery = "select top(1) c.name from club c where c.id= @club";
                            SqlCommand little = new SqlCommand(sqlquery, sqlconn2);
                            little.Parameters.AddWithValue("@club", club_id);

                            sqlconn2.Open();
                            String name = little.ExecuteScalar().ToString();
                            sqlconn2.Close();
                            Session["user"] = username.Text;
                            Session["pass"] = password.Text;
                            Session["clubname"] = name;
                            flag = true;
                            Response.Redirect("club_representative.aspx");
                           
                        }
                        
                    }
                    
                }
                if (!flag)
                {
                    Response.Write("<script>alert('INVALID USERNAME OR PASSWORD!')</script>");

                }

            }
            else if (RadioButton3.Checked)
            {
                SqlCommand cmd = new SqlCommand("select * from [Stadium_manager]", sqlconn);
                SqlDataReader reader = cmd.ExecuteReader();
                bool flag = false;
                while (reader.Read())
                {
                    if (reader[2].ToString() == username.Text)
                    {
                        if (reader[3].ToString() == password.Text)
                        {
                            String conn2 = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;
                            SqlConnection sqlconn2 = new SqlConnection(conn);
                            Int64 stadium_id = Int64.Parse(reader[4].ToString());

                            String sqlquery = "select top(1) s.name from Stadium s where s.id= @stadium";
                            SqlCommand little = new SqlCommand(sqlquery, sqlconn2);
                            little.Parameters.AddWithValue("@stadium", stadium_id);

                            sqlconn2.Open();
                            String name = little.ExecuteScalar().ToString();
                            sqlconn2.Close();

                            Session["user"] = username.Text;
                            Session["pass"] = password.Text;
                            Session["stadiumname"] = name;
                            flag =true;
                            Response.Redirect("stadium_manager.aspx");
                        }
                    }   
                }
                if (!flag)
                {
                    Response.Write("<script>alert('INVALID USERNAME OR PASSWORD!')</script>");

                }
            }
            else if (RadioButton4.Checked)
            {
                SqlCommand cmd = new SqlCommand("select * from [Fan]", sqlconn);
                SqlDataReader reader = cmd.ExecuteReader();
                bool flag = false;
                while (reader.Read())
                {
                    if (reader[6].ToString() == username.Text)
                    {
                        if (reader[7].ToString() == password.Text)
                        {
                            if (reader[5].Equals(false))
                            {
                                Response.Write("<script>alert('YOU ARE BLOCKED!')</script>");
                                return;
                            }
                            else
                            {
                                String national = reader[0].ToString();
                                Session["user"] = username.Text;
                                Session["pass"] = password.Text;
                                Session["national"] = national;
                                flag = true;
                                Response.Redirect("fanpage.aspx");
                            }
                        }
                    }
                }
                if (!flag)
                {
                    Response.Write("<script>alert('INVALID USERNAME OR PASSWORD!')</script>");

                }
            }
            else if (!(RadioButton1.Checked|| RadioButton2.Checked || RadioButton3.Checked || RadioButton4.Checked))  
            {
                Response.Write("<script>alert('PLEASE CHOOSE YOUR ROLE!')</script>");

            }
            sqlconn.Close();


        }
    }
}