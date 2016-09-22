using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zero.Log.Util;

namespace WX.Code.lib
{
    public class WxPayApi
    {
        #region 刷卡支付
       /// <summary>
       /// 刷卡支付
       /// </summary>
       /// <param name="inputObj"></param>
       /// <param name="timeOut"></param>
       /// <returns></returns>
        public static WxPayData Micropay(WxPayData inputObj, int _timeOut = 10)
        {
            string _url = "https://api.mch.weixin.qq.com/pay/micropay";
            //检测必填参数
            if (!inputObj.IsSet("body"))
            {
                throw new WxPayException("提交被扫支付API接口中，缺少必填参数body！");
            }
            else if (!inputObj.IsSet("out_trade_no"))
            {
                throw new WxPayException("提交被扫支付API接口中，缺少必填参数out_trade_no！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new WxPayException("提交被扫支付API接口中，缺少必填参数total_fee！");
            }
            else if (!inputObj.IsSet("auth_code"))
            {
                throw new WxPayException("提交被扫支付API接口中，缺少必填参数auth_code！");
            }

            inputObj.SetValue("spbill_create_ip", WxPayConfig.IP);//终端ip
            inputObj.SetValue("appid", WxPayConfig.APPID);//公众账号ID
            inputObj.SetValue("mch_id", WxPayConfig.MCHID);//商户号
            inputObj.SetValue("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名
            string _xml = inputObj.ToXml();

            var _start = DateTime.Now;//请求开始时间

            Log.Debug("WxPayApi", "MicroPay request : " + _xml);
            string _response = HttpService.Post(_xml, _url, false, _timeOut);//调用HTTP通信接口以提交数据到API
            Log.Debug("WxPayApi", "MicroPay response : " + _response);

            var _end = DateTime.Now;
            int _timeCost = (int)((_end - _start).TotalMilliseconds);//获得接口耗时

            //将xml格式的结果转换为对象以返回
            WxPayData _result = new WxPayData();
            _result.FromXml(_response);

            ReportCostTime(_url, _timeCost, _result);//测速上报

            return _result;
        }
        #endregion

        #region 订单查询
        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxPayData OrderQuery(WxPayData _inputObj, int _timeOut = 6)
        {
            string _url = "https://api.mch.weixin.qq.com/pay/orderquery";
            //检测必填参数
            if (!_inputObj.IsSet("out_trade_no") && !_inputObj.IsSet("transaction_id"))
            {
                throw new WxPayException("订单查询接口中，out_trade_no、transaction_id至少填一个！");
            }

            _inputObj.SetValue("appid", WxPayConfig.APPID);//公众账号ID
            _inputObj.SetValue("mch_id", WxPayConfig.MCHID);//商户号
            _inputObj.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
            _inputObj.SetValue("sign", _inputObj.MakeSign());//签名

            string _xml = _inputObj.ToXml();

            var _start = DateTime.Now;

            Log.Debug("WxPayApi", "OrderQuery request : " + _xml);
            string _response = HttpService.Post(_xml, _url, false, _timeOut);//调用HTTP通信接口提交数据
            Log.Debug("WxPayApi", "OrderQuery response : " + _response);

            var _end = DateTime.Now;
            int _timeCost = (int)((_end - _start).TotalMilliseconds);//获得接口耗时

            //将xml格式的数据转化为对象以返回
            WxPayData _result = new WxPayData();
            _result.FromXml(_response);

            ReportCostTime(_url, _timeCost, _result);//测速上报

            return _result;
        }
        #endregion

        #region 订单撤销
        /// <summary>
        /// 订单撤销
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxPayData Reverse(WxPayData _inputObj, int _timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/secapi/pay/reverse";
            //检测必填参数
            if (!_inputObj.IsSet("out_trade_no") && !_inputObj.IsSet("transaction_id"))
            {
                throw new WxPayException("撤销订单API接口中，参数out_trade_no和transaction_id必须填写一个！");
            }

            _inputObj.SetValue("appid", WxPayConfig.APPID);//公众账号ID
            _inputObj.SetValue("mch_id", WxPayConfig.MCHID);//商户号
            _inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            _inputObj.SetValue("sign", _inputObj.MakeSign());//签名
            string _xml = _inputObj.ToXml();

            var _start = DateTime.Now;//请求开始时间

            Log.Debug("WxPayApi", "Reverse request : " + _xml);

            string _response = HttpService.Post(_xml, url, true, _timeOut);

            Log.Debug("WxPayApi", "Reverse response : " + _response);

            var _end = DateTime.Now;
            int _timeCost = (int)((_end - _start).TotalMilliseconds);

            WxPayData _result = new WxPayData();
            _result.FromXml(_response);

            ReportCostTime(url, _timeCost, _result);//测速上报

            return _result;
        }
        #endregion

        #region 申请退款
          /// <summary>
         /// 申请退款
          /// </summa申请退款ry>
          /// <param name="_inputObj"></param>
          /// <param name="_timeOut"></param>
          /// <returns></returns>
        public static WxPayData Refund(WxPayData _inputObj, int _timeOut = 6)
        {
            string _url = "https://api.mch.weixin.qq.com/secapi/pay/refund";
            //检测必填参数
            if (!_inputObj.IsSet("out_trade_no") && !_inputObj.IsSet("transaction_id"))
            {
                throw new WxPayException("退款申请接口中，out_trade_no、transaction_id至少填一个！");
            }
            else if (!_inputObj.IsSet("out_refund_no"))
            {
                throw new WxPayException("退款申请接口中，缺少必填参数out_refund_no！");
            }
            else if (!_inputObj.IsSet("total_fee"))
            {
                throw new WxPayException("退款申请接口中，缺少必填参数total_fee！");
            }
            else if (!_inputObj.IsSet("refund_fee"))
            {
                throw new WxPayException("退款申请接口中，缺少必填参数refund_fee！");
            }
            else if (!_inputObj.IsSet("op_user_id"))
            {
                throw new WxPayException("退款申请接口中，缺少必填参数op_user_id！");
            }

            _inputObj.SetValue("appid", WxPayConfig.APPID);//公众账号ID
            _inputObj.SetValue("mch_id", WxPayConfig.MCHID);//商户号
            _inputObj.SetValue("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            _inputObj.SetValue("sign", _inputObj.MakeSign());//签名

            string _xml = _inputObj.ToXml();
            DateTime _start = DateTime.Now;

            Log.Debug("WxPayApi", "Refund request : " + _xml);
            string _response = HttpService.Post(_xml, _url, true, _timeOut);//调用HTTP通信接口提交数据到API
            Log.Debug("WxPayApi", "Refund response : " + _response);

            DateTime _end = DateTime.Now;
            int _timeCost = (int)((_end - _start).TotalMilliseconds);//获得接口耗时

            //将xml格式的结果转换为对象以返回
            WxPayData _result = new WxPayData();
            _result.FromXml(_response);

            ReportCostTime(_url, _timeCost, _result);//测速上报

            return _result;
        }
        #endregion

        #region 查询退款
        /**
	    * 
	    * 查询退款
	    * 提交退款申请后，通过该接口查询退款状态。退款有一定延时，
	    * 用零钱支付的退款20分钟内到账，银行卡支付的退款3个工作日后重新查询退款状态。
	    * out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个
	    * @param WxPayData inputObj 提交给查询退款API的参数
	    * @param int timeOut 接口超时时间
	    * @throws WxPayException
	    * @return 成功时返回，其他抛异常
	    */
       /// <summary>
       /// 查询退款
       /// </summary>
       /// <param name="inputObj"></param>
       /// <param name="timeOut"></param>
       /// <returns></returns>
        public static WxPayData RefundQuery(WxPayData _inputObj, int _timeOut = 6)
        {
            string _url = "https://api.mch.weixin.qq.com/pay/refundquery";
            //检测必填参数
            if (!_inputObj.IsSet("out_refund_no") && !_inputObj.IsSet("out_trade_no") &&
                !_inputObj.IsSet("transaction_id") && !_inputObj.IsSet("refund_id"))
            {
                throw new WxPayException("退款查询接口中，out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个！");
            }

            _inputObj.SetValue("appid", WxPayConfig.APPID);//公众账号ID
            _inputObj.SetValue("mch_id", WxPayConfig.MCHID);//商户号
            _inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            _inputObj.SetValue("sign", _inputObj.MakeSign());//签名

            string xml = _inputObj.ToXml();

            var _start = DateTime.Now;//请求开始时间

            Log.Debug("WxPayApi", "RefundQuery request : " + xml);
            string _response = HttpService.Post(xml, _url, false, _timeOut);//调用HTTP通信接口以提交数据到API
            Log.Debug("WxPayApi", "RefundQuery response : " + _response);

            var _end = DateTime.Now;
            int _timeCost = (int)((_end - _start).TotalMilliseconds);//获得接口耗时

            //将xml格式的结果转换为对象以返回
            WxPayData _result = new WxPayData();
            _result.FromXml(_response);

            ReportCostTime(_url, _timeCost, _result);//测速上报

            return _result;
        }
        #endregion

        #region 下载对账单
        /// <summary>
        /// 下载对账单
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxPayData DownloadBill(WxPayData _inputObj, int _timeOut = 6)
        {
            string _url = "https://api.mch.weixin.qq.com/pay/downloadbill";
            //检测必填参数
            if (!_inputObj.IsSet("bill_date"))
            {
                throw new WxPayException("对账单接口中，缺少必填参数bill_date！");
            }

            _inputObj.SetValue("appid", WxPayConfig.APPID);//公众账号ID
            _inputObj.SetValue("mch_id", WxPayConfig.MCHID);//商户号
            _inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            _inputObj.SetValue("sign", _inputObj.MakeSign());//签名

            string _xml = _inputObj.ToXml();

            Log.Debug("WxPayApi", "DownloadBill request : " + _xml);
            string _response = HttpService.Post(_xml, _url, false, _timeOut);//调用HTTP通信接口以提交数据到API
            Log.Debug("WxPayApi", "DownloadBill result : " + _response);

            WxPayData _result = new WxPayData();
            //若接口调用失败会返回xml格式的结果
            if (_response.Substring(0, 5) == "<xml>")
            {
                _result.FromXml(_response);
            }
            //接口调用成功则返回非xml格式的数据
            else
                _result.SetValue("result",_response);

            return _result;
        }
        #endregion

        #region  转换短链接
        /**
	    * 
	    * 转换短链接
	    * 该接口主要用于扫码原生支付模式一中的二维码链接转成短链接(weixin://wxpay/s/XXXXXX)，
	    * 减小二维码数据量，提升扫描速度和精确度。
	    * @param WxPayData inputObj 提交给转换短连接API的参数
	    * @param int timeOut 接口超时时间
	    * @throws WxPayException
	    * @return 成功时返回，其他抛异常
	    */
        /// <summary>
        /// 转换短链接
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxPayData ShortUrl(WxPayData _inputObj, int _timeOut = 6)
        {
            string _url = "https://api.mch.weixin.qq.com/tools/shorturl";
            //检测必填参数
            if (!_inputObj.IsSet("long_url"))
            {
                throw new WxPayException("需要转换的URL，签名用原串，传输需URL encode！");
            }

            _inputObj.SetValue("appid", WxPayConfig.APPID);//公众账号ID
            _inputObj.SetValue("mch_id", WxPayConfig.MCHID);//商户号
            _inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串	
            _inputObj.SetValue("sign", _inputObj.MakeSign());//签名
            string _xml = _inputObj.ToXml();

            var _start = DateTime.Now;//请求开始时间

            Log.Debug("WxPayApi", "ShortUrl request : " + _xml);
            string _response = HttpService.Post(_xml, _url, false, _timeOut);
            Log.Debug("WxPayApi", "ShortUrl response : " + _response);

            var _end = DateTime.Now;
            int _timeCost = (int)((_end - _start).TotalMilliseconds);

            WxPayData _result = new WxPayData();
            _result.FromXml(_response);
            ReportCostTime(_url, _timeCost, _result);//测速上报

            return _result;
        }
        #endregion
        #region 统一下单
        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="_inputObj"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxPayData UnifiedOrder(WxPayData _inputObj, int _timeOut = 6)
        {
            string _url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            //检测必填参数
            if (!_inputObj.IsSet("out_trade_no"))
            {
                throw new WxPayException("缺少统一支付接口必填参数out_trade_no！");
            }
            else if (!_inputObj.IsSet("body"))
            {
                throw new WxPayException("缺少统一支付接口必填参数body！");
            }
            else if (!_inputObj.IsSet("total_fee"))
            {
                throw new WxPayException("缺少统一支付接口必填参数total_fee！");
            }
            else if (!_inputObj.IsSet("trade_type"))
            {
                throw new WxPayException("缺少统一支付接口必填参数trade_type！");
            }

            //关联参数
            if (_inputObj.GetValue("trade_type").ToString() == "JSAPI" && !_inputObj.IsSet("openid"))
            {
                throw new WxPayException("统一支付接口中，缺少必填参数openid！trade_type为JSAPI时，openid为必填参数！");
            }
            if (_inputObj.GetValue("trade_type").ToString() == "NATIVE" && !_inputObj.IsSet("product_id"))
            {
                throw new WxPayException("统一支付接口中，缺少必填参数product_id！trade_type为JSAPI时，product_id为必填参数！");
            }
            //异步通知url未设置，则使用配置文件中的url
            if (!_inputObj.IsSet("notify_url"))
            {
                _inputObj.SetValue("notify_url", WxPayConfig.NOTIFY_URL);//异步通知url
            }

            _inputObj.SetValue("appid", WxPayConfig.APPID);//公众账号ID
            _inputObj.SetValue("mch_id", WxPayConfig.MCHID);//商户号
            _inputObj.SetValue("spbill_create_ip", WxPayConfig.IP);//终端ip	 
            _inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串 

            //签名
            _inputObj.SetValue("sign", _inputObj.MakeSign());

            Log.Debug("input", _inputObj.ToXml());
            string _xml = _inputObj.ToXml();

            var _start = DateTime.Now;

            Log.Debug("WxPayApi", "UnfiedOrder request : " + _xml);
            string _response = HttpService.Post(_xml, _url, false, _timeOut);
            Log.Debug("WxPayApi", "UnfiedOrder response : " + _response);

            var _end = DateTime.Now;
            int _timeCost = (int)((_end - _start).TotalMilliseconds);

            WxPayData _result = new WxPayData();
            _result.FromXml(_response);

            ReportCostTime(_url, _timeCost, _result);//测速上报

            return _result;
        }
        #endregion 

        #region 关闭订单
        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxPayData CloseOrder(WxPayData _inputObj, int _timeOut = 6)
        {
            string _url = "https://api.mch.weixin.qq.com/pay/closeorder";
            //检测必填参数
            if (!_inputObj.IsSet("out_trade_no"))
            {
                throw new WxPayException("关闭订单接口中，out_trade_no必填！");
            }

            _inputObj.SetValue("appid", WxPayConfig.APPID);//公众账号ID
            _inputObj.SetValue("mch_id", WxPayConfig.MCHID);//商户号
            _inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串		
            _inputObj.SetValue("sign", _inputObj.MakeSign());//签名
            string _xml = _inputObj.ToXml();

            var _start = DateTime.Now;//请求开始时间

            string response = HttpService.Post(_xml, _url, false, _timeOut);

            var _end = DateTime.Now;
            int _timeCost = (int)((_end - _start).TotalMilliseconds);

            WxPayData _result = new WxPayData();
            _result.FromXml(response);

            ReportCostTime(_url, _timeCost, _result);//测速上报

            return _result;
        }
        #endregion
        #region 测速上报
        /**
	    * 
	    * 测速上报
	    * @param string interface_url 接口URL
	    * @param int timeCost 接口耗时
	    * @param WxPayData inputObj参数数组
	    */
        private static void ReportCostTime(string _interface_url, int _timeCost, WxPayData _inputObj)
        {
            //如果不需要进行上报
            if (WxPayConfig.REPORT_LEVENL == 0)
            {
                return;
            }

            //如果仅失败上报
            if (WxPayConfig.REPORT_LEVENL == 1 && _inputObj.IsSet("return_code") && _inputObj.GetValue("return_code").ToString() == "SUCCESS" &&
             _inputObj.IsSet("result_code") && _inputObj.GetValue("result_code").ToString() == "SUCCESS")
            {
                return;
            }

            //上报逻辑
            WxPayData _data = new WxPayData();
            _data.SetValue("interface_url", _interface_url);
            _data.SetValue("execute_time_", _timeCost);
            //返回状态码
            if (_inputObj.IsSet("return_code"))
            {
                _data.SetValue("return_code", _inputObj.GetValue("return_code"));
            }
            //返回信息
            if (_inputObj.IsSet("return_msg"))
            {
                _data.SetValue("return_msg", _inputObj.GetValue("return_msg"));
            }
            //业务结果
            if (_inputObj.IsSet("result_code"))
            {
                _data.SetValue("result_code", _inputObj.GetValue("result_code"));
            }
            //错误代码
            if (_inputObj.IsSet("err_code"))
            {
                _data.SetValue("err_code", _inputObj.GetValue("err_code"));
            }
            //错误代码描述
            if (_inputObj.IsSet("err_code_des"))
            {
                _data.SetValue("err_code_des", _inputObj.GetValue("err_code_des"));
            }
            //商户订单号
            if (_inputObj.IsSet("out_trade_no"))
            {
                _data.SetValue("out_trade_no", _inputObj.GetValue("out_trade_no"));
            }
            //设备号
            if (_inputObj.IsSet("device_info"))
            {
                _data.SetValue("device_info", _inputObj.GetValue("device_info"));
            }

            try
            {
                Report(_data);
            }
            catch (WxPayException ex)
            {
                //不做任何处理
            }
        }
        #endregion
        #region 测速上报
        /// <summary>
        /// 测速上报
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxPayData Report(WxPayData _inputObj, int _timeOut = 1)
        {
            string _url = "https://api.mch.weixin.qq.com/payitil/report";
            //检测必填参数
            if (!_inputObj.IsSet("interface_url"))
            {
                throw new WxPayException("接口URL，缺少必填参数interface_url！");
            }
            if (!_inputObj.IsSet("return_code"))
            {
                throw new WxPayException("返回状态码，缺少必填参数return_code！");
            }
            if (!_inputObj.IsSet("result_code"))
            {
                throw new WxPayException("业务结果，缺少必填参数result_code！");
            }
            if (!_inputObj.IsSet("user_ip"))
            {
                throw new WxPayException("访问接口IP，缺少必填参数user_ip！");
            }
            if (!_inputObj.IsSet("execute_time_"))
            {
                throw new WxPayException("接口耗时，缺少必填参数execute_time_！");
            }

            _inputObj.SetValue("appid", WxPayConfig.APPID);//公众账号ID
            _inputObj.SetValue("mch_id", WxPayConfig.MCHID);//商户号
            _inputObj.SetValue("user_ip", WxPayConfig.IP);//终端ip
            _inputObj.SetValue("time", DateTime.Now.ToString("yyyyMMddHHmmss"));//商户上报时间	 
            _inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            _inputObj.SetValue("sign", _inputObj.MakeSign());//签名
            string _xml = _inputObj.ToXml();

            Log.Info("WxPayApi", "Report request : " + _xml);

            string _response = HttpService.Post(_xml, _url, false, _timeOut);

            Log.Info("WxPayApi", "Report response : " + _response);

            WxPayData _result = new WxPayData();
            _result.FromXml(_response);
            return _result;
        }
        #endregion 
        #region  根据当前系统时间加随机序列来生成订单号
        /// <summary>
        /// 根据当前系统时间加随机序列来生成订单号
        /// </summary>
        /// <returns></returns>
        public static string GenerateOutTradeNo()
        {
            var ran = new Random();
            return string.Format("{0}{1}{2}", WxPayConfig.MCHID, DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
        }
        #endregion
        #region 时间戳
        /// <summary>
        /// 时间戳
        /// </summary>
        /// <returns></returns>
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        #endregion
        #region 随机字符串
        /// <summary>
        /// 随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        #endregion
    }
}
