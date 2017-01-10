/// <reference path="../plugins/sweetalert/sweetalert.min.js" />
/// <reference path="../jquery.min.js" />
/// <reference path="../plugins/hot/Jquery.util.js" />
/// <reference path="../plugins/layui/layui.js" />
/// <reference path="../plugins/CtiyPicker/js/cityPicker.js" />

/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-2016. All rights reserved.
**/

var goodsHelper = {
    ajaxUrl: "/handler/HQ.ashx",
    loaclData: [],
    pageIndex: 1,
    reset: null,
    type: hotUtil.getQuery("type"),
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
                        $("#sltbrand,#sltbrands").html(html);
                    }
                }
            }
        });
    },
    loadList: function (page) {
        var self = this;
        self.loaclData = [];
        this.pageIndex = page;
        var postData = {
            action: "getgoodslist",
            pageIndex: page,
            pageSize: 20,
            key: $("#keyword").val(),
            brandid: $("#sltbrand").val()
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
                            tempHtml = tempHtml.replace("{GoodsName}", item.GoodsName);
                            tempHtml = tempHtml.replace("{BrandTitle}", item.Title);
                            tempHtml = tempHtml.replace(/{GoodsId}/gm, item.GoodsId);
                            tempHtml = tempHtml.replace("{CreateTime}", item.CreateTime);
                            listhtml += tempHtml;
                        });
                        $("#listMode").html(listhtml);

                        //初始化分页
                        var pageinate = new hotUtil.paging(".pagination", ret.data.PageIndex, ret.data.PageSize, ret.data.PageCount, ret.data.Total, 7);
                        pageinate.init(function (p) {
                            goTo(p, function (page) {
                                goodsHelper.loadList(page);
                            });
                        });
                    }
                }
            }
            hotUtil.loading.close();
        });
    },
    search: function () {
        goodsHelper.loadList(1);
    },
    searchAll: function () {
        $("#keyword").val("");
        $("#sltbrand").val(0);
        goodsHelper.loadList(1);
    },
    getModel: function (dataId) {
        var model = null;
        if (this.loaclData != null && this.loaclData.length > 0) {
            $.each(this.loaclData, function (i, item) {
                if (item.GoodsId == dataId) {
                    model = item;
                    return false;
                }
            });
        }
        return model;
    },
    edit: function () {
        var param = hotUtil.serializeForm("#signupForm .form-control");
        param.action = "updateGoods";
        if ($("#sltbrands").val() == "0") {
            swal("请选择品牌");
            return false;
        }
        hotUtil.loading.show();
        hotUtil.ajaxCall(this.ajaxUrl, param, function (ret, err) {
            if (ret) {
                if (ret.status == 200) {
                    swal("提交成功", "", "success");
                    goodsHelper.loadList(goodsHelper.pageIndex);
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
                action: "deletegoods",
                goodsId: dataId
            }
            hotUtil.loading.show();
            hotUtil.ajaxCall(goodsHelper.ajaxUrl, param, function (ret, err) {
                if (ret) {
                    if (ret.status == 200) {
                        swal("删除成功", "您已经永久删除了这条信息。", "success");
                        goodsHelper.loadList(goodsHelper.pageIndex);
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
        var data = this.getModel(dataId);
        if (data != null) {
            $("#modal-title").text("编辑");
            $("#goodsId").val(dataId);
            $("#goodsname").val(data.GoodsName);
            $("#sltbrands").val(data.BrandId);
        }
        else {            
            $("#modal-title").text("添加");
            $("#signupForm input").val("");
            $("#sltbrands").val(0);
        }
    }
};

$(function () {
    goodsHelper.loadList(goodsHelper.pageIndex);
    goodsHelper.loadBrandList();

    if (parseInt(goodsHelper.type) != 2)
        $("#btnShop").show();

    var e = "<i class='fa fa-times-circle'></i> ";
    goodsHelper.reset = $("#signupForm").validate({
        rules: {
            goodsname: "required"
        },
        messages: {
            goodsname: e + "请输入商品名称"
        },
        submitHandler: function (form) {
            goodsHelper.edit();
        }
    })
});


