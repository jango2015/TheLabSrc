using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jango.Lab.Wechat.Controllers
{
    public class MemberController : BaseController
    {
        public ActionResult Index(string code)
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
    }
}