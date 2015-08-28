using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Gets a reference to a TextBox control inside a ContentPlaceHolder
        ContentPlaceHolder mpContentPlaceHolder;
        TextBox mpTextBox;
        mpContentPlaceHolder =(ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
        if (mpContentPlaceHolder != null)
        {
            mpTextBox = (TextBox)mpContentPlaceHolder.FindControl("TextBox1");
            if (mpTextBox != null)
            {
                mpTextBox.Text = "TextBox found!";
            }
        }



        ProductModel model = new ProductModel();
        List<Product> products = model.GetAllProducts();

        if (products != null)
        {
            foreach (Product product in products)
            {
                Panel productPanel = new Panel();
                ImageButton imageButton = new ImageButton
                {
                    ImageUrl = "~/Images/Products/" + product.Image,
                    CssClass = "productImage",
                    PostBackUrl = string.Format("~/Pages/Product.aspx?id={0}", product.Id)
                };
                Label lblName = new Label
                {
                    Text = product.Name,
                    CssClass = "productName"
                };
                Label lblPrice = new Label
                {
                    Text = "£ " + product.Price,
                    CssClass = "productPrice"
                };

                productPanel.Controls.Add(imageButton);
                productPanel.Controls.Add(new Literal {Text = "<br/>"});
                productPanel.Controls.Add(lblName);
                productPanel.Controls.Add(new Literal {Text = "<br/>"});
                productPanel.Controls.Add(lblPrice);

                //Add dynamic controls to static control
                pnlProducts.Controls.Add(productPanel);
            }
        }
        else
            pnlProducts.Controls.Add(new Literal {Text = "No products found!"});
    }
}