using System;

namespace Electricity_Billing_Project
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User"] != null)
                {
                    login.Visible = false;
                    welcome.Visible = true;
                    add.Visible = true;
                    view.Visible = true;
                    logout.Visible = true;
                }
                else
                {
                    login.Visible = true;
                    welcome.Visible = false;
                    add.Visible = false;
                    view.Visible = false;
                    logout.Visible = false;
                }
            }
        }
    }
}