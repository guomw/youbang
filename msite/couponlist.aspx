<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="couponlist.aspx.cs" Inherits="msite.couponlist" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width,minimum-scale=1,user-scalable=no,maximum-scale=1,initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>优惠券中心</title>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="JSDK/RegConfig.aspx?debug=0"></script>
    <link href="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/weui.min.css" rel="stylesheet" type="text/css" />
    <link href="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/ybdiy.css" />
</head>
<body class="ddbg">
    <div style="padding-bottom: 24px;" class="hot-pullToRefresh">

        <div id="listMode">
            <img src="images/klkl.jpg" style="width: 100%" />
        </div>

    </div>
    <script type="text/template" id="couponlistTemplate">
        <div class="q-card_content_pane">
            <!--券中心列表“+zhuanfa”-->
            <a class="q-coupon_item zhuanfa" href="coupondetail.aspx?couponid={couponid}&userid={userid}">
                <p class="date">{Title}</p>
                <p class="ids">使用期限</p>
                <p class="id">编号: </p>
                <p class="time" title="">{time}</p>
                <p class="name" title="">{Amounts}</p>
                <p class="names" title="">{Amounts}</p>
                <div class="q-count">
                    <em>¥</em>{Money}.<span>{Money2}</span>
                    <p class="dsfe">立即转发</p>
                </div>
            </a>
        </div>
    </script>
    <p style="height: 40px"></p>
    <div class="huayang">
        <a href="javascript:;" class="weui_btn weui_btn_default " style="position: fixed; bottom: 0px; left: 0px; right: 0px;" onclick="document.getElementById('mcover').style.display='block';">转发全部</a>
    </div>
    <div id="mcover" onclick="document.getElementById('mcover').style.display='';" style="display: none;">
        <img src="images/mengbanzis.png" />
    </div>

</body>
</html>
<script src="http://resali.huobanplus.com/cdn/jquery/2.2.4/jquery.min.js"></script>
<script src="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.js"></script>
<script src="js/fastclick.js"></script>
<script src="js/Jquery.util.js"></script>
<script src="js/wxShare.js"></script>
<script src="js/pullToRefresh.js"></script>
<script src="js/couponHelper.js"></script>
<script>
    wxShare.shareData.title = "测试分享标题";
    wxShare.shareData.desc = "测试分享详情";
    wxShare.shareData.img_url = "https://mp.weixin.qq.com/misc/getheadimg?token=712455382&fakeid=2390356254&r=358626";
    wxShare.shareData.link = "http://" + window.location.host + "/turncouponlist.aspx?from=list&userid=" + userid;
    wxShare.shareUserId = hotUtil.getQuery("userid", 0);
    wxShare.shareCouponId = -1;
    wxShare.callback = insertShareLog;

    $(function () {
        FastClick.attach(document.body);
        couponlist(hotUtil.pageIndex);
        $('.hot-pullToRefresh').on("pull-to-refresh", function () {
            couponlist(1);
        });
    });
</script>
