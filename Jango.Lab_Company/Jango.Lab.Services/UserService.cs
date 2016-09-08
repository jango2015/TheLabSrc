using Jango.Lab.Models;
using Jango.Lab.Models.Exceptions;
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
    public class UserService : IUserService
    {
        #region ctor
        private readonly IUserRep _userRep;
        private readonly ILabUow _uow;
        public UserService(IUserRep userRep
            , ILabUow uow
            )
        {
            _userRep = userRep;
            _uow = uow;
        }
        #endregion

        #region bg
        public User GetById(long id)
        {
            if (id == 0) return new User();
            return _userRep.GetById(id);
        }

        public IPagedList<User> GetUserList(UserQuery query)
        {
            var users = _userRep.GetAllList().AsPagedList(query);
            return users;
        }

        public void Save(User model)
        {
            if (string.IsNullOrEmpty(model.Mobile))
            {
                throw new NullEntityException("mobile is null");
            }
            model.ModifiedAt = DateTime.Now;
            if (model.ID == 0)
            {
                model.CreatedAt = DateTime.Now;
                _userRep.Add(model);
            }
            else
            {
                _userRep.Update(model);
            }
            _uow.Commit();
        }
        #endregion

        #region api


        public User GetByMobile(string mobile)
        {
            if (string.IsNullOrEmpty(mobile)) return new User();
            return _userRep.FindBy(x => x.Mobile == mobile).FirstOrDefault();
        }

        public User GetByOpenId(string openid)
        {
            if (string.IsNullOrEmpty(openid)) return new User();
            return _userRep.FindBy(x => x.OpenID == openid).FirstOrDefault();
        }

        #endregion

    }

    public interface IUserService
    {
        IPagedList<User> GetUserList(UserQuery query);

        User GetById(long id);

        void Save(User model);


        /*
         * 
         * api
         * 
         */
        User GetByMobile(string mobile);
        User GetByOpenId(string openid);
    }
}
