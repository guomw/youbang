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

var drawHelper = {
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
            action: "GetDrawMoneyList",
            pageIndex: page,
            pageSize: 20,
            key: $("#keyword").val(),
            //status: -100,
            startTime: $("#beginTime").val(),
            endTime: $("#endTime").val()
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
                            tempHtml = tempHtml.replace(/{ID}/gm, item.ID);
                            tempHtml = tempHtml.replace("{UserName}", item.UserName);
                            tempHtml = tempHtml.replace("{UserMobile}", item.UserMobile);
                            tempHtml = tempHtml.replace("{PayAccount}", item.PayAccount);
                            tempHtml = tempHtml.replace("{ApplyMoney}", item.ApplyMoney);
                            tempHtml = tempHtml.replace("{CreateTime}", item.CreateTime);
                            tempHtml = tempHtml.replace("{Remark}", item.Remark);
                            if (item.ApplyStatus == 1) {
                                tempHtml = tempHtml.replace("{ApplyStatus}", "已同意");
                                tempHtml = tempHtml.replace("{display}", "display:none;");
                                tempHtml = tempHtml.replace("{disnone}", "");
                            }
                            else if (item.ApplyStatus == 2) {
                                tempHtml = tempHtml.replace("{ApplyStatus}", "已打款");
                                tempHtml = tempHtml.replace("{display}", "display:none;");
                                tempHtml = tempHtml.replace("{disnone}", "display:none;");
                            }
                            else if (item.ApplyStatus == 3) {
                                tempHtml = tempHtml.replace("{ApplyStatus}", "已拒绝");
                                tempHtml = tempHtml.replace("{display}", "display:none;");
                                tempHtml = tempHtml.replace("{disnone}", "display:none;");
                            }
                            else {
                                tempHtml = tempHtml.replace("{ApplyStatus}", "申请中");
                                tempHtml = tempHtml.replace("{display}", "");
                                tempHtml = tempHtml.replace("{disnone}", "display:none;");
                            }
                            listhtml += tempHtml;
                        });
                        $("#listMode").html(listhtml);
                        if (self.isAlly == 2)
                            $(".applyHtml").show();
                        //初始化分页
                        var pageinate = new hotUtil.paging(".pagination", ret.data.PageIndex, ret.data.PageSize, ret.data.PageCount, ret.data.Total, 7);
                        pageinate.init(function () {
                            goTo(p, function (page) {
                                drawHelper.loadList(page);
                            });
                        });
                    }
                }
            }
            hotUtil.loading.close();
        });
    },
    search: function () {
        drawHelper.loadList(1);
    },
    searchAll: function () {
        $("#keyword,#beginTime,#endTime,#createTimePick").val("");
        drawHelper.loadList(1);
    },
    getModel: function (dataId) {
        var model = null;
        if (!hotUtil.isNullOrEmpty(dataId) && this.loaclData != null && this.loaclData.length > 0) {
            $.each(this.loaclData, function (i, item) {
                if (item.ID == dataId) {
                    model = item;
                    return false;
                }
            });
        }
        return model;
    },
    UpdateApplyStatus: function (dataId, code) {
        swal({
            title: "are you sure ?",
            text: code == 3 ? "please enter the reason" : "",
            type: code == 3 ? "input" : "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            
            closeOnConfirm: false,
            inputPlaceholder: ""
        }, function (inputValue) {
            if (inputValue) {
                var param = {
                    action: "UpdateDrawMoneyStatus",
                    drawid: dataId,
                    status: code,
                    remark: inputValue
                }
                hotUtil.loading.show();
                hotUtil.ajaxCall(drawHelper.ajaxUrl, param, function (ret, err) {
                    if (ret) {
                        if (ret.status == 200) {
                            swal("操作成功", "", "success");
                            drawHelper.loadList(drawHelper.pageIndex);
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
    pageInit: function () {
        drawHelper.loadList(drawHelper.pageIndex);
    }
};


$(function () {
    drawHelper.pageInit();
});


