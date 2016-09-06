/**
 * 
 * jango 2016-09-06
 * 
 * 
 */

define([
    'model/BaseModel'
], function(b) {
    'use strict';
    var ConsigneeAddressModel = b.extend({
        defaults:{
            Province:0,
            City:0,
            district:0,
            Address:'',
            Isvalid:false
        }
    });
    return ConsigneeAddressModel;
});