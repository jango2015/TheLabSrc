using Jango.Lab.Models;
using Jango.Lab.Repositories;
using Jango.Lab.Repositories.Lab;
using Jango.Lab.ViewModels.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;
using WX.Code.pay;


namespace Jango.Lab.Services
{
    public class ChargeService : IChargeService
    {
        public object obj = new object();
        private readonly ILabUow _uow = LoadReps._uow;
        private readonly IChargeCardRep _chargeCardRep = LoadReps._chargeCardRep;
        private readonly IChargeRecordRep _chargeRecordRep = LoadReps._chargeRecordRep;
        private readonly IUserRep _userRep = LoadReps._userRep;
        private readonly IUserAccountRep _userAccountRep = LoadReps._userAccountRep;


        public IQueryable<ChargeCard> GetValidChargeCards()
        {
            return _chargeCardRep.FindBy(x => x.IsValid);
        }

        public IQueryable<ChargeCard> GetAllChargeCards()
        {
            return _chargeCardRep.GetAll();
        }

        public ChargeCard GetById(long id)
        {
            if (id == 0) return new ChargeCard();
            return _chargeCardRep.FindBy(x => x.ID == id).FirstOrDefault();
        }

        public void Save(ChargeCard model)
        {
            if (!model.CardNO.StartsWith("CZ"))
            {
                model.CardNO = "CZ" + model.CardNO;
            }
            if (model.ID == 0)
            {
                ValidCardNo(model.CardNO);
                _chargeCardRep.Add(model);
            }
            else
            {
                _chargeCardRep.Update(model);
            }
            _uow.Commit();
        }

        private void ValidCardNo(string cardNo)
        {
            if (string.IsNullOrEmpty(cardNo))
            {
                throw new ArgumentNullException("card no");
            }
            var items = _chargeCardRep.FindBy(x => x.CardNO == cardNo);
            if (items.Any())
            {
                throw new Exception("card no has exist");
            }
        }
        public string Charge(ChargeRecord model)
        {
            if (model.CardID == 0)
            {
                throw new Exception("card information error;");
            }
            var card = _chargeCardRep.GetById(model.CardID);
            if (card == null)
            {
                throw new Exception("card information error;");
            }
            model.Amount = card.Amount;
            var user = _userRep.GetById(model.UserID);

            model.SubmitAt = DateTime.Now;
            model.PaySatus = EnumPayStatus.ToPay;
            model.tradeId = GetCardNo("CZP");
            //add record
            _chargeRecordRep.Add(model);

            if (string.IsNullOrEmpty(user.OpenID)) throw new ArgumentNullException("openid");
            var payApi = new WX.Code.pay.PayApi();
            var data = payApi.GetUnifiedOrderResult(new PayModel()
            {
                OpenId = user.OpenID,
                TradeType = "JSAPI",
                Body = card.Remark,
                TotalFee = (Math.Round(card.Price * 1000)).ToString(),//change yuan to fen
                DeviceInfo = "jsapi",
                TimeStart = model.SubmitAt.ToString("yyyyMMddHHmmss")
            });
            var str = payApi.GetJsApiParameters(data);
            // pay 
            //change status
            //paysuccess
            SaveAccount(model, card.Amount, card.GiftIntegral);

            _uow.Commit();

            return str;
        }

        private void SaveAccount(ChargeRecord model, decimal cardAmount, decimal giftIntegral)
        {
            var userAccouts = _userAccountRep.FindBy(x => x.UserID == model.UserID);
            var balance = userAccouts.FirstOrDefault(x => x.AccountType == (int)EnumAccountType.Balance);
            var integral = userAccouts.FirstOrDefault(x => x.AccountType == (int)EnumAccountType.Integral);
            if (balance == null) balance = new UserAccount();
            if (integral == null) integral = new UserAccount();
            balance.Amount += cardAmount;
            balance.AccountType = (int)EnumAccountType.Balance;
            balance.UserID = model.UserID;
            integral.AccountType = (int)EnumAccountType.Integral;
            integral.Amount += giftIntegral;
            integral.UserID = model.UserID;
            if (balance.ID == 0)
            {
                balance.CreatedAt = DateTime.Now;
                balance.ModifiedAt = DateTime.Now;
                _userAccountRep.Add(balance);
            }
            else
            {
                balance.ModifiedAt = DateTime.Now;
                _userAccountRep.Update(balance);
            }
            if (integral.ID == 0)
            {
                integral.CreatedAt = DateTime.Now;
                integral.ModifiedAt = DateTime.Now;
                _userAccountRep.Add(integral);
            }
            else
            {
                integral.ModifiedAt = DateTime.Now;
                _userAccountRep.Update(integral);
            }
            model.CurrentAmount = balance.Amount;
        }

        public IQueryable<ChargeRecord> GetAllRecords()
        {
            return _chargeRecordRep.GetAll();
        }

        public IQueryable<ChargeRecord> GetRecordByUserId(long userId)
        {
            return _chargeRecordRep.FindBy(x => x.UserID == userId);
        }

        public IPagedList<ChargeCard> GetAllChargeCardList(ChargeQuery query)
        {
            return _chargeCardRep.GetAllList().OrderByDescending(x => x.ID).ToPagedList(query.PageNumber, query.PageSize);
        }

        public IPagedList<ChargeRecord> GetAllChargeRecordList(ChargeRecordQuery query)
        {
            return _chargeRecordRep.GetAllList().OrderByDescending(x => x.ID).ToPagedList(query.PageNumber, query.PageSize);
        }

        public string GetCardNo(string pre)
        {
            string str = pre;
            lock (obj)
            {
                var it = _chargeCardRep.GetAll();
                var gerno = (it.Any() ? it.Max(x => x.ID) + 1 : 1);
                str += DateTime.Now.Ticks.ToString();
                var s = str.PadLeft(15, '0');
                str = s + gerno;
            }
            return str;
        }
    }

    public interface IChargeService
    {
        IPagedList<ChargeCard> GetAllChargeCardList(ChargeQuery query);
        IQueryable<ChargeCard> GetValidChargeCards();
        IQueryable<ChargeCard> GetAllChargeCards();
        ChargeCard GetById(long id);
        void Save(ChargeCard model);

        string GetCardNo(string pre);
        string Charge(ChargeRecord model);
        IQueryable<ChargeRecord> GetAllRecords();
        IPagedList<ChargeRecord> GetAllChargeRecordList(ChargeRecordQuery query);
        IQueryable<ChargeRecord> GetRecordByUserId(long userId);
    }
}
