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
/*注册界面事件*/
$(function(){
    $("#register .get_yzm").click(function(){
        var val=$("#register .data .phone").val();
        console.log(val)
        var reg=/^1[3|4|5|7|8]\d{9}$/;
        if(val==null||val==""){
            $("#register .xs").text("请输入您的手机号");
            return false;
        }else if(reg.test(val)==false){
            $("#register .xs").text("请输入正确的手机号");
            return false;
        }else {
            $("#register .xs").text("");
        }
    })
});
/*我的账户界面事件*/
$(function(){
    $("#account .tabs .box").click(function(){
        $(this).addClass("active").siblings().removeClass("active");
    })
});
