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
    public partial class index : BaseClass
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
            //获取用户信息
            userInfo = GetUserBaseInfo();
            if (userInfo == null)
            {
                //this.WriteLog("用户信息丢失");
                //return;

                Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("用户信息丢失"));
            }

            if (userInfo.IsActive == 0)
            {
                //this.WriteLog("账号已冻结，请联系管理员!");
                //return;
                Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("账号已冻结，请联系管理员!"));
            }

            if (userInfo.UserIdentity == 0 && userInfo.ApplyStatus == 0)
            {
                //this.WriteLog("跳转分销商审核页面!", "success");
                //return;
                Response.Redirect("/error/success.html");
            }

            if (userInfo.UserIdentity == 0)
            {
                Response.Redirect("apply.aspx");
            }

            //获取我的券
            myCouponAmountInfo = UserLogic.Instance.GetMyCouponAmonut(userInfo.UserId);

            //我的返利总数
            userInfo.UserTotalMoney = UserLogic.Instance.GetUserTotalRebate(userInfo.UserId);

            couponTotalAmount = UserLogic.Instance.GetAppCouponTotalCount();
        }

        public int couponTotalAmount { get; set; }

        public UserBaseInfoModel userInfo { get; set; }

        public MyCouponAmountModel myCouponAmountInfo { get; set; }
    }
}