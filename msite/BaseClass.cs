using HotCoreUtils.Helper;
using Logic;
using Model;
using Model.Config;
using Senparc.Weixin.MP.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace msite
{
    public class BaseClass : basicPage
    {
        protected override void OnPreInit(EventArgs e)
        {
            this.LoadInit(ConstConfig.storeId);
        }
        public void LoadInit(int customerid, bool tryreg = true)
        {
            this.CurrentCustomerID = customerid;
            //判断来源微信环境
            bool isWechatEnvironment = this.ValidateMicroMessenger();
            if (!isWechatEnvironment)
            {
                Response.Redirect("/WechatPropmt.html");
            }
            string callbakUrl = Server.UrlEncode(HttpContext.Current.Request.RawUrl);

            string oauthUrl = "/OAuth2/WeixinAuthorize.aspx?userinfo=0&scope=1&uda=0&customerid=" + this.CurrentCustomerID + "&tryreg=" + (tryreg ? 1 : 0) + "&redirecturl=" + callbakUrl;

            //用户授权
            WeixinOAuthUserInfoModel model = UserLogic.Instance.GetUserInfo(this.CurrentCustomerID);
            if (model != null)
            {
                this.OpenId = model.Openid;
                if (!string.IsNullOrEmpty(model.Nickname))
                {
                    this.SetOpenIdVal(this.OpenId);
                }
            }
            else
            {
                Response.Redirect(oauthUrl);
            }
        }



        public int CurrentCustomerID { get; set; }
        private void SetOpenIdVal(string openid)
        {
            CookieHelper.SetCookieVal(this.GetOpenIdKey(), 60 * 24 * 7, openid);
        }

        /// <summary>
        /// 获取用户基础信息
        /// </summary>
        /// <returns>UserBaseInfoModel.</returns>
        public UserBaseInfoModel GetUserBaseInfo()
        {
            //获取openid
            string openid = GetOpenIdVal();
            //获取用户信息
            return UserLogic.Instance.GetUserInfoByOpenId(openid);
        }


        public string GetOpenIdVal()
        {
            return CookieHelper.GetCookieVal(this.GetOpenIdKey());
        }

        private string GetOpenIdKey()
        {
            return "youb_userinfo_openid";
        }
        /// <summary>
        /// 默认用户授权
        /// </summary>
        public int scope
        {
            get
            {
                return this.GetQueryString("scope", 0);
            }
        }
        public string NoticeText { get; set; }
        public string OpenId
        {
            get;
            set;
        }


        public void WriteLog(string str, string code="404")
        {
            string content = this.LoadErrorNote(code, str);
            Response.Write(content);
        }
    }
}