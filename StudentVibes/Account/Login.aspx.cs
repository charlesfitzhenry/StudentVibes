using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using StudentVibes.Models;

namespace StudentVibes.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
      string err = Request.QueryString["Err"];
            string user = Request.QueryString["u"];

            
            RegisterHyperLink.NavigateUrl = "Register";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }

            if (err != null)
            {
                UserName.Text = user;
                //Trigger Alert Message
            }
        }

        public void ExPageLoad(String uName)
        {
            //Response.Redirect("~/Account/Login");
            UserName.Text = uName;
            



        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                var manager = new UserManager();
                ApplicationUser user = manager.Find(UserName.Text, Password.Text);
                if (user != null)
                {
                    IdentityHelper.SignIn(manager, user, RememberMe.Checked);
                    StudentVibes.Logic.ShoppingCartActions usersShoppingCart = new StudentVibes.Logic.ShoppingCartActions();
                    String cartId = usersShoppingCart.GetCartId(); usersShoppingCart.MigrateCart(cartId, UserName.Text); 
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    FailureText.Text = "Invalid username or password.";
                    ErrorMessage.Visible = true;
                }
            }
        }

        public bool ExternalLogIn(String uN, String pass, bool rememberMe)
        {

            /*if (IsValid)
            {*/
                // Validate the user password
                var manager = new UserManager();
                ApplicationUser user = manager.Find(uN, pass);
                if (user != null)
                {
                    IdentityHelper.SignIn(manager, user, rememberMe);
                    StudentVibes.Logic.ShoppingCartActions usersShoppingCart = new StudentVibes.Logic.ShoppingCartActions();
                    String cartId = usersShoppingCart.GetCartId(); usersShoppingCart.MigrateCart(cartId, uN);
                    //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    //Response.Redirect(Request.RawUrl);
                    return true;
                }
                else
                {
                    return false;


                }

           /* }
            return "NotValid";*/
        }
    }
}