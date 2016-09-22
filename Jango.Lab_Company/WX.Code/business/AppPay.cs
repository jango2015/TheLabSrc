using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zero.Log.Util;
using WX.Code.lib;

namespace WX.Code.business
{
    public class AppPay
    {
        #region app支付
        public string GetAppParameters(WxPayData _model) {
            Log.Debug(this.GetType().ToString(), "appPay:GetAppParameters is processing...");
            WxPayData _jsApiParam = new WxPayData();
            _jsApiParam.SetValue("appid", _model.GetValue("appid")); //公众账号ID
            _jsApiParam.SetValue("partnerid", WxPayConfig.MCHID);//商户号
            _jsApiParam.SetValue("prepayid", _model.GetValue("prepay_id"));//预支付交易会话ID
            _jsApiParam.SetValue("package", "Sign=WXPay");//扩展字段
            _jsApiParam.SetValue("noncestr", WxPayApi.GenerateNonceStr());//随机字符串
            _jsApiParam.SetValue("timestamp", WxPayApi.GenerateTimeStamp());//时间戳
            _jsApiParam.SetValue("sign", _jsApiParam.MakeSign());//签名
            string _parameters = _jsApiParam.ToJson();
            Log.Debug(this.GetType().ToString(), "Get GetAppParameters : " + _parameters);
            return _parameters;
        }
        #endregion
    }
}
