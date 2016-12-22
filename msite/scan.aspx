<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scan.aspx.cs" Inherits="msite.scan" %>

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
    <script src="JSDK/RegConfig.aspx?debug=0&apilist=scanQRCode"></script>
    <title>扫一扫</title>
</head>
<body>
</body>
</html>
<script>
    wx.ready(function () {
        wx.scanQRCode({
            needResult: 0, // 默认为0，扫描结果由微信处理，1则直接返回扫描结果，
            scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是一维码，默认二者都有
            success: function (res) {
                var result = res.resultStr; // 当needResult 为 1 时，扫码返回的结果
            }
        });
    });
</script>
