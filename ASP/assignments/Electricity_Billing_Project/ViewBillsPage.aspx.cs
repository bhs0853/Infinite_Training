using System;

namespace Electricity_Billing_Project
{
    public partial class ViewBillsPage1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/LoginPage.aspx");
        }

        protected void GetDataBtn_Click(object sender, EventArgs e)
        {
            int n = 0;
            int.TryParse(nValue.Text, out n);
            if (n <= 0)
            {
                error.Visible = true;
                error.Text = "N cannot be less than or equal to zero";
                return;
            }
            ElectricityBoard eb = new ElectricityBoard();
            var bills = eb.Generate_N_BillDetails(n);
            txt.Visible = true;
            nbills.Visible = true;
            if (bills.Count <= 0)
                nbills.Text = "<p> No Bills Found </p>";
            else
            {
                nbills.Text = "";
                for (int i = 0; i < Math.Min(n, bills.Count); i++)
                    nbills.Text += $"<p>EB Bill for {bills[i].ConsumerName} is {bills[i].BillAmount}</p>";
            }
        }
    }
}