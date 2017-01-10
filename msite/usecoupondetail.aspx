<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usecoupondetail.aspx.cs" Inherits="msite.usecoupondetail" %>

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
    <title>优惠券详情</title>
    <style>
        #myQrCode img {
            width: 100%;
            display: block;
        }
    </style>
</head>
<body class="ddbg">

    <div class="lodinggoss">
        <div class="bacfff textc">
            <div style="color: #999; text-align: center; padding: 6px 10%;">
                <p style="font-size: 14px; color: #111;"><b><%=couponInfo.ShopName %></b></p>
                <p style="font-size: 14px">【&nbsp;券编号：<%=couponInfo.CouponNo %>&nbsp;】</p>
                <p style="font-size: 20px; color: #111; line-height: 20px; margin-top: 10px"><b><%=couponInfo.Title %></b></p>
            </div>

            <p style="padding: 6px 15%" id="myQrCode">
            </p>
            <div style="font-size: 12px; color: #666; line-height: 14px; padding: 0px 10%;">
                <p style="margin-bottom: 6px">有效期</p>
                <p>
                    <%=couponInfo.time %>
                </p>
                <p style="font-size: 14px; color: #111; margin-top: 4px"><b><%=couponInfo.BrandName %>【<%=couponInfo.GoodsName %>】</b></p>
                <div style="margin-top: 10px; background-color: #f8f8f8; font-size: 12px; text-align: left; padding: 10px; border-radius: 10px; color: #999">

                    <p>•&nbsp;<%=couponInfo.Remark %></p>
                </div>
            </div>
        </div>
    </div>



</body>
</html>
<script src="http://resali.huobanplus.com/cdn/jquery/2.2.4/jquery.min.js"></script>
<script src="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.js"></script>
<script src="js/jquery-qrcode-0.14.0.min.js"></script>
<script src="js/Jquery.util.js"></script>
<script>
    var userid = hotUtil.getQuery("userid", 0);
    var couponid = hotUtil.getQuery("couponid", 0);
    $(function () {
        $("#myQrCode").qrcode({
            render: "image",
            size: 150,
            text: "http://" + window.location.host + "/hxresult.aspx?couponid=" + couponid + "&userid=" + userid
        });
    });
</script>
