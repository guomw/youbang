<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hxresult.aspx.cs" Inherits="msite.hxresult" %>

<!doctype html>
<html>
<head>
    <meta name="viewport" content="width=device-width,minimum-scale=1,user-scalable=no,maximum-scale=1,initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="description" content="" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/weui.min.css">
    <link rel="stylesheet" type="text/css" href="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.css">
    <link rel="stylesheet" type="text/css" href="css/ybdiy.css">
    <title></title>
</head>
<body class="ddbg b">

    <div class="weui_msg">

        <h2 class="weui_msg_title"><%=resultTitle %></h2>
        <div class="weui_icon_area"><i class="weui_icon_msg <%=resultClass %>"></i></div>
        <div class="weui_text_area">
            <div style="font-size: 18px; margin: 0 2%; background-color: #fff; padding: 10px 20px; border-radius: 4px">
                <p style="font-size: 16px"><b>编号: <%=couponInfo.CouponNo %></b></p>
                <p><%=couponInfo.Title %></p>
                <p>使用期限</p>
                <p><%=couponInfo.time %></p>
            </div>
        </div>
        <%if (couponType == 200)
            { %>
        <div class="weui_opr_area">
            <p class="weui_btn_area">
                <a href="javascript:submitHx();" class="weui_btn weui_btn_primary anniu">提交确认</a>
            </p>
        </div>
        <%} %>
    </div>
</body>
</html>
<script src="http://resali.huobanplus.com/cdn/jquery/2.2.4/jquery.min.js"></script>
<script src="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.js"></script>
<script src="js/fastclick.js"></script>
<script src="js/Jquery.util.js"></script>
<script src="js/couponHelper.js"></script>
<script>
    var currentUserId = '<%=userInfo.UserId%>';
    $(function () {
        FastClick.attach(document.body);
    });
</script>
