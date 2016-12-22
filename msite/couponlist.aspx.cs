using HotCoreUtils.Helper;
using Model.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace msite
{
    public partial class couponlist : BaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        public int UserId
        {
            get { return this.GetQueryString("userid", 0); }
        }
        /// <summary>
        /// 获取Sign
        /// </summary>
        /// <param name="couponid">The couponid.</param>
        /// <returns>System.String.</returns>
        public string GetSign(int couponid)
        {
            Dictionary<string, string> paramters = new Dictionary<string, string>();
            paramters.Add("couponid", couponid.ToString());
            paramters.Add("userid", UserId.ToString());            
            return SignatureHelper.BuildSign(paramters, ConstConfig.SECRET_KEY);
        }
    }
}