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
            return JsonMapper.ToObject(str);

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
    }
}
