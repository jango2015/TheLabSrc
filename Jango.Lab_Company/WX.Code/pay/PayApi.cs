using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WX.Code.lib;
using Zero.Log.Util;
using System.Web;

namespace WX.Code.pay
{
    public class PayApi
    {
        public static Dictionary<int, string> TradeType
        {
            get
            {
                Dictionary<int, string> _dic = new Dictionary<int, string>() {
                    {1,"JSAPI"}, //公众号支付
                    {2,"NATIVE"}, // 原生扫码支付
                    {3,"APP"}, // app支付
                    {4,"MICROPAY"} //刷卡支付
                };
                return _dic;
            }
        }
        public string openid { get; set; }
        public WxPayData unifiedOrderResult { get; set; }
        #region 统一下单接口
        /// <summary>
        /// 统一下单接口
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        public WxPayData GetUnifiedOrderResult(PayModel _model)
        {
            //统一下单
            WxPayData _data = new WxPayData();
            _data.SetValue("body", _model.Body); //商品描述       
            _data.SetValue("out_trade_no", WxPayApi.GenerateOutTradeNo());//商户订单号
            _data.SetValue("total_fee", _model.TotalFee); //总金额
            _data.SetValue("trade_type", _model.TradeType);
            if (!string.IsNullOrEmpty(_model.Attach)) _data.SetValue("attach", _model.Attach);
            if (!string.IsNullOrEmpty(_model.NotifyUrl)) _data.SetValue("notify_url", _model.NotifyUrl);
            if (!string.IsNullOrEmpty(_model.LimitPay)) _data.SetValue("limit_pay", _model.LimitPay);
            if (!string.IsNullOrEmpty(_model.DeviceInfo)) _data.SetValue("device_info", _model.DeviceInfo);
            if (!string.IsNullOrEmpty(_model.GoodsTag)) _data.SetValue("goods_tag", _model.GoodsTag);
            if (!string.IsNullOrEmpty(_model.ProductId)) _data.SetValue("product_id", _model.ProductId);
            if (!string.IsNullOrEmpty(_model.TimeStart)) _data.SetValue("time_start", _model.TimeStart);
            if (!string.IsNullOrEmpty(_model.TimeExpire)) _data.SetValue("time_expire", _model.TimeExpire);
            if (!string.IsNullOrEmpty(_model.OpenId)) { _data.SetValue("openid", _model.OpenId); openid = _model.OpenId; }

            WxPayData _result = WxPayApi.UnifiedOrder(_data);
            if (_model.TradeType == "JSAPI" && !_result.IsSet("appid"))
            {
                Log.Error(this.GetType().ToString(), "UnifiedOrder response error no appid!");
            }
            Log.Debug("prepay_id", _result.GetValue("prepay_id").ToString());
            if (!_result.IsSet("prepay_id") || _result.GetValue("prepay_id").ToString() == "")
            {
                Log.Error(this.GetType().ToString(), "UnifiedOrder response error no prepay_id!");
            }
            unifiedOrderResult = _result;
            return _result;
        }
        #endregion
        #region js支付回调
        /// <summary>
        /// js支付回调
        /// </summary>
        /// <returns></returns>
        public string GetJsApiParameters(WxPayData _data)
        {
            Log.Debug(this.GetType().ToString(), "JsApiPay:GetJsApiParam is processing...");
            WxPayData _jsApiParam = new WxPayData();
            _jsApiParam.SetValue("appId", _data.GetValue("appid"));
            Log.Debug(this.GetType().ToString(), "appId:" + _data.GetValue("appid"));
            _jsApiParam.SetValue("timeStamp", WxPayApi.GenerateTimeStamp());
            _jsApiParam.SetValue("nonceStr", WxPayApi.GenerateNonceStr());
            _jsApiParam.SetValue("package", "prepay_id=" + _data.GetValue("prepay_id"));
            _jsApiParam.SetValue("signType", "MD5");
            _jsApiParam.SetValue("paySign", _jsApiParam.MakeSign());
            string _parameters = _jsApiParam.ToJson();
            Log.Debug(this.GetType().ToString(), "Get jsApiParam : " + _parameters);
            return _parameters;
        }
        #endregion
        #region 二维码支付
        #region 支付模式一
        /// <summary>
        /// 二维码 支付模式一
        /// </summary>
        /// <param name="_productId"></param>
        /// <returns></returns>
        public string NativePayMode1(string _productId)
        {
            Log.Info(this.GetType().ToString(), "Native pay mode 1 url is producing...");
            WxPayData _data = new WxPayData();
            _data.SetValue("appid", WxPayConfig.APPID);//公众帐号id
            _data.SetValue("mch_id", WxPayConfig.MCHID);//商户号
            _data.SetValue("time_stamp", WxPayApi.GenerateTimeStamp());//时间戳
            _data.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
            _data.SetValue("product_id", _productId);//商品ID
            _data.SetValue("sign", _data.MakeSign());//签名
            string _str = ToUrlParams(_data.GetValues());//转换为URL串
            string _url = "weixin://wxpay/bizpayurl?" + _str;
            Log.Info(this.GetType().ToString(), "Get native pay mode 1 url : " + _url);
            return _url;
        }
        #endregion
        #endregion
        #region app支付回调
        public string GetAppParameters(WxPayData _model)
        {
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
        #region 参数数组转换为url格式
        private string ToUrlParams(SortedDictionary<string, object> _map)
        {
            string _buff = "";
            foreach (KeyValuePair<string, object> _pair in _map)  _buff += _pair.Key + "=" + _pair.Value + "&";
            _buff = _buff.Trim('&');
            return _buff;
        }
        #endregion
    }
}
