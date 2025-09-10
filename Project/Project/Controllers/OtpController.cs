using System.Web.Mvc;
using Project.Repository;
using Project.Services;

namespace Project.Controllers
{
    public class OtpController : Controller
    {
        private readonly OtpService otpService = new OtpService(new OtpRepositoryImpl());
        public ActionResult RequestOtp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RequestOtp(string email)
        {
            int otp = otpService.GenerateOtp(email);
            ViewBag.Message = "OTP sent to your registered email.";
            return RedirectToAction("VerifyOtp");
        }
        public ActionResult VerifyOtp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult VerifyOtp(string email, int otp)
        {
            bool valid = otpService.VerifyOtp(email, otp);
            ViewBag.Message = valid ? "OTP Verified." : "Invalid OTP";
            return View();
        }
    }
}