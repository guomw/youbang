using HotCoreUtils.Helper;
using Logic;
using Model;
using Model.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace msite
{
    public partial class coupondetail : BaseClass
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
            couponInfo = UserLogic.Instance.GetCouponDetailById(CouponId);
            if (couponInfo == null)
            {
                Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("优惠券信息丢失"));
            }
            if (couponInfo.Amounts <= 0)
            {
                Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("优惠券已领完"));
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

        /// <summary>
        /// 签名
        /// </summary>
        /// <value>The sign.</value>
        public string Sign
        {
            get
            {
                Dictionary<string, string> paramters = new Dictionary<string, string>();
                paramters.Add("couponid", CouponId.ToString());
                paramters.Add("userid", UserId.ToString());
                return SignatureHelper.BuildSign(paramters, ConstConfig.SECRET_KEY);
            }
        }

    }
}