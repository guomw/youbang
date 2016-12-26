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
            //couponget.aspx
            var userInfo = GetUserBaseInfo();
            if (userInfo == null)
            {
                Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("用户信息丢失"));
            }
            else if (userInfo.IsActive == 0)
            {
                Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("账号已冻结，请联系管理员!"));
            }
            else if (userInfo.UserIdentity == 0 || userInfo.UserId != UserId)
            {
                Response.Redirect(string.Format("couponget.aspx?couponid={0}&userid={1}", CouponId, UserId));
            }

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