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
        private IUserService _userSrv;
        public MemberController(IUserService userSrv) : base(userSrv)
        {
            _userSrv = userSrv;
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
        public ActionResult GetUserConsigneeInfo(string code)
        {
            try
            {
                if (null == MemberInfo || string.IsNullOrEmpty(Code))
                {
                    Code = code;
                }
                base.LoadMemberInfo();
                var memberInfo = MemberInfo;
                var consignee = _userSrv.GetConsignneInfoByUid(_user.ID);
                if (consignee != null && consignee.ID > 0)
                {
                    memberInfo.Name = consignee.ConsigneeUserName;
                    memberInfo.Mobile = consignee.ConsigneeUserMobile;
                    var address = consignee.ConsigneeUserAddress.Split('|');
                    memberInfo.Province = address[0];
                    memberInfo.City = address[1];
                    memberInfo.District = address[2];
                    memberInfo.Address = address[3];
                }
                return Json(new { success = true, data = memberInfo }, JsonRequestBehavior.AllowGet);
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
            //LoadMemberInfo();
            return View();
        }

        public ActionResult Save(MemberVM model)
        {
            if (model == null) throw new ArgumentNullException("");
            try
            {
                _userSrv.Save(model);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}