using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Jango.Lab.Models;
using Jango.Lab.Services;

namespace Jango.Lab.Wechat.Controllers
{
    public class RegisterController : BaseController
    {
        private readonly IMessageService _msgSrv = LoadServices._MessageService;
        private readonly IUserService _userService = LoadServices._UserService;
        [HttpGet]
        public ActionResult Index(string code, string state)
        {
            var info = WxService.GetOpenId(code);
            ViewBag.openid = info;
            return View("Register");
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
        public ActionResult Register(string mobile, string code, string openid)
        {
            if (string.IsNullOrEmpty(mobile)) return Json(new { success = false, msg = "手机号不能为空" }, JsonRequestBehavior.AllowGet);
            //Code = Convert.ToBase64String(Encoding.UTF8.GetBytes(mobile));
            if (_msgSrv.ValidateMsg(mobile, code))
            {
                if (!_userService.IsExist(mobile))
                {
                    _userService.Save(new User() { Mobile = mobile, OpenID = openid });
                }
                var user = _userService.GetByMobile(mobile);
                if (user != null && user.ID > 0)
                {
                    Code = user.Code;
                }
                if (string.IsNullOrEmpty(user.OpenID))
                {
                    user.OpenID = openid;
                    _userService.Save(user);
                }
                return Json(new { success = true, code = Code }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, msg = "验证码错误" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}