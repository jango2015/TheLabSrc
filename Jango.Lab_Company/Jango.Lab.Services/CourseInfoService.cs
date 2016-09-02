using Jango.Lab.Models;
using Jango.Lab.Repositories;
using Jango.Lab.Repositories.Lab;
using Jango.Lib.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Services
{
    public class CourseInfoService : ICourseInfoService
    {
        private readonly ICourseInfoRep _courseInfoRep;
        private readonly ICourseCategoryRep _courseCategoryRep;
        private readonly ILabUow _uow;
        public CourseInfoService(
            ICourseCategoryRep courseCategoryRep,
            ICourseInfoRep courseInfoRep,
            ILabUow uow
            )
        {
            _courseCategoryRep = courseCategoryRep;
            _courseInfoRep = courseInfoRep;
            _uow = uow;
        }
        public IEnumerable<CourseCategory> GetCourseCategoryList()
        {
            return _courseCategoryRep.GetAllList();
        }

        public IEnumerable<CourseInfo> GetCourseList()
        {
            return _courseInfoRep.GetAllList();
        }

        public CourseCategory GetById(long id)
        {
            if (id == 0) return new CourseCategory();
            else
            {
                var item = _courseCategoryRep.GetById(id);
                return item;
            }
        }

        public void Save(CourseCategory model)
        {
            model.ModifiedAt = DateTime.Now;
            model.ModifiedUser = "sys";
            if (model.Id == 0)
            {
                model.CreatedAt = DateTime.Now;
                model.Creator = "system";
                _courseCategoryRep.Add(model);
            }
            else
            {
                _courseCategoryRep.Update(model);
            }
            _uow.Commit();
        }
    }

    public interface ICourseInfoService
    {
        IEnumerable<CourseCategory> GetCourseCategoryList();

        CourseCategory GetById(long id);

        void Save(CourseCategory model);


        IEnumerable<CourseInfo> GetCourseList();
    }
}
