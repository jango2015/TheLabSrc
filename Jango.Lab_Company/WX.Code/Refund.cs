using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zero.Log.Util;
using WX.Code.lib;

namespace WX.Code
{
    public class Refund
    {
        public static string Run(string _transaction_id, string _out_trade_no, string _total_fee, string _refund_fee)
        {
            Log.Info("Refund", "Refund is processing...");

            WxPayData _data = new WxPayData();
            if (!string.IsNullOrEmpty(_transaction_id))//微信订单号存在的条件下，则已微信订单号为准
            {
                _data.SetValue("transaction_id", _transaction_id);
            }
            else//微信订单号不存在，才根据商户订单号去退款
            {
                _data.SetValue("out_trade_no", _out_trade_no);
            }

            _data.SetValue("total_fee", int.Parse(_total_fee));//订单总金额
            _data.SetValue("refund_fee", int.Parse(_refund_fee));//退款金额
            _data.SetValue("out_refund_no", WxPayApi.GenerateOutTradeNo());//随机生成商户退款单号
            _data.SetValue("op_user_id", WxPayConfig.MCHID);//操作员，默认为商户号

            WxPayData _result = WxPayApi.Refund(_data);//提交退款申请给API，接收返回数据

            Log.Info("Refund", "Refund process complete, result : " + _result.ToJson());
            return _result.ToJson();
        }
    }
}
