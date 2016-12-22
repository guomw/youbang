/// <reference path="../plugins/switchery/switchery.js" />
/// <reference path="../plugins/sweetalert/sweetalert.min.js" />
/// <reference path="../jquery.min.js" />
/// <reference path="../plugins/hot/Jquery.util.js" />

/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-2016. All rights reserved.
**/

var couponHelper = {
    ajaxUrl: "/handler/HQ.ashx",
    loaclData: [],
    pageIndex: 1,
    reset: null,
    switchery: null,
    loadList: function (page) {
        var self = this;
        self.loaclData = [];
        this.pageIndex = page;
        var postData = {
            action: "GetCashCouponList",
            pageIndex: page,
            pageSize: 20,
            startTime: "",
            endTime: "",
            key: $("#keyword").val(),
            status: $("#sltStatus").val()
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
                            tempHtml = tempHtml.replace("{NO}", (i + 1));
                            tempHtml = tempHtml.replace(/{CouponId}/gm, item.CouponId);
                            tempHtml = tempHtml.replace("{Title}", item.Title);
                            tempHtml = tempHtml.replace("{BrandName}", item.BrandName);
                            tempHtml = tempHtml.replace("{Money}", item.Money);
                            tempHtml = tempHtml.replace("{Amounts}", item.Amounts);
                            tempHtml = tempHtml.replace("{RebateMoney}", item.RebateMoney);
                            tempHtml = tempHtml.replace("{StatusName}", "<span style='color:red;'>" + item.StatusName + "</span>");
                            tempHtml = tempHtml.replace("{Time}", item.time);
                            tempHtml = tempHtml.replace("{Remark}", item.Remark);

                            tempHtml = tempHtml.replace("{ActiveText}", item.IsEnable == 1 ? "禁用" : "启用");
                            listhtml += tempHtml;
                        });
                        $("#listMode").html(listhtml);

                        //初始化分页
                        var pageinate = new hotUtil.paging(".pagination", ret.data.PageIndex, ret.data.PageSize, ret.data.PageCount, ret.data.Total, 7);
                        pageinate.init(function (p) {
                            goTo(p, function (page) {
                                couponHelper.loadList(page);
                            });
                        });
                    }
                }
            }
            hotUtil.loading.close();
        });
    },
    loadBrandList: function () {
        var postData = {
            action: "GetBrandList"
        }
        hotUtil.ajaxCall(this.ajaxUrl, postData, function (ret, err) {
            if (ret) {
                if (ret.status == 200) {
                    if (ret.data) {
                        var html = '<option value="0">请选择</option>';
                        $.each(ret.data, function (i, item) {
                            html += '<option value="' + item.BrandId + '">' + item.Title + '</option>';
                        });
                        $("#sltBrand").html(html);
                    }
                }
            }
        });
    },
    search: function () {
        couponHelper.loadList(1);
    },
    searchAll: function () {
        $("#keyword").val("");
        $("#sltStatus").val("0");
        couponHelper.loadList(1);
    },
    getModel: function (dataId) {
        var model = null;
        if (!hotUtil.isNullOrEmpty(dataId) && this.loaclData != null && this.loaclData.length > 0) {
            $.each(this.loaclData, function (i, item) {
                if (item.CouponId == dataId) {
                    model = item;
                    return false;
                }
            });
        }
        return model;
    },
    edit: function () {
        var self = this;
        var postData = hotUtil.serializeForm("#signupForm .form-control");
        postData.action = "EditCashCoupon";
        postData.couponenable = $("#couponenable").attr("checked") ? 1 : 0;
        if ($("#sltBrand").val() == "0") {
            swal("请选择品牌");
            return false;
        }
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
    del: function (dataId) {
        swal({
            title: "您确定要删除这条信息吗",
            text: "删除后将无法恢复，请谨慎操作！",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "删除",
            closeOnConfirm: false,
        }, function () {
            var param = {
                action: "DeleteCashCoupon",
                couponId: dataId
            }
            hotUtil.loading.show();
            hotUtil.ajaxCall(couponHelper.ajaxUrl, param, function (ret, err) {
                if (ret) {
                    if (ret.status == 200) {
                        swal("删除成功", "您已经永久删除了这条信息。", "success");
                        couponHelper.loadList(couponHelper.pageIndex);
                    }
                    else {
                        swal(ret.statusText, "", "warning");
                    }
                }
                hotUtil.loading.close();
            });
        });
    },
    updateActive: function (dataId, obj) {
        $(obj).text("禁用");
        var param = {
            action: "SetCouponEnable",
            couponId: dataId
        }
        hotUtil.loading.show();
        hotUtil.ajaxCall(this.ajaxUrl, param, function (ret, err) {
            if (ret) {
                if (ret.status == 200) {
                    swal("设置成功", "", "success");
                    couponHelper.loadList(couponHelper.pageIndex);
                }
                else {
                    swal(ret.statusText, "", "warning");
                }
            }
            hotUtil.loading.close();
        });
    },
    newTab: function (dataId) {
        hotUtil.newTab("admin/coupongetlist.html?couponid=" + dataId, "现金券领取记录");
    },
    dialog: function (dataId) {
        if (this.reset)
            this.reset.resetForm();
        var data = this.getModel(dataId);
        if (data != null) {
            $("#modal-title").text("编辑优惠券");
            $("#couponid").val(dataId);
            $("#coupontitle").val(data.Title);
            $("#couponmoney").val(data.Money);
            $("#couponamount").val(data.Amounts);
            $("#couponrebate").val(data.RebateMoney);
            $("#couponstarttime").val(data.StartTime);
            $("#couponendtime").val(data.EndTime);
            $("#couponenable").setChecked(data.IsEnable == 1);
            $("#sltBrand").val(data.BrandId);
        }
        else {
            $("#modal-title").text("添加优惠券");
            $("#signupForm input").val("");
            $("#sltBrand").val(0);
        }
    },
    pageInit: function () {
        couponHelper.loadBrandList();
        couponHelper.loadList(couponHelper.pageIndex);
        couponHelper.validate();
        var elem = document.querySelector('.js-switch');
        this.switchery = new Switchery(elem);
    },
    validate: function () {
        var e = "<i class='fa fa-times-circle'></i> ";
        this.reset = $("#signupForm").validate({
            rules: {
                coupontitle: "required",
                couponmoney: {
                    required: !0,
                    number: true
                },
                couponamount: {
                    required: !0,
                    number: true
                },
                couponrebate: {
                    required: !0,
                    number: true
                },
                couponstarttime: "required",
                couponendtime: "required",
                couponremark: {
                    maxlength:50
                }
            },
            messages: {
                coupontitle: e + "请输入标题",
                couponmoney: {
                    required: e + "请输入优惠券金额",
                    number: e + "只允许输入数字"
                },
                couponamount: {
                    required: e + "请输入优惠券数量",
                    number: e + "只允许输入数字"
                },
                couponrebate: {
                    required: e + "请输入优惠券返利",
                    number: e + "只允许输入数字"
                },
                couponstarttime: e + "请输入开始时间",
                couponendtime: e + "请输入结束时间",
                couponremark: {
                    maxlength: "最多50个字符"
                }
            },
            submitHandler: function (form) {
                couponHelper.edit();
            }
        })
    }
};

$(function () {

    $('.OnlyNum').OnlyNum();

    couponHelper.pageInit();
    $("#couponenable").change(function () {
        if ($(this).attr("checked"))
            $(this).setChecked(false);
        else
            $(this).setChecked(true);
    });
});


