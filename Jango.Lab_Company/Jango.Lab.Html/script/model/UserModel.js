/**
 * 
 * jango 2016-09-06
 * 
 * 
 */

define([
    'jquery',
    'model/BaseModel',
    'model/ConsigneeAddressModel',
    'app/config'
], function($,b,consigneeMod,config) {
    'use strict';
    var UserModel = b.extend({
        defaults:{
            ID:0,
            Name:"",
            Email:"",
            integral:0,
            Balance:0,
            Mobile:'',
            Birthday:'',
            OpenID:'',
            ConsigneeAddress:new consigneeMod()
        },
        load:function(){
            var self = this;        
            $.get(config.lab.interface.userInfobymobile,
            {
                mobile:'aa'
            },function(res){
                // console.log(res);
                self.set(res);
                self.trigger("loaded");
                // console.log(self);
            });
            return self;
        }
    });
    return UserModel;
});