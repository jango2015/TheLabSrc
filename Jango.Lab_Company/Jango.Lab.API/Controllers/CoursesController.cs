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
    public class CoursesController : ApiController
    {
        private readonly ICourseInfoService _courseSrv;
        public CoursesController(ICourseInfoService courseSrv)
        {
            _courseSrv = courseSrv;
        }
        public IEnumerable<CourseInfo> Get()
        {
            var items = _courseSrv.GetCourseList(new Models.Query.CourseQuery() { PageSize = 20 });
            return items;
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
