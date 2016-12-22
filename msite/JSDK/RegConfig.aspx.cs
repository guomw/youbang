using System;
using System.Collections.Generic;
using HotCoreUtils.Helper;
using Model;
using HotCoreUtils.Http;
using Logic;
using Model.Config;
using Senparc.Weixin.MP.Containers;

namespace msite.JSDK
{
    /// <summary>
    /// 通过config接口注入权限验证配置
    /// </summary>
    public partial class RegConfig : basicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string errorInfo;
            JsSDKConfigModel sdkConfig = this.GetSignPackage(out errorInfo);
            LogHelper.Log(JsonHelper.JsonSerializer(sdkConfig));
            if (sdkConfig != null)
            {
                Response.Write(string.Format("wx.config({0});", JsonHelper.JsonSerializer(sdkConfig)));
            }
            else
            {
                Response.Write(string.Format("/*出错：{0}*/", errorInfo));
            }
        }

        public int CurrentCustomerID
        {
            get
            {
                return ConstConfig.storeId;// this.GetQueryString("customerid", 0);
            }
        }


        /// <summary>
        /// 获得要生成注入配置的实体
        /// </summary>
        /// <param name="errorInfo">出错信息</param>
        /// <returns></returns>
        private JsSDKConfigModel GetSignPackage(out string errorInfo)
        {
            errorInfo = "";
            try
            {
                string appId = this.AppId(this.CurrentCustomerID);
                string jsapi_ticket = JsApiTicketContainer.TryGetJsApiTicket(appId, this.AppSecret(this.CurrentCustomerID));// GetTicket(this.CurrentCustomerID, appId, this.AppSecret(this.CurrentCustomerID));
                string url = this.CallUrl;
                string timestamp = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();
                string noncestr = CreateNoncestr();

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("jsapi_ticket", jsapi_ticket);
                parameters.Add("noncestr", noncestr);
                parameters.Add("timestamp", timestamp);
                parameters.Add("url", url);

                string bizString = FormatBizQueryParaMap(parameters, false, false);

                string signature = Sha1(bizString);

                return new JsSDKConfigModel()
                {
                    appId = appId,
                    debug = this.DebugMode,
                    jsApiList = this.JsApiList,
                    nonceStr = noncestr,
                    signature = signature,
                    timestamp = timestamp
                };
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                LogHelper.Log(ex.Message + "|" + ex.StackTrace, LogHelperTag.ERROR);
                return null;
            }
        }

        //public string GetTicket(int customerId, string appid, string appsecret)
        //{
        //    string accessToken = "", ticket;
        //    WxAccessTokenTicket model = UserLogic.Instance.GetToken(customerId);
        //    if (model != null)
        //    {
        //        if (model.gettime.AddMinutes(90) > DateTime.Now)
        //        {
        //            accessToken = model.value;
        //            model = UserLogic.Instance.GetTicket(customerId);
        //            if (model != null)
        //            {
        //                if (model.gettime.AddMinutes(90) > DateTime.Now)
        //                    return model.value;
        //            }
        //            else
        //            {
        //                //ticket = BuildTicket(accessToken);
        //                ticket = JsApiTicketContainer.GetJsApiTicket(appid);
        //                UserLogic.Instance.updateTicket(customerId, ticket);
        //                return ticket;
        //            }
        //        }
        //    }

        //    accessToken = AccessTokenContainer.TryGetAccessToken(appid, appsecret);// BuildAccessToken(appid, appsecret, customerId);
        //    //ticket = BuildTicket(accessToken);
        //    ticket = JsApiTicketContainer.TryGetJsApiTicket(appid, appsecret);
        //    UserLogic.Instance.UpdateToken(customerId, accessToken);
        //    UserLogic.Instance.updateTicket(customerId, ticket);

        //    return ticket;
        //}


        public string BuildAccessToken(string appid, string appsecret, int customerId)
        {
            return AccessTokenContainer.GetAccessToken(appid, false);


            //string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, appsecret);
            //IHttpForm _httpForm = new HttpForm("", 15000, true, 8);
            //HttpFormResponse _response = _httpForm.Get(new HttpFormGetRequest()
            //{
            //    Url = url
            //});
            //Dictionary<object, object> dict = JsonHelper.JsonDeserialize<Dictionary<object, object>>(_response.Response);
            //if (dict.ContainsKey("access_token"))
            //{
            //    return dict["access_token"].ToString();
            //}
            //throw new Exception(string.Format("非Senparc获取AccessToken失败->appid:{0},appsecret:{1}，商户Id:{2}", appid, appsecret, customerId));
        }

        private string BuildTicket(string accessToken)
        {
            try
            {
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?type=jsapi&access_token={0}", accessToken);
                IHttpForm _httpForm = new HttpForm("", 1500, true, 8);
                HttpFormResponse _response = _httpForm.Get(new HttpFormGetRequest()
                {
                    Url = url
                });
                Dictionary<object, object> dict = JsonHelper.JsonDeserialize<Dictionary<object, object>>(_response.Response);
                if (dict.ContainsKey("ticket"))
                {
                    return dict["ticket"].ToString();
                }
                LogHelper.Log("GetTicket(获取jsapi_ticket失败):" + _response.Response, LogHelperTag.ERROR);
                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Log("WxJsApiTicketProvider->GetTicket(string accessToken)报错：" + ex.Message + "|accessToken：" + accessToken, LogHelperTag.ERROR);
                return "";
            }
        }


        /// <summary>
        /// 要调用的接口列表
        /// </summary>
        public List<string> JsApiList
        {
            get
            {
                string apilist = this.GetQueryString("apilist", "checkJsApi,onMenuShareTimeline,onMenuShareAppMessage,onMenuShareQQ,onMenuShareWeibo,onMenuShareQZone");
                return new List<string>(apilist.Split(','));
            }
        }

        /// <summary>
        /// 调试模式
        /// </summary>
        public bool DebugMode
        {
            get
            {
                return this.GetQueryString("debug", 0) == 1;
            }
        }

        /// <summary>
        /// 调用接口的当前网址
        /// </summary>
        public string CallUrl
        {
            get
            {
                string url = Server.UrlDecode(this.GetQueryString("url", ""));
                if (url == "" && Request.UrlReferrer != null)
                {
                    url = Request.UrlReferrer.ToString();
                }
                return url;
            }
        }

    }

    public class JsSDKConfigModel
    {
        public bool debug { get; set; }
        public string appId { get; set; }
        public string timestamp { get; set; }
        public string nonceStr { get; set; }
        public string signature { get; set; }
        public List<string> jsApiList { get; set; }
    }
}