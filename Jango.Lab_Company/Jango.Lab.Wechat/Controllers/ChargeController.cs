using Jango.Lab.Models;
using Jango.Lab.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jango.Lab.Wechat.Controllers
{
    public class ChargeController : BaseController
    {
        private readonly IChargeService _chargeSrv;
        public ChargeController(IChargeService chargeSrv, IUserService userSrv) : base(userSrv)
        {
            _chargeSrv = chargeSrv;
        }

        public ActionResult Index(string code)
        {
            Code = code;
            ViewBag.Code = Code;
            return View();
        }

        public ActionResult GetChargeCards()
        {
            try
            {
                var items = _chargeSrv.GetValidChargeCards();
                return Json(new { success = true, data = items }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Record(string code)
        {
            Code = code;
            ViewBag.Code = Code;
            return View();
        }
        public ActionResult Records(string code)
        {
            try
            {
                Code = code;
                ViewBag.Code = Code;
                LoadMemberInfo();
                var items = _chargeSrv.GetRecordByUserId(_user.ID);
                return Json(new { success = true, data = items }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Charge(string code, long cid = 0)
        {
            try
            {
                Code = code;
                ViewBag.Code = Code;
                LoadMemberInfo();
                var chargeRecord = new ChargeRecord() { UserID = _user.ID, CardID = cid };
                _chargeSrv.Charge(chargeRecord);
                return Json(new { success = true, data = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}