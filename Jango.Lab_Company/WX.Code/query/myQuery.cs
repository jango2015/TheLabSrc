using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WX.Code.lib;
using Zero.Log.Util;
namespace WX.Code.query
{
    public class myQuery
    {
        #region 订单查询
        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="_transaction_id">微信订单号（优先使用）</param>
        /// <param name="_out_trade_no">商户订单号</param>
        /// <returns></returns>
        public string OrderQuery(string _transaction_id, string _out_trade_no)
        {
            Log.Info("OrderQuery", "OrderQuery is processing...");

            WxPayData data = new WxPayData();
            if (!string.IsNullOrEmpty(_transaction_id))//如果微信订单号存在，则以微信订单号为准
            {
                data.SetValue("transaction_id", _transaction_id);
            }
            else//微信订单号不存在，才根据商户订单号去查单
            {
                data.SetValue("out_trade_no", _out_trade_no);
            }

            WxPayData _result = WxPayApi.OrderQuery(data);//提交订单查询请求给API，接收返回数据

            Log.Info("OrderQuery", "OrderQuery process complete, result : " + _result.ToXml());
            return _result.ToJson();
        }
        #endregion
    }
}
