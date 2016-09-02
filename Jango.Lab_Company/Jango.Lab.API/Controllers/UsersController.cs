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
        public IEnumerable<User> Get()
        {
            return _userSrv.GetUserList();
        }

        // GET: api/Users/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }
    }
}
