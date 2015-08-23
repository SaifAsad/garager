using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

public partial class Pages_Account_Register : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        // Default UserStore constructor uses the default connection string named: DefaultConnection
        var userStore = new UserStore<IdentityUser>();

        //Set ConnectionString to GarageConnectionString
        userStore.Context.Database.Connection.ConnectionString =
            System.Configuration.ConfigurationManager.ConnectionStrings["GarageDBConnectionString"].ConnectionString;
        var manager = new UserManager<IdentityUser>(userStore);

        //Create new user and try to store in DB.
        var user = new IdentityUser { UserName = txtUserName.Text };

        if (txtPassword.Text == txtConfirmPassword.Text)
        {
            try
            {
                //Create user object
                //Database will be created / expanded automatically
                IdentityResult result = manager.Create(user, txtPassword.Text);

                if (result.Succeeded)
                {
                    UserInformation info = new UserInformation()
                    {
                        Address = txtAddress.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        PostalCode = Convert.ToInt32(txtPostalCode.Text),
                        //retreive the dynamically generateed key from the current logged in user
                        GUID = user.Id
                    };

                    //send the user information object to the database
                    UserInfoModel model = new UserInfoModel();
                    model.InsertUserDetail(info);


                     //Store user in DB
                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

                    //set to login new user by Cookie
                    var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                    //If succeedeed, log in the new user and set a cookie and redirect to homepage
                    authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                    Response.Redirect("~/Index.aspx");
                }
                else
                {
                    litStatusMessage.Text = result.Errors.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                litStatusMessage.Text = ex.ToString();
            }
        }
        else
        {
            litStatusMessage.Text = "Passwords must match!";
        }
    }
}