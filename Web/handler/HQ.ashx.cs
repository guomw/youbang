using HotCoreUtils.Helper;
using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.handler
{
    /// <summary>
    /// HQ 的摘要说明
    /// </summary>
    public class HQ : BaseLogicFactory, IHttpHandler
    {
        public AdminLoginModel user { get; set; }
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
                DoRequest(context);
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

        public int UserId
        {
            get
            {
                return GetFormValue("userid", 0);
            }
        }


        public string action { get { return GetFormValue("action", ""); } }

        public string json { get; set; }

        private void DoRequest(HttpContext context)
        {
            user = GetCurrentUser();
            if (user == null)
            {
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.没有登录));
                context.Response.ContentType = "application/json";
                context.Response.Write(json);
                return;
            }
            try
            {
                switch (action.ToUpper())
                {
                    case "ISLOGIN":
                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        dict["userData"] = user;
                        json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK, dict));
                        break;
                    case "GETCASHCOUPONLIST":
                        GetCashCouponList();
                        break;
                    case "EDITCASHCOUPON":
                        EditCashCoupon();
                        break;
                    case "DELETECASHCOUPON":
                        DeleteCashCoupon();
                        break;
                    case "SETCOUPONENABLE":
                        SetCouponEnable();
                        break;
                    case "GETBRANDLIST":
                        GetBrandList();
                        break;
                    case "GETCOUPONLOGLIST":
                        GetCouponlogList();
                        break;

                    case "GETUSERLIST":
                        GetUserList();
                        break;
                    case "UPDATEUSERACTIVE":
                        UpdateUserActive();
                        break;
                    case "UPDATEAPPLYSTATUS":
                        UpdateApplyStatus();
                        break;
                    case "EDITPWD":
                        EditPwd();
                        break;

                    case "EDITBRAND":
                        EditBrand();
                        break;
                    case "DELETEBRAND":
                        DeleteBrand();
                        break;
                    case "GETREBATELIST":
                        GetRebateList();
                        break;
                    case "GETDRAWMONEYLIST":
                        GetDrawMoneyList();
                        break;
                    case "UPDATEDRAWMONEYSTATUS":
                        UpdateDrawMoneyStatus();
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



        private void GetCashCouponList()
        {
            SearchModel model = new SearchModel()
            {
                PageIndex = Convert.ToInt32(GetFormValue("pageIndex", 1)),
                PageSize = Convert.ToInt32(GetFormValue("pageSize", 20)),
                startTime = GetFormValue("startTime", ""),
                endTime = GetFormValue("endTime", ""),
                key = GetFormValue("key", ""),
                Status = GetFormValue("status", -100)
            };
            var data = UserLogic.Instance.GetCashCouponList(model);
            json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK, data));
        }

        private void EditCashCoupon()
        {
            bool flag = UserLogic.Instance.EditCashCoupon(new CashCouponModel()
            {
                CouponId = GetFormValue("couponid", 0),
                Money = GetFormValue("couponmoney", 0),
                Title = GetFormValue("coupontitle", ""),
                StartTime = Convert.ToDateTime(GetFormValue("couponstarttime", DateTime.Now.ToString("yyyy-MM-dd"))),
                EndTime = Convert.ToDateTime(GetFormValue("couponendtime", DateTime.Now.AddDays(5).ToString("yyyy-MM-dd"))),
                BrandId = GetFormValue("sltBrand", 0),
                IsEnable = GetFormValue("couponenable", 1),
                Amounts = GetFormValue("couponamount", 0),
                RebateMoney = GetFormValue("couponrebate", 0),
                Remark = GetFormValue("couponremark", "")
            });
            json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
        }
        /// <summary>
        /// 删除现金券
        /// </summary>
        private void DeleteCashCoupon()
        {
            int couponId = GetFormValue("couponId", 0);
            UserLogic.Instance.DeleteCashCoupon(couponId);
            json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
        }
        /// <summary>
        /// Sets the coupon enable.
        /// </summary>
        private void SetCouponEnable()
        {
            if (UserLogic.Instance.SetCouponEnable(GetFormValue("couponId", 0)))
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.操作失败));
        }


        private void GetBrandList()
        {
            var data = UserLogic.Instance.GetBrandList();
            json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK, data));
        }


        /// <summary>
        /// 获取领取记录
        /// </summary>
        private void GetCouponlogList()
        {
            SearchModel model = new SearchModel()
            {
                PageIndex = Convert.ToInt32(GetFormValue("pageIndex", 1)),
                PageSize = Convert.ToInt32(GetFormValue("pageSize", 20)),
                startTime = GetFormValue("startTime", ""),
                endTime = GetFormValue("endTime", ""),
                key = GetFormValue("key", ""),
                Status = GetFormValue("searchType", -1)
            };
            int couponId = GetFormValue("couponId", 0);
            var data = UserLogic.Instance.GetUserCashCouponLogList(couponId, GetFormValue("searchType", -1), model);
            json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK, data));
        }


        private void GetUserList()
        {
            SearchModel model = new SearchModel()
            {
                PageIndex = Convert.ToInt32(GetFormValue("pageIndex", 1)),
                PageSize = Convert.ToInt32(GetFormValue("pageSize", 20)),
                startTime = GetFormValue("startTime", ""),
                endTime = GetFormValue("endTime", ""),
                key = GetFormValue("key", "")
            };
            int applystatus = GetFormValue("applystatus", -2);
            var data = UserLogic.Instance.GetUserList(1, applystatus, model);
            json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK, data));
        }
        private void UpdateUserActive()
        {
            if (UserLogic.Instance.UpdateUserActive(GetFormValue("userid", 0)))
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.操作失败));
        }

        /// <summary>
        /// 修改分销商申请状态
        /// </summary>
        private void UpdateApplyStatus()
        {
            int userid = GetFormValue("userid", 0);
            int active = GetFormValue("active", 0);
            string remark = GetFormValue("remark", "");
            var flag = UserLogic.Instance.SetUserApplyStatus(userid, active, remark);

            if (flag)
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.更新失败));
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        private void EditPwd()
        {
            int userid = GetFormValue("userid", 0);
            string userpwd = EncryptHelper.MD5(GetFormValue("userpwd", ""));
            var flag = UserLogic.Instance.SetUserPwd(userid, userpwd);
            if (flag)
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.更新失败));

        }

        /// <summary>
        /// 编辑品牌
        /// </summary>
        private void EditBrand()
        {
            int brandid = GetFormValue("brandid", 0);
            string brandname = GetFormValue("brandname", "");
            var flag = UserLogic.Instance.EditBrand(brandid, brandname);
            if (flag)
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.更新失败));
        }
        /// <summary>
        /// 删除品牌
        /// </summary>
        private void DeleteBrand()
        {
            int brandid = GetFormValue("brandid", 0);
            var flag = UserLogic.Instance.DeleteBrand(brandid);
            if (flag)
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
            else
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.更新失败));
        }


        /// <summary>
        /// 获取返利记录
        /// </summary>
        private void GetRebateList()
        {
            SearchModel model = new SearchModel()
            {
                PageIndex = Convert.ToInt32(GetFormValue("pageIndex", 1)),
                PageSize = Convert.ToInt32(GetFormValue("pageSize", 20)),
                startTime = GetFormValue("startTime", ""),
                endTime = GetFormValue("endTime", ""),
                key = GetFormValue("key", "")
            };
            var data = UserLogic.Instance.GetRebateList(model);
            json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK, data));
        }

        /// <summary>
        /// 提现列表
        /// </summary>
        private void GetDrawMoneyList()
        {
            SearchModel model = new SearchModel()
            {
                PageIndex = Convert.ToInt32(GetFormValue("pageIndex", 1)),
                PageSize = Convert.ToInt32(GetFormValue("pageSize", 20)),
                startTime = GetFormValue("startTime", ""),
                endTime = GetFormValue("endTime", ""),
                key = GetFormValue("key", ""),
                Status = GetFormValue("status", -100)
            };
            var data = UserLogic.Instance.GetDrawMoneyList(model);
            json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK, data));
        }

        /// <summary>
        /// 修改提现状态
        /// </summary>
        private void UpdateDrawMoneyStatus()
        {
            int drawid = GetFormValue("drawid", 0);
            int status = GetFormValue("status", -1);
            string remark = GetFormValue("remark", "");
            //申请状态0申请中，1同意，2已打款 3拒绝
            if (status == 1 || status == 2 || status == 3)
            {
                var flag = UserLogic.Instance.UpdateDrawMoneyStatus(drawid, status, remark);
                if (flag > 0)
                    json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
                else
                    json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.更新失败));
            }
            else
                json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.SERVICEERROR));
        }

    }
}