/**
 * created by jangocheng on 2016-09-02 14:10:25
 * 
 */
define([
    'jquery',
    'underscore',
    'backbone',
    'router'
], function($,_,Backbone,router) {
    'use strict';

    var App = {
        initialize:function(){
        require([
        ],function(){
             Backbone.history.start();
        })
        }
    };
    return App;
});