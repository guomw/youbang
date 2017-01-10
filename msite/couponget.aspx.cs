using HotCoreUtils.Helper;
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
    public partial class couponget : BaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }


        private void LoadData()
        {
            userInfo = GetUserBaseInfo();
            //userInfo = new UserBaseInfoModel();
            couponInfo = UserLogic.Instance.GetCouponDetailById(CouponId);
            if (couponInfo == null)
            {
                Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("优惠券信息丢失"));
            }
            if (couponInfo.Amounts <= 0)
            {
                Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("优惠券已领完"));
            }

            shopJson = JsonHelper.JsonSerializer(UserLogic.Instance.GetShopListByCouponId(CouponId));


        }

        public string shopJson { get; set; }

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