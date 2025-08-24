using System;
using System.Web.Security;

namespace Electricity_Billing_Project
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            if (FormsAuthentication.Authenticate(txtuser.Text, txtpass.Text))
            {
                Session["User"] = txtuser.Text;
                FormsAuthentication.RedirectFromLoginPage(txtuser.Text, false);
            }
            else
            {
                lblmsg.Text = "Invalid User Name or Password";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}