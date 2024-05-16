using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminManage_Admin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            // Check if user is authenticated and session variables are set
            if (Session["Username"] != null && Session["ProfileImage"] != null)
            {
                show();
                Label1.Text = Session["Username"].ToString();
                Image1.ImageUrl = Session["ProfileImage"].ToString();
                // Load data when page first loads
            }
            else
            {
                Response.Redirect("~/AdminManage/LoginPage.aspx");
            }
        }
    }

    private void show()
    {
        try
        {
            // Establish database connection and retrieve data
            string connectionString = "Data Source=DESKTOP-E9ANIN8;Initial Catalog=FashionAdda;Integrated Security=True"; // Update with your database connection string
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Notices", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else
                {
                    Repeater1.DataSource = null;
                    Repeater1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            // Display error message using JavaScript alert
            messagebox("Failed: " + ex.Message);
        }
    }

    private void messagebox(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            // Register JavaScript to display alert message
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "')", true);
        }
    }

}

