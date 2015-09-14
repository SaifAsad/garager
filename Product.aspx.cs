﻿using Microsoft.AspNet.Identity;
using System;
using System.Linq;

public partial class Pages_Product : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FillPage();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
        {
            //get the id of the current logged in user
            string clientId = Context.User.Identity.GetUserId();

            //implement a check to make sure only logged in user can order a product 
            if (clientId != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                int amount = Convert.ToInt32(ddlAmount.SelectedValue);

                Cart cart = new Cart
                {
                    Amount = amount,
                    ClientID = clientId,
                    DatePurchased = DateTime.Now,
                    IsInCart = true,
                    ProductID = id
                };

                CartModel cartModel = new CartModel();
                lblResult.Text = cartModel.InsertCart(cart);
                Response.Redirect("~/Index.aspx");
            }
            else
            {
                lblResult.Text = "Please log in to order items";
            }
        }
    }

    private void FillPage()
    {
        //Get selected product data
        if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            ProductModel model = new ProductModel();
            Product product = model.GetProduct(id);

            //Fill page with data
            lblTitle.Text = product.Name;
            lblDescription.Text = product.Description;
            lblPrice.Text = "Price per unit:<br/>$ " + product.Price;
            imgProduct.ImageUrl = "~/Images/Products/" + product.Image;
            lblItemNr.Text = product.Id.ToString();
            
            //Fill amount list with numbers 1-20
            int[] amount = Enumerable.Range(1, 20).ToArray();
            ddlAmount.DataSource = amount;
            ddlAmount.AppendDataBoundItems = true;
            ddlAmount.DataBind();
        }
    }
    
}