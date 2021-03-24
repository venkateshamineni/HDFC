using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication8
{
    public partial class User_Transaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Account account = new BusinessLogic().GetAccountByUserID(new Account() { UserId = Session["UserId"].ToString() });
            Label1.Text = account.AcName;
            Label2.Text = account.AcNo;
            Label4.Text = account.UserId;
            Label5.Text = account.Balance.ToString();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (new BusinessLogic().TransferFunds(new Transaction() { FromAccount = Label2.Text, ToAccount = TextBox1.Text, Amount = int.Parse(TextBox2.Text) }))
            {
                Label5.Text = "Amount Transfered Sucessfully......";
            }
            else
            {
                Label5.Text = "Transaction Failed....";

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (new BusinessLogic().ChangePassword(new Account() { UserId = Label4.Text, password = TextBox3.Text }))
            {
                Label6.Text = "Password changed sucessufully";
            }
            else
            {
                Label6.Text = "Failed";
            }
        }
    }
}