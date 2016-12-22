﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="drawmoneylist.aspx.cs" Inherits="msite.drawmoneylist" %>

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
    <title>提现明细</title>
</head>
<body class="ddbg">
    <div class="sl-zdy">
        <ul class="sl-float-ul  pjf_1ge ">
            <li style="text-align: left; margin-left: 15px">
                <p class="bm totalMoney">0.00</p>
                <p class="sm">&nbsp;总共提现金额/元</p>
            </li>
        </ul>
    </div>

    <div class="p6t3 hot-pullToRefresh">

        <div class="weui_cells_title">提现流水</div>
        <div class="tixianlist">
            <ul id="listMode">
                <li>
                    <img src="images/klkl.jpg" style="width: 100%" />
                </li>
            </ul>
        </div>
    </div>
    <script type="text/template" id="couponlistTemplate">
        <li>
            <div class="ao">
                <div class="zh"><b>提现{ApplyMoney}元</b></div>
                <div class="zhs">提现账户：{PayAccount}</div>
                <div class="time">{CreateTime}</div>
                <div class="time">{Remark}</div>
                <div class="zt">{ApplyStatusText}</div>
            </div>
        </li>
    </script>
</body>
</html>
<script src="http://resali.huobanplus.com/cdn/jquery/2.2.4/jquery.min.js"></script>
<script src="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.js"></script>
<script src="js/Jquery.util.js"></script>
<script src="js/pullToRefresh.js"></script>
<script src="js/couponHelper.js"></script>
<script>
    $(function () {
        var m = '<%=TotalMoney%>';
        $(".totalMoney").text(fmoney(m));
        myDrawMoneyList(hotUtil.pageIndex);
        $('.hot-pullToRefresh').on("pull-to-refresh", function () {
            myDrawMoneyList(1);
        });
    });
</script>
