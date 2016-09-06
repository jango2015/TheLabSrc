﻿using Jango.Lab.Models;
using Jango.Lab.Models.Query;
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
        public IEnumerable<User> Get(UserQuery query)
        {
            return _userSrv.GetUserList(query);
        }


        public string Get(int id)
        {
            return "value";
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