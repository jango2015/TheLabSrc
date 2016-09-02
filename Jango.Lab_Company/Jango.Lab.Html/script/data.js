/**
 * created by jangocheng on 2016-09-02 14:10:25
 * 
 */
require.config({
    baseUrl:'scripts',
    paths:{
         jquery:'vendor/zepto.1.2.min',
         backbone:'vendor/backbone.1.3.3.min',
         underscore:'vendor/underscore.1.8.3.min',
         then:'vendor/then.min'
    },
    shim:{
         
         'backbone':{
              deps:['underscore','jquery'],
              exports:'Backbone'
         },
         'underscore':{
             exports:'_'
         }
    }
    
})

require([
  'app'
], function(App){
  App.initialize();
});
