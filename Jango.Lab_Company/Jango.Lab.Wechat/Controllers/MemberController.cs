using AutoMapper;
using Jango.Lab.Services;
using Jango.Lab.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jango.Lab.Wechat.Controllers
{
    public class MemberController : BaseController
    {
        public MemberController(IUserService userSrv) : base(userSrv)
        {
        }

        public ActionResult Index(string code)
        {
            Code = code;
            ViewBag.Code = Code;
            return View();
        }

        public ActionResult GetUserInfo(string code)
        {
            try
            {
                if (null == MemberInfo || string.IsNullOrEmpty(Code))
                {
                    Code = code;
                }
                base.LoadMemberInfo();
                return Json(new { success = true, data = MemberInfo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Edit(string code)
        {
            Code = code;
            ViewBag.Code = Code;
            return View();
        }


    }
}