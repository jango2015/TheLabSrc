/*课程详情界面事件*/
$(function(){
    $("#details nav #picker").click(function(){
        $("#details .rs").css("display","block");
        $("#details .rs li").click(function(){
            $("#details .rs").css("display","none");
            var num=$(this).text();
            $("#details nav #picker").text(num+"人");
        })
    })
});
/*商品列表界面事件*/
$(function(){
    $("#product .order .shop").click(function(){
        $("#product .shop_order").toggle();
    })
});

