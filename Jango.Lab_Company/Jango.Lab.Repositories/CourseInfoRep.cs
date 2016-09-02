using Jango.Lab.Models;
using Jango.Lab.Repositories.Lab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jango.Lib.Repository.Core;

namespace Jango.Lab.Repositories
{
    public class CourseInfoRep : LabBaseRepository<CourseInfo>, ICourseInfoRep
    {
        public CourseInfoRep(ILabDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
    public class CourseCategoryRep : LabBaseRepository<CourseCategory>, ICourseCategoryRep
    {
        public CourseCategoryRep(ILabDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
    public interface ICourseInfoRep : IRepository<CourseInfo>, ILabBaseRepository { }

    public interface ICourseCategoryRep : IRepository<CourseCategory>, ILabBaseRepository { }
}
