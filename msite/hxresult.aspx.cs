using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace msite
{
    public partial class hxresult : BaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            couponType = 200;
            resultTitle = "有效编号";
            resultClass = "weui_icon_success";
            //获取用户信息
            userInfo = GetUserBaseInfo();
            if (userInfo == null)
            {                
                Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("权限不足"));
            }
            if (userInfo.IsActive == 0)
            {                
                Response.Redirect("/error/500.html?note="+HttpUtility.UrlEncode("账号已冻结，请联系管理员"));
            }
            if (userInfo.UserIdentity != 2)
            {                
                Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("权限不足"));
            }

            couponInfo = UserLogic.Instance.GetMyCouponModel(UserId, CouponId);
            if (couponInfo == null || couponInfo.IsEnable == 0)
            {   
                Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("该优惠券已作废"));
            }
            if (couponInfo.expire == 1)
            {
                couponType = 1;
                resultTitle = "已过期";
                resultClass = "weui_icon_msg";
            }
            if (couponInfo.IsRecycle == 1)
            {
                couponType = 2;
                resultTitle = "已回收";
                resultClass = "weui_icon_msg";
            }
            if (couponInfo.IsUse == 1)
            {
                couponType = 3;
                resultTitle = "已使用";
                resultClass = "weui_icon_info";
            }

        }
        public string resultTitle { get; set; }

        public string resultClass { get; set; }
        /// <summary>
        /// 1过期，2回收，3已使用
        /// </summary>
        /// <value>The type of the coupon.</value>
        public int couponType { get; set; }

        public UserBaseInfoModel userInfo { get; set; }
        public AppCashCouponModel couponInfo { get; set; }

        public int CouponId
        {
            get { return this.GetQueryString("couponid", 0); }
        }

        public int UserId
        {
            get { return this.GetQueryString("userid", 0); }
        }
    }
}