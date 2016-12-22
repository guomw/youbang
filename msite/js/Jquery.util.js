﻿
/*
    版权所有:杭州火图科技有限公司
    地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
    (c) Copyright Hangzhou Hot Technology Co., Ltd.
    Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
    2013-2016. All rights reserved.
**/

$.extend({});

$.fn.extend({
    trim: function () {
        return $.trim(this.val());
    },
    lTrim: function () {
        return this.val().replace(/^\s+/, '');
    },
    rTrim: function () {
        return this.val().replace(/\s+$/, '');
    },

    setDisabled: function (disabled) {
        return this.each(function () {
            $(this).attr('disabled', disabled).css('opacity', disabled ? 0.5 : 1.0);
        });
    },
    setReadOnly: function (readonly) {
        return this.each(function () {
            $(this).attr('readonly', readonly).css('opacity', readonly ? 0.5 : 1.0);
        });
    },
    setChecked: function (checked, value) {
        return this.each(function () {
            if (value == undefined) {
                $(this).attr('checked', checked);
            } else if ($(this).val() == value.toString()) {
                $(this).attr('checked', checked);
            }
        });
    }
});


function getIEVersion() {
    var e = -1;
    if (navigator.appName == "Microsoft Internet Explorer") {
        var t = navigator.userAgent,
        n = new RegExp("MSIE ([0-9]{1,}[.0-9]{0,})");
        n.exec(t) != null && (e = parseFloat(RegExp.$1))
    }
    return e;
}
$.fn.OnlyNum = function () {//文本框只允许输入数字，使用：$('.OnlyNum').OnlyNum()
    $(this).on("keydown", function (e) {
        if (e.ctrlKey) return !0;
        var t = t || window.event,
        n = t.charCode || t.keyCode;
        n != 8 && n != 9 && n != 46 && (n < 37 || n > 40) && (n < 48 || n > 57) && (n < 96 || n > 105) && (t.preventDefault ? t.preventDefault() : t.returnValue = !1, $(this).show("highlight", 150))
    }).on("focus", function () {
        this.style.imeMode = "disabled"
    }).on("blur", function () {
        $(this).val($(this).val().replace(/[^\d]/g, ""))
    }),
    getIEVersion() > 0 && $(this).bind("beforepaste",
    function (e) {
        clipboardData.setData("text", clipboardData.getData("text").replace(/[^\d]/g, ""))
    })
};

$.fn.OnlyFloat = function () {//文本框只允许输入浮点类型
    $(this).on("keydown",
    function (e) {
        function r(e, t) {
            t.preventDefault ? t.preventDefault() : t.returnValue = !1,
            $(e).show("highlight", 150)
        }
        if (e.ctrlKey) return !0;
        var t = t || window.event,
        n = t.charCode || t.keyCode;
        n == 110 || n == 190 ? ($(this).val().indexOf(".") >= 0 || !$(this).val().length) && r(this, t) : n != 8 && n != 9 && n != 46 && (n < 37 || n > 40) && (n < 48 || n > 57) && (n < 96 || n > 105) && r(this, t)
    }).on("focus",
    function () {
        this.style.imeMode = "disabled"
    }).on("blur",
    function () {
        $(this).val($(this).val().replace(/[^\d.]/g, "").replace(/^\./g, ""))
    }),
    getIEVersion() > 0 && $(this).bind("beforepaste",
    function (e) {
        clipboardData.setData("text", clipboardData.getData("text").replace(/(^[0-9]([.][0-9]{1,2})?$)|(^1[0-9]([.][0-9]{1,2})?$)|(^2[0-3]([.][0-9]{1,2})?$)|(^24([.]0{1,2})?$)/g, ""))
    })
};

var hotUtil = hotUtil || {};
$.extend(hotUtil, {
    pageIndex: 1,
    pageSize: 10,
    ajaxUrl: "/handler/AjaxHandler.ashx",
    GetCookie: function (name) {
        var r = new RegExp('(^|;|\\s+)' + name + '=([^;]*)(;|$)');
        var m = document.cookie.match(r);
        return (!m ? '' : decodeURIComponent(m[2]));
    },
    SetCookie: function (name, value, expire, domain, path) {
        var s = name + '=' + encodeURIComponent(value);
        if (!J.IsUndefined(path))
            s = s + '; path=' + path;
        if (expire > 0) {
            var d = new Date();
            d.setTime(d.getTime() + expire * 1000);
            if (!J.IsUndefined(domain))
                s = s + '; domain=' + domain;
            s = s + '; expires=' + d.toGMTString();
        }
        document.cookie = s;
    },
    RemoveCookie: function (name, domain, path) {
        var s = name + '=';
        if (!J.IsUndefined(domain))
            s = s + '; domain=' + domain;
        if (!J.IsUndefined(path))
            s = s + '; path=' + path;
        s = s + '; expires=Fri, 02-Jan-1970 00:00:00 GMT';
        document.cookie = s;
    },

    isInt: function (str) { return /^-?\d+$/.test(str); },
    isNumber: function (obj) { return typeof obj == 'number'; },
    isFloat: function (str) { return /^(-?\d+)(\.\d+)?$/.test(str); },
    isEmail: function (str) { return /^[A-Z_a-z0-9-\.]+@([A-Z_a-z0-9-]+\.)+[a-z0-9A-Z]{2,4}$/.test(str); },
    isMobile: function (str) { return /^(1)\d{10}$/.test(str); },
    encode: function (str) { return encodeURIComponent(str); },
    decode: function (str) { return decodeURIComponent(str); },
    /*
    * @brief 判断字符是否为null或空或undefined
    */
    isNullOrEmpty: function (str) {
        if (typeof str != 'undefined' && str != null && str.length > 0)
            return false;
        else
            return true;
    },
    /*
    * @brief 获得页面参数
    * @param 参数名
    * @param 默认值
    * */
    getQuery: function (name, def) {
        var strHref = window.document.location.href;
        var intPos = strHref.indexOf("?");
        var strRight = strHref.substr(intPos + 1);
        var arrTmp = strRight.split("&");
        for (var i = 0; i < arrTmp.length; i++) {
            var arrTemp = arrTmp[i].split("=");
            if (arrTemp[0].toUpperCase() == name.toUpperCase()) return arrTemp[1];
        }
        if (arguments.length == 1)
            return def || "";
        if (arguments.length == 2)
            return arguments[1];
    },
    /*
     * @brief ajaxPost请求
     * @param data 请求数据
     * @param callback 回调函数(ret,err)
     * */
    ajaxCall: function (url, data, callback) {
        var op = {
            url: url,
            data: data,
            type: "post",
            dataType: "json",
            success: function (ret) {
                if (typeof callback == "function") {
                    if (typeof ret == "undefined")
                        ret = null;
                    callback(ret, null);
                }
            },
            error: function (err) {
                if (typeof callback == "function") {
                    if (typeof err == "undefined")
                        err = null;
                    callback(null, err);
                }
            }
        };
        $.ajax(op);
    },
    /*
     * @brief ajaxGet请求
     * @param data 请求数据
     * @param callback 回调函数(ret,err)
     * */
    ajaxCallGet: function (url, data, callback) {
        var op = {
            url: url,
            data: data,
            type: "get",
            dataType: "json",
            success: function (ret) {
                if (typeof callback == "function") {
                    if (typeof ret == "undefined")
                        ret = null;
                    callback(ret, null);
                }
            },
            error: function (err) {
                if (typeof callback == "function") {
                    if (typeof err == "undefined")
                        err = null;
                    callback(null, err);
                }
            }
        };
        $.ajax(op);
    },
    /*
     * @brief 序列化表单
     * @param obj 序列化对象
     */
    serializeForm: function (obj) {
        var data = {};
        $(obj).each(function (idx, element) {
            data[element.id] = $(element).val();
        });
        return data;
    },
    /*
     * @brief 手机或身份证号部分隐藏
     * @param value 值
     */
    subStringText: function (value) {
        if (value != null) {
            var result = "";
            if (value.length == 11) {
                for (var i = 0; i < value.length; i++) {
                    result += (i < 3 || i > 8) ? value.substr(i, 1) : "*";
                }
            }
            else if (value.length == 18) {
                for (var i = 0; i < value.length; i++) {
                    result += (i < 3 || i > 15) ? value.substr(i, 1) : "*";
                }
            }
            else if (value.length == 15) {
                for (var i = 0; i < value.length; i++) {
                    result += (i < 3 || i > 12) ? value.substr(i, 1) : "*";
                }
            }
            return result;
        }
        return "";
    },
    /*
     * @brief 手机或身份证号部分隐藏
     * @param el 操作对象
     * @param disCls 禁用样式名
     */
    LastTime: function (el, disCls) {
        if (el.length < 1)
            return;
        var oldText = el.html();
        var functionName = el.attr("onclick");
        el.removeAttr("onclick", "");
        if (el.hasClass(disCls)) {
            el.addClass("disabled");
            el.removeClass(disCls);
            var time = 60;
            var elTime = el.find("span");
            if (elTime.length == 0) {
                el.html("<span>" + time + "</span>秒后重发");
                elTime = el.find("span");
            }
            var timer = setInterval(function () {
                if (time > 1) {
                    var str = time - 1;
                    time = time - 1;
                    elTime.html(str);
                } else {
                    str = "";
                    el.html(oldText);
                    el.addClass(disCls);
                    el.removeClass("disabled");
                    if (functionName) { el.attr("onclick", functionName); }
                    clearInterval(timer);
                }

            }, 1000);
        } else {
            el.addClass(disCls);
            var time = 60;
            var elTime = el.find("span");
            if (elTime.length == 0) {
                el.html("<span>" + time + "</span>秒后重发");
                elTime = el.find("span");
            }
            var timer = setInterval(function () {
                if (time > 1) {
                    var str = time - 1;
                    time = time - 1;
                    elTime.html(str);
                } else {
                    str = "";
                    el.html(oldText);
                    if (functionName) { el.attr("onclick", functionName); }
                    el.removeClass(disCls);
                    clearInterval(timer);
                }

            }, 1000)
        }
    }
});
