using iRefundUmbraco7.Custom.Data.Model;
using iRefundUmbraco7.Custom.Data.Repository;
using iRefundUmbraco7.Custom.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Umbraco.Web.Mvc;

namespace iRefundUmbraco7.Custom.SurfaceControllers
{
    public class ApplicationFormController : SurfaceController
    {
        public ActionResult Index()
        {
            return Content("hello");
        }

        [HttpPost]
        public async Task<ActionResult> Submit(RefundApplication refundApplication)
        {
            var home = Umbraco.TypedContentAtRoot().First();
            var email = home.GetProperty("mailTo").Value.ToString();
            refundApplication.ApplicationDate = SetDateForMongo(DateTime.Now);
            var refundApplicationRepository = new RefundApplicationRepository();
            await refundApplicationRepository.CreateSync(refundApplication);

            var mailService = new MailService();
            mailService.Send(email, Server.MapPath("~/RegistrationNotification.html"), "", refundApplication);

            return Json(new { success = true, responseText = "Added." }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> List()
        {
            var home = Umbraco.TypedContentAtRoot().First();

            var refundApplicationRepository = new RefundApplicationRepository();
            var refundApplication = await refundApplicationRepository.GetAll();
            //return this.Json(refundApplication);
            var serializer = new JavaScriptSerializer();
            var result = serializer.Serialize(refundApplication);
            //return Json(result, JsonRequestBehavior.AllowGet);
            return View(refundApplication);
        }

        public static DateTime SetDateForMongo(DateTime datetime)
        {
            return DateTime.SpecifyKind(datetime, DateTimeKind.Utc);
        }
    }
}
