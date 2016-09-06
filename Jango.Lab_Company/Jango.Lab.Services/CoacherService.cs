using Jango.Lab.Models;
using Jango.Lab.Models.Query;
using Jango.Lab.Repositories;
using Jango.Lab.Repositories.Lab;
using Jango.Lib.CastleWindsor.MVC.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Services
{
    public class CoacherService : ICoacherService
    {
        private readonly ICoacherRep _coacherRep;
        private readonly ICourseCoacherRep _courseCoacherRep;
        private readonly ICourseInfoRep _courseRep;
        private ILabUow _uow;
        public CoacherService(ICoacherRep coacherRep, ICourseCoacherRep courseCoacherRep, ICourseInfoRep courseRep, ILabUow uow)
        {
            _coacherRep = coacherRep;
            _courseCoacherRep = courseCoacherRep;
            _courseRep = courseRep;
            _uow = uow;
        }
        public void Delte(long id)
        {
            if (id > 0)
            {
                var item = GetById(id);
                _coacherRep.Delete(item);
            }
        }

        public IPagedList<Coacher> GetAllList(CoacherQuery query)
        {
            return _coacherRep.GetAllList().AsPagedList(query);
        }

        public Coacher GetById(long id)
        {
            if (id == 0)
            {
                return new Coacher();
            }
            var item = _coacherRep.GetById(id);
            var courseCoachers = _courseCoacherRep.FindBy(x => x.CoacherID == id);
            if (courseCoachers.Any())
            {
                var now = DateTime.Now;
                var courses = _courseRep.GetMany(x => x.CourseBeginTime <= now && x.CourseEndTime >= now);
                var cos = from c in courses
                          join b in courseCoachers
                          on c.ID equals b.CourseID
                          select c;
                item.Courses = cos.ToList();
            }
            return item;

        }

        public void Save(Coacher model)
        {
            if (model.ID == 0)
            {
                model.CreatedAt = DateTime.Now;
                model.CreatedUser = "system";
                _coacherRep.Add(model);
            }
            else
            {
                _coacherRep.Update(model);
            }
            _uow.Commit();
        }
    }

    public interface ICoacherService
    {
        IPagedList<Coacher> GetAllList(CoacherQuery query);

        Coacher GetById(long id);

        void Save(Coacher model);

        void Delte(long id);

    }
}
