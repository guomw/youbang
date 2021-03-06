﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="couponget.aspx.cs" Inherits="msite.couponget" %>

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
    <title><%=couponInfo.Title %></title>
</head>

<body class="ddbg b">
    <div class="lqkk">
        <div class="q-card_content_pane">
            <!--用户券中心列表-->
            <a class="q-coupon_item" href="#">
                <p class="date"><%=couponInfo.Title %></p>
                <p class="ids">使用期限</p>
                <p class="time" title=""><%=couponInfo.time %></p>
                <p class="pingpai" title=""><span><%=couponInfo.BrandName %></span><span>【<%=couponInfo.GoodsName %>】</span></p>
                <div class="q-count">
                    <em>¥</em><%=couponInfo.Money.ToString("f2").Split('.')[0] %>.<span><%=couponInfo.Money.ToString("f2").Split('.')[1] %></span>
                </div>
            </a>
        </div>
    </div>
    <div class="loding">
        <div class="weui_cells_title">填写信息</div>
        <div class="weui_cells weui_cells_form">
            <div class="weui_cell">
                <div class="weui_cell_hd margt">
                    <label class="weui_label">
                        <img src="images/name.png" width="25px;"></label>
                </div>
                <div class="weui_cell_bd weui_cell_primary">
                    <input class="weui_input" type="text" id="txtname" value="<%=userInfo.RealName%>" placeholder="姓名">
                </div>
            </div>
            <div class="weui_cell">
                <div class="weui_cell_hd margt">
                    <label class="weui_label">
                        <img src="images/dhhh.png" width="25px;"></label>
                </div>
                <div class="weui_cell_bd weui_cell_primary">
                    <input class="weui_input" type="tel" id="txtmobile" value="<%=userInfo.Mobile%>" placeholder="手机">
                </div>
            </div>
            <div class="weui_cell">
                <div class="weui_cell_hd margt">
                    <label for="name" class="weui_label">
                        <img src="images/mdmd.png" width="25px;"></label>
                </div>
                <div class="weui_cell_bd weui_cell_primary">
                    <input class="weui_input" style="width: 95%;" id="shops" type="text" value="" readonly="" data-values="" placeholder="选择门店">
                </div>
            </div>
        </div>
        <p style="height: 20px"></p>
        <a href="javascript:onCouponGet();" class="weui_btn weui_btn_warn anniu ">领取</a>
        <a href="javascript:;onMoreCouponList()" class="weui_btn weui_btn_default anniu ">查看更多券</a>
    </div>
    <div class="weui_panel_bd">
        <div class="weui_media_box weui_media_text" style="padding-top: 4px;">
            <h4 class="weui_media_title" style="font-size: 12px; color: #888">规则说明：</h4>
            <p class="weui_media_desc" style="font-size: 12px; color: #888"><%=couponInfo.Remark %></p>
        </div>
        <p style="height: 30px"></p>
    </div>
</body>
</html>
<script src="http://resali.huobanplus.com/cdn/jquery/2.2.4/jquery.min.js"></script>
<script src="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.js"></script>
<script src="js/fastclick.js"></script>
<script src="js/Jquery.util.js"></script>
<script src="js/couponHelper.js"></script>
<script>
    var currentUserId = '<%=userInfo.UserId%>';
    var _shopstr = '<%=shopJson%>';
    var shopJson = eval('(' + _shopstr + ')');
    var shops = "";
    $(function () {
        FastClick.attach(document.body);
    });

    //获取更多优惠券
    function onMoreCouponList() {
        window.location.href ="turncouponlist.aspx?fr=list&userid=" + userid;
    }
    $("#shops").select({
        title: "选择门店",
        multi: true,
        min: 1,
        max: 10,
        items: shopJson,
        beforeClose: function (values, titles) {         
            shops = values;            
            return true;
        },
        onChange: function (d) {
            //console.log(this, d);
        }
    });
</script>
