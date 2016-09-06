/**
 * created by jango 2016-09-06
 * 
 */
define([
    'jquery',
    'view/temp',
    'view/BaseView',
    'underscore'
], function ($, temps, b, UserModel, _) {
    'use strict';
    return b.extend({
        el: '#content-container',
        $viewName: '',
        title: 'aa',
        events: {
        },
        initialize: function () {
            console.log('aaaa');
            this.render();
        },
        setuserinfo: function () {

        },
        render: function () {
            console.log(temps);
            var temp = temps.revisedata;
            console.log(temps.revisedata);
            this.$el.html(temp);
            return this;
        }
    });
});