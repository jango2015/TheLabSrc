/**
 * created by jango 2016-09-06
 * 
 */
define([
    'jquery',
    'view/BaseView',
    'model/UserModel',
    'view/templates',
    'underscore',
    'backbone'
], function ($, b, UserModel, templates, _, Backbone) {
    'use strict';
    return b.extend({
        el: '#content-container',
        $viewName: '',
        title: 'aa',
        template: templates['widget.member.header'],
        events: {
            "click .myorder ": "orderlists",
            "click .myreserve": 'reservelists',
            "click .coursehis": "courselist"
        },
        initialize: function () {
            this.user=(new UserModel()).load();
            this.listenTo(this.user,'loaded',this.render);
        },
      
        render: function () {
            // console.log(this.user);
            this.$el.append(this.template(this.user));
            return this;
        }
    });
});