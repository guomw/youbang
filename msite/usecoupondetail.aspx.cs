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
    public partial class usecoupondetail : BaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            couponInfo = UserLogic.Instance.GetMyCouponModel(UserId, CouponId);
            if (couponInfo == null || couponInfo.IsEnable == 0)
            {
                Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("该优惠券已作废"));
            }
            if (couponInfo.IsUse == 1)
            {
                Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("优惠券已使用"));
            }
            if (couponInfo.IsRecycle == 1)
            {
                Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("优惠券已回收"));
            }
        }

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