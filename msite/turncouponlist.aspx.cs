using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace msite
{
    public partial class turncouponlist : BaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userInfo = GetUserBaseInfo();
            }
        }

        public UserBaseInfoModel userInfo { get; set; }
    }
}