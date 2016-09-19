using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using Jango.Lab.Models;

namespace Jango.Lab.Repositories.Lab
{
    public class EFContextFactory
    {
        /// <summary>
        /// �����Ƿ��ص�ǰ�߳��ڵ����ݿ������ģ������ǰ�߳���û�������ģ���ô����һ�������ģ�����֤
        /// ������ʵ�����߳��ڲ���Ψһ��
        /// </summary>
        /// <returns></returns>
        public static DbContext GetCurrentDbContext()
        {
            var dbContext = CallContext.GetData("DbContext") as DbContext;
            if (dbContext == null)  //�߳������ݲ�����û�д�������
            {
                dbContext = new LabModels(); //����һ��EF������
                CallContext.SetData("DbContext", dbContext);
            }
            return dbContext;
        }
    }
}