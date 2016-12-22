using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace msite
{
    public partial class drawmoneylist : BaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TotalMoney = UserLogic.Instance.GetUserDrawTotalMoney(UserId);
            }
        }

        public decimal TotalMoney { get; set; }

        public int UserId
        {
            get
            {
                return GetQueryString("userid", 0);
            }
        }

    }
}