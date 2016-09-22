using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WX.Code.lib;
using Zero.Log.Util;
using WX.Code.pay;
using System.Web.UI;

namespace WX.Code.notify
{
    public class myNotify : Notify
    {
        public myNotify(Page page)
            : base(page)
        {

        }
        public string openid { get; set; }
        public string productid { get; set; }
        #region  js回调
        /// <summary>
        /// js回调
        /// </summary>
        /// <param name="_s"></param>
        /// <returns></returns>
        public  string jsPayNotify(string _xml)
        {
            Log.Info(this.GetType().ToString(), "js回调...");
            WxPayData _notifyData = GetNotifyData(_xml);
            Log.Info("js回调data", _notifyData.ToJson());
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
                _res.SetValue("openid", _notifyData.GetValue("openid") + "");
            }
            // Log.Info(this.GetType().ToString(), "ProcessNotify : " + _res.ToJson());
            return _res.ToJson();
        }
        #endregion

        #region 发起二维码回调
        #region mode1
        /// <summary>
        /// 二维码回调第一步 获得产品id
        /// </summary>
        /// <param name="_post"></param>
        public string QrCodeNotifyOne(string _post)
        {
            Log.Info("二维码回调", "start...");
            WxPayData _notifyData = GetNotifyData(_post);
             WxPayData _res = new WxPayData();
            //检查openid和product_id是否返回
            if (!_notifyData.IsSet("openid") || !_notifyData.IsSet("product_id"))
            {
                _res.SetValue("return_code", "FAIL");
                _res.SetValue("return_msg", "回调数据异常");
            }
            else
            {
                string _openid = _notifyData.GetValue("openid").ToString();
                string _product_id = _notifyData.GetValue("product_id").ToString();
                openid = _openid;
                productid = _product_id;
                _res.SetValue("return_code", "SUCCESS");
                _res.SetValue("openid", _openid);
                _res.SetValue("productid", _product_id);
            }
            Log.Info("二维码回调", "end...");

            //调统一下单接口，获得下单结果
            return _res.ToJson();
        }
        /// <summary>
        /// 扫码支付回调第二步 发起支付
        /// </summary>
        /// <param name="_model"></param>
        public string QrCodeNotifyTwo(PayModel _model)
        {
            WxPayData _result = new WxPayData();
            WxPayData _response = new WxPayData();
            try
            {
                PayApi _pay = new PayApi();
                _result = _pay.GetUnifiedOrderResult(_model);
            }
            catch (Exception ex)//若在调统一下单接口时抛异常，立即返回结果给微信支付后台
            {
                _response.SetValue("return_code", "FAIL");
                _response.SetValue("return_msg", "统一下单失败");
                Log.Error(this.GetType().ToString(), "UnifiedOrder failure : " + _response.ToXml());
            }

            //若下单失败，则立即返回结果给微信支付后台
            if (!_result.IsSet("appid") || !_result.IsSet("mch_id") || !_result.IsSet("prepay_id"))
            {
                _response.SetValue("return_code", "FAIL");
                _response.SetValue("return_msg", "统一下单失败");
                Log.Error(this.GetType().ToString(), "UnifiedOrder failure : " + _response.ToXml());
            }

            //统一下单成功,则返回成功结果给微信支付后台
            _response.SetValue("return_code", "SUCCESS");
            _response.SetValue("return_msg", "OK");
            _response.SetValue("appid", WxPayConfig.APPID);
            _response.SetValue("mch_id", WxPayConfig.MCHID);
            _response.SetValue("nonce_str", WxPayApi.GenerateNonceStr());
            _response.SetValue("prepay_id", _result.GetValue("prepay_id"));
            _response.SetValue("result_code", "SUCCESS");
            _response.SetValue("err_code_des", "OK");
            _response.SetValue("sign", _response.MakeSign());
            Log.Info(this.GetType().ToString(), "UnifiedOrder success , send data to WeChat : " + _response.ToXml());
            return _response.ToXml();
        }
        #endregion
        #endregion

        #region 验证订单
        /// <summary>
        /// 验证订单
        /// </summary>
        /// <param name="_transaction_id"></param>
        /// <returns></returns>
        private bool QueryOrder(string _transaction_id)
        {
            WxPayData _req = new WxPayData();
            _req.SetValue("transaction_id", _transaction_id);
            WxPayData _res = WxPayApi.OrderQuery(_req);
            return (_res.GetValue("return_code").ToString() == "SUCCESS" && _res.GetValue("result_code").ToString() == "SUCCESS");
        }
        #endregion
    }
}
