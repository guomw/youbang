<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rebatelist.aspx.cs" Inherits="msite.rebatelist" %>

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
    <title>返利明细</title>
</head>
<body class="ddbg">
    <div class="bd">
        <div class="sl-zdy">
            <ul class="sl-float-ul  pjf_1ge ">
                <li style="text-align: left; margin-left: 15px">
                    <p class="bm coupongetamount">0.00</p>
                    <p class="sm">&nbsp;返利总金额/元</p>
                </li>
            </ul>
        </div>

        <div class="p6t3 hot-pullToRefresh">
            <div class="weui_cells_title">返利明细</div>

            <div class="weui_cells liushui" id="listMode">
            </div>

        </div>
    </div>


    <script type="text/template" id="couponlistTemplate">
        <div class="weui_cell">
            <div class="weui_cell_bd weui_cell_primary">
                <p style="font-size: 16px;"><b>{Remark}</b></p>
                <p style="font-size: 12px; color: #ccc">{Time}</p>
            </div>
            <div class="weui_cell_ft red">{Money}元</div>
        </div>
    </script>

</body>
</html>
<script src="http://resali.huobanplus.com/cdn/jquery/2.2.4/jquery.min.js"></script>
<script src="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.js"></script>
<script src="js/pullToRefresh.js"></script>
<script src="js/Jquery.util.js"></script>
<script src="js/couponHelper.js"></script>
<script>
    $(function () {

        $(".coupongetamount").text(fmoney(hotUtil.getQuery("c", 0)));

        myRebateList(hotUtil.pageIndex);
        $('.hot-pullToRefresh').on("pull-to-refresh", function () {
            myRebateList(1);
        });
    });
</script>