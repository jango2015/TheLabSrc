using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Jango.Lab.Services;

namespace Jango.Lab.Wechat.Controllers
{
    public class RegisterController : BaseController
    {
        private readonly IMessageService _msgSrv;
        public RegisterController(IUserService userSrv, IMessageService msgSrv) : base(userSrv)
        {
            _msgSrv = msgSrv;
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult SendCode(string mobile)
        {
            try
            {
                if (string.IsNullOrEmpty(mobile)) return Json(new { success = false, msg = "手机号不能为空" }, JsonRequestBehavior.AllowGet);
                _msgSrv.SendSms(mobile);
                return Json(new { success = true, msg = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult Register(string mobile, string code)
        {
            if (string.IsNullOrEmpty(mobile)) return Json(new { success = false, msg = "手机号不能为空" }, JsonRequestBehavior.AllowGet);
            Code = Convert.ToBase64String(Encoding.UTF8.GetBytes(mobile));
            return View("");
        }
    }
}