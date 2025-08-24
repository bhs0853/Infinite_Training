using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace Electricity_Billing_Project
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/LoginPage.aspx");
            status.Text = "";
            error.Text = "";
            if (IsPostBack && ViewState["BillCount"] != null)
            {
                int count = (int)ViewState["BillCount"];
                CreateBillControls(count);
            }
        }

        protected void addBtn_Click(object sender, EventArgs e)
        {
            int count;
            int.TryParse(txtBillCount.Text, out count);
            if (count <= 0)
                return;

            ViewState["BillCount"] = count;
            CreateBillControls(count);
            saveBtn.Visible = true;
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            int count;
            int.TryParse(txtBillCount.Text, out count);
            if (count <= 0)
                return;

            status.Text = "";

            for (int i = 1; i <= count; i++)
            {
                try
                {
                    var num = (TextBox)bills.FindControl($"num{i}");
                    var name = (TextBox)bills.FindControl($"name{i}");
                    var units = (TextBox)bills.FindControl($"units{i}");
                    if (!Regex.IsMatch(num.Text.Trim(), @"^EB\d{5}$"))
                        throw new FormatException("Invalid Consumer Number" + num.Text.Trim());

                    if (num != null && name != null && units != null)
                    {
                        string consumerno = num.Text.Trim();
                        string consumername = name.Text.Trim();
                        int unitsconsumed;
                        int.TryParse(units.Text.Trim(), out unitsconsumed);

                        BillValidator bv = new BillValidator();

                        if (bv.ValidateUnitsConsumed(unitsconsumed) != "Valid units")
                        {
                            error.Text += $"<p>Bill {i}: Invalid units <p>";
                            continue;
                        }
                        ElectricityBill bill = new ElectricityBill
                        {
                            ConsumerNumber = consumerno,
                            ConsumerName = consumername,
                            UnitsConsumed = unitsconsumed
                        };
                        ElectricityBoard eb = new ElectricityBoard();
                        eb.CalculateBill(bill);
                        eb.AddBill(bill);
                    }
                }
                catch (FormatException fe)
                {
                    error.Text += $"<p>Bill {i} Error: {fe.Message}</p>";
                    continue;
                }
                status.Text += $"<p>Saved Bill {i}</p>";
            }
        }
        private void CreateBillControls(int count)
        {
            bills.Controls.Clear();

            for (int i = 0; i < count; i++)
            {
                var bill = new Panel();

                bill.Controls.Add(new Literal { Text = $"<h4>Bill No. {i + 1}</h4>" });

                bill.Controls.Add(new Literal { Text = "<br /><label>Enter Consumer Number: </label>" });
                bill.Controls.Add(new TextBox { ID = $"num{i + 1}" });

                bill.Controls.Add(new Literal { Text = "<br /><br /><label>Enter Consumer Name: </label>&nbsp;" });
                bill.Controls.Add(new TextBox { ID = $"name{i + 1}" });

                bill.Controls.Add(new Literal { Text = "<br /><br /><label>Enter Units Consumed: </label>&nbsp;" });
                bill.Controls.Add(new TextBox { ID = $"units{i + 1}" });

                bills.Controls.Add(bill);
            }
        }
    }
}