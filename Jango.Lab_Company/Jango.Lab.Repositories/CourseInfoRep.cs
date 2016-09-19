using Jango.Lab.Models;
using Jango.Lab.Repositories.Lab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Jango.Lab.Repositories
{
    public class CourseInfoRep : BaseRepository<CourseInfo>, ICourseInfoRep
    {
    }
    public class CourseCategoryRep : BaseRepository<CourseCategory>, ICourseCategoryRep
    {
    }
    public interface ICourseInfoRep : IBaseRepository<CourseInfo> { }

    public interface ICourseCategoryRep : IBaseRepository<CourseCategory> { }
}
