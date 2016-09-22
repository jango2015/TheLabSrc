using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WX.Code.lib;
using Zero.Log.Util;
using System.Web.UI;
using System.IO;
namespace WX.Code.business
{
    /// <summary>
    /// 支付结果通知回调处理类
    /// 负责接收微信支付后台发送的支付结果并对订单有效性进行验证，将验证结果反馈给微信支付后台
    /// </summary>
    public class ResultNotify : Notify
    {
        public string TransactionId { get; set; }
        public string Attach { get; set; }
        public decimal Price { get; set; }

        public ResultNotify(Page page)
            : base(page)
        {
        }

        public override void ProcessNotify()
        {
            WxPayData _notifyData = GetNotifyData();
            WxPayData _res = new WxPayData();

            //检查支付结果中transaction_id是否存在
            if (!_notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                _res.SetValue("return_code", "FAIL");
                _res.SetValue("return_msg", "支付结果中微信订单号不存在");
                Log.Error(this.GetType().ToString(), "The Pay result is error : " + _res.ToXml());
                page.Response.Write(_res.ToXml());
                page.Response.End();
            }

            string _transaction_id = _notifyData.GetValue("transaction_id").ToString();

            //查询订单，判断订单真实性
            if (!QueryOrder(_transaction_id))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                _res.SetValue("return_code", "FAIL");
                _res.SetValue("return_msg", "订单查询失败");
                Log.Error(this.GetType().ToString(), "Order query failure : " + _res.ToXml());
                page.Response.Write(_res.ToXml());
               // page.Response.End();
            }
            //查询订单成功
            else
            {
               
                TransactionId = _transaction_id;
                Attach = _notifyData.GetValue("attach") + "";
                _res.SetValue("return_code", "SUCCESS");
                _res.SetValue("return_msg", "OK");
                Log.Info(this.GetType().ToString(), "order query success : " + _res.ToXml());
                page.Response.Write(_res.ToXml());
                //page.Response.End();
            }
        }

        public override string ProcessNotify(string _s)
        {
            WxPayData _notifyData = GetNotifyData(_s);
            WxPayData _res = new WxPayData();

            //检查支付结果中transaction_id是否存在
            if (!_notifyData.IsSet("transaction_id"))  //若transaction_id不存在，则立即返回结果给微信支付后台
            {
                _res.SetValue("return_code", "FAIL");
                _res.SetValue("return_msg", "支付结果中微信订单号不存在");
            }

            string _transaction_id = _notifyData.GetValue("transaction_id").ToString();

            if (!QueryOrder(_transaction_id))    //查询订单，判断订单真实性
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                _res.SetValue("return_code", "FAIL");
                _res.SetValue("return_msg", "订单查询失败");
            }
            else  //查询订单成功
            {
                _res.SetValue("return_code", "SUCCESS");
                _res.SetValue("return_msg", "OK");
                _res.SetValue("transaction_id", _transaction_id);
                _res.SetValue("out_trade_no", _notifyData.GetValue("out_trade_no") + "");
                _res.SetValue("attach", _notifyData.GetValue("attach") + "");
            }
            Log.Info(this.GetType().ToString(), "ProcessNotify : " + _res.ToJson());
            return _res.ToJson();
        }

        //查询订单
        private bool QueryOrder(string _transaction_id)
        {
            WxPayData _req = new WxPayData();
            _req.SetValue("transaction_id", _transaction_id);
            WxPayData _res = WxPayApi.OrderQuery(_req);
            return (_res.GetValue("return_code").ToString() == "SUCCESS" && _res.GetValue("result_code").ToString() == "SUCCESS");
        }
    }
}
