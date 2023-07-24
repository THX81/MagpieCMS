using System;
using System.Web.Security;
using Castle.MonoRail.ActiveRecordScaffold.Helpers;
using Castle.MonoRail.Framework;
using ExclusiveReality.Helpers;

namespace ExclusiveReality.Controllers
{
    [Layout("admin_login"), Rescue("generalerror")]
    [Helper(typeof (NexusARFormHelper))]
    [Helper(typeof (PresentationHelper))]
    public class LoginController : SmartDispatcherController
    {
        public void Index()
        {
        }

        public void LogIn(String username, String password, bool rememberme, string ReturnUrl)
        {
            // We authenticate against the users defined on the web.config.

            //	<credentials passwordFormat="Clear">
            //		<user name="admin" password="admin" />
            //		<user name="user" password="user" />
            //	</credentials>

            if (FormsAuthentication.Authenticate(username, password))
            {
                CancelView();

                FormsAuthentication.RedirectFromLoginPage(username, rememberme, Context.ApplicationPath);

                //				The RedirectFromLoginPage is roughly equivalent to 
                //
                //				FormsAuthentication.SetAuthCookie(username, rememberme, Context.ApplicationPath);
                //				
                //				if (ReturnUrl != null)
                //				{
                //					Redirect(ReturnUrl);
                //				}
                //				else
                //				{
                //					Redirect("home", "index");
                //				}

                return;
            }

            // If we got here then something is wrong with the supplied username/password

            Flash["error"] = "Invalid user name or password. Try again.";
            RedirectToAction("Index", "ReturnUrl=" + ReturnUrl);
        }

        public void LogOut()
        {
            if (Context.UnderlyingContext.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
        }
    }
}