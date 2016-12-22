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
    public partial class apply : BaseClass
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
                Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("用户信息丢失"));
            }

            if (userInfo.UserIdentity != 0)
            {
                Response.Redirect("index.aspx");
            }

            if (userInfo.ApplyStatus != -1)
            {
                Response.Redirect("/error/success.html");
            }

        }


        public UserBaseInfoModel userInfo { get; set; }

    }
}