/**
 * created by jangocheng on 2016-09-02
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
            '(!/)blogs/:id':'detail',
            'revisedata':'revisedata'
        },
        blogs:function(){
            require([
                'view/UserView'
            ],function(UserView){
                var view = new UserView();
            });
        },
        revisedata:function(){
            require([
                'view/Revisedataview'
                ,'csls'
            ],function(UserView,util){
                var view = new UserView();
            });
        }
    });
  return new Router();
    
});