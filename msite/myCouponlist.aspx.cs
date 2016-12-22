using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace msite
{
    public partial class myCouponlist : BaseClass
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
            int ut = GetQueryString("ut", 0);
            if (ut > 0)
                UserType = userInfo.UserIdentity == 0 ? 0 : 1;
            else
                UserType = ut;


            UserId = userInfo.UserId;
        }


        public UserBaseInfoModel userInfo { get; set; }

        public int UserType { get; set; }

        public int UserId { get; set; }
    }
}