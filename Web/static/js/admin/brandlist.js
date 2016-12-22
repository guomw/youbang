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

var brandHelper = {
    ajaxUrl: "/handler/HQ.ashx",
    loaclData: [],
    isAlly: 1,
    reset: null,
    loadList: function () {
        var self = this;
        var postData = {
            action: "GetBrandList"
        }
        hotUtil.loading.show();
        hotUtil.ajaxCall(this.ajaxUrl, postData, function (ret, err) {
            if (ret) {
                if (ret.status == 200) {
                    if (ret.data) {
                        var listhtml = "";
                        self.loaclData = ret.data;
                        $.each(ret.data, function (i, item) {
                            var tempHtml = $("#templist").html();
                            tempHtml = tempHtml.replace("{NO}", i + 1);
                            tempHtml = tempHtml.replace("{Title}", item.Title);
                            tempHtml = tempHtml.replace(/{BrandId}/gm, item.BrandId);
                            listhtml += tempHtml;
                        });
                        $("#listMode").html(listhtml);
                    }
                }
            }
            hotUtil.loading.close();
        });
    },
    getModel: function (dataId) {
        var model = null;
        if (this.loaclData != null && this.loaclData.length > 0) {
            $.each(this.loaclData, function (i, item) {
                if (item.BrandId == dataId) {
                    model = item;
                    return false;
                }
            });
        }
        return model;
    },
    edit: function () {
        var param = hotUtil.serializeForm("#signupForm .form-control");
        param.action = "editbrand";
        hotUtil.loading.show();
        hotUtil.ajaxCall(this.ajaxUrl, param, function (ret, err) {
            if (ret) {
                if (ret.status == 200) {
                    swal("提交成功", "", "success");
                    brandHelper.loadList();
                    $(".close").click();
                }
                else {
                    swal(ret.statusText, "", "warning");
                }
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
                action: "DeleteBrand",
                brandid: dataId
            }
            hotUtil.loading.show();
            hotUtil.ajaxCall(brandHelper.ajaxUrl, param, function (ret, err) {
                if (ret) {
                    if (ret.status == 200) {
                        swal("删除成功", "您已经永久删除了这条信息。", "success");
                        brandHelper.loadList();

                    }
                    else {
                        swal(ret.statusText, "", "warning");
                    }
                }
                hotUtil.loading.close();
            });
        });
    },
    dialog: function (dataId) {
        if (this.reset)
            this.reset.resetForm();
        brandHelper.validate();
        var data = this.getModel(dataId);
        if (data != null) {
            $("#modal-title").text("编辑品牌");
            $("#brandid").val(dataId);
            $("#brandname").val(data.Title);
        }
        else {
            $("#modal-title").text("添加品牌");
            $("#signupForm input").val("");
            $("#brandid").val(0);
        }
    },
    pageInit: function () {
        brandHelper.loadList();
    },
    validate: function () {
        var e = "<i class='fa fa-times-circle'></i> ";
        this.reset = $("#signupForm").validate({
            rules: {
                brandname: {
                    required: !0,
                    minlength: 2
                }
            },
            messages: {
                brandname: {
                    required: e + "请输入品牌名称",
                    minlength: e + "必须两个字符以上"
                }
            },
            submitHandler: function (form) {
                brandHelper.edit();
            }
        })
    }
};

$.validator.setDefaults({
    highlight: function (e) {
        $(e).closest(".form-group").removeClass("has-success").addClass("has-error")
    },
    success: function (e) {
        e.closest(".form-group").removeClass("has-error").addClass("has-success")
    },
    errorElement: "span",
    errorPlacement: function (e, r) {
        e.appendTo(r.is(":radio") || r.is(":checkbox") ? r.parent().parent().parent() : r.parent())
    },
    errorClass: "help-block m-b-none",
    validClass: "help-block m-b-none"
});

$(function () {
    brandHelper.pageInit();
});


