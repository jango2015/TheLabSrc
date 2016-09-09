using Jango.Lab.Models;
using Jango.Lab.Repositories;
using Jango.Lab.Repositories.Lab;
using Jango.Lab.ViewModels.Query;
using Jango.Lib.CastleWindsor.MVC.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Services
{
    public class CourseReserveService : ICourseReserveService
    {
        private readonly ICourseReserveRecordRep _courseReserveRep;
        private readonly ILabUow _uow;
        public CourseReserveService(ICourseReserveRecordRep courseReserveRep, ILabUow uow)
        {
            _courseReserveRep = courseReserveRep;
            _uow = uow;
        }
        public IPagedList<CourseReserveRecord> GetCoourseReservesPageList(CourseReserveQuery query)
        {
            return _courseReserveRep.GetAll().AsPagedList(query);
        }

        public IQueryable<CourseReserveRecord> GetCourseReserveList()
        {
            return _courseReserveRep.GetAll();
        }

        public IQueryable<CourseReserveRecord> GetCourseReserveListByUserId(long userid)
        {
            if (userid == 0) return null;
            var items = _courseReserveRep.FindBy(x => x.UserID == userid);
            return items;
        }

        public IQueryable<CourseReserveRecord> GetCourseReserveLists(Expression<Func<CourseReserveRecord, bool>> fuc)
        {
            var items = _courseReserveRep.GetAll().Where(fuc);
            return items;
        }

        public void ReserveCourse(long userId, long courseId)
        {
            ValidCourseHasReserved(userId, courseId);
            var model = new CourseReserveRecord();
            model.CourseID = courseId;
            model.UserID = userId;
            model.ReserveTime = DateTime.Now;
            model.CreatedAt = DateTime.Now;
            model.QRCode = Guid.NewGuid().ToString().Replace("-", "");
            model.Status = EnumCourseReserveStatus.HasReserved;
            model.IsQRCodeUsded = false;
            model.ModifiedAt = DateTime.Now;
            model.ModifiedUser = userId.ToString();
            _courseReserveRep.Add(model);
            _uow.Commit();

        }

        private void ValidCourseHasReserved(long userId, long courseId)
        {
            var m = _courseReserveRep.FindBy(x => x.UserID == userId && x.CourseID == courseId);
            if (m.Any())
            {
                throw new Exception("you hava reserved this course");
            }
        }
    }

    public interface ICourseReserveService
    {

        IPagedList<CourseReserveRecord> GetCoourseReservesPageList(CourseReserveQuery query);
        IQueryable<CourseReserveRecord> GetCourseReserveList();

        IQueryable<CourseReserveRecord> GetCourseReserveListByUserId(long userid);

        IQueryable<CourseReserveRecord> GetCourseReserveLists(Expression<Func<CourseReserveRecord, bool>> @fuc);

        void ReserveCourse(long userId, long courseId);
    }
}
