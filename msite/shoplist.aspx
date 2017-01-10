<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shoplist.aspx.cs" Inherits="msite.shoplist" %>

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
    <div class="ybdiy hot-pullToRefresh">
        <div class="weui_cells weui_cells_access mdd kong" id="listMode">
             <img src="images/klkl.jpg" class="kongshuju" />
        </div>
    </div>

    <script type="text/template" id="couponlistTemplate">
        <a class="weui_cell" href="{url}">
            <div class="weui_cell_bd weui_cell_primary">
                <p>
                    <img src="images/icon/mendian.png" class="w-icon">{shopName}</p>
            </div>
            <div class="weui_cell_ft" style="font-size: 12px"></div>
        </a>
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
        userid = '<%=userInfo.UserId%>';
        shopList(hotUtil.pageIndex);
        $('.hot-pullToRefresh').on("pull-to-refresh", function () {
            shopList(1);
        });
        
    });      

</script>
