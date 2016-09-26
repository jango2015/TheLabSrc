using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LitJson;

namespace Jango.Lab.Services
{
    public class WxService
    {
        private static JsonData _jsonData;
        private static JsonData _userInfoData;
        public static JsonData GetTokenAndOpenId(string code)
        {
            var requrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + ConfigService.AppID + "&secret=" + ConfigService.AppSeret + "&code=" + code + "&grant_type=authorization_code";
            var req = WebRequest.Create(requrl);
            var res = req.GetResponse().GetResponseStream();
            var str = string.Empty;
            if (res != null)
            {
                var sr = new StreamReader(res);
                str = sr.ReadToEnd();
            }
            var jd = JsonMapper.ToObject(str);
            _jsonData = jd;
            return _jsonData;

        }

        public static string GetOpenId(string code)
        {
            var jsData = GetTokenAndOpenId(code);
            if (jsData.Count > 0)
            {
                return jsData["openid"].ToString();
            }
            return string.Empty;
        }

        public static JsonData GetUserInfo()
        {
            var accesstoken = _jsonData["access_token"].ToString();
            var openid = _jsonData["openid"].ToString();
            var url = "https://api.weixin.qq.com/sns/userinfo?access_token=" + accesstoken + "&openid=" + openid +
                      "&lang=zh_CN";
            var req = WebRequest.Create(url);
            var res = req.GetResponse().GetResponseStream();
            var str = string.Empty;
            if (res != null)
            {
                var sr = new StreamReader(res);
                str = sr.ReadToEnd();
            }
            var jd = JsonMapper.ToObject(str);
            _userInfoData = jd;
            return _userInfoData;
        }

        public static string GetNickName()
        {
            var info = GetUserInfo();
            if (info != null && info.Count > 0)
            {
                return info["nickname"].ToString();
            }
            return string.Empty;
        }
    }
}
