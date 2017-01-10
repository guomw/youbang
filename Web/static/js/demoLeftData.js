/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-2016. All rights reserved.
**/

function getDemoData() {
    var menuList = [];

    menuList.push({
        ItemCode: "10001",
        ItemNavLabel: "分销管理",
        ItemUrl: "",
        ItemParentCode: "0",
        ItemShow: 1,
        ItemIcons: "fa-home"
    });

    menuList.push({
        ItemCode: "1000101",
        ItemNavLabel: "分销商列表",
        ItemUrl: "admin/userlist.html",
        ItemParentCode: "10001",
        ItemShow: 1,
        ItemIcons: "fa-home"
    });
    menuList.push({
        ItemCode: "1000102",
        ItemNavLabel: "分销商申请列表",
        ItemUrl: "admin/userlist.html?type=2",
        ItemParentCode: "10001",
        ItemShow: 1,
        ItemIcons: "fa-home"
    });

    menuList.push({
        ItemCode: "10002",
        ItemNavLabel: "品牌管理",
        ItemUrl: "admin/brandlist.html",
        ItemParentCode: "0",
        ItemShow: 1,
        ItemIcons: "fa-home"
    });

    menuList.push({
        ItemCode: "1000201",
        ItemNavLabel: "品牌管理",
        ItemUrl: "admin/brandlist.html",
        ItemParentCode: "10002",
        ItemShow: 1,
        ItemIcons: "fa-home"
    });
    menuList.push({
        ItemCode: "1000202",
        ItemNavLabel: "门店管理",
        ItemUrl: "admin/shoplist.html",
        ItemParentCode: "10002",
        ItemShow: 1,
        ItemIcons: "fa-home"
    });

    menuList.push({
        ItemCode: "1000203",
        ItemNavLabel: "商品管理",
        ItemUrl: "admin/goodslist.html",
        ItemParentCode: "10002",
        ItemShow: 1,
        ItemIcons: "fa-home"
    });

    menuList.push({
        ItemCode: "10003",
        ItemNavLabel: "优惠券管理",
        ItemUrl: "",
        ItemParentCode: "0",
        ItemShow: 1,
        ItemIcons: "fa-home"
    });

    menuList.push({
        ItemCode: "1000301",
        ItemNavLabel: "优惠券列表",
        ItemUrl: "admin/cashcouponlist.html",
        ItemParentCode: "10003",
        ItemShow: 1,
        ItemIcons: "fa-home"
    });
    menuList.push({
        ItemCode: "1000302",
        ItemNavLabel: "领取记录",
        ItemUrl: "admin/coupongetlist.html",
        ItemParentCode: "10003",
        ItemShow: 1,
        ItemIcons: "fa-home"
    });
    menuList.push({
        ItemCode: "1000303",
        ItemNavLabel: "核销记录",
        ItemUrl: "admin/coupongetlist1.html",
        ItemParentCode: "10003",
        ItemShow: 1,
        ItemIcons: "fa-home"
    });
    menuList.push({
        ItemCode: "1000304",
        ItemNavLabel: "回收记录",
        ItemUrl: "admin/coupongetlist2.html",
        ItemParentCode: "10003",
        ItemShow: 1,
        ItemIcons: "fa-home"
    });
    menuList.push({
        ItemCode: "1000305",
        ItemNavLabel: "返利记录",
        ItemUrl: "admin/rebatelist.html",
        ItemParentCode: "10003",
        ItemShow: 1,
        ItemIcons: "fa-home"
    });
    menuList.push({
        ItemCode: "1000306",
        ItemNavLabel: "提现申请列表",
        ItemUrl: "admin/drawmoneylist.html",
        ItemParentCode: "10003",
        ItemShow: 1,
        ItemIcons: "fa-home"
    });


    return menuList;
}