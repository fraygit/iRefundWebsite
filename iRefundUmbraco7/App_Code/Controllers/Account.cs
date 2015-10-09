using iRefundUmbraco7.Custom.Data.Model;
using iRefundUmbraco7.Custom.Data.Repository;
using iRefundUmbraco7.Custom.Model;
using iRefundUmbraco7.Custom.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace iRefundUmbraco7.App_Code.Controllers
{
    public class AccountController : Umbraco.Web.Mvc.SurfaceController
    {
        [HttpGet]
        [ActionName("MemberLogin")]
        public ActionResult MemberLoginGet()
        {
            return PartialView("MemberLogin", new MemberLoginModel());
        }

        [HttpGet]
        public ActionResult CheckSession()
        {
            if (Session["IRDNumber"] != null)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new { 
                    success = true, 
                    irdNumber = Session["IRDNumber"],
                    title = Session["Title"],
                    firstName = Session["FirstName"],
                    lastName = Session["LastName"],
                    email = Session["Email"],
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, responseText = "Not authenticated." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { success = true, responseText = "User logged out." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(string email)
        {
            var refundAppRepo = new RefundApplicationRepository();
            var existing = await refundAppRepo.FindByEmail(email);
            if (existing.Count > 0)
            {
                var password = existing.FirstOrDefault().Password;
                var mail = new MailService();
                mail.SendPassword(email, password);
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, responseText = "User already exist." }, JsonRequestBehavior.AllowGet);
            }
        } 


        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            var refundAppRepo = new RefundApplicationRepository();
            var existing = await refundAppRepo.FindIRD(username);
            if (existing.Count > 0)
            {
                Session["IRDNumber"] = username;
                Session["Title"] = existing.FirstOrDefault().Title;
                Session["FirstName"] = existing.FirstOrDefault().FirstName;
                Session["LastName"] = existing.FirstOrDefault().LastName;
                Session["Email"] = existing.FirstOrDefault().Email;
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new
                {
                    success = true,
                    irdNumber = Session["IRDNumber"],
                    title = Session["Title"],
                    firstName = Session["FirstName"],
                    lastName = Session["LastName"],
                    email = Session["Email"],
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, responseText = "User already exist." }, JsonRequestBehavior.AllowGet);
            }
        } 

        [HttpPost]
        public async Task<ActionResult> Register(string username, string password)
        {
            var refundAppRepo = new RefundApplicationRepository();
            var existing = await refundAppRepo.FindIRD(username);
            if (existing.Count == 0)
            {
                var refunApp = new RefundApplication
                {
                    IRDNumber = username,
                    Password = password
                };
                await refundAppRepo.CreateSync(refunApp);
                Session["IRDNumber"] = username;
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new { success = true, responseText = "Added." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, responseText = "User already exist." }, JsonRequestBehavior.AllowGet);
            }
        } 
    }
}
