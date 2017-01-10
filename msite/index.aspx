<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="msite.index" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1,user-scalable=no,maximum-scale=1,initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="description" content="" />
    <link rel="stylesheet" type="text/css" href="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.css" />
    <link rel="stylesheet" type="text/css" href="css/ybdiy.css" />
    <title>个人中心</title>
</head>
<body class="ddbg">
    <div class="ybdiy">
        <div class="f-bigbox">
            <div class="fa-bigbox">
                <p class="f-moren" style="line-height: 0px;">
                    <img style="width: 54px; height: 54px;" src="<%=userInfo.HeadImg %>">
                </p>
                <div class="f-number">
                    <p class="phone"><b><%=userInfo.NickName %></b></p>
                    <p class="tit"><span><%=userInfo.UserIdentity==1?"分销商":"店员" %></span></p>

                </div>
                <p style="clear: both"></p>
            </div>
        </div>
        <div class="weui_cells weui_cells_access" style="margin-top: 0px">
            <a class="weui_cell" href="couponlist.aspx?userid=<%=userInfo.UserId %>&shopid=0">
                <div class="weui_cell_bd weui_cell_primary">
                    <p>
                        <img src="images/icon/iconfont-quan.png" class="w-icon">发券中心
                    </p>
                </div>
                <div class="weui_cell_ft" style="font-size: 12px">剩余<%=couponTotalAmount %>张</div>
            </a>
        </div>
        <%if (userInfo.UserIdentity == 2)
            { %>
        <div class="weui_cells weui_cells_access">
            <a class="weui_cell" href="myVerifyList.aspx?userid=<%=userInfo.UserId %>">
                <div class="weui_cell_bd weui_cell_primary">
                    <p>
                        <img src="images/icon/hexiao.png" class="w-icon">我的核销
                    </p>
                </div>
                <div class="weui_cell_ft" style="font-size: 12px">历史记录</div>
            </a>
        </div>
        <%} %>
        <div class="weui_cells weui_cells_access">
            <a class="weui_cell" href="myCouponlist.aspx?ut=1">
                <div class="weui_cell_bd weui_cell_primary">
                    <p>
                        <img src="images/icon/quan.png" class="w-icon">我发的券
                    </p>
                </div>
                <div class="weui_cell_ft" style="font-size: 12px">查看全部</div>
            </a>
        </div>
        <div class="weui_tab wdq">
            <div class="weui_navbar">
                <a href="javascript:void(0);" class="weui_navbar_item">
                    <p class="bm"><%=myCouponAmountInfo.myGetCount %></p>
                    <p class="sm">已领取/张</p>
                </a><a href="javascript:void(0);" class="weui_navbar_item">
                    <p class="bm"><%=myCouponAmountInfo.myRecycleCount %></p>
                    <p class="sm">已回收/张</p>
                </a><a href="javascript:void(0);" class="weui_navbar_item">
                    <p class="bm"><%=myCouponAmountInfo.myUseCount %></p>
                    <p class="sm">已使用/张</p>
                </a>
            </div>
            <div class="weui_tab_bd"></div>
        </div>
        <div class="weui_cells weui_cells_access">
            <a class="weui_cell" href="rebatelist.aspx?userid=<%=userInfo.UserId %>&c=<%=userInfo.UserMoney %>">
                <div class="weui_cell_bd weui_cell_primary">
                    <p>
                        <img src="images/icon/qian.png" class="w-icon">我的返利
                    </p>
                </div>
                <div class="weui_cell_ft" style="font-size: 12px">查看明细</div>
            </a>
        </div>
        <div class="weui_tab wdfl">
            <div class="weui_navbar">
                <div class="weui_navbar_item pjia">
                    <p class="bm">￥<%=userInfo.UserMoney.ToString("f2").Split('.')[0] %><span style="font-size: 12px">.<%=userInfo.UserMoney.ToString("f2").Split('.')[1] %></span></p>
                    <p class="sm">&nbsp;&nbsp;可提现总金额/元</p>
                </div>
            </div>
            <div class="weui_tab_bd"></div>
        </div>
        <a href="drawmoney.aspx" class="weui_btn weui_btn_primary anniu">提现</a>
        <a href="drawmoneylist.aspx?userid=<%=userInfo.UserId %>" class="cktxx">查看提现明细</a>
        <div class="weui_panel_bd" style="clear: both">
            <div class="weui_media_box weui_media_text" style="padding-top: 4px;">
                <h4 class="weui_media_title" style="font-size: 12px; color: #888">提现说明：</h4>
                <p class="weui_media_desc" style="font-size: 12px; color: #888">提现说明提现说明提现说明</p>
            </div>
            <p style="height: 30px"></p>
        </div>
    </div>
</body>
</html>
<script src="http://resali.huobanplus.com/cdn/jquery/2.2.4/jquery.min.js"></script>
<script src="http://resali.huobanplus.com/cdn/jquery-weui/0.8.2/jquery-weui.min.js"></script>
<script src="js/fastclick.js"></script>
<script>
    $(function () {
        FastClick.attach(document.body);
        
    })
</script>
