<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="coupondetail.aspx.cs" Inherits="msite.coupondetail" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta name="viewport" content="width=device-width,minimum-scale=1,user-scalable=no,maximum-scale=1,initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="description" content="" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="JSDK/RegConfig.aspx?debug=0"></script>
    <link href="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/weui.min.css" rel="stylesheet" type="text/css" />
    <link href="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/ybdiy.css" />
    <title></title>
    <style>
        html {
            background-color: #2F2F2F;
        }
    </style>
</head>
<body>
    <div class="hjkf">
        <p style="float: right">
            <img src="images/mengbanzis.png" style="width: 200px; display: block; margin-right: 20px">
        </p>
        <p style="clear: both"></p>
        <div class="lqkk">
            <div class="q-card_content_pane">
                <!--用户券中心列表-->
                <a class="q-coupon_item zhuanfa">
                    <p class="date"><%=couponInfo.Title %></p>
                    <p class="time" title=""><%=couponInfo.time %></p>
                    <p class="pingpai" title=""><span><%=couponInfo.BrandName %></span><span>【<%=couponInfo.GoodsName %>】</span></p>
                    <p class="name" title="">剩余<%=couponInfo.Amounts %>张</p>
                    <div class="q-count">
                        <em>¥</em><%=couponInfo.Money.ToString("f2").Split('.')[0] %>.<span><%=couponInfo.Money.ToString("f2").Split('.')[1] %></span>
                    </div>
                </a>
            </div>
        </div>
    </div>
    <div class="weui_panel_bd">
        <div class="weui_media_box weui_media_text" style="padding-top: 4px;">
            <p class="weui_media_desc" style="font-size: 12px; color: #888">点击APP右上角转发到微信朋友圈等。</p>
        </div>
        <p style="height: 30px"></p>
    </div>
</body>
</html>
<script src="http://resali.huobanplus.com/cdn/jquery/2.2.4/jquery.min.js"></script>
<script src="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.js"></script>
<script src="js/Jquery.util.js"></script>
<script src="js/couponHelper.js"></script>
<script src="js/wxShare.js"></script>
<script>
    wxShare.shareData.title = "<%=couponInfo.Title %>";
    wxShare.shareData.desc = "测试分享详情";
    wxShare.shareData.img_url = "https://mp.weixin.qq.com/misc/getheadimg?token=712455382&fakeid=2390356254&r=358626";
    wxShare.shareData.link = "http://" + window.location.host + "/couponget.aspx?userid=" + userid + "&couponid=" + couponid;
    wxShare.shareUserId = userid;
    wxShare.shareCouponId = couponid;
    wxShare.callback = insertShareLog;

</script>
