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
using Jango.Lab.ViewModels.ViewModel;
using Webdiyer.WebControls.Mvc;

namespace Jango.Lab.Services
{
    public class UserService : IUserService
    {
        #region ctor
        private readonly IUserRep _userRep = LoadReps._userRep;
        private readonly ILabUow _uow = LoadReps._uow;
        private readonly IUserConsigneeInfoRep _consignneRep = LoadReps._userConsigneeInfoRep;
        private readonly IUserAccountRep _userAccountRep = LoadReps._userAccountRep;
        #endregion

        #region bg

        public IPagedList<UserAccountVM> GetAccounts(UserAccountQuery query)
        {
            var userAccounts = _userAccountRep.GetAll();//.OrderByDescending(x => x.ID).ToPagedList<UserAccount>(query.PageNumber, query.PageSize)
            var items = (from a in userAccounts
                         join b in (_userRep.GetAll())
                             on a.UserID equals b.ID
                         select new UserAccountVM()
                         {
                             ID = a.ID,
                             UserID = b.ID,
                             AccountType = a.AccountType,
                             Amount = a.Amount,
                             Mobile = b.Mobile,
                             Name = b.Name
                         }).OrderByDescending(x => x.ID).ToPagedList<UserAccountVM>(query.PageNumber, query.PageSize);
            return items;
        }

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
                model.Code = Guid.NewGuid().ToString().Replace("-", ""); /*Convert.ToBase64String(Encoding.UTF8.GetBytes(model.Mobile))*/
                model.CreatedAt = DateTime.Now;
                model.Birthday = DateTime.MinValue;
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
                item.OpenID = model.OpenID;
                _userRep.Update(item);
            }
            _uow.Commit();
        }
        #endregion

        #region api

        public void Save(UserAccountVM model)
        {
            var account = new UserAccount
            {
                ID = model.ID,
                AccountType = model.AccountType,
                UserID = model.UserID,
                ModifiedAt = DateTime.Now
            };
            if (account.ID == 0)
            {
                account.CreatedAt = DateTime.Now;
                _userAccountRep.Add(account);
            }
            else
            {
                var item = _userAccountRep.GetById(model.ID);
                item.AccountType = model.AccountType;
                item.Amount = model.Amount;
                item.UserID = model.UserID;
                _userAccountRep.Update(item);
            }
            _uow.Commit();
        }

        public IQueryable<UserAccount> GetAccountsByUserId(long userId)
        {
            return _userAccountRep.FindBy(x => x.UserID == userId);
        }

        public UserAccountVM GetAccountVmById(long id)
        {
            if (id == 0) return new UserAccountVM();
            var a = _userAccountRep.GetById(id);//.OrderByDescending(x => x.ID).ToPagedList<UserAccount>(query.PageNumber, query.PageSize)
            if (a == null || a.ID == 0) return new UserAccountVM();
            var b = _userRep.GetById(a.UserID);
            var item = new UserAccountVM();
            item.ID = a.ID;
            item.UserID = b.ID;
            item.AccountType = a.AccountType;
            item.Amount = a.Amount;
            item.Mobile = b.Mobile;
            item.Name = b.Name;
            return item;
        }

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

        public bool IsExist(string mobile)
        {
            return _userRep.FindBy(x => x.Mobile == mobile).Any();
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

        IPagedList<UserAccountVM> GetAccounts(UserAccountQuery query);

        User GetById(long id);

        void Save(User model);

        void Save(MemberVM model);

        void Save(UserAccountVM model);

        IQueryable<UserAccount> GetAccountsByUserId(long userId);
        UserAccountVM GetAccountVmById(long id);
        /*
         * 
         * app
         * 
         */
        User GetByMobile(string mobile);
        User GetByOpenId(string openid);

        bool IsExist(string mobile);
        User GetByCode(string code);

        UserConsigneeInfo GetConsignneInfoByCode(string code);
        UserConsigneeInfo GetConsignneInfoByUid(long userid);
    }
}
