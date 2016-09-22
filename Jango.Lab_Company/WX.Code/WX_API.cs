using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WX.Code.lib;
using CvtUtil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Zero.Log.Util;
using System.Dynamic;
using System.Web;
using System.Security.Cryptography;
namespace WX.Code
{
    public class WX_API
    {
        public string access_token { get; set; }
        public string openid { get; set; }
        
        #region 获取access token
        /// <summary>
        /// 获得access token
        /// </summary>
        /// <returns></returns>
        public string GetAccessToken() {
            dynamic _dym = new ExpandoObject();
            try
            {
                WxPayData _data = new WxPayData();
                _data.SetValue("grant_type", "client_credential");
                _data.SetValue("appid", WxPayConfig.APPID);
                _data.SetValue("secret", WxPayConfig.APPSECRET);             
                string _url = "https://api.weixin.qq.com/cgi-bin/token?" + _data.ToUrl();
                string _result = HttpService.Get(_url);
                JObject _json = JsonConvert.DeserializeObject<JObject>(_result);
                if (_json["expires_in"] != null) { _dym.code = 0; _dym.access_token = _json["access_token"].ToString(); }
                else { _dym.code = _json["errcode"].ToString(); }
            }
            catch (Exception _ex)
            {
                _dym.code = "40001";
                Log.Error("GetAccessToken", _ex.Message);
            }
            return  JsonConvert.SerializeObject(_dym);
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
        public string GetOpenidAndAccessTokenFromCode(string _code)
        {
            dynamic _dym = new ExpandoObject();
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
                JObject _json = JsonConvert.DeserializeObject<JObject>(_result);

                _dym.access_token = (string)_json["access_token"];

                //获取用户openid
                _dym.openid = (string)_json["openid"];

                //Log.Debug(this.GetType().ToString(), "Get openid : " + _dym.opendi);
                //Log.Debug(this.GetType().ToString(), "Get access_token : " + _dym.access_token);
            }
            catch (Exception ex)
            {
                Log.Error(this.GetType().ToString(), ex.ToString());
            }
            return JsonConvert.SerializeObject(_dym);
        }
        #endregion 

        #region 获取用户信息 通过网页授权
        /// <summary>
        /// 获取用户信息 通过网页授权
        /// </summary>
        /// <param name="_access_token">网页临时accesstoken</param>
        /// <param name="_opendId">用户openid</param>
        /// <returns></returns>
        public string GetUserInfoByWeb(string _access_token, string _opendId)
        {
            dynamic _dym = new ExpandoObject();
            try
            {
                WxPayData _data = new WxPayData();
                _data.SetValue("access_token", _access_token);
                _data.SetValue("openid", _opendId);
                _data.SetValue("lang", "zh_CN");
                string _url = "https://api.weixin.qq.com/sns/userinfo?" + _data.ToUrl();
                string _result = HttpService.Get(_url);
                Log.Info("result", _result);
                JObject _jsonData = JsonConvert.DeserializeObject<JObject>(_result);
                _dym.openid = _jsonData["openid"].ToString();
                _dym.nickname = _jsonData["nickname"].ToString();
                _dym.sex = _jsonData["sex"].ToString();
                _dym.province = _jsonData["province"].ToString();
                _dym.country = _jsonData["country"].ToString();
                _dym.headimgurl = _jsonData["headimgurl"].ToString();
                _dym.language = _jsonData["language"].ToString();
                 _dym.errcode = "4000"; ;
            }
            catch (Exception _ex)
            {
                _dym.errcode = "40001";
                Log.Error("GetUserInfo", _ex.Message);
            }
            return JsonConvert.SerializeObject(_dym);
        }
        #endregion

        #region 获取用户信息 
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="_access_token">全局accesstoken</param>
        /// <param name="_opendId">用户openid</param>
        /// <returns></returns>
        public string GetUserInfo(string _access_token, string _opendId)
        {
            dynamic _dym = new ExpandoObject();
            try
            {
                WxPayData _data = new WxPayData();
                _data.SetValue("access_token", _access_token);
                _data.SetValue("openid", _opendId);
                _data.SetValue("lang", "zh_CN");
                string _url = "https://api.weixin.qq.com/cgi-bin/user/info?" + _data.ToUrl();
                string _result = HttpService.Get(_url);
                JObject _jsonData = JsonConvert.DeserializeObject<JObject>(_result);
                if (_jsonData["errcode"].ToString() == "0")
                {
                    _dym.subscribe = _jsonData["subscribe"].ToString();
                    _dym.openid = _jsonData["openid"].ToString();
                    _dym.nickname = _jsonData["nickname"].ToString();
                    _dym.sex = _jsonData["sex"].ToString();
                    _dym.language = _jsonData["language"].ToString();
                    _dym.city = _jsonData["city"].ToString();
                    _dym.province = _jsonData["province"].ToString();
                    _dym.country = _jsonData["country"].ToString();
                    _dym.headimgurl = _jsonData["headimgurl"].ToString();
                }
                _dym.errcode = _jsonData["errcode"].ToString();
            }
            catch (Exception _ex)
            {
                _dym.errcode = "40001";
                Log.Error("GetUserInfo", _ex.Message);
            }
            return JsonConvert.SerializeObject(_dym);
        }
        #endregion

        #region 创建菜单
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="_access_token">全局accesstoken</param>
        /// <param name="_post">post参数 详情查看 http://mp.weixin.qq.com/wiki/6/95cade7d98b6c1e1040cde5d9a2f9c26.html# </param>
        /// <returns></returns>
        public string CreateMenu(string _access_token, string _post) {
            dynamic _dym = new ExpandoObject();
            try
            {
                WxPayData _data = new WxPayData();
                _data.SetValue("access_token", _access_token);
                string _url = "https://api.weixin.qq.com/cgi-bin/menu/create?" + _data.ToUrl();
                string _json = HttpService.Post(_url, _post, false, 6);
                JObject _jsonData = JsonConvert.DeserializeObject<JObject>(_json);
                _dym.errorcode = _jsonData["errcode"].ToString();
            }
            catch (Exception)
            {
                _dym.errorcode = "10000";
            }
            return JsonConvert.SerializeObject(_dym);
        }
        #endregion

        #region js config
        public string JsConfig(string _ticket, string _url)
        {
            string _timestamp = ConvertDateTimeInt(DateTime.Now).ToString();
            string _nonceStr = createNonceStr();
            WxPayData _data = new WxPayData();
            _data.SetValue("jsapi_ticket", _ticket);
            _data.SetValue("noncestr", _nonceStr);
            _data.SetValue("timestamp", _timestamp);
            _data.SetValue("url", _url);
            // 这里参数的顺序要按照 key 值 ASCII 码升序排序  
            string _rawstring = _data.ToUrl();
            string _result = @"{""appid"": """ + WxPayConfig.APPID + @""",""timestamp"": """ + _timestamp + @""",""nonceStr"": """ + _nonceStr + @""",""signature"": """ + SHA1_Hash(_rawstring) + @""",""jsApiList"": [""onMenuShareTimeline"",""onMenuShareAppMessage"",""onMenuShareQQ"",""onMenuShareWeibo"",""onMenuShareQZone"",""startRecord"",""stopRecord"",""onVoiceRecordEnd"",""playVoice"",""pauseVoice"",""stopVoice"",""onVoicePlayEnd"",""uploadVoice"",""downloadVoice"",""chooseImage"",""previewImage"",""uploadImage"",""downloadImage"",""translateVoice"",""getNetworkType"",""openLocation"",""getLocation"",""hideOptionMenu"",""showOptionMenu"",""hideMenuItems"",""showMenuItems"",""hideAllNonBaseMenuItem"",""showAllNonBaseMenuItem"",""closeWindow"",""scanQRCode"",""chooseWXPay"",""openProductSpecificView"",""addCard"",""chooseCard"",""openCard""]}";
            return _result;
        }
        //SHA1哈希加密算法  
        public string SHA1_Hash(string str_sha1_in)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = System.Text.UTF8Encoding.Default.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            str_sha1_out = str_sha1_out.Replace("-", "").ToLower();
            return str_sha1_out;
        }
        #endregion

        #region 获得 js_api_ticket
        /// <summary>
        /// 获取JsApiTicket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetJsApiTicket(string _accessToken)
        {
            WxPayData _data = new WxPayData();
            _data.SetValue("access_token", _accessToken);
            _data.SetValue("type", "jsapi");
            string _url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?" + _data.ToUrl();
            string _result = HttpService.Get(_url);
            return _result;
        }
        #endregion

        #region 创建随机字符串
        public string createNonceStr()
        {
            int length = 16;
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string str = "";
            Random rad = new Random();
            for (int i = 0; i < length; i++)
            {
                str += chars.Substring(rad.Next(0, chars.Length - 1), 1);
            }
            return str;
        }
        #endregion

        #region
        public int ConvertDateTimeInt(DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        #endregion
    }
}
