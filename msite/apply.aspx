<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="apply.aspx.cs" Inherits="msite.apply" %>

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
    <title>申请分销商</title>
</head>
<body class="ddbg">
    <div class="loding">
        <div class="weui_cells_title">填写信息</div>
        <div class="weui_cells weui_cells_form">
            <div class="weui_cell">
                <div class="weui_cell_hd margt">
                    <label class="weui_label">
                        <img src="images/name.png" width="25px;"></label>
                </div>
                <div class="weui_cell_bd weui_cell_primary">
                    <input class="weui_input" type="text" id="txtname" placeholder="姓名">
                </div>
            </div>
            <div class="weui_cell">
                <div class="weui_cell_hd margt">
                    <label class="weui_label">
                        <img src="images/dhhh.png" width="25px;"></label>
                </div>
                <div class="weui_cell_bd weui_cell_primary">
                    <input class="weui_input" type="tel" id="txtmobile" placeholder="手机">
                </div>
            </div>
            <%--            <div class="weui_cell weui_vcode">
                <div class="weui_cell_hd margt">
                    <label class="weui_label">
                        <img src="images/yzm.png" width="25px;"></label>
                </div>
                <div class="weui_cell_bd weui_cell_primary">
                    <input class="weui_input" type="number" placeholder="验证码">
                </div>
                <div class="weui_cell_ft">
                    <a href="javascript:;" class="weui_btn weui_btn_mini weui_btn_primary">获取验证码</a>
                </div>
            </div>--%>
        </div>


        <p style="height: 20px"></p>
        <a href="javascript:applyDistributor();" class="weui_btn weui_btn_warn anniu ">申请分销商</a>

    </div>
    <div class="weui_panel_bd">
        <div class="weui_media_box weui_media_text" style="padding-top: 4px;">
            <h4 class="weui_media_title" style="font-size: 12px; color: #888">申请说明：</h4>
            <p class="weui_media_desc" style="font-size: 12px; color: #888">申请提交后请等待系统审核，一般1-3个工作日内完成。</p>
        </div>
        <p style="height: 30px"></p>
    </div>


</body>
</html>
<script src="http://resali.huobanplus.com/cdn/jquery/2.2.4/jquery.min.js"></script>
<script src="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.js"></script>
<script src="js/Jquery.util.js"></script>
<script src="js/couponHelper.js"></script>
