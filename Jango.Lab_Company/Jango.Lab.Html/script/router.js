/**
 * created by jangocheng on 2016-09-02 14:10:25
 * 
 */
define([
    'jquery',
    'underscore',
    'backbone'
], function(require$,_,Backbone) {
    'use strict';

    var Router = Backbone.Router.extend({
        routes:{
            '(!/)':'blogs',
            '(!/)blogs/:id':'detail'
        },
        blogs:function(){
            require([
                'view/EProductsInDeviceView'
            ],function(productView){
                var view = new productView();
            });
        }
    });
  return new Router();
    
});