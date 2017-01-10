using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public class UserBaseInfoModel
    {
        public int UserId { get; set; }
        /// <summary>
        /// 用户身份，0普通 1分销商 2店员
        /// </summary>
        /// <value>The user identity.</value>
        public int UserIdentity { get; set; }


        public int shopId { get; set; }

        public string RealName { get; set; }

        public string NickName { get; set; }
        public string HeadImg { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        /// <summary>
        /// 申请状态0申请中，1，审核通过，2，拒绝
        /// </summary>
        /// <value>The apply status.</value>
        public int ApplyStatus { get; set; }

        public int IsActive { get; set; }

        /// <summary>
        /// 可用返利
        /// </summary>
        /// <value>The user money.</value>
        public decimal UserMoney { get; set; }

        /// <summary>
        /// 总返利
        /// </summary>
        /// <value>The user total money.</value>
        public decimal UserTotalMoney { get; set; }

    }


    public class UserModel : UserBaseInfoModel
    {

        public string LoginName { get; set; }

        public string LoginPassword { get; set; }

        public string openId { get; set; }

        public decimal Money { get; set; }

        public decimal MoneyLocked { get; set; }

        public DateTime LastLoginTime { get; set; }

        public string UserToken { get; set; }
        public DateTime CreateTime { get; set; }

        public DateTime ApplyTime { get; set; }

        public string Remark { get; set; }


        public string ShopName { get; set; }
    }




    /// <summary>
    /// 后台登录用户实体
    /// </summary>
    [Serializable]
    public class AdminLoginModel
    {
        public int ID { get; set; }

        public string LoginName { get; set; }

        public string LoginPassword { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string UserName { get; set; }

        public string UserMobile { get; set; }

        public int UserStatus { get; set; }


        public string UserEmail { get; set; }

        public DateTime LastLoginTime { get; set; }


        public DateTime CreateTime { get; set; }

    }




    public class CashCouponModel
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <value>The coupon identifier.</value>
        public int CouponId { get; set; }



        /// <summary>
        /// 品牌ID
        /// </summary>
        /// <value>The shop identifier.</value>
        public int BrandId { get; set; }

        public string BrandName { get; set; }

        /// <summary>
        /// 现金券标题
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        /// <value>The shop identifier.</value>
        public int Amounts { get; set; }

        /// <summary>
        /// 返利
        /// </summary>
        /// <value>The shop identifier.</value>
        public decimal RebateMoney { get; set; }

        /// <summary>
        /// 现金券额度
        /// </summary>
        /// <value>The money.</value>
        public decimal Money { get; set; }


        /// <summary>
        /// 开始时间
        /// </summary>
        /// <value>The start time.</value>
        public DateTime StartTime { get; set; }



        /// <summary>
        /// 过期时间
        /// </summary>
        /// <value>The end time.</value>
        public DateTime EndTime { get; set; }



        /// <summary>
        /// 是否启用
        /// </summary>
        /// <value>The is enable.</value>
        public int IsEnable { get; set; }



        /// <summary>
        /// 状态名称
        /// </summary>
        /// <value>The name of the status.</value>
        public string StatusName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <value>The create time.</value>
        public DateTime CreateTime { get; set; }


        public string time { get; set; }

        public string Remark { get; set; }


        public string ShopIds { get; set; }

        public string GoodsIds { get; set; }
    }


    /// <summary>
    /// 前端调用实体
    /// </summary>
    public class AppCashCouponModel
    {

        public int LogId { get; set; }
        public int CouponId { get; set; }

        public int ShopId { get; set; }

        public int BrandId { get; set; }

        public string CouponNo { get; set; }

        public string Title { get; set; }

        public decimal Money { get; set; }
        [JsonIgnore()]
        public DateTime StartTime { get; set; }

        [JsonIgnore()]
        public DateTime EndTime { get; set; }

        public decimal RebateMoney { get; set; }

        public string Remark { get; set; }

        public int Amounts { get; set; }

        public string time { get; set; }

        [JsonIgnore()]
        public int IsUse { get; set; }

        [JsonIgnore()]
        public int IsRecycle { get; set; }
        [JsonIgnore()]
        public int IsEnable { get; set; }
        [JsonIgnore()]
        public int ShareUserId { get; set; }

        /// <summary>
        /// 过期
        /// </summary>
        /// <value>The expire.</value>
        public int expire { get; set; }


        public string ShopName { get; set; }

        public string ShopAddress { get; set; }

        public string BrandName { get; set; }

        [JsonIgnore()]
        public string GoodsIds { get; set; }

        public string GoodsName { get; set; }


    }


    public class AppMyCouponGetModel
    {
        public string HeadImg { get; set; }


        public string Name { get; set; }

        [JsonIgnore()]
        public DateTime GetTime { get; set; }


        public string CouponNo { get; set; }

        public int IsUse { get; set; }
        [JsonIgnore()]
        public int IsRecycle { get; set; }

        public string StatusText { get; set; }

        public string time { get; set; }

    }


    /// <summary>
    /// 我的优惠券汇总
    /// </summary>
    public class MyCouponAmountModel
    {
        /// <summary>
        /// 领取数量
        /// </summary>
        /// <value>My get count.</value>
        public int myGetCount { get; set; }

        /// <summary>
        /// 使用数量
        /// </summary>
        /// <value>My use count.</value>
        public int myUseCount { get; set; }

        /// <summary>
        /// 回收数量
        /// </summary>
        /// <value>My recycle count.</value>
        public int myRecycleCount { get; set; }
    }


    /// <summary>
    /// 现金券领取记录实体
    /// </summary>
    public class CashCouponLogModel
    {
        /// <summary>
        /// 现金券码ID 
        /// </summary>
        /// <value>The identifier.</value>
        public int ID { get; set; }


        /// <summary>
        /// 用户ID
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }


        /// <summary>
        /// 现金券码
        /// </summary>
        /// <value>The coupon no.</value>
        public string CouponNo { get; set; }


        /// <summary>
        /// 现金券规则ID
        /// </summary>
        /// <value>The coupon identifier.</value>
        public int CouponId { get; set; }


        /// <summary>
        /// 核销用户
        /// </summary>
        /// <value>The verify user identifier.</value>
        public int VerifyUserId { get; set; }

        /// <summary>
        /// 分享用户
        /// </summary>
        /// <value>The share user identifier.</value>
        public int ShareUserId { get; set; }


        /// <summary>
        /// 是否回收
        /// </summary>
        /// <value>The is recycle.</value>
        public int IsRecycle { get; set; }


        /// <summary>
        /// 回收时间
        /// </summary>
        /// <value>The recycle time.</value>
        public DateTime RecycleTime { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// 客户手机号码
        /// </summary>
        /// <value>The mobile.</value>
        public string Mobile { get; set; }




        /// <summary>
        /// 是否获取
        /// </summary>
        /// <value>The is get.</value>
        public int IsGet { get; set; }

        /// <summary>
        /// 获取时间
        /// </summary>
        /// <value>The get time.</value>
        public DateTime GetTime { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        /// <value>The is use.</value>
        public int IsUse { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        /// <value>The use time.</value>
        public DateTime UseTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <value>The create time.</value>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// 现金券额度
        /// </summary>
        /// <value>The money.</value>
        public decimal Money { get; set; }



        /// <summary>
        /// 开始时间
        /// </summary>
        /// <value>The start time.</value>
        public DateTime StartTime { get; set; }



        /// <summary>
        /// 过期时间
        /// </summary>
        /// <value>The end time.</value>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 只后台用到
        /// </summary>
        /// <value>The time.</value>
        public string time { get; set; }


        public int ShopId { get; set; }


        public string CouponName { get; set; }


        public decimal RebateMoney { get; set; }

        public string RealName { get; set; }

        public string UserMobile { get; set; }

        public string BrandName { get; set; }

        public string ShopName { get; set; }


    }

    public class BrandModel
    {
        public int BrandId { get; set; }

        public string Title { get; set; }

        public DateTime CreateTime { get; set; }
    }



    public class RebateListModel
    {
        public int ID { get; set; }

        /// <summary>
        /// 返利
        /// </summary>
        /// <value>The rebate money.</value>
        public decimal RebateMoney { get; set; }


        /// <summary>
        /// 返利描述
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get; set; }


        /// <summary>
        /// 获取返利的用户昵称
        /// </summary>
        /// <value>The name of the nick.</value>
        public string NickName { get; set; }


        /// <summary>
        /// 获取返利的用户头像
        /// </summary>
        /// <value>The head img.</value>
        public string HeadImg { get; set; }


        /// <summary>
        /// 获取返利的用户姓名
        /// </summary>
        /// <value>The name of the real.</value>
        public string RealName { get; set; }


        /// <summary>
        ///获得返利的用户手机
        /// </summary>
        /// <value>The user mobile.</value>
        public string UserMobile { get; set; }


        /// <summary>
        ///优惠券标题
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }


        /// <summary>
        /// 获得返利的时间
        /// </summary>
        /// <value>The create time.</value>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 使用优惠券的用户名称
        /// </summary>
        /// <value>The name.</value>
        public string UseCouponName { get; set; }

        /// <summary>
        ///使用优惠券的用户手机
        /// </summary>
        /// <value>The mobile.</value>
        public string UseCouponMobile { get; set; }
    }


    public class RebateLogModel
    {
        public int ID { get; set; }



        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }



        /// <summary>
        /// Gets or sets the rebate money.
        /// </summary>
        /// <value>The rebate money.</value>
        public decimal RebateMoney { get; set; }

        /// <summary>
        /// Gets or sets the coupon identifier.
        /// </summary>
        /// <value>The coupon identifier.</value>
        public int CouponId { get; set; }


        /// <summary>
        /// Gets or sets the coupon get identifier.
        /// </summary>
        /// <value>The coupon get identifier.</value>
        public int CouponGetId { get; set; }

        public int Income { get; set; }

        public string Remark { get; set; }

        public DateTime CreateTime { get; set; }


        public string Time { get; set; }
    }



    /// <summary>
    ///提现实体
    /// </summary>
    public class DrawMoneyModel
    {
        public int ID { get; set; }


        public int UserId { get; set; }


        /// <summary>
        /// 用户姓名
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }


        /// <summary>
        /// 用户手机
        /// </summary>
        /// <value>The user mobile.</value>
        public string UserMobile { get; set; }


        /// <summary>
        /// 支付账号
        /// </summary>
        /// <value>The pay account.</value>
        public string PayAccount { get; set; }



        /// <summary>
        /// 申请状态0申请中，1同意，2已打款 3拒绝
        /// </summary>
        /// <value>The apply status.</value>
        public int ApplyStatus { get; set; }



        /// <summary>
        /// 金额
        /// </summary>
        /// <value>The apply money.</value>
        public decimal ApplyMoney { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get; set; }


        /// <summary>
        /// Gets or sets the create time.
        /// </summary>
        /// <value>The create time.</value>
        public DateTime CreateTime { get; set; }



        public string Time { get; set; }


    }


    public class VerifyCouponModel
    {
        public string CouponName { get; set; }

        public string CouponNo { get; set; }

        public string Time { get; set; }
        [JsonIgnore()]
        public DateTime UseTime { get; set; }
    }

}
