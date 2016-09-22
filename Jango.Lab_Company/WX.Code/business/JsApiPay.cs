using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WX.Code.lib;
using LitJson;
using Zero.Log.Util;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Dynamic;
namespace WX.Code.business
{
    public class JsApiPay
    {
        public static Dictionary<string, string> TradeType {
            get {
                Dictionary<string, string> _dic = new Dictionary<string, string>() {
                    {"公众号支付","JSAPI"},
                    {"原生扫码支付","NATIVE"},
                    {"app支付","APP"},
                    {"刷卡支付","MICROPAY"}
                };
                return _dic;
            }
        }
        /// <summary>
        /// 保存页面对象，因为要在类的方法中使用Page的Request对象
        /// </summary>
        private Page page { get; set; }

        /// <summary>
        /// openid用于调用统一下单接口
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// access_token用于获取收货地址js函数入口参数
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 商品金额，用于统一下单
        /// </summary>
        public int total_fee { get; set; }

        /// <summary>
        /// 统一下单接口返回结果
        /// </summary>
        public WxPayData unifiedOrderResult { get; set; }

        public JsApiPay(Page page)
        {
            this.page = page;
        }

        #region  网页授权获取用户基本信息的全部过程
        /**
        * 
        * 网页授权获取用户基本信息的全部过程
        * 详情请参看网页授权获取用户基本信息：http://mp.weixin.qq.com/wiki/17/c0f37d5704f0b64713d5d2c37b468d75.html
        * 第一步：利用url跳转获取code
        * 第二步：利用code去获取openid和access_token
        * 
        */
        public void GetOpenidAndAccessToken(string _code)
        {
            if (!string.IsNullOrEmpty(_code))
            {
                //获取code码，以获取openid和access_token
                //string _code = page.Request.QueryString["code"];
                Log.Debug(this.GetType().ToString(), "Get code : " + _code);
                GetOpenidAndAccessTokenFromCode(_code);
            }
            else
            {
                //构造网页授权获取code的URL
                string _host = page.Request.Url.Host;
                string _path = page.Request.Path;
                try
                {
                    //触发微信返回code码         
                    page.Response.Redirect(GetUrlByAppId(_host, _path));//Redirect函数会抛出ThreadAbortException异常，不用处理这个异常
                }
                catch (System.Threading.ThreadAbortException ex)
                {
                }
            }
        }
        #endregion
        #region 跳转微信链接 微信授权
        /// <summary>
        /// 跳转微信链接  微信授权
        /// </summary>
        /// <param name="_host">主机</param>
        /// <param name="_path">链接</param>
        /// <param name="_scope">snsapi_base snsapi_userinfo </param>
        /// <returns></returns>
        public string GetUrlByAppId(string _host, string _path, string _scope = "snsapi_base")
        {
            //构造网页授权获取code的URL
            string _redirectUri = HttpUtility.UrlEncode("http://" + _host + _path);
            WxPayData _data = new WxPayData();
            _data.SetValue("appid", WxPayConfig.APPID);
            _data.SetValue("redirect_uri", _redirectUri);
            _data.SetValue("response_type", "code");
            _data.SetValue("scope", _scope);
            _data.SetValue("state", "STATE" + "#wechat_redirect");
            string _url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + _data.ToUrl();
            Log.Debug("Page", "Will Redirect to URL : " + _url);
            return _url;
        }
        #endregion
        #region  通过code换取网页授权access_token和openid的返回数据
        /**
	    * 
	    * 通过code换取网页授权access_token和openid的返回数据，正确时返回的JSON数据包如下：
	    * {
	    *  "access_token":"ACCESS_TOKEN",
	    *  "expires_in":7200,
	    *  "refresh_token":"REFRESH_TOKEN",
	    *  "openid":"OPENID",
	    *  "scope":"SCOPE",
	    *  "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"
	    * }
	    * 其中access_token可用于获取共享收货地址
	    * openid是微信支付jsapi支付接口统一下单时必须的参数
        * 更详细的说明请参考网页授权获取用户基本信息：http://mp.weixin.qq.com/wiki/17/c0f37d5704f0b64713d5d2c37b468d75.html
        * @失败时抛异常WxPayException
	    */
        public void GetOpenidAndAccessTokenFromCode(string _code)
        {
            try
            {
                //构造获取openid及access_token的url
                WxPayData _data = new WxPayData();
                _data.SetValue("appid", WxPayConfig.APPID);
                _data.SetValue("secret", WxPayConfig.APPSECRET);
                _data.SetValue("code", _code);
                _data.SetValue("grant_type", "authorization_code");
                string _url = "https://api.weixin.qq.com/sns/oauth2/access_token?" + _data.ToUrl();

                //请求url以获取数据
                string _result = HttpService.Get(_url);

                Log.Debug(this.GetType().ToString(), "GetOpenidAndAccessTokenFromCode response : " + _result);

                //保存access_token，用于收货地址获取
                JsonData _jd = JsonMapper.ToObject(_result);
                access_token = (string)_jd["access_token"];

                //获取用户openid
                openid = (string)_jd["openid"];

                Log.Debug(this.GetType().ToString(), "Get openid : " + openid);
                Log.Debug(this.GetType().ToString(), "Get access_token : " + access_token);
            }
            catch (Exception ex)
            {
                Log.Error(this.GetType().ToString(), ex.ToString());
                throw new WxPayException(ex.ToString());
            }
        }
        #endregion 
        #region 调用统一下单，获得下单结果
        /**
         * 调用统一下单，获得下单结果
         * @return 统一下单结果
         * @失败时抛异常WxPayException
         */
        public WxPayData GetUnifiedOrderResult()
        {
            //统一下单
            WxPayData _data = new WxPayData();
            _data.SetValue("body", "test");
            _data.SetValue("attach", "test");
            _data.SetValue("out_trade_no", WxPayApi.GenerateOutTradeNo());
            _data.SetValue("total_fee", total_fee);
            _data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            _data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            _data.SetValue("goods_tag", "test");
            _data.SetValue("trade_type", "JSAPI"); 
            _data.SetValue("openid", openid);

            WxPayData result = WxPayApi.UnifiedOrder(_data);
            if (!result.IsSet("appid") || !result.IsSet("prepay_id") || result.GetValue("prepay_id").ToString() == "")
            {
                Log.Error(this.GetType().ToString(), "UnifiedOrder response error!");
                throw new WxPayException("UnifiedOrder response error!");
            }

            unifiedOrderResult = result;
            return result;
        }
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
            if (!_result.IsSet("appid") || !_result.IsSet("prepay_id") || _result.GetValue("prepay_id").ToString() == "")
            {
                Log.Error(this.GetType().ToString(), "UnifiedOrder response error!");
            }
            unifiedOrderResult = _result;
            return _result;
        }
        #region  
        public class PayModel {
            public string Body { get; set; }  //商品描述
            public string Attach { get; set; } //附加数据
            public string TotalFee { get; set; } //总金额
            public string TradeType { get; set; } //交易类型 公众号支付 = JSAPI, 原生扫码支付 = NATIVE, app支付 = APP, 刷卡支付 = MICROPAY
            public string OpenId { get; set; } //用户标识
            public string NotifyUrl { get; set; } //通知地址
            public string LimitPay { get; set; } //指定支付方式 no_credit--指定不能使用信用卡支付
            public string DeviceInfo { get; set; }//设备号
            public string GoodsTag { get; set; }//商品标记
            public string TimeStart { get; set; }//交易起始时间
            public string TimeExpire { get; set; }//交易结束时间
            public string ProductId { get; set; }//商品ID trade_type=NATIVE，此参数必传。此id为二维码中包含的商品ID，商户自行定义。
        }
        #endregion
        #endregion
        #region 从统一下单成功返回的数据中获取微信浏览器调起jsapi支付所需的参数
        /**
        *  
        * 从统一下单成功返回的数据中获取微信浏览器调起jsapi支付所需的参数，
        * 微信浏览器调起JSAPI时的输入参数格式如下：
        * {
        *   "appId" : "wx2421b1c4370ec43b",     //公众号名称，由商户传入     
        *   "timeStamp":" 1395712654",         //时间戳，自1970年以来的秒数     
        *   "nonceStr" : "e61463f8efa94090b1f366cccfbbb444", //随机串     
        *   "package" : "prepay_id=u802345jgfjsdfgsdg888",     
        *   "signType" : "MD5",         //微信签名方式:    
        *   "paySign" : "70EA570631E4BB79628FBCA90534C63FF7FADD89" //微信签名 
        * }
        * @return string 微信浏览器调起JSAPI时的输入参数，json格式可以直接做参数用
        * 更详细的说明请参考网页端调起支付API：http://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=7_7
        * 
        */
        public string GetJsApiParameters()
        {
            Log.Debug(this.GetType().ToString(), "JsApiPay:GetJsApiParam is processing...");

            WxPayData _jsApiParam = new WxPayData();
            _jsApiParam.SetValue("appId", unifiedOrderResult.GetValue("appid"));
            Log.Debug(this.GetType().ToString(), "appId:" + unifiedOrderResult.GetValue("appid"));
            _jsApiParam.SetValue("timeStamp", WxPayApi.GenerateTimeStamp());
            _jsApiParam.SetValue("nonceStr", WxPayApi.GenerateNonceStr());
            _jsApiParam.SetValue("package", "prepay_id=" + unifiedOrderResult.GetValue("prepay_id"));
            _jsApiParam.SetValue("signType", "MD5");    
            _jsApiParam.SetValue("paySign", _jsApiParam.MakeSign());

            string _parameters = _jsApiParam.ToJson();

            Log.Debug(this.GetType().ToString(), "Get jsApiParam : " + _parameters);
            return _parameters;
        }
        #endregion
        #region 获取收货地址js函数入口参数
        /**
	    * 
	    * 获取收货地址js函数入口参数,详情请参考收货地址共享接口：http://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=7_9
	    * @return string 共享收货地址js函数需要的参数，json格式可以直接做参数使用
	    */
        public string GetEditAddressParameters()
        {
            string parameter = "";
            try
            {
                string host = page.Request.Url.Host;
                string path = page.Request.Path;
                string queryString = page.Request.Url.Query;
                //这个地方要注意，参与签名的是网页授权获取用户信息时微信后台回传的完整url
                string url = "http://" + host + path + queryString;

                //构造需要用SHA1算法加密的数据
                WxPayData _signData = new WxPayData();
                _signData.SetValue("appid", WxPayConfig.APPID);
                _signData.SetValue("url", url);
                _signData.SetValue("timestamp", WxPayApi.GenerateTimeStamp());
                _signData.SetValue("noncestr", WxPayApi.GenerateNonceStr());
                _signData.SetValue("accesstoken", access_token);
                string _param = _signData.ToUrl();

                Log.Debug(this.GetType().ToString(), "SHA1 encrypt param : " + _param);
                //SHA1加密
                string addrSign = FormsAuthentication.HashPasswordForStoringInConfigFile(_param, "SHA1");
                Log.Debug(this.GetType().ToString(), "SHA1 encrypt result : " + addrSign);

                //获取收货地址js函数入口参数
                WxPayData _afterData = new WxPayData();
                _afterData.SetValue("appId", WxPayConfig.APPID);
                _afterData.SetValue("scope", "jsapi_address");
                _afterData.SetValue("signType", "sha1");
                _afterData.SetValue("addrSign", addrSign);
                _afterData.SetValue("timeStamp", _signData.GetValue("timestamp"));
                _afterData.SetValue("nonceStr", _signData.GetValue("noncestr"));

                //转为json格式
                parameter = _afterData.ToJson();
                Log.Debug(this.GetType().ToString(), "Get EditAddressParam : " + parameter);
            }
            catch (Exception ex)
            {
                Log.Error(this.GetType().ToString(), ex.ToString());
                throw new WxPayException(ex.ToString());
            }

            return parameter;
        }
        #endregion
    }
}
