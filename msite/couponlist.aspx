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
    <style>
        #full #weapp {
            height: 100%;
            overflow: hidden;
        }

        * {
            box-sizing: border-box;
        }

        .weapp-content {
            padding: 0px 0px 50px 0px;
            overflow: hidden;
            position: relative;
            height: 100%;
        }

        .we_full_inner {
            position: relative;
            height: 100%;
            overflow: auto;
        }
    </style>
</head>
<body class="ddbg">

    <div id="full" class="weui-popup-container">
        <div id="weapp">
            <div class="weapp-content">
                <div class="we_full_inner">
                    <div class="weui-popup-modal">
                        <article class="weui_article" style="padding: 0px">
                            <section>
                                <div class="con">
                                    <div class="msg">品牌</div>
                                    <ul class="list" id="brandlist">
                                        <li>
                                            <a href="javascript:GetBrandList(0);">全部
                                            </a>
                                        </li>
                                    </ul>
                                    <div class="msg tip" disabled-message="" origin-message="点击选择分类">类型</div>
                                    <ul class="list" data-toggle="menu-unselected" id="goodslist">
                                    </ul>
                                </div>
                            </section>
                        </article>
                    </div>
                </div>
            </div>
            <div style="position: fixed; bottom: 0px; left: 0px; right: 0px;">
                <a href="javascript:couponlist(1);" class="weui_btn weui_btn_plain_primary close-popup" style="border-radius: 0px; border: #FF4949; color: #fff; background-color: #FF4949; padding: 4px 0px">筛选</a>
            </div>
        </div>
    </div>


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
                <p class="pingpai" title=""><span>{BrandName}</span><span>【{GoodsName}】</span></p>
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
    <div class="huayang" style="position: fixed; bottom: 0px; left: 0px; right: 0px;">
        <a href="javascript:;" class="weui_btn weui_btn_default " style="position: fixed; bottom: 0px; width: 100%; padding-right: 58px;" onclick="document.getElementById('mcover').style.display='block';">转发全部</a>
        <a href="javascript:;" data-target="#full" class="weui_btn weui_btn_default open-popup" style="position: absolute; bottom: 0px; right: 0px; height: 58px; line-height: 0px;">
            <img src="images/ssxx.png" style="width: 30px; vertical-align: middle; display: block; padding: 0px; margin-top: 5px"></a>
    </div>
    <div id="mcover" onclick="document.getElementById('mcover').style.display='';" style="display: none;">
        <img src="images/mengbanzis.png" />
    </div>

</body>
</html>
<script src="http://resali.huobanplus.com/cdn/jquery/2.2.4/jquery.min.js"></script>
<script src="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.js"></script>
<%--<script src="js/jquery-weui.js"></script>--%>
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
    wxShare.shareCouponId = -1;
    $(function () {
        couponlist(hotUtil.pageIndex);
        $('.hot-pullToRefresh').on("pull-to-refresh", function () {
            couponlist(1);
        });
        GetBrandList();
    });
</script>
