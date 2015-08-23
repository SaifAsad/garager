﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //return the identity of the currently logged in user
        var user = Context.User.Identity;

        //to check if the user is logged in
        if(user.IsAuthenticated)
        {
            //to retrieve the name of the cuurrent user
            litStatus.Text = Context.User.Identity.Name;

            lnkLogin.Visible = false;
            lnkRegister.Visible = false;

            lnkLogut.Visible = true;
            litStatus.Visible = true;


        }
        else
        {
            lnkLogin.Visible = true;
            lnkRegister.Visible = true;

            lnkLogut.Visible = false;
            litStatus.Visible = false;

        }
    }

    protected void lnkLogut_Click(object sender, EventArgs e)
    {
        //integrate OWIN functionaliuty
        var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
        //call the signout method
        authenticationManager.SignOut();

        Response.Redirect("~/Index.aspx");
    }
}
