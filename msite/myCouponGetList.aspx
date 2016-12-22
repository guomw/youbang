<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myCouponGetList.aspx.cs" Inherits="msite.myCouponGetList" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width,minimum-scale=1,user-scalable=no,maximum-scale=1,initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>领取用户</title>
    <link href="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/weui.min.css" rel="stylesheet" type="text/css" />
    <link href="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/ybdiy.css" />
    <style>
        .kong .kongshuju {
            width: 100%;
            display: block;
        }

        .kongshuju {
            display: none;
        }

        .kong.weui_cells:after {
            border-bottom: 0px;
        }
    </style>
</head>
<body class="ddbg">
    <div class="bd">
        <div class="weui_tab">
            <div class="weui_navbar s vs">
                <a class="weui_navbar_item weui_bar_item_on tab0" onclick="toTab(0)">全部
                </a>
                <a class="weui_navbar_item tab1" onclick="toTab(1)">回收
                </a>
                <a class="weui_navbar_item tab2" onclick="toTab(2)">已用
                </a>
            </div>
            <div class="weui_tab_bd">
            </div>
        </div>

        <div class="hot-pullToRefresh">
            <div class="weui_cells_title">已有<span class="coupongetamount">0</span>人领取</div>
            <div class="weui_cells liushui kong" id="listMode">
                <img src="images/klkl.jpg" class="kongshuju" />
            </div>
        </div>
    </div>


    <script type="text/template" id="couponlistTemplate">
        <div class="weui_cell">
            <div class="weui_cell_hd">
                <img src="{HeadImg}" alt="" style="width: 45px; margin-right: 5px; display: block" />
            </div>
            <div class="weui_cell_bd weui_cell_primary">
                <p style="font-size: 12px;"><b>{Name}</b></p>
                <p style="font-size: 12px;">券号{CouponNo}</p>
                <p style="font-size: 12px; color: #ccc">{time}</p>
            </div>
            <div class="weui_cell_ft {color}" style="font-size: 12px">{StatusText}</div>
        </div>
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
        $(".coupongetamount").text(hotUtil.getQuery("c", 0));
        myCouponGetList(hotUtil.pageIndex);
        $('.hot-pullToRefresh').on("pull-to-refresh", function () {
            myCouponGetList(1);
        });
    });

    function toTab(code) {
        tabtype = code;
        $(".weui_bar_item_on").removeClass("weui_bar_item_on");
        $(".tab" + code).addClass("weui_bar_item_on");
        $("#listMode").html('<img src="images/klkl.jpg" class="kongshuju" />');
        myCouponGetList(1);
    }
</script>
