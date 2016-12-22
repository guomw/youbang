using HotCoreUtils.Helper;
using Logic;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace msite.handler
{
    /// <summary>
    /// AjaxHandler 的摘要说明
    /// </summary>
    public class AjaxHandler : basicPage, IHttpHandler
    {
        private HttpContext ctx { get; set; }

        public new void ProcessRequest(HttpContext context)
        {
            string resultMsg = string.Format(@"{0} header:{1} Form:{2} UserAgent:{3} IP:{4};referrer:{5}"
                 , context.Request.Url.ToString()
                 , context.Request.Headers.ToString()
                 , context.Request.Form.ToString()
                 , StringHelper.ToString(context.Request.UserAgent)
                 , StringHelper.GetClientIP()
                 , context.Request.UrlReferrer != null ? StringHelper.ToString(context.Request.UrlReferrer.AbsoluteUri) : ""
                );
            try
            {
                ctx = context;
                DoRequest(context);
                LogHelper.Log(resultMsg, LogHelperTag.INFO, WebConfig.debugMode());
            }
            catch (Exception ex)
            {
                LogHelper.Log(string.Format("{0} StackTrace:{1} Message:{2}", resultMsg, ex.StackTrace, ex.Message), LogHelperTag.ERROR);
            }
        }

        public new bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public string action { get { return GetFormValue("action", ""); } }

        public string json { get; set; }

        private void DoRequest(HttpContext context)
        {
            try
            {
                switch (action.ToUpper())
                {
                    case "COUPONLIST":
                        CouponList();
                        break;
                    case "MYSHARECOUPONLIST":
                        MyShareCouponList();
                        break;
                    case "MYCOUPONGETLIST":
                        MyCouponGetList();
                        break;
                    case "INSERTSHARELOG":
                        insertShareLog();
                        break;
                    case "ONCOUPONGET":
                        onCouponGet();
                        break;
                    case "APPLYDISTRIBUTOR":
                        ApplyDistributor();
                        break;
                    case "MYREBATELIST":
                        myRebateList();
                        break;
                    case "SUBMITDRAWMONEY":
                        submitDrawmoney();
                        break;
                    case "SUBMITHX":
                        submitHx();
                        break;
                    case "DRAWMONEYLIST":
                        DrawMoneyList();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(string.Format("action:{0} StackTrace:{1} Message:{2}", action, ex.StackTrace, ex.Message), LogHelperTag.ERROR);
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.SERVICEERROR));
            }
            context.Response.ContentType = "application/json";
            context.Response.Write(json);
        }


        /// <summary>
        /// 获取优惠券列表
        /// </summary>
        private void CouponList()
        {
            SearchModel model = new SearchModel()
            {
                PageIndex = GetFormValue("pageIndex", 1),
                PageSize = GetFormValue("pageSize", 20),
            };
            string from = GetFormValue("from", "");
            var data = UserLogic.Instance.GetAppCashCouponList(model);
            json = JsonConvert.SerializeObject(new ResultModel(ApiStatusCode.OK, data));
        }

        /// <summary>
        /// 获取我分享的优惠券
        /// </summary>
        private void MyShareCouponList()
        {
            SearchModel model = new SearchModel()
            {
                PageIndex = GetFormValue("pageIndex", 1),
                PageSize = GetFormValue("pageSize", 20),
                type = GetFormValue("type", 0),
                UserId = GetFormValue("userid", 0),
                searchType = GetFormValue("usertype", 1)
            };
            var data = UserLogic.Instance.GetAppMyShareCouponList(model);
            json = JsonConvert.SerializeObject(new ResultModel(ApiStatusCode.OK, data));
        }


        /// <summary>
        /// 获取我分享的优惠券领取列表
        /// </summary>
        private void MyCouponGetList()
        {
            SearchModel model = new SearchModel()
            {
                PageIndex = GetFormValue("pageIndex", 1),
                PageSize = GetFormValue("pageSize", 20),
                type = GetFormValue("type", 0), //1回收，2使用 其他全部
                UserId = GetFormValue("userid", 0)
            };
            int couponid = GetFormValue("couponid", 0);
            var data = UserLogic.Instance.GetMyCouponGetList(couponid, model);
            json = JsonConvert.SerializeObject(new ResultModel(ApiStatusCode.OK, data));
        }

        /// <summary>
        /// 插入分享日志
        /// </summary>
        private void insertShareLog()
        {
            int couponid = GetFormValue("couponid", 0);
            int userid = GetFormValue("userid", 0);
            if (userid > 0 && couponid != 0 && UserLogic.Instance.AddCouponShareLog(couponid, userid))
                json = JsonConvert.SerializeObject(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonConvert.SerializeObject(new ResultModel(ApiStatusCode.发送失败));
        }


        /// <summary>
        /// 领取优惠券
        /// </summary>
        private void onCouponGet()
        {
            int couponid = GetFormValue("couponid", 0);
            int userid = GetFormValue("userid", 0);
            int currentuserid = GetFormValue("currentuserid", 0);
            string name = GetFormValue("name", "");
            string mobile = GetFormValue("mobile", "");
            string from = GetFormValue("from", "");
            bool flag = UserLogic.Instance.onCouponGet(couponid, userid, currentuserid, name, mobile, from);
            if (flag)
                json = JsonConvert.SerializeObject(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonConvert.SerializeObject(new ResultModel(ApiStatusCode.优惠券已领完));
        }


        /// <summary>
        /// 申请成功分销商
        /// </summary>
        private void ApplyDistributor()
        {
            int userid = GetFormValue("userid", 0);
            string name = GetFormValue("name", "");
            string mobile = GetFormValue("mobile", "");
            bool flag = UserLogic.Instance.ApplyDistributor(userid, name, mobile);
            if (flag)
                json = JsonConvert.SerializeObject(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonConvert.SerializeObject(new ResultModel(ApiStatusCode.保存失败));
        }



        /// <summary>
        /// 我的返利明细
        /// </summary>
        private void myRebateList()
        {
            SearchModel model = new SearchModel()
            {
                PageIndex = GetFormValue("pageIndex", 1),
                PageSize = GetFormValue("pageSize", 20),
                UserId = GetFormValue("userid", 0)
            };
            var data = UserLogic.Instance.GetAppRebateList(model);
            json = JsonConvert.SerializeObject(new ResultModel(ApiStatusCode.OK, data));
        }


        /// <summary>
        /// 提现申请
        /// </summary>
        private void submitDrawmoney()
        {
            int userid = GetFormValue("userid", 0);
            decimal money = Convert.ToDecimal(GetFormValue("money", "0"));
            string name = GetFormValue("name", "");
            string mobile = GetFormValue("mobile", "");
            string payaccount = GetFormValue("payaccount", "");

            DrawMoneyModel model = new DrawMoneyModel()
            {
                UserId = userid,
                ApplyMoney = money,
                PayAccount = payaccount,
                UserName = name,
                UserMobile = mobile
            };
            string msg = "";
            bool flag = UserLogic.Instance.AddDrawMoney(model, out msg);
            if (flag)
                json = JsonConvert.SerializeObject(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonConvert.SerializeObject(new ResultModel(ApiStatusCode.保存失败, msg));
        }


        /// <summary>
        /// 核销
        /// </summary>
        private void submitHx()
        {
            int userid = GetFormValue("userid", 0);
            int couponid = GetFormValue("couponid", 0);
            int currentuserid = GetFormValue("currentuserid", 0);
            string msg = "";
            bool flag = UserLogic.Instance.HxCoupon(userid, couponid, currentuserid, out msg);
            if (flag)
                json = JsonConvert.SerializeObject(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonConvert.SerializeObject(new ResultModel(ApiStatusCode.保存失败, msg));
        }



        /// <summary>
        /// 提现明细
        /// </summary>
        private void DrawMoneyList()
        {
            SearchModel model = new SearchModel()
            {
                PageIndex = GetFormValue("pageIndex", 1),
                PageSize = GetFormValue("pageSize", 20),
                UserId = GetFormValue("userid", 0)
            };
            var data = UserLogic.Instance.GetDrawMoneyList(model);
            json = JsonConvert.SerializeObject(new ResultModel(ApiStatusCode.OK, data));
        }
    }
}