using Jango.Lab.Models;
using Jango.Lab.Repositories;
using Jango.Lab.Repositories.Lab;
using Jango.Lab.ViewModels;
using Jango.Lab.ViewModels.Query;
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
                model.Code = Guid.NewGuid().ToString().Replace("-", ""); /*Convert.ToBase64String(Encoding.UTF8.GetBytes(model.Mobile))*/;
                model.CreatedAt = DateTime.Now;
                _userRep.Add(model);
            }
            else
            {
                var item = _userRep.GetById(model.ID);
                item.Name = model.Name;
                item.Mobile = model.Mobile;
                item.Email = model.Email;
                item.Level = model.Level;
                item.Birthday = model.Birthday;
                _userRep.Update(item);
            }
            _uow.Commit();
        }
        #endregion

        #region api


        public User GetByMobile(string mobile)
        {
            if (string.IsNullOrEmpty(mobile)) return new User();
            return _userRep.Get(x => x.Mobile == mobile);
        }

        public User GetByOpenId(string openid)
        {
            if (string.IsNullOrEmpty(openid)) return new User();
            return _userRep.Get(x => x.OpenID == openid);
        }

        public User GetByCode(string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code)) return new User();
                return _userRep.Get(x => x.Code == code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
         * app
         * 
         */
        User GetByMobile(string mobile);
        User GetByOpenId(string openid);

        User GetByCode(string code);
    }
}
