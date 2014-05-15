using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using StudentVibes.Models;
using StudentVibes.Logic;
using StudentVibes.Account;

namespace StudentVibes
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        TextBox ExUserName;
        TextBox ExPassword;
        CheckBox RememberMe;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (HttpContext.Current.User.IsInRole("Administrator")) { adminLink.Visible = true; }

        }

        protected override void OnLoad(EventArgs e)
        {
            ExUserName = FindControl("LogV").FindControl("ExUserName") as TextBox;
            ExPassword = FindControl("LogV").FindControl("ExPassword") as TextBox;
            RememberMe = FindControl("LogV").FindControl("RememberMe") as CheckBox;
            base.OnLoad(e);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            using (ShoppingCartActions usersShoppingCart = new
            ShoppingCartActions())
            {
                string cartStr = string.Format("Cart ({0})",
usersShoppingCart.GetCount());
                cartCount.InnerText = cartStr;
            }
        }



        public IQueryable<Category> GetCategories()
        {
            var _db = new StudentVibes.Models.ProductContext();
            IQueryable<Category> query = _db.Categories;
            return query;
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut();
            
        }


        protected void ExLogin(object sender, EventArgs e)
        {
            StudentVibes.Account.Login login = new StudentVibes.Account.Login();

            //http://odetocode.com/Articles/450.aspx read for issue on name
            if (!login.ExternalLogIn(ExUserName.Text, ExPassword.Text, RememberMe.Checked))
            {
                //Login Unsucesful, Redirect to Login Page and autocomplete Username Field
                Response.Redirect("~/Account/Login?Err=1&u=" + ExUserName.Text);
               // login.ExPageLoad(ExUserName.Text);
               

            }
            else
            {
                // This happens when login is sucessful
                Response.Redirect(Request.RawUrl);
              

            }
           


        }

        protected void ExPassword_TextChanged(object sender, EventArgs e)
        {

        }




    }

}