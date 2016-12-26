/// <reference path="Jquery.util.js" />

var loading = false;  //状态标记
var usertype = 1;
var tabtype = 0;
var sign = "";
var userid = hotUtil.getQuery("userid", 0);
var couponid = hotUtil.getQuery("couponid", 0);
var ctxUserId = 0;
//下一页
function nextPage(currentPageIndex, PageCount, callback) {
    if (currentPageIndex == 1) {
        $('.hot-pullToRefresh').pullToRefreshDone();
    }
    loading = false;
    if (currentPageIndex >= PageCount) {
        $(document.body).destroyInfinite();
        $(".weui-infinite-scroll").hide();
    }
    else {
        $(".weui-infinite-scroll").show();
        $(document.body).infinite().on("infinite", function () {
            if (loading) return;
            loading = true;
            callback(currentPageIndex + 1);
        });
    }
}

function fmoney(s, n) {
    n = n > 0 && n <= 20 ? n : 2;
    s = parseFloat((s + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";
    var l = s.split(".")[0].split("").reverse(),
    r = s.split(".")[1];
    t = "";
    for (i = 0; i < l.length; i++) {
        t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
    }
    return t.split("").reverse().join("") + "." + r;
}


//优惠券列表
function couponlist(page) {
    hotUtil.pageIndex = page;
    var param = {
        action: "CouponList",
        pageIndex: page,
        pageSize: hotUtil.pageSize,
        from: hotUtil.getQuery("fr", ""),
        userid: ctxUserId
    }
    $.showLoading("正在加载...");
    var from = param.from;
    hotUtil.ajaxCall(hotUtil.ajaxUrl, param, function (ret, err) {
        if (ret) {
            if (ret.status == 200) {
                if (ret.data) {
                    var listhtml = "";
                    $.each(ret.data.Rows, function (i, item) {
                        listhtml += $("#couponlistTemplate").html();
                        if (hotUtil.isNullOrEmpty(item.CouponNo)) {
                            listhtml = listhtml.replace("{url}", "couponget.aspx?fr=" + from + "&couponid=" + item.CouponId + "&userid=" + userid);
                            listhtml = listhtml.replace("{class}", "");
                            listhtml = listhtml.replace("{LabalText}", "立即领取");
                        }
                        else {
                            listhtml = listhtml.replace("{url}", "usecoupondetail.aspx?couponid=" + item.CouponId + "&userid=" + userid);
                            listhtml = listhtml.replace("{class}", "linguo");
                            listhtml = listhtml.replace("{LabalText}", "立即使用");
                        }
                        listhtml = listhtml.replace("{couponid}", item.CouponId);
                        listhtml = listhtml.replace("{CouponNo}", item.CouponNo);
                        listhtml = listhtml.replace("{userid}", userid);
                        listhtml = listhtml.replace("{Title}", item.Title);
                        listhtml = listhtml.replace("{time}", item.time);
                        listhtml = listhtml.replace(/{Amounts}/gm, "剩余" + item.Amounts + "张");
                        listhtml = listhtml.replace("{Money}", item.Money.toFixed(2).split(".")[0]);
                        listhtml = listhtml.replace("{Money2}", item.Money.toFixed(2).split(".")[1]);
                    });
                    if (!hotUtil.isNullOrEmpty(listhtml)) {
                        if (page == 1)
                            $("#listMode").html(listhtml);
                        else
                            $("#listMode").append(listhtml);
                    }
                    nextPage(page, ret.data.PageCount, couponlist);
                    $.hideLoading();
                }
            }
            else {
                $.hideLoading();
                $.alert(ret.statusText);
                $('.hot-pullToRefresh').pullToRefreshDone();
            }
        }
    });
}




//获取我分享的优惠券列表
function myCouponlist(page) {
    hotUtil.pageIndex = page;
    var param = {
        action: "MyShareCouponList",
        pageIndex: page,
        pageSize: hotUtil.pageSize,
        userid: userid,
        type: tabtype,
        usertype: usertype
    }
    $.showLoading("正在加载...");
    hotUtil.ajaxCall(hotUtil.ajaxUrl, param, function (ret, err) {
        if (ret) {
            if (ret.status == 200) {
                if (ret.data) {
                    var listhtml = "";
                    $.each(ret.data.Rows, function (i, item) {
                        listhtml += $("#couponlistTemplate").html();
                        if (usertype == 1) {
                            listhtml = listhtml.replace("{url}", "myCouponGetList.aspx?couponid=" + item.CouponId + "&userid=" + userid + "&c=" + item.Amounts);
                            listhtml = listhtml.replace("{class}", tabtype == 0 ? "wd" : "guoqi");
                            listhtml = listhtml.replace("{LabalText}", tabtype == 0 ? "立即查看" : "已过期");
                        }
                        else {
                            listhtml = listhtml.replace("{url}", item.expire == 0 ? "usecoupondetail.aspx?couponid=" + item.CouponId + "&userid=" + userid : "");
                            listhtml = listhtml.replace("{class}", item.expire == 0 ? "linguo" : "guoqi");
                            listhtml = listhtml.replace("{LabalText}", item.expire == 0 ? "立即使用" : "已过期");
                        }
                        listhtml = listhtml.replace("{couponid}", item.CouponId);
                        listhtml = listhtml.replace("{CouponNo}", item.CouponNo);
                        listhtml = listhtml.replace("{userid}", userid);
                        listhtml = listhtml.replace("{Title}", item.Title);
                        listhtml = listhtml.replace("{time}", item.time);
                        listhtml = listhtml.replace(/{Amounts}/gm, "已有" + item.Amounts + "人领取");
                        listhtml = listhtml.replace("{c}", item.Amounts);
                        listhtml = listhtml.replace("{Money}", item.Money.toFixed(2).split(".")[0]);
                        listhtml = listhtml.replace("{Money2}", item.Money.toFixed(2).split(".")[1]);
                    });
                    if (!hotUtil.isNullOrEmpty(listhtml)) {
                        if (page == 1)
                            $("#listMode").html(listhtml);
                        else
                            $("#listMode").append(listhtml);
                    }
                    nextPage(page, ret.data.PageCount, myCouponlist);
                    $.hideLoading();
                }
            }
            else {
                $.hideLoading();
                $.alert(ret.statusText);
                $('.hot-pullToRefresh').pullToRefreshDone();
            }
        }
    });
}



//我分享的券的领取列表
function myCouponGetList(page) {
    hotUtil.pageIndex = page;
    var param = {
        action: "MyCouponGetList",
        pageIndex: page,
        pageSize: hotUtil.pageSize,
        userid: userid,
        type: tabtype,
        couponid: couponid
    }
    $.showLoading("正在加载...");
    hotUtil.ajaxCall(hotUtil.ajaxUrl, param, function (ret, err) {
        if (ret) {
            if (ret.status == 200) {
                if (ret.data) {
                    var listhtml = "";
                    $.each(ret.data.Rows, function (i, item) {
                        listhtml += $("#couponlistTemplate").html();
                        listhtml = listhtml.replace("{HeadImg}", item.HeadImg);
                        listhtml = listhtml.replace("{Name}", item.Name);
                        listhtml = listhtml.replace("{CouponNo}", item.CouponNo);
                        listhtml = listhtml.replace("{time}", item.time);
                        listhtml = listhtml.replace("{StatusText}", item.StatusText);
                        listhtml = listhtml.replace("{color}", item.IsUse == 1 ? "red" : "");
                    });
                    if (!hotUtil.isNullOrEmpty(listhtml)) {
                        if (page == 1)
                            $("#listMode").html(listhtml);
                        else
                            $("#listMode").append(listhtml);
                    }
                    nextPage(page, ret.data.PageCount, myCouponGetList);
                    $.hideLoading();
                }
            }
            else {
                $.hideLoading();
                $.alert(ret.statusText);
                $('.hot-pullToRefresh').pullToRefreshDone();
            }
        }
    });
}




//添加优惠券分享日志
function insertShareLog(shareUserId, shareCouponId) {
    var param = {
        action: "insertShareLog",
        userid: shareUserId,
        couponid: shareCouponId,
        sign: sign
    }
    $.showLoading("正在加载...");
    hotUtil.ajaxCall(hotUtil.ajaxUrl, param, function (ret, err) {
        $.hideLoading();
        if (ret) {
            if (ret.status == 200) {
                $.alert("已分享", function () {
                    window.history.back();
                });
            }
            else
                $.toast("分享失败", "cancel");
        }
        else
            $.toast("分享失败", "cancel");
    });
}


//领取优惠券
function onCouponGet() {

    if (hotUtil.isNullOrEmpty($("#txtname").val())) {
        $.alert("请输入您的姓名");
        return false;
    }
    if (hotUtil.isNullOrEmpty($("#txtmobile").val())) {
        $.alert("请输入您的手机号码");
        return false;
    }
    if (!hotUtil.isMobile($("#txtmobile").val())) {
        $.alert("请输入有效的手机号码");
        return false;
    }
    var param = {
        action: "oncouponget",
        userid: userid,
        couponid: couponid,
        name: $("#txtname").val(),
        mobile: $("#txtmobile").val(),
        currentuserid: currentUserId,
        from: hotUtil.getQuery("fr", ""),
        sign: sign
    }
    $.showLoading("正在加载...");
    hotUtil.ajaxCall(hotUtil.ajaxUrl, param, function (ret, err) {
        $.hideLoading();
        if (ret) {
            if (ret.status == 200) {
                window.location.href = "error/200.html?note=" + hotUtil.encode("领取成功");
            }
            else
                $.alert(ret.statusText);// window.location.href = "error/200.html?note=" + hotUtil.encode("优惠券已领完");
        }
        else
            window.location.href = "error/200.html?note=" + hotUtil.encode("优惠券已领完");
    });
}

//申请
function applyDistributor() {
    if (hotUtil.isNullOrEmpty($("#txtname").val())) {
        $.alert("请输入您的姓名");
        return false;
    }
    if (hotUtil.isNullOrEmpty($("#txtmobile").val())) {
        $.alert("请输入您的手机号码");
        return false;
    }
    if (!hotUtil.isMobile($("#txtmobile").val())) {
        $.alert("请输入有效的手机号码");
        return false;
    }
    var param = {
        action: "ApplyDistributor",
        userid: userid,
        name: $("#txtname").val(),
        mobile: $("#txtmobile").val(),
        sign: sign
    }
    $.showLoading("正在加载...");
    hotUtil.ajaxCall(hotUtil.ajaxUrl, param, function (ret, err) {
        $.hideLoading();
        if (ret) {
            if (ret.status == 200) {
                window.location.href = "error/success.html";
            }
            else
                $.alert("申请失败");
        }
    });
}




//我的返利
function myRebateList(page) {
    hotUtil.pageIndex = page;
    var param = {
        action: "myRebateList",
        pageIndex: page,
        pageSize: hotUtil.pageSize,
        userid: userid
    }
    $.showLoading("正在加载...");
    hotUtil.ajaxCall(hotUtil.ajaxUrl, param, function (ret, err) {
        if (ret) {
            if (ret.status == 200) {
                if (ret.data) {
                    var listhtml = "";
                    $.each(ret.data.Rows, function (i, item) {
                        listhtml += $("#couponlistTemplate").html();
                        listhtml = listhtml.replace("{Remark}", item.Remark);
                        listhtml = listhtml.replace("{Money}", item.RebateMoney);
                        listhtml = listhtml.replace("{Time}", item.Time);
                    });
                    if (!hotUtil.isNullOrEmpty(listhtml)) {
                        if (page == 1)
                            $("#listMode").html(listhtml);
                        else
                            $("#listMode").append(listhtml);
                    }
                    nextPage(page, ret.data.PageCount, myRebateList);
                    $.hideLoading();
                }
            }
            else {
                $.hideLoading();
                $.alert(ret.statusText);
                $('.hot-pullToRefresh').pullToRefreshDone();
            }
        }
    });
}



function submitHx() {
    var param = {
        action: "submitHx",
        userid: userid,
        couponid: couponid,
        currentuserid: currentUserId,
        sign: sign
    }
    $.showLoading("正在加载...");
    hotUtil.ajaxCall(hotUtil.ajaxUrl, param, function (ret, err) {
        $.hideLoading();
        if (ret) {
            if (ret.status == 200) {
                window.location.href = "error/success.html";
            }
            else
                $.alert(ret.statusText);// window.location.href = "error/200.html?note=" + hotUtil.encode("优惠券已领完");
        }
    });
}



//提现
function submitDrawmoney() {
    if (hotUtil.isNullOrEmpty($("#txtname").val())) {
        $.alert("请输入您的姓名");
        return false;
    }
    if (hotUtil.isNullOrEmpty($("#txtmobile").val())) {
        $.alert("请输入您的手机号码");
        return false;
    }
    if (!hotUtil.isMobile($("#txtmobile").val())) {
        $.alert("请输入有效的手机号码");
        return false;
    }


    if (hotUtil.isNullOrEmpty($("#txtpayaccount").val())) {
        $.alert("请输入您的支付宝账号");
        return false;
    }
    if (hotUtil.isNullOrEmpty($("#txtmoney").val()) || parseFloat($("#txtmoney").val()) <= 0) {
        $.alert("请输入提现金额");
        return false;
    }
    var param = {
        action: "submitDrawmoney",
        userid: userid,
        name: $("#txtname").val(),
        mobile: $("#txtmobile").val(),
        payaccount: $("#txtpayaccount").val(),
        money: $("#txtmoney").val(),
        sign: sign
    }
    $.showLoading("正在加载...");
    hotUtil.ajaxCall(hotUtil.ajaxUrl, param, function (ret, err) {
        $.hideLoading();
        if (ret) {
            if (ret.status == 200) {
                window.location.href = "error/success.html";
            }
            else
                $.alert(ret.statusText);
        }
    });
}






//我的提现明细
function myDrawMoneyList(page) {
    hotUtil.pageIndex = page;
    var param = {
        action: "DrawMoneyList",
        pageIndex: page,
        pageSize: hotUtil.pageSize,
        userid: userid
    }
    $.showLoading("正在加载...");
    hotUtil.ajaxCall(hotUtil.ajaxUrl, param, function (ret, err) {
        if (ret) {
            if (ret.status == 200) {
                if (ret.data) {
                    var listhtml = "";
                    $.each(ret.data.Rows, function (i, item) {
                        listhtml += $("#couponlistTemplate").html();
                        if (item.ApplyStatus == 3)
                            listhtml = listhtml.replace("{Remark}", "原因：" + item.Remark);
                        else
                            listhtml = listhtml.replace("{Remark}", "");
                        listhtml = listhtml.replace("{ApplyMoney}", item.ApplyMoney);
                        listhtml = listhtml.replace("{CreateTime}", item.Time);
                        listhtml = listhtml.replace("{PayAccount}", item.PayAccount);
                        if (item.ApplyStatus == 0)
                            listhtml = listhtml.replace("{ApplyStatusText}", "申请中");
                        else if (item.ApplyStatus == 1)
                            listhtml = listhtml.replace("{ApplyStatusText}", "待打款");
                        else if (item.ApplyStatus == 2)
                            listhtml = listhtml.replace("{ApplyStatusText}", "已成功");
                        else if (item.ApplyStatus == 3)
                            listhtml = listhtml.replace("{ApplyStatusText}", "失败");
                    });
                    if (!hotUtil.isNullOrEmpty(listhtml)) {
                        if (page == 1)
                            $("#listMode").html(listhtml);
                        else
                            $("#listMode").append(listhtml);
                    }
                    nextPage(page, ret.data.PageCount, myDrawMoneyList);
                    $.hideLoading();
                }
            }
            else {
                $.hideLoading();
                $.alert(ret.statusText);
                $('.hot-pullToRefresh').pullToRefreshDone();
            }
        }
    });
}




//核销明细
function myVerifyList(page) {
    hotUtil.pageIndex = page;
    var param = {
        action: "VerifyList",
        pageIndex: page,
        pageSize: hotUtil.pageSize,
        userid: userid
    }
    $.showLoading("正在加载...");
    hotUtil.ajaxCall(hotUtil.ajaxUrl, param, function (ret, err) {
        if (ret) {
            if (ret.status == 200) {
                if (ret.data) {
                    var listhtml = "";
                    $.each(ret.data.Rows, function (i, item) {
                        listhtml += $("#couponlistTemplate").html();
                        listhtml = listhtml.replace("{CouponName}", item.CouponName);
                        listhtml = listhtml.replace("{CouponNo}", item.CouponNo);
                        listhtml = listhtml.replace("{CreateTime}", item.Time);
                    });
                    if (!hotUtil.isNullOrEmpty(listhtml)) {
                        if (page == 1)
                            $("#listMode").html(listhtml);
                        else
                            $("#listMode").append(listhtml);
                    }
                    nextPage(page, ret.data.PageCount, myVerifyList);
                    $.hideLoading();
                }
            }
            else {
                $.hideLoading();
                $.alert(ret.statusText);
                $('.hot-pullToRefresh').pullToRefreshDone();
            }
        }
    });
}
