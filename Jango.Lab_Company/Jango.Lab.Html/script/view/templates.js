/**
 * created by jangocheng on 2016-09-02 14:10:25
 * 
 */
define([
        'underscore'
    ]
    ,function (_) {
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
            }
    }
});