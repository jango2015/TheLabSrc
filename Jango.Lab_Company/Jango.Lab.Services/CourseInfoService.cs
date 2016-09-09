using Jango.Lab.Models;
using Jango.Lab.Repositories;
using Jango.Lab.Repositories.Lab;
using Jango.Lab.ViewModels.Query;
using Jango.Lib.CastleWindsor.MVC.Extensions;
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
        private readonly ICourseCoacherRep _courseCoacherRep;
        private readonly ILabUow _uow;
        public CourseInfoService(
            ICourseCategoryRep courseCategoryRep,
            ICourseInfoRep courseInfoRep,
            ICourseCoacherRep courseCoacherRep,
            ILabUow uow
            )
        {
            _courseCategoryRep = courseCategoryRep;
            _courseInfoRep = courseInfoRep;
            _courseCoacherRep = courseCoacherRep;
            _uow = uow;
        }
        public IPagedList<CourseCategory> GetCourseCategoryList(CourseCategoryQuery query)
        {
            return _courseCategoryRep.GetAllList().AsPagedList(query);
        }

        public IPagedList<CourseInfo> GetCourseList(CourseQuery query)
        {
            if (query.SearchDate.HasValue)
            {
                var searchDate = Convert.ToDateTime(query.SearchDate.Value.Date.ToString());
                var lsearchDate = searchDate.AddDays(1).AddSeconds(-1);
                return _courseInfoRep.GetMany(x => (x.CourseBeginTime >= searchDate && x.CourseBeginTime <= lsearchDate)).AsPagedList(query);
            }
            return _courseInfoRep.GetAllList().AsPagedList(query);
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

        public CourseInfo GetCourseById(long id)
        {
            if (id == 0) return new CourseInfo() { m_Coacher = new Coacher(), m_CourseCategory = new CourseCategory() };
            var item = _courseInfoRep.GetById(id);
            var courseCoacher = _courseCoacherRep.FindBy(x => x.CourseID == id).FirstOrDefault();
            item.CoacherID = courseCoacher == null ? 0 : courseCoacher.CoacherID;
            item.m_CourseCategory = _courseCategoryRep.GetById(item.m_CourseCategoryId);
            return item;
            //var item = _courseInfoRep.GetByIdIncludeEntitys<CourseCategory>(id, x => x.m_CourseCategory);
            //return item;
        }

        public void SaveCourse(CourseInfo model)
        {

            model.m_CourseCategory = _courseCategoryRep.GetById(model.m_CourseCategoryId);
            var courseCoacher = _courseCoacherRep.FindBy(x => x.CourseID == model.ID)?.FirstOrDefault();

            if (model.ID == 0)
            {
                _courseInfoRep.Add(model);
            }
            else
            {
                var item = _courseInfoRep.GetById(model.ID);
                item.Title = model.Title;
                item.IntegralUse = model.IntegralUse;
                item.BalanceUse = model.BalanceUse;
                item.CourseType = model.CourseType;
                item.CoacherID = model.CoacherID;
                //item.m_CourseCategory = model.m_CourseCategory;
                item.CourseBeginTime = model.CourseBeginTime;
                item.CourseEndTime = model.CourseEndTime;
                item.m_CourseCategoryId = model.m_CourseCategoryId;
                item.Desc = model.Desc;
                _courseInfoRep.Update(item);
                //_courseInfoRep

            }
            if (null == courseCoacher)
            {
                courseCoacher = new CourseCoacher();
                courseCoacher.CourseID = model.ID;
                courseCoacher.CoacherID = model.CoacherID;
                _courseCoacherRep.Add(courseCoacher);
            }
            else
            {
                courseCoacher.CoacherID = model.CoacherID;
                _courseCoacherRep.Update(courseCoacher);
            }

            _uow.Commit();
        }
    }

    public interface ICourseInfoService
    {
        IPagedList<CourseCategory> GetCourseCategoryList(CourseCategoryQuery query);

        CourseCategory GetById(long id);

        void Save(CourseCategory model);

        CourseInfo GetCourseById(long id);

        void SaveCourse(CourseInfo model);
        IPagedList<CourseInfo> GetCourseList(CourseQuery query);
    }
}
