using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace assignment1
{
    public partial class Question1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void checkBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
                Response.Write("Valid Data");
            else
                Response.Write("Invalid Data");            
        }
    }
}