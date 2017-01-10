/*
 * 版权所有:杭州火图科技有限公司
 * 地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
 * (c) Copyright Hangzhou Hot Technology Co., Ltd.
 * Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
 * 2013-2016. All rights reserved.
 * author guomw
**/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 接口业务状态码
    /// 作者:郭孟稳
    /// </summary>
    public enum ApiStatusCode
    {
        /// <summary>
        /// 服务器成功处理了请求，但没有返回任何内容。
        /// </summary>
        [Description("服务器成功处理了请求，但没有返回任何内容")]
        无返回 = 204,
        /// <summary>
        /// 要完成请求，需要进一步操作
        /// </summary>
        [Description("要完成请求，需要进一步操作")]
        失败 = 300,
        /// <summary>
        /// 请求要求身份验证
        /// </summary>
        [Description("请求要求身份验证")]
        未授权 = 401,
        /// <summary>
        /// 服务器拒绝请求。
        /// </summary>
        [Description("服务器拒绝请求")]
        禁止请求 = 403,
        /// <summary>
        /// 服务器找不到请求的网页。
        /// </summary>
        [Description("服务器找不到请求的网页")]
        地址错误 = 404,

        /// <summary>
        /// 服务器遇到错误
        /// </summary>
        [Description("服务器开小差了，请稍后再试!")]
        SERVICEERROR = 500,
        /// <summary>
        /// 无数据
        /// </summary>
        [Description("无数据")]
        NULL = 0,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        OK = 200,

        [Description("更新失败")]
        更新失败 = 6001,
        [Description("操作失败")]
        操作失败 = 6002,
        [Description("删除失败")]
        删除失败 = 6003,
        [Description("添加失败")]
        添加失败 = 6004,
        [Description("发送失败")]
        发送失败 = 6005,
        [Description("缺少发送目标")]
        缺少发送目标 = 6006,
        [Description("客户已存在")]
        客户已存在 = 6007,

        [Description("保存失败")]
        保存失败 = 6008,

        [Description("无操作权限")]
        无操作权限 = 6009,

        [Description("找回密码失败")]
        找回密码失败 = 6010,

        [Description("密码修改失败")]
        密码修改失败 = 6011,

        [Description("旧密码不正确")]
        旧密码不对 = 6012,

        [Description("无效手机号")]
        无效手机号 = 6013,


        [Description("账户已存在")]
        账户已存在 = 7000,
        [Description("账户不存在")]
        账户不存在 = 7001,
        [Description("账户或密码不正确")]
        账户密码不正确 = 7002,
        [Description("账户已禁用")]
        账户已禁用 = 7003,

        [Description("用户名已存在")]
        用户名已存在 = 7004,

        [Description("手机用户已存在")]
        手机用户已存在 = 7005,

        [Description("你已申请，请耐心等待")]
        你已申请请耐心等到审核 = 7006,

        [Description("用户信息丢失，请重新登录")]
        没有登录 = 70034,
        [Description("你的账号已在另一台设备登录。如非本人操作，则密码可能已泄露，建议修改密码。")]
        令牌失效 = 70035,

        [Description("兑换审核存在异常")]
        兑换审核存在异常 = 70036,

        [Description("用户信息丢失。")]
        用户信息丢失 = 70037,
        [Description("内容不能为空。")]
        内容不能为空 = 70038,


        [Description("服务器开小差了, 请重新签到")]
        请重新签到 = 74003,


        [Description("现金券已过期")]
        现金券已过期 = 75001,

        [Description("您来晚了，现金券已领完!")]
        现金券已领完 = 75002,
        [Description("现金券不存在或已使用")]
        优惠券不存在 = 75003,

        [Description("该优惠券您已转发")]
        您已转发 = 75004,


        [Description("优惠券已领完")]
        优惠券已领完 = 75004,

        [Description("优惠券数量不够")]
        优惠券数量不够 = 75005,

        [Description("请选择门店")]
        请选择门店 = 75006,

    }
}
