using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

public partial class Pages_ShoppingCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Check if user is logged in
        string userId = User.Identity.GetUserId();

        //Display all items in user's cart.
        GetPurchasesInCart(userId);
    }

    protected void Delete_Item(object sender, EventArgs e)
    {
        LinkButton selectedLink = (LinkButton)sender;
        //remove the del prefix from the id string (it was added because there are 2 ids on the same page )
        string link = selectedLink.ID.Replace("del", "");
        int cartId = Convert.ToInt32(link);

        var cartModel = new CartModel();
        cartModel.DeleteCart(cartId);

        Response.Redirect("~/Pages/ShoppingCart.aspx");
    }

    private void ddlAmount_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Get ID of product that has had its quantity dropdownlist changed.
        DropDownList selectedList = (DropDownList)sender;
        int cartId = Convert.ToInt32(selectedList.ID);
        int quantity = Convert.ToInt32(selectedList.SelectedValue);


        //Update purchase with new quantity and refresh page
        CartModel cartModel = new CartModel();
        cartModel.UpdateQuantity(cartId, quantity);
        Response.Redirect("~/Pages/ShoppingCart.aspx");
    }

    private void GetPurchasesInCart(string userId)
    {
        CartModel cartModel = new CartModel();
        double subTotal = 0; //will be used to add the total amount of each product type in the shopping cart

        //Get all purchases for current user and display in table
        List<Cart> purchaseList = cartModel.GetOrdersInCart(userId);
        CreateShopTable(purchaseList, out subTotal); //will be used to generate html for each cart object found in the purchase list

        //out paramter allows the method to return more than one value


        //Add totals to webpage
        double vat = subTotal * 0.21;  //vat : value added taxes
        double totalAmount = subTotal + 15 + vat; //15 is the shipping cost

        litTotal.Text = "$ " + subTotal;
        litVat.Text = "$ " + vat;
        litTotalAmount.Text = "$ " + totalAmount;

        //Add paypal button to checkout
        //string paypal = GeneratePaypalButton(subTotal);
        //litPaypal.Text = paypal;
    }

    //out double subTotal: allow the mehod to return more than one value
    private void CreateShopTable(IEnumerable<Cart> carts, out double subTotal)
    {
        subTotal = new double();
        ProductModel model = new ProductModel();

        foreach (Cart cart in carts)
        {
            //Create HTML elements and fill values with database data
            Product product = model.GetProduct(cart.ProductID);

            //Create the image button
            ImageButton btnImage = new ImageButton
            {
                ImageUrl = string.Format("~/Images/Products/{0}", product.Image),
                PostBackUrl = string.Format("~/Pages/Product.aspx?id={0}", product.Id)
            };

            //Create the delete link
            /*
            when the use clicks on the delete link, the user will be directed back to this page
            and if a product id is found in the url, this means a product needs to be deleted 
            from the shopping cart
            */
            LinkButton lnkDelete = new LinkButton
            {
                PostBackUrl = string.Format("~/Pages/ShoppingCart.aspx?productId={0}", cart.id),
                Text = "Delete Item",
                //ID is needed when working with dynamically generated controlls
                //will be needed to access this control later in the code 
                ID = "del" + cart.id,
            };

            //Add onClick event
            lnkDelete.Click += Delete_Item;

            //Create the quantity dropdown list
            //Fill amount list with numbers 1-20
            int[] amount = Enumerable.Range(1, 20).ToArray();
            DropDownList ddlAmount = new DropDownList
            {
                DataSource = amount,
                AppendDataBoundItems = true,
                AutoPostBack = true, //when a value is selected in the drop down list, the page will refresh itself
                                     //and calculate the amount depending on what quantity value is selected

                //set the id of the dropdown list to the id of the current cart
                ID = cart.id.ToString()
            };
            ddlAmount.DataBind(); //is needed when dealing with dynamically generated data sources
            ddlAmount.SelectedValue = cart.Amount.ToString();
            ddlAmount.SelectedIndexChanged += ddlAmount_SelectedIndexChanged; //when dyncamically generating events, no need to add parantheses after the method name

            //Create table to hold shopping cart details (2 rows and 6 columns)
            Table table = new Table { CssClass = "CartTable" };
            TableRow row1 = new TableRow();
            TableRow row2 = new TableRow();

            //create 6 cells for a row
            TableCell cell1_1 = new TableCell { RowSpan = 2, Width = 50 };
            TableCell cell1_2 = new TableCell
            {
                Text = string.Format("<h4>{0}</h4><br />{1}<br/>In Stock",
                product.Name, "Item No:" + product.Id),
                HorizontalAlign = HorizontalAlign.Left,
                Width = 350,
            };
            TableCell cell1_3 = new TableCell { Text = "Unit Price<hr/>" };
            TableCell cell1_4 = new TableCell { Text = "Quantity<hr/>" };
            TableCell cell1_5 = new TableCell { Text = "Item Total<hr/>" };
            TableCell cell1_6 = new TableCell();

            TableCell cell2_1 = new TableCell();
            TableCell cell2_2 = new TableCell { Text = "$ " + product.Price };
            TableCell cell2_3 = new TableCell();
            TableCell cell2_4 = new TableCell { Text = "$ " + Math.Round((cart.Amount * (double)product.Price), 2) };
            TableCell cell2_5 = new TableCell();



            //Set custom controls
            cell1_1.Controls.Add(btnImage);
            cell1_6.Controls.Add(lnkDelete);
            cell2_3.Controls.Add(ddlAmount);

            //Add rows & cells to table
            row1.Cells.Add(cell1_1);
            row1.Cells.Add(cell1_2);
            row1.Cells.Add(cell1_3);
            row1.Cells.Add(cell1_4);
            row1.Cells.Add(cell1_5);
            row1.Cells.Add(cell1_6);

            row2.Cells.Add(cell2_1);
            row2.Cells.Add(cell2_2);
            row2.Cells.Add(cell2_3);
            row2.Cells.Add(cell2_4);
            row2.Cells.Add(cell2_5);
            table.Rows.Add(row1);
            table.Rows.Add(row2);
            pnlShoppingCart.Controls.Add(table);

            //Add total of current purchased item to subtotal
            //no need to use a return statement to use this value outside the current method, because of the out keyword
            subTotal += (int)(cart.Amount * product.Price);
        }

        //Add selected objects to Session
        //Add current user's shopping cart to user specific session value
        //session variable is specific for the current user, contains the user's shopping cart
        //will be used to determine if the products in the cart have already been paid for or not
        Session[User.Identity.GetUserId()] = carts;
    }

    private string GeneratePaypalButton(double subTotal)
    {
        //Set Paypal parameters
        string paypal = string.Format(
            @"<script async='async' src='https://www.paypalobjects.com/js/external/paypal-button.min.js?merchant=garageseller@gmail.com' 
                data-button='buynow' 
                data-name='Garage Purchases' 
                data-quantity=1
                data-amount='{0}' 
                data-tax='{1}'
                data-shipping='15'
                data-callback='http://localhost:50992/Pages/Success.aspx'
                data-sendback='http://localhost:50992/Pages/Success.aspx'
                data-env='sandbox'>
             </script>", subTotal, (subTotal * 0.21));

        return paypal;
    }
}