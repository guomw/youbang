
var wxShare = {
    shareCouponId: 0,//分享优惠券ID，0表示分享优惠券列表
    shareUserId: 0,//分享用户ID
    shareData: {
        title: "",
        desc: "",
        link: "",
        img_url: ""
    },
    InitShare: function (shareData) {
        this.shareData = shareData;
    },
    callback: function () { }
}
wx.ready(function () {
    wx.onMenuShareAppMessage({//监听“分享给朋友”，按钮点击、自定义分享内容及分享结果接口
        title: wxShare.shareData.title,
        desc: wxShare.shareData.desc,
        link: wxShare.shareData.link,
        imgUrl: wxShare.shareData.img_url,
        trigger: function (res) {
            //alert('用户点击发送给朋友');
        },
        success: function (res) {
            //alert('已分享');
            wxShare.callback(wxShare.shareUserId, wxShare.shareCouponId);
        },
        cancel: function (res) {
            //alert('已取消');
        },
        fail: function (res) {
            //alert(JSON.stringify(res));
        }
    });

    wx.onMenuShareTimeline({//监听“分享到朋友圈”按钮点击、自定义分享内容及分享结果接口
        title: wxShare.shareData.title,
        link: wxShare.shareData.link,
        imgUrl: wxShare.shareData.img_url,
        trigger: function (res) {
            // alert('用户点击分享到朋友圈');
        },
        success: function (res) {
            //alert('已分享');
            wxShare.callback(wxShare.shareUserId, wxShare.shareCouponId);
        },
        cancel: function (res) {
            // alert('已取消');
        },
        fail: function (res) {
            //alert(JSON.stringify(res));
        }
    });

    wx.onMenuShareQQ({//监听“分享到QQ”按钮点击、自定义分享内容及分享结果接口
        title: wxShare.shareData.title,
        desc: wxShare.shareData.desc,
        link: wxShare.shareData.link,
        imgUrl: wxShare.shareData.img_url,
        trigger: function (res) {
            //alert('用户点击分享到QQ');
        },
        complete: function (res) {
            //alert(JSON.stringify(res));
        },
        success: function (res) {
            //alert('已分享');
            wxShare.callback(wxShare.shareUserId, wxShare.shareCouponId);
        },
        cancel: function (res) {
            //alert('已取消');
        },
        fail: function (res) {
            //alert(JSON.stringify(res));
        }
    });

    wx.onMenuShareWeibo({//监听“分享到微博”按钮点击、自定义分享内容及分享结果接口
        title: wxShare.shareData.title,
        desc: wxShare.shareData.desc,
        link: wxShare.shareData.link,
        imgUrl: wxShare.shareData.img_url,
        trigger: function (res) {
            //alert('用户点击分享到微博');
        },
        complete: function (res) {
            //alert(JSON.stringify(res));
        },
        success: function (res) {
            //alert('已分享');
            wxShare.callback(wxShare.shareUserId, wxShare.shareCouponId);
        },
        cancel: function (res) {
            //alert('已取消');
        },
        fail: function (res) {
            //alert(JSON.stringify(res));
        }
    });

    wx.onMenuShareQZone({
        title: wxShare.shareData.title,
        desc: wxShare.shareData.desc,
        link: wxShare.shareData.link,
        imgUrl: wxShare.shareData.img_url,
        success: function () {
            // 用户确认分享后执行的回调函数
            wxShare.callback(wxShare.shareUserId, wxShare.shareCouponId);
        },
        cancel: function () {
            // 用户取消分享后执行的回调函数
        }
    });
});