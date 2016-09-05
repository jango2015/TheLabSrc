/**
 * Created by 潘学平 on 2016/8/10.
 */
/*会员中心界面事件*/
$(function(){
    $("#member_tabs .order").on("click",function(){
        $(this).addClass("on").siblings().removeClass("on");
        $("#tabs_ct .order_ct").css("display","block");
        $("#tabs_ct .history_ct").css("display","none");
        $("#tabs_ct .appointment_ct").css("display","none");
    });
    $("#member_tabs .appointment").on("click",function(){
        $(this).addClass("on").siblings().removeClass("on");
        $("#tabs_ct .appointment_ct").css("display","block");
        $("#tabs_ct .order_ct").css("display","none");
        $("#tabs_ct .history_ct").css("display","none");
    });
    $("#member_tabs .history").on("click",function(){
        $(this).addClass("on").siblings().removeClass("on");
        $("#tabs_ct .history_ct").css("display","block");
        $("#tabs_ct .order_ct").css("display","none");
        $("#tabs_ct .appointment_ct").css("display","none");
    });
    $("#tabs_ct .order_ct .order_ct_tabs .order_obligations").on("click",function(){
        $(this).addClass("on").siblings().removeClass("on");
        $("#tabs_ct .order_ct .order_ct_tabs_ct .order_obligations_ct").css("display","block");
        $("#tabs_ct .order_ct .order_ct_tabs_ct .order_unfinished_ct").css("display","none");
        $("#tabs_ct .order_ct .order_ct_tabs_ct .order_history_ct").css("display","none");
    });
    $("#tabs_ct .order_ct .order_ct_tabs .order_unfinished").on("click",function(){
        $(this).addClass("on").siblings().removeClass("on");
        $("#tabs_ct .order_ct .order_ct_tabs_ct .order_unfinished_ct").css("display","block");
        $("#tabs_ct .order_ct .order_ct_tabs_ct .order_obligations_ct").css("display","none");
        $("#tabs_ct .order_ct .order_ct_tabs_ct .order_history_ct").css("display","none");
    });
    $("#tabs_ct .order_ct .order_ct_tabs .order_history").on("click",function(){
        $(this).addClass("on").siblings().removeClass("on");
        $("#tabs_ct .order_ct .order_ct_tabs_ct .order_history_ct").css("display","block");
        $("#tabs_ct .order_ct .order_ct_tabs_ct .order_obligations_ct").css("display","none");
        $("#tabs_ct .order_ct .order_ct_tabs_ct .order_unfinished_ct").css("display","none");
    });
});
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
/*修改资料界面事件*/
$(function(){
    $("#revise_data input[type=date]").blur(function(){
        var text=$(this).val();
        var month=Number(text.substr(5,2));
        var date=text.substr(8,2);
        if(month == 1 && date >=20 || month == 2 && date <=18){
            $("#revise_data .constellation").text("水瓶座")
        }
        if(month == 2 && date >=19 || month == 3 && date <=20){
            $("#revise_data .constellation").text("双鱼座")
        }
        if(month == 3 && date >=21 || month == 4 && date <=19){
            $("#revise_data .constellation").text("白羊座")
        }
        if(month == 4 && date >=20 || month == 5 && date <=20){
            $("#revise_data .constellation").text("金牛座")
        }
        if(month == 5 && date >=21 || month == 6 && date <=21){
            $("#revise_data .constellation").text("双子座")
        }
        if(month == 6 && date >=22 || month == 7 && date <=22){
            $("#revise_data .constellation").text("巨蟹座")
        }
        if(month == 7 && date >=23 || month == 8 && date <=22){
            $("#revise_data .constellation").text("狮子座")
        }
        if(month == 8 && date >=23 || month == 9 && date <=22){
            $("#revise_data .constellation").text("室女座")
        }
        if(month == 9 && date >=23 || month == 10 && date <=22){
            $("#revise_data .constellation").text("天秤座")
        }
        if(month == 10 && date >=23 || month == 11 && date <=21){
            $("#revise_data .constellation").text("天蝎座")
        }
        if(month == 11 && date >=22 || month == 12 && date <=21){
            $("#revise_data .constellation").text("人马座")
        }
        if(month == 12 && date >=22 || month == 1 && date <=19){
            $("#revise_data .constellation").text("摩羯座")
        }
    })
});
