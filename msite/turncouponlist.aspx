<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="turncouponlist.aspx.cs" Inherits="msite.turncouponlist" %>

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
<script src="js/Jquery.util.js"></script>
<script src="js/wxShare.js"></script>
<script src="js/pullToRefresh.js"></script>
<script src="js/couponHelper.js"></script>
<script>
    wxShare.shareData.title = "测试分享标题";
    wxShare.shareData.desc = "测试分享详情";
    wxShare.shareData.img_url = "https://mp.weixin.qq.com/misc/getheadimg?token=712455382&fakeid=2390356254&r=358626";    
    wxShare.shareData.link = "http://" + window.location.host + "/turncouponlist.aspx?fr=list&userid=" + userid + "&bid=" + brandId + "&gid=" + goodsId;
    wxShare.shareUserId = hotUtil.getQuery("userid", 0);
    ctxUserId = '<%=userInfo.UserId%>';
    $(function () {
        couponlist(hotUtil.pageIndex);
        $('.hot-pullToRefresh').on("pull-to-refresh", function () {
            couponlist(1);
        });
    });
</script>
