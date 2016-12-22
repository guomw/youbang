<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="drawmoney.aspx.cs" Inherits="msite.drawmoney" %>

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
    <title>提现申请</title>
</head>
<body class="ddbg">


    <div class="loding">
        <div class="weui_cells_title">收款人账户信息</div>
        <div class="weui_cells weui_cells_form">
            <div class="weui_cell">
                <div class="weui_cell_hd margt">
                    <label class="weui_label">
                        <img src="images/name.png" width="25px;"></label>
                </div>
                <div class="weui_cell_bd weui_cell_primary">
                    <input class="weui_input" type="text" id="txtname" value="<%=userInfo.RealName %>" placeholder="姓名">
                </div>
            </div>
            <div class="weui_cell">
                <div class="weui_cell_hd margt">
                    <label class="weui_label">
                        <img src="images/dhhh.png" width="25px;"></label>
                </div>
                <div class="weui_cell_bd weui_cell_primary">
                    <input class="weui_input" type="tel" id="txtmobile" value="<%=userInfo.Mobile %>" placeholder="电话">
                </div>
            </div>
            <div class="weui_cell">
                <div class="weui_cell_hd margt">
                    <label class="weui_label">
                        <img src="images/zfb.png" width="25px;"></label>
                </div>
                <div class="weui_cell_bd weui_cell_primary">
                    <input class="weui_input" type="text" id="txtpayaccount" placeholder="支付宝账号">
                </div>
            </div>
        </div>

        <div class="weui_cells_title">请输入提现金额</div>
        <div class="weui_cells weui_cells_form">
            <div class="weui_cell">
                <div class="weui_cell_hd margt">
                    <label class="weui_label">
                        <img src="images/tixx.png" width="25px;"></label>
                </div>
                <div class="weui_cell_bd weui_cell_primary">
                    <input class="weui_input" type="text" id="txtmoney" placeholder="￥0.00">
                </div>
            </div>
        </div>
        <p style="text-align: right; padding: 4px 15px 0px 15px; font-size: 12px; color: #666">可提现总额：<%=userInfo.UserMoney %>元</p>


        <p style="height: 20px"></p>
        <a href="javascript:submitDrawmoney();" class="weui_btn weui_btn_warn anniu ">提交确认</a>

    </div>
    <div class="weui_panel_bd">
        <div class="weui_media_box weui_media_text" style="padding-top: 4px;">
            <h4 class="weui_media_title" style="font-size: 12px; color: #888">提示说明：</h4>
            <p class="weui_media_desc" style="font-size: 12px; color: #888">提交确认后不会立即到账，请等待系统审核，一般1-3个工作日内完成。</p>
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
    userid = '<%=userInfo.UserId%>';
    $(function () {
        FastClick.attach(document.body);
    });
</script>
