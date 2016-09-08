using Jango.Lab.Models;

using Jango.Lab.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jango.Lab.API.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService _userSrv;
        public UsersController(IUserService userSrv)
        {
            _userSrv = userSrv;
        }

        [Route("~/api/users/getbymobile")]
        [HttpGet]
        public User GetbyMobile([FromUri]string mobile)
        {
            return _userSrv.GetByMobile(mobile);
        }


        public void Post([FromBody]string value)
        {
        }


        public void Put(int id, [FromBody]string value)
        {
        }


        public void Delete(int id)
        {
        }
    }
}
