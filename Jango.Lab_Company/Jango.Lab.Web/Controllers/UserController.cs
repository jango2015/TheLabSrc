using Jango.Lab.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jango.Lab.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userSrv;
        public UserController(IUserService userSrv)
        {
            _userSrv = userSrv;
        }

        // GET: User
        public ActionResult Index()
        {
            var users = _userSrv.GetUserList();
            return View(users);
        }
    }
}