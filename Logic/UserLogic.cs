/*
 * 版权所有:杭州火图科技有限公司
 * 地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
 * (c) Copyright Hangzhou Hot Technology Co., Ltd.
 * Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
 * 2013-2016. All rights reserved.
 * author guomw
**/


using HotCoreUtils.Helper;
using Logic.DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace Logic
{
    public class UserLogic
    {
        private UserDAL dal = new UserDAL();
        private static UserLogic _instance = new UserLogic();
        public static UserLogic Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="pwd">The password.</param>
        /// <returns>AdminLoginModel.</returns>
        public AdminLoginModel Login(string loginName, string pwd)
        {
            return dal.Login(loginName, pwd);
        }




        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userIdentity">用户身份，0普通 1分销商 2店员</param>
        /// <param name="applystatus">审核通过的分销商</param>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetUserList(int userIdentity, int applystatus, SearchModel model)
        {
            return dal.GetUserList(userIdentity, applystatus, model);
        }




        public ResultPageModel GetCashCouponList(SearchModel model)
        {
            return dal.GetCashCouponList(model);
        }

        /// <summary>
        /// 编辑优惠券
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool EditCashCoupon(CashCouponModel model)
        {
            string[] ids = model.ShopIds.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            string[] goodsids = model.GoodsIds.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            bool result = false;
            using (TransactionScope scope = new TransactionScope())
            {

                int couponId = 0, couponIdTwo = 0;

                if (model.CouponId > 0)
                {
                    var couponModel = dal.GetCouponModel(model.CouponId);
                    if (couponModel != null)
                    {
                        result = dal.UpdateCashCoupon(model);
                        if (result)
                        {
                            if (string.IsNullOrEmpty(couponModel.ShopIds))
                                couponId = couponModel.CouponId;

                            if (string.IsNullOrEmpty(couponModel.GoodsIds))
                                couponIdTwo = couponModel.CouponId;

                        }
                    }
                }
                else
                {
                    couponId = dal.AddCashCoupon(model);
                    couponIdTwo = couponId;
                }
                if (couponId > 0)
                {
                    if (model.ShopIds != "-100")
                    {
                        foreach (var shopId in ids)
                        {
                            if (!string.IsNullOrEmpty(shopId))
                                dal.AddShopCouponInfo(Convert.ToInt32(shopId), couponId);
                        }
                    }
                    else
                    {
                        List<ShopModel> lst = dal.GetShopIds(model.BrandId);
                        foreach (var item in lst)
                        {
                            dal.AddShopCouponInfo(item.ShopID, couponId);
                        }
                    }
                    result = true;
                }

                if (couponIdTwo > 0)
                {
                    if (model.GoodsIds != "-100")
                    {
                        foreach (var goodsid in goodsids)
                        {
                            if (!string.IsNullOrEmpty(goodsid))
                                dal.AddGoodsCouponInfo(Convert.ToInt32(goodsid), couponId);
                        }
                    }
                    result = true;
                }
                scope.Complete();
            }
            return result;
        }

        /// <summary>
        /// 删除现金券
        /// </summary>
        /// <param name="couponId">The coupon identifier.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool DeleteCashCoupon(int couponId)
        {
            return dal.DeleteCashCoupon(couponId);
        }
        /// <summary>
        /// 设置优惠券启用和禁用
        /// </summary>
        /// <param name="couponId">The coupon identifier.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool SetCouponEnable(int couponId)
        {
            return dal.SetCouponEnable(couponId);
        }


        public bool UpdateUserActive(int userId)
        {
            return dal.UpdateUserActive(userId);

        }

        /// <summary>
        /// 设置用户申请状态
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="status">The status.</param>
        /// <param name="remark">The remark.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool SetUserApplyStatus(int userId, int status, string remark)
        {
            return dal.SetUserApplyStatus(userId, status, remark);
        }

        /// <summary>
        /// 设置用户身份
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="identity">The identity.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool SetUserIdentity(int userId, int identity)
        {
            return dal.SetUserIdentity(userId, identity);
        }

        public List<BrandModel> GetBrandList()
        {
            return dal.GetBrandList();
        }


        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="brandId">品牌ID</param>
        /// <returns>List&lt;GoodsModel&gt;.</returns>
        public List<GoodsModel> GetGoodsList(int brandId)
        {
            return dal.GetGoodsList(brandId);

        }


        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool SetUserPwd(int userId, string password)
        {
            return dal.SetUserPwd(userId, password);
        }

        /// <summary>
        /// 获取现金券领取记录列表
        /// </summary>
        /// <param name="couponId">The coupon identifier.</param>
        /// <param name="type">0领取记录，1使用记录，2回收记录</param>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetUserCashCouponLogList(int couponId, int type, SearchModel model)
        {
            return dal.GetUserCashCouponLogList(couponId, type, model);
        }

        public bool DeleteBrand(int brandid)
        {
            return dal.DeleteBrand(brandid);
        }

        public bool EditBrand(int brandId, string title)
        {
            if (brandId > 0)
                return dal.UpdateBrand(brandId, title);
            else
                return dal.AddBrand(title);
        }


        /// <summary>
        /// 添加优惠券分享日志
        /// </summary>
        /// <param name="CouponId">The coupon identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool AddCouponShareLog(int CouponId, int userId)
        {
            return dal.AddCouponShareLog(CouponId, userId);
        }
        /// <summary>
        /// 添加优惠券领取数量
        /// </summary>
        /// <param name="CouponId">The coupon identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public int AddCouponGetAmount(int CouponId, int userId)
        {
            return dal.AddCouponGetAmount(CouponId, userId);
        }




        public const string ENCRYPTKEY = "lechen20";
        /// <summary>
        /// 获取微信授权用户信息
        /// </summary>
        /// <param name="customerId">商户Id</param>
        /// <returns></returns>
        public WeixinOAuthUserInfoModel GetUserInfo(int customerId)
        {
            string keyUserinfo = this.GetUserinfoDataKey(customerId);
            string encryptedUserInfo = CookieHelper.GetCookieVal(keyUserinfo);
            if (string.IsNullOrEmpty(encryptedUserInfo))
            {
                //尝试从session中读取
                if (HttpContext.Current.Session[keyUserinfo] != null)
                {
                    WeixinOAuthUserInfoModel seModel = HttpContext.Current.Session[keyUserinfo] as WeixinOAuthUserInfoModel;
                    return seModel;
                }
                return null;
            }
            try
            {
                WeixinOAuthUserInfoModel model = JsonHelper.JsonDeserialize<WeixinOAuthUserInfoModel>(EncryptHelper.Decrypt(encryptedUserInfo, ENCRYPTKEY));
                return model;
            }
            catch (Exception ex)
            {
                LogHelper.Log(string.Format("WeixinOAuthUserDataProvider->GetUserInfo发生异常：{0}", ex.Message), LogHelperTag.ERROR);
            }
            return null;
        }
        private string GetUserinfoDataKey(int customerId)
        {
            return "wxoauth_uinfo_" + customerId;
        }

        /// <summary>
        /// 记录授权完后要去的地址
        /// </summary>
        /// <param name="nonceCode">随机码</param>
        /// <param name="redirectUrl">目标URL</param>
        public void AddOAuthUrl(string nonceCode, string redirectUrl)
        {
            CookieHelper.SetCookieVal("OAUTH_" + nonceCode, 3, redirectUrl);
            dal.AddOAuthUrl(nonceCode, redirectUrl);
        }

        /// <summary>
        /// 获取授权完后要去的网址
        /// </summary>
        /// <param name="nonceCode">随机码</param>
        /// <param name="afterDel">获取完后是否删除</param>
        /// <returns>目标URL</returns>
        public string GetOAuthUrl(string nonceCode, bool afterDel = true)
        {
            string url = dal.GetOAuthUrl(nonceCode);
            if (afterDel)
            {
                dal.DelOAuthUrl(nonceCode);
            }

            string datakey = "OAUTH_" + nonceCode;
            string _bakUrl = CookieHelper.GetCookieVal("OAUTH_" + nonceCode);

            if (!string.IsNullOrEmpty(_bakUrl) && url == "")
            {
                url = _bakUrl;
            }
            CookieHelper.DelCookieVal(datakey);
            return url;
        }

        /// <summary>
        /// 得到第一个注册过的会员
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="identification"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetTopOAuthedUserId(int customerId, string identification)
        {
            return dal.GetTopOAuthedUserId(identification);
        }

        public int RegisterUser(WeixinOAuthUserInfoModel oauth)
        {
            UserModel model = new UserModel()
            {
                HeadImg = oauth.Headimgurl,
                openId = oauth.Openid,
                NickName = oauth.Nickname
            };
            return dal.AddUserInfo(model);
        }

        /// <summary>
        /// 申请分销商
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="mobile">The mobile.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool ApplyDistributor(int userId, string userName, string mobile)
        {
            return dal.ApplyDistributor(userId, userName, mobile);
        }

        public WxAccessTokenTicket GetToken(int customerid)
        {
            return dal.GetToken(customerid);
        }
        public WxAccessTokenTicket GetTicket(int customerid)
        {
            return dal.GetTicket(customerid);
        }

        public int UpdateToken(int customerid, string token)
        {
            return dal.UpdateToken(customerid, token);
        }
        public int updateTicket(int customerid, string ticket)
        {
            return dal.updateTicket(customerid, ticket);
        }



        public UserBaseInfoModel GetUserInfoByOpenId(string openid)
        {
            return dal.GetUserInfo(openid);
        }


        public UserBaseInfoModel GetUserInfoByUserId(int userid)
        {
            return dal.GetUserInfo(userid);
        }






        /// <summary>
        /// 获取优惠券剩余总张数
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetAppCouponTotalCount()
        {
            return dal.GetAppCouponTotalCount();
        }



        /// <summary>
        /// 获取优惠券--前端
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetAppCashCouponList(SearchModel model, string from, int shopId)
        {
            return dal.GetAppCashCouponList(model, from, shopId);
        }
        /// <summary>
        /// 获取优惠券详情
        /// </summary>
        /// <param name="couponId">The coupon identifier.</param>
        /// <returns>AppCashCouponModel.</returns>
        public AppCashCouponModel GetCouponDetailById(int couponId)
        {
            return dal.GetCouponDetailById(couponId);
        }

        /// <summary>
        /// 获取我的优惠券详情
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="couponId">The coupon identifier.</param>
        /// <returns>AppCashCouponModel.</returns>
        public AppCashCouponModel GetMyCouponModel(int userId, int couponId)
        {
            return dal.GetMyCouponModel(userId, couponId);
        }


        /// <summary>
        /// 添加返利日志
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>System.Int32.</returns>
        public int AddRebateLog(RebateLogModel model)
        {
            return dal.AddRebateLog(model);
        }
        /// <summary>
        /// 获取返利列表
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetRebateList(SearchModel model)
        {
            return dal.GetRebateList(model);
        }

        public ResultPageModel GetAppRebateList(SearchModel model)
        {
            return dal.GetAppRebateList(model);
        }

        /// <summary>
        /// 我的券领取的数量
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>System.Int32.</returns>
        public int MyCouponGetCount(int userId)
        {
            return dal.MyCouponGetCount(userId);
        }
        /// <summary>
        /// 我的券使用的数量
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>System.Int32.</returns>
        public int MyCouponUseCount(int userId)
        {
            return dal.MyCouponUseCount(userId);
        }
        /// <summary>
        /// 我的券回收的数量
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>System.Int32.</returns>
        public int MyCouponRecycleCount(int userId)
        {
            return dal.MyCouponRecycleCount(userId);
        }


        /// <summary>
        /// 获取我的优惠券数量
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>MyCouponAmountModel.</returns>
        public MyCouponAmountModel GetMyCouponAmonut(int userId)
        {
            MyCouponAmountModel data = new MyCouponAmountModel();
            data.myGetCount = MyCouponGetCount(userId);
            data.myUseCount = MyCouponUseCount(userId);
            data.myRecycleCount = MyCouponRecycleCount(userId);
            return data;
        }




        #region 提现


        /// <summary>
        /// 获取返利列表
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetDrawMoneyList(SearchModel model)
        {
            return dal.GetDrawMoneyList(model);
        }
        private static object dmObj = new object();
        /// <summary>
        /// 更新提现申请状态
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="status">提现状态0申请中，1同意，2已打款 3拒绝</param>
        /// <param name="remark">The remark.</param>
        /// <returns>System.Int32.</returns>
        public int UpdateDrawMoneyStatus(int id, int status, string remark)
        {
            lock (dmObj)
            {
                var model = dal.GetUserDrawMoneyModel(id);
                if (model == null)
                    return 0;
                using (TransactionScope scope = new TransactionScope())
                {
                    int flag = dal.UpdateDrawMoneyStatus(id, status, remark);

                    if (status == 2 && model.ApplyStatus == 1 && flag > 0)
                    {
                        //提现打款成功后，扣除用户余额
                        dal.addUserMoney(model.UserId, -model.ApplyMoney);
                        //扣除用户锁定余额
                        dal.addUserMoneyLocked(model.UserId, -model.ApplyMoney);
                    }

                    else if (status == 3 && flag > 0)
                    {
                        //提现打款成功后，扣除用户余额
                        //扣除用户锁定余额
                        dal.addUserMoneyLocked(model.UserId, -model.ApplyMoney);
                        //添加提现记录
                        dal.AddRebateLog(new RebateLogModel()
                        {
                            UserId = model.UserId,
                            Income = 1,
                            RebateMoney = model.ApplyMoney,
                            Remark = "提现失败"
                        });
                    }
                    scope.Complete();
                    return flag;
                }
            }

        }

        #endregion



        /// <summary>
        /// 获取用户总返利
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>System.Decimal.</returns>
        public decimal GetUserTotalRebate(int userId)
        {
            return dal.GetUserTotalRebate(userId);
        }

        /// <summary>
        /// 获取我分享的优惠券
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetAppMyShareCouponList(SearchModel model, int shopId)
        {
            return dal.GetAppMyShareCouponList(model, shopId);
        }

        /// <summary>
        /// 获取我分享的优惠券领取列表
        /// </summary>
        /// <param name="couponId">The coupon identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetMyCouponGetList(int couponId, SearchModel model)
        {
            return dal.GetMyCouponGetList(couponId, model);
        }

        /// <summary>
        /// 获取用户总提现金额
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>System.Decimal.</returns>
        public decimal GetUserDrawTotalMoney(int userId)
        {
            return dal.GetUserDrawTotalMoney(userId);
        }

        /// <summary>
        /// 获取用户数量
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>System.Decimal.</returns>
        public int GetUservVerifyCount(int userId)
        {
            return dal.GetUservVerifyCount(userId);
        }

        private static object couponGetObj = new object();
        /// <summary>
        /// 领取优惠券
        /// </summary>
        /// <param name="couponid">The couponid.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="currentuserid">The currentuserid.</param>
        /// <param name="name">The name.</param>
        /// <param name="mobile">The mobile.</param>
        /// <param name="from">From.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool onCouponGet(int couponid, int userid, int currentuserid, string name, string mobile, string from, string shops, ref ApiStatusCode apiCode)
        {
            lock (couponGetObj)
            {
                string[] ids = shops.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (couponid == 0 || currentuserid == 0)
                    {
                        apiCode = ApiStatusCode.操作失败;
                        return false;
                    }
                    var couponInfo = dal.GetCouponDetailById(couponid);
                    if (couponInfo == null || couponInfo.Amounts <= 0)
                    {
                        apiCode = ApiStatusCode.优惠券已领完;
                        return false;
                    }
                    if (ids.Count() > couponInfo.Amounts)
                    {
                        apiCode = ApiStatusCode.优惠券数量不够;
                        return false;
                    }
                    if (ids.Count() <= 0)
                    {
                        apiCode = ApiStatusCode.请选择门店;
                        return false;
                    }


                    if (from == "list" && userid > 0)
                        dal.AddCouponShareLog(couponid, userid);

                    Random rnd = new Random(Guid.NewGuid().GetHashCode());
                    var logModel = new CashCouponLogModel()
                    {
                        UserId = currentuserid,
                        CouponId = couponid,
                        CouponNo = StringHelper.CreateCheckCode(10, 1, rnd),
                        IsGet = 1,
                        GetTime = DateTime.Now,
                        ShareUserId = userid,
                        Name = name,
                        Mobile = mobile
                    };

                    foreach (var shopid in ids)
                    {
                        logModel.ShopId = Convert.ToInt32(shopid);
                        logModel.CouponNo = StringHelper.CreateCheckCode(10, 1, rnd);
                        if (dal.AddCouponGetLog(logModel) > 0)
                            dal.AddCouponGetAmount(couponid, userid);
                    }
                    dal.UpdateUserInfo(currentuserid, name, mobile);
                    scope.Complete();
                    return true;
                }
            }
        }

        private static object drawObj = new object();

        /// <summary>
        /// 添加提现申请
        /// </summary>
        /// <param name="CouponId">The coupon identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool AddDrawMoney(DrawMoneyModel model, out string msg)
        {
            lock (drawObj)
            {
                msg = "";


                UserBaseInfoModel userInfo = dal.GetUserInfo(model.UserId);

                if (userInfo == null)
                {
                    msg = "无效用户";
                    return false;
                }
                if (userInfo.IsActive == 0)
                {
                    msg = "账号已冻结，请联系管理员!";
                    return false;
                }
                if (userInfo.UserMoney < model.ApplyMoney)
                {
                    msg = "提现金额不能大于可用金额";
                    return false;
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    if (dal.AddDrawMoney(model))
                    {
                        //添加锁定金额
                        dal.addUserMoneyLocked(model.UserId, model.ApplyMoney);
                        //添加提现记录
                        dal.AddRebateLog(new RebateLogModel()
                        {
                            UserId = model.UserId,
                            Income = 0,
                            RebateMoney = -model.ApplyMoney,
                            Remark = "申请提现"
                        });
                    }
                    scope.Complete();
                }
                return true;
            }
        }

        private static object hxObj = new object();


        /// <summary>
        /// 核销优惠券
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="couponId">The coupon identifier.</param>
        /// <param name="currentUserId">The current user identifier.</param>
        /// <param name="msg">The MSG.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool HxCoupon(int userid, int couponId, int currentUserId, out string msg)
        {
            msg = "";

            lock (hxObj)
            {
                AppCashCouponModel couponInfo = dal.GetMyCouponModel(userid, couponId);
                if (couponInfo == null)
                {
                    msg = "优惠券已作废";
                    return false;
                }
                if (couponInfo.IsUse == 1)
                {
                    msg = "优惠券已使用";
                    return false;
                }
                if (couponInfo.IsRecycle == 1)
                {
                    msg = "优惠券已回收";
                    return false;
                }
                if (couponInfo.expire == 1)
                {
                    msg = "优惠券已过期";
                    return false;
                }
                var shareUserId = couponInfo.ShareUserId;
                var rebateMoney = couponInfo.RebateMoney;

                //如果分享用户ID未空，则表示该优惠是用户自己领取的，则用户归属为核销人，返利也给核销人
                shareUserId = shareUserId > 0 ? shareUserId : currentUserId;
                using (TransactionScope scope = new TransactionScope())
                {
                    //修改优惠券使用状态
                    if (dal.HxCouponInfo(couponInfo.LogId, currentUserId, couponInfo.ShareUserId > 0))
                    {
                        //添加金额
                        dal.addUserMoney(shareUserId, rebateMoney);
                        //添加返利记录
                        dal.AddRebateLog(new RebateLogModel()
                        {
                            UserId = shareUserId,
                            Income = 1,
                            RebateMoney = rebateMoney,
                            Remark = "返利"
                        });
                    }
                    scope.Complete();
                }
                return true;
            }

        }


        public ResultPageModel GetVerifyCouponList(SearchModel model)
        {
            return dal.GetVerifyList(model);
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="password">The password.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool ChanagePassword(int userId, string oldPassword, string password)
        {
            return dal.ChanagePassword(userId, oldPassword, password);
        }


        /// <summary>
        /// 获取门店
        /// </summary>
        /// <param name="BrandId">The brand identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetShopList(int BrandId, SearchModel model)
        {
            return dal.GetShopList(BrandId, model);
        }
        /// <summary>
        /// 编辑门店信息
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="apiCode">The API code.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool EditShopInfo(ShopModel model, out ApiStatusCode apiCode)
        {
            apiCode = ApiStatusCode.OK;
            if (model.ShopID > 0)
            {
                if (dal.UpdateShopInfo(model))
                {
                    apiCode = ApiStatusCode.OK;
                    return true;
                }
                else
                    apiCode = ApiStatusCode.更新失败;
            }
            else
            {
                int flag = dal.AddShopInfo(model);
                if (flag > 0)
                    return true;
                else
                    apiCode = ApiStatusCode.添加失败;
            }
            return false;
        }
        /// <summary>
        /// 删除门店
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool DeleteShopInfo(int shopId)
        {
            return dal.DeleteShopInfo(shopId);
        }
        /// <summary>
        /// 修改用户的门店
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool UpdateUserShopId(int shopId, int userId)
        {
            return dal.UpdateUserShopId(shopId, userId);
        }

        /// <summary>
        /// 根据优惠券ID，获取门店ID
        /// </summary>
        /// <param name="couponId">The coupon identifier.</param>
        /// <returns>List&lt;ShopJsonModel&gt;.</returns>
        public List<ShopJsonModel> GetShopListByCouponId(int couponId)
        {
            return dal.GetShopListByCouponId(couponId);
        }


        /// <summary>
        /// 获取门店
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="couponId">The coupon identifier.</param>
        /// <param name="currentUserId">The current user identifier.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetShopList(SearchModel model, int couponId, int currentUserId)
        {
            if (model.type == 1 || model.type == 3)//发券中心
            {
                return dal.GetShopList(0, model);
            }
            else if (model.type == 2)//我领的券
            {
                return dal.GetShopList(model);
            }
            return new ResultPageModel();
        }


        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="BrandId">The brand identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetGoodsList(int BrandId, SearchModel model)
        {
            return dal.GetGoodsList(BrandId, model);
        }


        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool DeleteGoodsInfo(int goodsId)
        {
            return dal.DeleteGoodsInfo(goodsId);
        }
        /// <summary>
        /// 更新商品信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateGoodsInfo(GoodsModel model)
        {
            return dal.UpdateGoodsInfo(model);
        }


        /// <summary>
        /// 编辑门店信息
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="apiCode">The API code.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool EditGoodsInfo(GoodsModel model, out ApiStatusCode apiCode)
        {
            apiCode = ApiStatusCode.OK;
            if (model.GoodsId > 0)
            {
                if (dal.UpdateGoodsInfo(model))
                {
                    apiCode = ApiStatusCode.OK;
                    return true;
                }
                else
                    apiCode = ApiStatusCode.更新失败;
            }
            else
            {
                int flag = dal.AddGoodsInfo(model);
                if (flag > 0)
                    return true;
                else
                    apiCode = ApiStatusCode.添加失败;
            }
            return false;
        }

    }
}
