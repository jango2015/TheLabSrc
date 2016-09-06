/**
 * created by jangocheng on 2016-09-02 14:10:25
 * 
 */
define([
        'underscore',
        'backbone'
    ]
    ,function (_,b) {
    return {

        "widget.eProductInDevice": function (obj) {
            obj || (obj = {});
            var __t, __p = '',
                __e = _.escape,
                __j = Array.prototype.join;
            with (obj || {}) {
                __p+="<div class='dd'>"
                for (var index = 0; index < obj.length; index++) {
                    var val = obj[index];
                      __p +="<li>"+
                     __e(val.date)+ "</br/>"+
                    __e(val.dateStr)+"</br/>"+
                    __e(val.deviceAddress)+
                    '</li>'
                }
                __p+="</div>";
            }
            return __p;
            },
        'widget.member.header':function(obj){
            obj||(obj={});
            console.log(obj);
             var __t, __p = '',
                __e = _.escape,
                __j = Array.prototype.join;
            with (obj || {}) {
                console.log(obj.attributes);
                console.log( obj.get('Name'));
                __p+="<div id='member'> <header class='bar bar-nav'> <div id='member_top' class='pr cfff'><div class='avatar'></div><div class='xx pa fs14 ml5'><h4 class='fw400'>"+obj.get("Name")+"</h4> <p class='mt5'>我的积分：<span>"+obj.get("Integral")+"</span></p>"+
                "<p class='mt5'>我的余额：<span>"+obj.get("Balance")+"</span></p></div><a href='#revisedata' class='xg pa cfff fs16 setuserinfo'>修改<span class='iconfont icon-xiugai ml10'></span></a><a href='#' class='cz pa fs16 c000 tc charge'>充值</a></div>"+
                "<div id='member_tabs' class='clearfix'><div class='w33 fl order tc on r_line'><span class='iconfont icon-wodedingdan dpb'></span><span class='dpb fs14 myorder'>我的订单</span> </div><div class='w33 fl appointment tc r_line'>"+
                "<span class='iconfont icon-yuyue dpb'></span><span class='dpb fs14 myreserve'>我的预约</span></div><div class='w33 fl history tc'><span class='iconfont icon-lishi dpb'></span><span class='dpb fs14 coursehis'>课程历史</span></div></div></header>"+
                "<div class='content'><div id='tabs_ct'></div></div></div></div>";
            }
            return __p;
        },
       
    }
});