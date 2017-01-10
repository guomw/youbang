/// <reference path="../plugins/sweetalert/sweetalert.min.js" />
/// <reference path="../jquery.min.js" />
/// <reference path="../plugins/hot/Jquery.util.js" />
/// <reference path="http://resali.huobanplus.com/cdn/hotui/js/plugins/datetimepick/daterangepicker.js" />

/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-2016. All rights reserved.
**/

var userHelper = {
    ajaxUrl: "/handler/HQ.ashx",
    loaclData: [],
    isAlly: hotUtil.getQuery("type"),
    pageIndex: 1,
    reset: null,
    loadList: function (page) {
        var self = this;
        self.loaclData = [];
        this.pageIndex = page;
        var postData = {
            action: "GetUserList",
            pageIndex: page,
            pageSize: 20,
            key: $("#keyword").val(),
            applystatus: self.isAlly == 2 ? 0 : 1,
            startTime: $("#beginTime").val(),
            endTime: $("#endTime").val(),
            shopname: $("#shopname").val(),
            usermobile: $("#usermobile").val()
        }
        hotUtil.loading.show();
        hotUtil.ajaxCall(this.ajaxUrl, postData, function (ret, err) {
            if (ret) {
                if (ret.status == 200) {
                    if (ret.data) {
                        var listhtml = "";
                        self.loaclData = ret.data.Rows;
                        $.each(ret.data.Rows, function (i, item) {
                            var tempHtml = $("#templist").html();
                            tempHtml = tempHtml.replace("{NO}", i + 1);
                            tempHtml = tempHtml.replace("{NickName}", item.NickName);
                            tempHtml = tempHtml.replace(/{UserId}/gm, item.UserId);
                            tempHtml = tempHtml.replace("{RealName}", item.RealName);
                            tempHtml = tempHtml.replace("{LevelName}", item.UserIdentity == 2 ? "店员" : "分销商");
                            tempHtml = tempHtml.replace("{Mobile}", item.Mobile);
                            tempHtml = tempHtml.replace("{ShopName}", item.ShopName);
                            if (!hotUtil.isNullOrEmpty(item.HeadImg))
                                tempHtml = tempHtml.replace("{UserHeadImg}", item.HeadImg);
                            else
                                tempHtml = tempHtml.replace("{UserHeadImg}", "/static/img/bg.png");

                            tempHtml = tempHtml.replace("{ApplyTime}", item.ApplyTime);
                            if (item.ApplyStatus == 1) {
                                tempHtml = tempHtml.replace("{LableText}", item.IsActive == 1 ? "冻结" : "激活");
                                tempHtml = tempHtml.replace("{ActiveStatus}", item.IsActive == 1 ? "<span style='color:red;'>激活</span>" : "已冻结")
                            }
                            else {
                                tempHtml = tempHtml.replace("{ActiveStatus}", item.ApplyStatus == 0 ? "申请中" : "已拒绝")
                                tempHtml = tempHtml.replace("{LableText}", "");
                            }

                            if (item.ApplyStatus != 1) {
                                tempHtml = tempHtml.replace(/{display}/gm, item.ApplyStatus == 0 ? "" : "display:none");
                            }
                            else {
                                tempHtml = tempHtml.replace(/{display}/gm, "");
                            }


                            if (item.UserIdentity > 0)
                                tempHtml = tempHtml.replace("{LableText2}", item.UserIdentity == 1 ? "设置成店员" : "");
                            else
                                tempHtml = tempHtml.replace("{LableText2}", "");

                            tempHtml = tempHtml.replace("{identity}", item.UserIdentity);

                            listhtml += tempHtml;
                        });
                        $("#listMode").html(listhtml);
                        if (self.isAlly == 2) {
                            $(".applyHtml").show();
                            $(".applyOk").hide();
                        }
                        //初始化分页
                        var pageinate = new hotUtil.paging(".pagination1", ret.data.PageIndex, ret.data.PageSize, ret.data.PageCount, ret.data.Total, 7);
                        pageinate.init(function () {
                            goTo(p, function (page) {
                                userHelper.loadList(page);
                            });
                        });
                    }
                }
            }
            hotUtil.loading.close();
        });
    },
    search: function () {
        userHelper.loadList(1);
    },
    searchAll: function () {
        $("#keyword,#beginTime,#endTime,#createTimePick,#usermobile,#shopname").val("");
        userHelper.loadList(1);
    },
    getModel: function (dataId) {
        var model = null;
        if (!hotUtil.isNullOrEmpty(dataId) && this.loaclData != null && this.loaclData.length > 0) {
            $.each(this.loaclData, function (i, item) {
                if (item.UserId == dataId) {
                    model = item;
                    return false;
                }
            });
        }
        return model;
    },
    SelectedUserId: 0,
    setShop: function (dataId) {
        this.SelectedUserId = dataId;
        this.loadShopList(1);
    },
    closeShopModal: function () {
        var shopId = 0;
        $("input[name='rdshopid']").each(function (i) {
            if (this.checked) {
                shopId = $(this).val();
                return false;
            }
        });
        if (hotUtil.isNullOrEmpty(shopId))
            return false;


        var param = {
            action: "setShopId",
            userid: this.SelectedUserId,
            shopid: shopId
        }
        hotUtil.loading.show();
        hotUtil.ajaxCall(this.ajaxUrl, param, function (ret, err) {
            if (ret) {
                if (ret.status == 200) {
                    swal("设置成功", "", "success");
                    userHelper.loadList(userHelper.pageIndex);
                }
                else {
                    swal(ret.statusText, "", "warning");
                }
            }
            hotUtil.loading.close();
        });
        //关闭窗口
        $(".closeShopModal").click();
    },
    loadShopList: function (page) {
        var self = this;
        var postData = {
            action: "getshoplist",
            pageIndex: page,
            pageSize: 8,
            key: "",
            brandid: 0
        }
        hotUtil.loading.show();
        hotUtil.ajaxCall(this.ajaxUrl, postData, function (ret, err) {
            if (ret) {
                if (ret.status == 200) {
                    if (ret.data) {
                        var listhtml = "";
                        $.each(ret.data.Rows, function (i, item) {
                            var tempHtml = $("#templist_shop").html();
                            tempHtml = tempHtml.replace("{chbname}", "rdshopid");
                            tempHtml = tempHtml.replace("{shopName}", item.ShopName);
                            tempHtml = tempHtml.replace(/{shopId}/gm, item.ShopID);
                            listhtml += tempHtml;
                        });
                        $("#listMode2").html(listhtml);

                        //初始化分页
                        var pageinate = new hotUtil.paging(".pagination2", ret.data.PageIndex, ret.data.PageSize, ret.data.PageCount, ret.data.Total, 7);
                        pageinate.init(function (p) {
                            goTo(p, function (page) {
                                userHelper.loadShopList(page);
                            });
                        });
                    }
                }
            }
            hotUtil.loading.close();
        });
    },
    updateActive: function (dataId) {
        var param = {
            action: "UpdateUserActive",
            userid: dataId
        }
        hotUtil.loading.show();
        hotUtil.ajaxCall(this.ajaxUrl, param, function (ret, err) {
            if (ret) {
                if (ret.status == 200) {
                    swal("设置成功", "", "success");
                    userHelper.loadList(userHelper.pageIndex);
                }
                else {
                    swal(ret.statusText, "", "warning");
                }
            }
            hotUtil.loading.close();
        });
    },
    updateIdentity: function (dataId, identity) {
        var param = {
            action: "updateIdentity",
            userid: dataId,
            identity: identity
        }
        hotUtil.loading.show();
        hotUtil.ajaxCall(this.ajaxUrl, param, function (ret, err) {
            if (ret) {
                if (ret.status == 200) {
                    swal("设置成功", "", "success");
                    userHelper.loadList(userHelper.pageIndex);
                }
                else {
                    swal(ret.statusText, "", "warning");
                }
            }
            hotUtil.loading.close();
        });
    },
    UpdateApplyStatus: function (dataId, code) {
        swal({
            title: code == 1 ? "您确定要同意吗？" : "您确定要拒绝吗？",
            text: code == 1 ? "" : "请输入拒绝理由",
            type: code == 1 ? "warning" : "input",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "确定",
            cancelButtonText: "取消",
            closeOnConfirm: false,
            inputPlaceholder: "理由"
        }, function (inputValue) {
            if (inputValue) {
                var param = {
                    action: "UpdateApplyStatus",
                    userid: dataId,
                    active: code,
                    remark: inputValue
                }
                hotUtil.loading.show();
                hotUtil.ajaxCall(userHelper.ajaxUrl, param, function (ret, err) {
                    if (ret) {
                        if (ret.status == 200) {
                            swal("操作成功", "", "success");
                            userHelper.loadList(userHelper.pageIndex);
                        }
                        else {
                            swal(ret.statusText, "", "warning");
                        }
                    }
                    hotUtil.loading.close();
                });
            }
        });
    },
    dialog: function (dataId) {
        if (this.reset)
            this.reset.resetForm();
        $("#userid").val(dataId);
        $("#userpwd").val("");
    },
    editpwd: function () {
        var self = this;
        var postData = hotUtil.serializeForm("#signupForm .form-control");
        postData.action = "editpwd";
        hotUtil.loading.show();
        hotUtil.ajaxCall(self.ajaxUrl, postData, function (ret, err) {
            if (ret) {
                if (ret.status == 200) {
                    couponHelper.loadList(couponHelper.pageIndex);
                    swal("提交成功", "", "success");
                    $(".close").click();
                }
                else
                    swal(ret.statusText, "", "warning");
            }
            hotUtil.loading.close();
        });
    },
    pageInit: function () {
        if (this.isAlly == 2) {
            $(".applyHtml").show();
            $(".applyOk").hide();
        }
        userHelper.loadList(userHelper.pageIndex);
        userHelper.validate();
    },
    validate: function () {
        var e = "<i class='fa fa-times-circle'></i> ";
        this.reset = $("#signupForm").validate({
            rules: {
                userpwd: {
                    required: !0,
                    minlength: 6,
                    maxlength: 20
                }
            },
            messages: {
                userpwd: {
                    required: e + "请输入新密码",
                    minlength: e + "最少6个字符",
                    maxlength: e + "最多20个字符",
                }
            },
            submitHandler: function (form) {
                userHelper.editpwd();
            }
        })
    }
};


$(function () {
    userHelper.pageInit();
});


