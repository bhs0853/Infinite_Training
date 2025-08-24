using System;

namespace Electricity_Billing_Project
{
    public partial class WelcomePage1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/LoginPage.aspx");
        }
        protected void AddBillsBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddBillsPage.aspx");
        }

        protected void ViewBillsBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewBillsPage.aspx");
        }
    }
}