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
        public RegisterController(IUserService userSrv) : base(userSrv)
        {
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string mobile, string code)
        {

            Code = Convert.ToBase64String(Encoding.UTF8.GetBytes(mobile));
            return View("");
        }
    }
}