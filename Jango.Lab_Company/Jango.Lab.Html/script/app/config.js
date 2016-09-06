/**
 * created by jango 2016-09-02
 * 
 * 
 */

define(
    ['app/baseApi']
    ,
    function(base) {
    return{
        lab:{
            interface:{
                courselist:base+"courses",
                userInfobymobile:base+'users/getbymobile',

            }
        }
    }
});
