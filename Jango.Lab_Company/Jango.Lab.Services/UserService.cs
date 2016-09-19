using Jango.Lab.Models;
using Jango.Lab.Repositories;
using Jango.Lab.Repositories.Lab;
using Jango.Lab.ViewModels;
using Jango.Lab.ViewModels.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;

namespace Jango.Lab.Services
{
    public class UserService : IUserService
    {
        #region ctor
        private readonly IUserRep _userRep = LoadReps._userRep;
        private readonly ILabUow _uow = LoadReps._uow;
        private readonly IUserConsigneeInfoRep _consignneRep = LoadReps._userConsigneeInfoRep;

        #endregion

        #region bg
        public User GetById(long id)
        {
            if (id == 0) return new User();
            return _userRep.GetById(id);
        }

        public IPagedList<User> GetUserList(UserQuery query)
        {
            var users = _userRep.GetAllList().OrderByDescending(x => x.ID).ToPagedList<User>(query.PageNumber, query.PageSize);
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

        public UserConsigneeInfo GetConsignneInfoByCode(string code)
        {
            throw new NotImplementedException();
        }

        public UserConsigneeInfo GetConsignneInfoByUid(long userid)
        {
            if (userid == 0)
            {
                return new UserConsigneeInfo();
            }
            return _consignneRep.FindBy(x => x.UserID == userid).FirstOrDefault();
        }

        public void Save(MemberVM model)
        {
            var userInfo = GetByCode(model.Code);
            if (userInfo == null || userInfo.ID == 0)
            {
                throw new ArgumentNullException("");
            }
            //userInfo.Mobile = model.Mobile;
            //userInfo.Name = model.Name;
            //userInfo.Birthday = model.Birthday;
            //_userRep.Update(userInfo);

            var consigneeInfo = GetConsignneInfoByUid(userInfo.ID);
            if (consigneeInfo == null)
            {
                consigneeInfo = new UserConsigneeInfo();
            }
            consigneeInfo.UserID = userInfo.ID;
            consigneeInfo.ConsigneeUserMobile = model.Mobile;
            consigneeInfo.ConsigneeUserName = model.Name;
            consigneeInfo.ConsigneeUserAddress = string.Format("{0}|{1}|{2}|{3}", model.Province, model.City, model.District, model.Address);
            consigneeInfo.ModifiedAt = DateTime.Now;
            if (consigneeInfo.ID == 0)
            {
                consigneeInfo.CreatedAt = consigneeInfo.ModifiedAt;
                _consignneRep.Add(consigneeInfo);
            }
            else
            {
                _consignneRep.Update(consigneeInfo);
            }
            _uow.Commit();
        }

        #endregion

    }

    public interface IUserService
    {
        IPagedList<User> GetUserList(UserQuery query);

        User GetById(long id);

        void Save(User model);

        void Save(MemberVM model);

        /*
         * 
         * app
         * 
         */
        User GetByMobile(string mobile);
        User GetByOpenId(string openid);

        User GetByCode(string code);

        UserConsigneeInfo GetConsignneInfoByCode(string code);
        UserConsigneeInfo GetConsignneInfoByUid(long userid);
    }
}
