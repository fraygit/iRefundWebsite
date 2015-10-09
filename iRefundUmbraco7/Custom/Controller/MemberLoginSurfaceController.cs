using iRefundUmbraco7.Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Web.Mvc;

namespace Controllers
{
    public class MemberLoginSurfaceController : Umbraco.Web.Mvc.SurfaceController
    {
        [HttpGet]
        [ActionName("MemberLogin")]
        public ActionResult MemberLoginGet()
        {
            return PartialView("MemberLogin", new MemberLoginModel());
        }

        // The MemberLogout Action signs out the user and redirects to the site home page:

        [HttpGet]
        public ActionResult MemberLogout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        // The MemberLoginPost Action checks the entered credentials using the standard Asp Net membership provider and redirects the user to the same page. Either as logged in, or with a message set in the TempData dictionary:

        [HttpPost]
        [ActionName("MemberLogin")]
        public ActionResult MemberLoginPost(MemberLoginModel model)
        {
            if (Membership.ValidateUser(model.Username, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                return RedirectToCurrentUmbracoPage();

            }
            else
            {
                TempData["Status"] = "Invalid username or password";
                return RedirectToCurrentUmbracoPage();
            }
        }

        public ActionResult Test()
        {
            //Membership.CreateUser(username, password);
            //return Json(new { success = true, responseText = "Added." }, JsonRequestBehavior.AllowGet);
            return PartialView("Test");
        } 

        [HttpPost]
        [ActionName("Register")]
        public ActionResult Register(string username, string password)
        {
            Membership.CreateUser(username, password);
            return Json(new { success = true, responseText = "Added." }, JsonRequestBehavior.AllowGet);
        }    
    }
}
