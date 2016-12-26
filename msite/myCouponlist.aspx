<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myCouponlist.aspx.cs" Inherits="msite.myCouponlist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width,minimum-scale=1,user-scalable=no,maximum-scale=1,initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我的券</title>
    <link href="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/weui.min.css" rel="stylesheet" type="text/css" />
    <link href="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/ybdiy.css" />
</head>
<body class="ddbg">
    <%if (UserType == 1)
        { %>
    <div class="bd">
        <div class="weui_tab">
            <div class="weui_navbar s vs">
                <a class="weui_navbar_item weui_bar_item_on tab0" onclick="toTab(0)">全部
                </a>
                <a class="weui_navbar_item tab1" onclick="toTab(1)">已过期
                </a>
            </div>
            <div class="weui_tab_bd">
            </div>
        </div>
    </div>
    <%} %>

    <div style="padding-bottom: 24px;" class="hot-pullToRefresh">

        <div id="listMode">
             <img src="images/klkl.jpg" style="width: 100%" />
        </div>

    </div>


    <script type="text/template" id="couponlistTemplate">
        <div class="q-card_content_pane">
            <!--券中心列表“+zhuanfa”-->
            <a class="q-coupon_item {class}" href="{url}">
                <p class="date">{Title}</p>
                <p class="ids">使用期限</p>
                <p class="id">编号:{CouponNo} </p>
                <p class="time" title="">{time}</p>
                <p class="name" title="">{Amounts}</p>
                <p class="names" title="">{Amounts}</p>
                <div class="q-count">
                    <em>¥</em>{Money}.<span>{Money2}</span>
                    <p class="dsfe">{LabalText}</p>
                </div>
            </a>
        </div>
    </script>
</body>
</html>
<script src="http://resali.huobanplus.com/cdn/jquery/2.2.4/jquery.min.js"></script>
<script src="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.js"></script>
<script src="/js/Jquery.util.js"></script>
<script src="/js/pullToRefresh.js"></script>
<script src="/js/couponHelper.js"></script>
<script>
    $(function () {        
        usertype = "<%=UserType%>";
        userid = "<%=UserId%>";
        myCouponlist(hotUtil.pageIndex);
        $('.hot-pullToRefresh').on("pull-to-refresh", function () {
            myCouponlist(1);
        });
    });

    function toTab(code) {
        tabtype = code;
        $(".weui_bar_item_on").removeClass("weui_bar_item_on");
        $(".tab" + code).addClass("weui_bar_item_on");
        $("#listMode").html('<img src="images/klkl.jpg" style="width: 100%" />');
        myCouponlist(1);
    }
</script>
