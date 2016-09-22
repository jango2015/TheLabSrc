﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WX.Code.lib;
using Zero.Log.Util;

namespace WX.Code.business
{
    public class NativePay
    {
        /**
        * 生成扫描支付模式一URL
        * @param productId 商品ID
        * @return 模式一URL
        */
        public string GetPrePayUrl(string _productId)
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

        /**
        * 生成直接支付url，支付url有效期为2小时,模式二
        * @param productId 商品ID
        * @return 模式二URL
        */
        public string GetPayUrl(string productId)
        {
            Log.Info(this.GetType().ToString(), "Native pay mode 2 url is producing...");

            WxPayData data = new WxPayData();
            data.SetValue("body", "test");//商品描述
            data.SetValue("attach", "test");//附加数据
            data.SetValue("out_trade_no", WxPayApi.GenerateOutTradeNo());//随机字符串
            data.SetValue("total_fee", 1);//总金额
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
            data.SetValue("goods_tag", "jjj");//商品标记
            data.SetValue("trade_type", "NATIVE");//交易类型
            data.SetValue("product_id", productId);//商品ID

            WxPayData result = WxPayApi.UnifiedOrder(data);//调用统一下单接口
            string url = result.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接

            Log.Info(this.GetType().ToString(), "Get native pay mode 2 url : " + url);
            return url;
        }

        /**
        * 参数数组转换为url格式
        * @param map 参数名与参数值的映射表
        * @return URL字符串
        */
        private string ToUrlParams(SortedDictionary<string, object> map)
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in map)
            {
                buff += pair.Key + "=" + pair.Value + "&";
            }
            buff = buff.Trim('&');
            return buff;
        }
    }
}
