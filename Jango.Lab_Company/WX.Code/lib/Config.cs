using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace WX.Code.lib
{
    public class WxPayConfig
    {
        public static string APPID = ConfigurationManager.AppSettings["APPID"].ToString(); //APPID：绑定支付的APPID（必须配置）
        public static string APPSECRET = ConfigurationManager.AppSettings["APPSECRET"].ToString();//APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置）
        public static string MCHID = ConfigurationManager.AppSettings["MCHID"].ToString();//MCHID：商户号（必须配置） 
        public static string KEY = ConfigurationManager.AppSettings["KEY"].ToString(); // KEY：商户支付密钥，参考开户邮件设置（必须配置）    
        public static string SSLCERT_PATH = ConfigurationManager.AppSettings["SSLCERT_PATH"].ToString();//证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        public static string SSLCERT_PASSWORD = ConfigurationManager.AppSettings["SSLCERT_PASSWORD"].ToString();
        public static string NOTIFY_URL = ConfigurationManager.AppSettings["NOTIFY_URL"].ToString(); //支付结果通知回调url，用于商户接收支付结果
        public static string IP = ConfigurationManager.AppSettings["IP"].ToString(); //商户系统后台机器IP 此参数可手动配置也可在程序中自动获取
        public static string PROXY_URL = ConfigurationManager.AppSettings["PROXY_URL"].ToString(); //代理服务器设置 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        public static int REPORT_LEVENL = Convert.ToInt32(ConfigurationManager.AppSettings["REPORT_LEVENL"].ToString()); //上报信息配置  测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        public static int LOG_LEVENL = Convert.ToInt32(ConfigurationManager.AppSettings["LOG_LEVENL"].ToString()); //日志级别  日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        public static void Reset(object[] _obj) {
            APPID = _obj[0].ToString();
            APPSECRET = _obj[1].ToString();
            MCHID = _obj[2].ToString();
            KEY = _obj[3].ToString();
            SSLCERT_PATH = _obj[4].ToString();
            SSLCERT_PASSWORD = _obj[5].ToString();
        }
    }
}
