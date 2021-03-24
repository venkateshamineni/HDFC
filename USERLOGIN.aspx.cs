using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication8
{
    public partial class USERLOGIN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (new BusinessLogic().LoginUser(new Account() { UserId = TextBox1.Text, password = TextBox2.Text }))
            {
                Session["UserId"] = TextBox1.Text;
                Response.Redirect("User Transaction.aspx");
            }
            else
            {
                Label1.Text = "Invalid Username or Password";
            }
        }
    }
}