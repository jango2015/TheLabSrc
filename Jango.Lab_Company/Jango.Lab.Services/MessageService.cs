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
        public MessageService(IMessageRep messageeRep, ILabUow uow)
        {
            _messageRep = messageeRep;
            _uow = uow;
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
    }

    public interface IMessageService
    {
        void AddMsg(Message msg);

        Message GetTop1SmsMsgByMobile(string mobile);

        IQueryable<Message> GetTemplateMsgByOpentId(string openId);

        Message GetById(long id);

        bool ValidateMsg(string mobile, string content);


    }
}
