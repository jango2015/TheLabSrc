using Jango.Lab.Models;
using Jango.Lab.Repositories;
using Jango.Lab.Repositories.Lab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Jango.Lab.Services
{
    public class MessageService : IMessageService
    {
        private readonly ILabUow _uow;
        private readonly IMessageRep _messageRep;
        private readonly ILogRep _logsrv;
        public MessageService(IMessageRep messageeRep, ILabUow uow, ILogRep logsrv)
        {
            _messageRep = messageeRep;
            _uow = uow;
            _logsrv = logsrv;
        }

        public void AddMsg(Message msg)
        {
            _messageRep.Add(msg);
            _uow.Commit();
        }

        public Message GetTop1SmsMsgByMobile(string mobile)
        {
            if (string.IsNullOrEmpty(mobile)) return null;

            return _messageRep.FindBy(x => x.OpenID.Equals(mobile) && x.MsgType == EnumMessageType.SMS).OrderByDescending(x => x.ID).FirstOrDefault();
        }

        public Message GetById(long id)
        {
            return _messageRep.GetById(id);
        }

        public bool ValidateMsg(string mobile, string content)
        {
            var item = GetTop1SmsMsgByMobile(mobile);
            if (null != item)
            {
                return content.Equals(item.Content);
            }
            else
            {
                return false;
            }
        }

        public IQueryable<Message> GetTemplateMsgByOpentId(string openId)
        {
            if (string.IsNullOrEmpty(openId)) return null;
            return _messageRep.FindBy(x => x.MsgType == EnumMessageType.Template && x.OpenID == openId);
        }

        private string GetCode()
        {
            var rand = new Random();
            var str = string.Empty;
            while (str.Length < 6)
            {
                var s = rand.Next(0, 9).ToString();
                if (!str.Contains(s))
                {
                    str += s;
                }
            }
            return str;
        }
        public void SendSms(string mobile)
        {
            var code = GetCode();
            var msg = new Message()
            {
                OpenID = mobile,
                Content = code,
                MsgType = EnumMessageType.SMS,
                Status = 1,
                SendTime = DateTime.Now
            };
            _messageRep.Add(msg);

            var cntemp_id = System.Configuration.ConfigurationManager.AppSettings["__juhe_cn_tempId"];
            var app = System.Configuration.ConfigurationManager.AppSettings["__juhe_cn_appName"];
            var tplvalue = "#app#=" + app + "&#code#=" + code;
            var res = Lib.JuheSMS.JuheSendSms.SendSms(mobile, cntemp_id, tplvalue);
            _logsrv.Add(new Log()
            {
                Action = "SendSms",
                Message = (res.result == null ? "" : LitJson.JsonMapper.ToJson(res.result)) + res.reason,
                CreatedAt = DateTime.Now,
                Operator = mobile,
                UserID = 0
            });
            _uow.Commit();
            if (res.IsSuccess)
            {//send success
            }
            else
            {//send error
                throw new Exception(res.reason);
            }

        }
    }

    public interface IMessageService
    {
        void SendSms(string mobile);
        void AddMsg(Message msg);

        Message GetTop1SmsMsgByMobile(string mobile);

        IQueryable<Message> GetTemplateMsgByOpentId(string openId);

        Message GetById(long id);

        bool ValidateMsg(string mobile, string content);


    }
}
