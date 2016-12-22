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

var rebateHelper = {
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
            action: "GetRebateList",
            pageIndex: page,
            pageSize: 20,
            key: $("#keyword").val(),            
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
                            tempHtml = tempHtml.replace("{UserMobile}", item.UserMobile);                            
                            tempHtml = tempHtml.replace("{RealName}", item.RealName);
                            tempHtml = tempHtml.replace("{RebateMoney}", item.RebateMoney);
                            tempHtml = tempHtml.replace("{Title}", item.Title);
                            if (!hotUtil.isNullOrEmpty(item.HeadImg))
                                tempHtml = tempHtml.replace("{UserHeadImg}", item.HeadImg);
                            else
                                tempHtml = tempHtml.replace("{UserHeadImg}", "/static/img/bg.png");

                            tempHtml = tempHtml.replace("{UseCouponName}", item.UseCouponName);
                            tempHtml = tempHtml.replace("{UseCouponMobile}", item.UseCouponMobile);
                            tempHtml = tempHtml.replace("{Remark}", item.Remark);
                            tempHtml = tempHtml.replace("{CreateTime}", item.CreateTime);
                            listhtml += tempHtml;
                        });
                        $("#listMode").html(listhtml);
                        if (self.isAlly == 2)
                            $(".applyHtml").show();
                        //初始化分页
                        var pageinate = new hotUtil.paging(".pagination", ret.data.PageIndex, ret.data.PageSize, ret.data.PageCount, ret.data.Total, 7);
                        pageinate.init(function () {
                            goTo(p, function (page) {
                                rebateHelper.loadList(page);
                            });
                        });
                    }
                }
            }
            hotUtil.loading.close();
        });
    },
    search: function () {
        rebateHelper.loadList(1);
    },
    searchAll: function () {
        $("#keyword,#beginTime,#endTime,#createTimePick").val("");
        rebateHelper.loadList(1);
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
    pageInit: function () {

        if (this.isAlly == 2)
            $(".applyHtml").show();

        rebateHelper.loadList(rebateHelper.pageIndex);
    }
};


$(function () {
    rebateHelper.pageInit();
});


