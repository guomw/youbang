using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace msite
{
    public partial class shoplist : BaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //获取用户信息
                userInfo = GetUserBaseInfo();
                if (userInfo == null)
                {
                    Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("用户信息丢失"));
                }

                if (userInfo.IsActive == 0)
                {
                    Response.Redirect("/error/500.html?note=" + HttpUtility.UrlEncode("账号已冻结，请联系管理员!"));
                }

            }
        }

        public UserBaseInfoModel userInfo { get; set; }

    }
}