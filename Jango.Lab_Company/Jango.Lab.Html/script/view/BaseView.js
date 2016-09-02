/**
 * created by jangocheng on 2016-09-02 14:10:25
 * 
 */
define([
    'jquery',
    'underscore',
    'backbone'
], function ($, _, Backbone) {
    'use strict';
    return Backbone.View.extend({
        container: $('body'),
        $viewName: '',
        title: 'aa',
        render: function () {
            this.container.append(this.$el);
            return this;
        }
    });
});