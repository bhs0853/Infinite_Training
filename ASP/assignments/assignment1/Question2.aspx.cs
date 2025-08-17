using System;
using System.Collections.Generic;

namespace assignment1
{
    public partial class Question2 : System.Web.UI.Page
    {
        Dictionary<string, (string, string)> products = new Dictionary<string, (string, string)>
        {
            { "Hp Envy", ("~/images/envy.jpg", "₹98,000") },
            { "Apple Macbook", ("~/images/macbook.jpg", "₹112,000") },
            { "Asus Vivobook", ("~/images/vivobook.jpg", "₹86,500") },
            { "Dell XPS", ("~/images/xps.jpg", "₹100,000") }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (var product in products.Keys)
                {
                    ddlProducts.Items.Add(product);
                }
            }
        }

        protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (products.TryGetValue(ddlProducts.SelectedValue, out var product))
            {
                imgProduct.ImageUrl = product.Item1;
                lblPrice.Text = "";
            }
            else
            {
                imgProduct.ImageUrl = "";
                lblPrice.Text = "";
            }
        }

        protected void getPriceBtn_Click(object sender, EventArgs e)
        {
            if (products.TryGetValue(ddlProducts.SelectedValue, out var product))
            {
                lblPrice.Text = $"Price of This Laptop: {product.Item2}";
            }
            else
            {
                lblPrice.Text = "Please select a product.";
            }
        }
    }
}