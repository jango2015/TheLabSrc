using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Code.pay
{
    public class PayModel
    {
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
}
