using Jango.Lab.Models;
using Jango.Lab.Repositories.Lab;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Repositories
{
    public class CoacherRep : BaseRepository<Coacher>, ICoacherRep
    {
        
    }
    public interface ICoacherRep : IBaseRepository<Coacher> { }

    public class CourseCoacherRep : BaseRepository<CourseCoacher>, ICourseCoacherRep
    {

    }
    public interface ICourseCoacherRep : IBaseRepository<CourseCoacher> { }
}
