using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace msite
{
    public partial class myVerifyList : BaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TotalCount = UserLogic.Instance.GetUservVerifyCount(UserId);
            }
        }

        public int TotalCount { get; set; }

        public int UserId
        {
            get
            {
                return GetQueryString("userid", 0);
            }
        }
    }
}