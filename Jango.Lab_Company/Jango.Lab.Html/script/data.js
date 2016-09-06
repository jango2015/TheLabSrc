/**
 * created by jangocheng on 2016-09-02
 * 
 */
require.config({
    baseUrl:'script',
    paths:{
         jquery:'plugins/zepto.1.2.min',
         backbone:'plugins/backbone.1.3.3.min',
         underscore:'plugins/underscore.1.8.3.min',
         then:'plugins/then.min',
         csls:['css!../../style.css','css!../../css/style2.css','css!../../style3.css'],
    },
    map:{
        '*':{
          'css':'plugins/css'
        }
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
