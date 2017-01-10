/*
 * 版权所有:杭州火图科技有限公司
 * 地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
 * (c) Copyright Hangzhou Hot Technology Co., Ltd.
 * Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
 * 2013-2016. All rights reserved.
 * author guomw
**/


using HotCoreUtils.DB;
using HotCoreUtils.Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DAL
{
    public class UserDAL : AbstractDAL
    {
        /// <summary>
        /// 后台登录
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPassword"></param>
        /// <returns></returns>
        public AdminLoginModel Login(string loginName, string loginPassword)
        {
            string strSql = string.Empty;
            strSql = "select ID,LoginName,LoginPassword,UserName,RoleId,UserMobile,UserStatus,UserEmail,LastLoginTime,CreateTime from Manager where LoginName=@LoginName and LoginPassword=@LoginPassword";
            SqlParameter[] parm = {
                   new SqlParameter("@LoginName", loginName),
                   new SqlParameter("@LoginPassword", loginPassword)
            };
            using (SqlDataReader dr = DbHelperSQLP.ExecuteReader(WebConfig.getConnectionString(), CommandType.Text, strSql, parm))
            {
                return DbHelperSQLP.GetEntity<AdminLoginModel>(dr);
            }
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
            ResultPageModel result = new ResultPageModel();
            if (model == null)
                return result;
            string strSql = @"select u.*,shop.ShopName from UserList u
                                left join ShopList shop on shop.ShopId=u.shopId
                                 where 1=1 ";

            if (!string.IsNullOrEmpty(model.key))
                strSql += string.Format(" and u.RealName like '%{0}%' ", model.key);

            if (!string.IsNullOrEmpty(model.mobile))
                strSql += " and u.Mobile=@Mobile ";

            if (!string.IsNullOrEmpty(model.shopname))
                strSql += " and shop.ShopName=@ShopName ";

            if (applystatus == 1)
                strSql += " and u.ApplyStatus=1 and u.UserIdentity<>0 ";
            else if (applystatus == 0)
                strSql += " and u.ApplyStatus=0 ";
            else
                strSql += " and u.ApplyStatus=-2 ";
            string orderbyField = "u.CreateTime";
            if (userIdentity == 1)
            {
                orderbyField = "u.ApplyTime";
            }

            if (!string.IsNullOrEmpty(model.startTime))
                strSql += " and CONVERT(nvarchar(10)," + orderbyField + ",121)>=CONVERT(nvarchar(10),@startTime,121)";
            if (!string.IsNullOrEmpty(model.endTime))
                strSql += " and CONVERT(nvarchar(10)," + orderbyField + ",121)<=CONVERT(nvarchar(10),@endTime,121)";
            var param = new[] {
                new SqlParameter("@startTime", model.startTime),
                new SqlParameter("@endTime", model.endTime),
                new SqlParameter("@IsActive", model.Status),
                new SqlParameter("@Mobile", model.mobile),
                new SqlParameter("@ShopName", model.shopname)
            };
            //生成sql语句
            return getPageData<UserModel>(model.PageSize, model.PageIndex, strSql, orderbyField, param);
        }




        /// <summary>
        ///添加用户
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>System.Int32.</returns>
        public int AddUserInfo(UserModel model)
        {
            string strSql = "insert into UserList(NickName,HeadImg,openId) values(@NickName,@HeadImg,@openId);select @@IDENTITY";
            var param = new[] {
                new SqlParameter("@NickName", model.NickName),
                new SqlParameter("@HeadImg", model.HeadImg),
                new SqlParameter("@openId", model.openId)
            };
            object obj = DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, strSql, param);
            return obj != null ? Convert.ToInt32(obj) : 0;
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
            string strSql = "update UserList set RealName=@RealName,Mobile=@Mobile ,ApplyStatus=0,ApplyTime=GETDATE() where UserId=@UserId and UserIdentity=0";
            var param = new[] {
                new SqlParameter("@RealName", userName),
                new SqlParameter("@Mobile", mobile),
                new SqlParameter("@UserId",userId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }



        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="mobile">The mobile.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool UpdateUserInfo(int userId, string userName, string mobile)
        {
            string strSql = "update UserList set RealName=@RealName,Mobile=@Mobile where UserId=@UserId";
            var param = new[] {
                new SqlParameter("@RealName", userName),
                new SqlParameter("@Mobile", mobile),
                new SqlParameter("@UserId",userId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }







        /// <summary>
        /// 获取优惠券
        /// </summary>        
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetCashCouponList(SearchModel model)
        {
            ResultPageModel result = new ResultPageModel();
            if (model == null)
                return result;
            string strSql = @"select c.CouponId,c.BrandId,c.Title,c.Money,c.StartTime,c.EndTime,c.IsEnable,c.Amounts,c.RebateMoney,c.CreateTime,b.Title as BrandName,c.Remark,c.ShopIds,c.GoodsIds from CouponList c
                            left join BrandList b on b.BrandId=c.BrandId
                            where IsDel=0 ";

            if (!string.IsNullOrEmpty(model.key))
                strSql += string.Format(" and c.Title like '%{0}%' ", model.key);


            if (model.Status != -100)
            {
                strSql += " and c.IsEnable=@IsEnable";
            }


            if (!string.IsNullOrEmpty(model.startTime))
                strSql += " and CONVERT(nvarchar(10),c.CreateTime,121)>=CONVERT(nvarchar(10),@startTime,121) ";
            if (!string.IsNullOrEmpty(model.endTime))
                strSql += " and CONVERT(nvarchar(10),c.CreateTime,121)<=CONVERT(nvarchar(10),@endTime,121) ";
            var param = new[] {
                new SqlParameter("@startTime", model.startTime),
                new SqlParameter("@endTime", model.endTime),
                new SqlParameter("@IsEnable", model.Status)
            };
            //生成sql语句
            return getPageData<CashCouponModel>(model.PageSize, model.PageIndex, strSql, "c.CreateTime", param, ((items) =>
            {
                items.ForEach((item) =>
                {
                    item.time = item.StartTime.ToString("yyyy.MM.dd") + " 至 " + item.EndTime.ToString("yyyy.MM.dd");

                    if (DateTime.Compare(item.EndTime.AddHours(24), DateTime.Now) > 0)
                    {
                        if (item.IsEnable == 1)
                            item.StatusName = "启用";
                        else
                            item.StatusName = "未启用";
                    }
                    else
                        item.StatusName = "已过期";
                });
            }));
        }



        /// <summary>
        /// 获取优惠券--前端
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetAppCashCouponList(SearchModel model, string from, int shopId)
        {
            ResultPageModel result = new ResultPageModel();
            if (model == null)
                return result;
            string strSql = @"select c.CouponId,c.Title,'' as CouponNo,c.Money,c.StartTime,c.EndTime,c.RebateMoney,c.Remark,c.Amounts-COUNT(l.ID) as Amounts,b.Title as BrandName,c.GoodsIds from CouponList c
                            left join CouponLog l on l.CouponId=c.CouponId and l.IsRecycle=0       
                            left join BrandList b on b.BrandId=c.BrandId              
                            left join GoodsCouponList g on g.CouponId=c.CouponId            
                            where c.IsDel=0  and c.IsEnable=1 {0}
                            and CONVERT(nvarchar(10),c.StartTime,121)<=CONVERT(nvarchar(10),GETDATE(),121) 
                            and CONVERT(nvarchar(10),c.EndTime,121)>=CONVERT(nvarchar(10),GETDATE(),121) 
                            group by c.CouponId,c.Title,c.Money,c.StartTime,c.EndTime,c.Amounts,c.Remark,c.RebateMoney,b.Title,c.GoodsIds";

            //if (from == "list")
            //{
            //    strSql = @"select c.CouponId,c.Title,ISNULL(l.CouponNo,'') as CouponNo,c.Money,c.StartTime,c.EndTime,c.RebateMoney,c.Remark,c.Amounts-COUNT(l.ID) as Amounts from CouponList c
            //                left join CouponLog l on l.CouponId=c.CouponId and l.IsRecycle=0 and l.UserId=@UserId
            //                left join ShopCouponList s on s.CouponId=c.CouponId
            //                where c.IsDel=0  and c.IsEnable=1  {0}
            //                and CONVERT(nvarchar(10),c.StartTime,121)<=CONVERT(nvarchar(10),GETDATE(),121) 
            //                and CONVERT(nvarchar(10),c.EndTime,121)>=CONVERT(nvarchar(10),GETDATE(),121) 
            //                group by c.CouponId,c.Title,c.Money,c.StartTime,c.EndTime,c.Amounts,c.Remark,c.RebateMoney,l.CouponNo";
            //}

            //strSql = string.Format(strSql, shopId > 0 ? " and s.ShopId=@shopId " : "");
            string sb = "";
            if (model.brandId > 0)
                sb += " and c.BrandId=@BrandId ";

            if (model.goodsId > 0)
                sb += " and g.GoodsId=@GoodsId ";

            strSql = string.Format(strSql, string.IsNullOrEmpty(sb) ? "" : sb);

            var param = new[] {
                new SqlParameter("@UserId", model.UserId),
                new SqlParameter("@BrandId", model.brandId),
                new SqlParameter("@GoodsId", model.goodsId)
            };
            //生成sql语句
            return getPageData<AppCashCouponModel>(model.PageSize, model.PageIndex, strSql, "c.EndTime", param, ((items) =>
              {
                  items.ForEach((item) =>
                  {
                      item.time = item.StartTime.ToString("yyyy.MM.dd") + " 至 " + item.EndTime.ToString("yyyy.MM.dd");
                      if (DateTime.Compare(item.EndTime.AddHours(24), DateTime.Now) > 0)
                          item.expire = 0;
                      else
                          item.expire = 1;
                      if (!string.IsNullOrEmpty(item.GoodsIds))
                          item.GoodsName = GetGoodsName(item.GoodsIds);
                      else
                          item.GoodsName = "";
                  });
              }));
        }


        /// <summary>
        /// 根据多个GoodsId，获取商品名称，并拼接在一起
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>System.String.</returns>
        private string GetGoodsName(string ids)
        {
            var arr = ids.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            string strSql = string.Format("select stuff((select '、'+GoodsName from GoodsList where GoodsId in ({0}) for xml path('')),1,1,'')", string.Join(",", arr));
            return DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, strSql).ToString();
        }


        /// <summary>
        /// 获取我分享的优惠券
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetAppMyShareCouponList(SearchModel model, int shopId)
        {
            ResultPageModel result = new ResultPageModel();
            if (model == null)
                return result;
            string strSql = string.Empty;
            if (model.searchType == 1)
            {
                strSql = @"select s.CouponId,'' as CouponNo, c.Title,c.Money,c.Remark,c.StartTime,c.EndTime,s.Amount as Amounts,s.CreateTime from CouponShareLog s
                                left join CouponList c on c.CouponId=s.CouponId
                                 where s.UserId=@UserId and c.IsDel=0 and c.IsEnable=1 ";

                if (model.type > 0)
                    strSql += "and CONVERT(nvarchar(10),c.EndTime,121)<=CONVERT(nvarchar(10),GETDATE(),121) ";
            }
            else
            {
                strSql = @"select s.CouponId,s.CouponNo, c.Title,c.Money,c.Remark,c.StartTime,c.EndTime,s.CreateTime ,0  as Amounts from CouponLog s
                            left join CouponList c on c.CouponId=s.CouponId
                            where c.IsDel=0 and c.IsEnable=1 and s.UserId=@UserId  and s.IsUse=0 and s.IsRecycle=0 and s.ShopId=@ShopId";
            }
            var parm = new[] {
                new SqlParameter("@UserId",model.UserId),
                new SqlParameter("@ShopId",shopId)
            };
            //生成sql语句
            return getPageData<AppCashCouponModel>(model.PageSize, model.PageIndex, strSql, "s.CreateTime", parm, ((items) =>
             {
                 items.ForEach((item) =>
                 {
                     item.time = item.StartTime.ToString("yyyy.MM.dd") + " 至 " + item.EndTime.ToString("yyyy.MM.dd");
                     if (DateTime.Compare(item.EndTime.AddHours(24), DateTime.Now) > 0)
                         item.expire = 0;
                     else
                         item.expire = 1;

                 });
             }));
        }



        /// <summary>
        /// 获取我的优惠券详情
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="couponId">The coupon identifier.</param>
        /// <returns>AppCashCouponModel.</returns>
        public AppCashCouponModel GetMyCouponModel(int userId, int couponId)
        {
            string strSql = @"select top 1 s.id as LogId,s.CouponId,s.CouponNo, c.Title,c.Money,c.RebateMoney,c.Remark,c.StartTime,c.EndTime,s.IsUse,s.IsRecycle,c.IsEnable,s.ShareUserId,s.ShopId,shop.ShopName,shop.ShopAddress,b.Title as BrandName,c.GoodsIds from CouponLog s
                                left join CouponList c on c.CouponId=s.CouponId
                                left join ShopList shop on shop.ShopId=s.ShopId
                                left join BrandList b on b.BrandId=c.BrandId   
                                where c.IsDel=0  and s.UserId=@UserId and s.CouponId=@CouponId";
            var parm = new[] {
                new SqlParameter("@UserId",userId),
                new SqlParameter("@CouponId",couponId)
            };
            using (SqlDataReader dr = DbHelperSQLP.ExecuteReader(WebConfig.getConnectionString(), CommandType.Text, strSql, parm))
            {
                var data = DbHelperSQLP.GetEntity<AppCashCouponModel>(dr);
                if (data != null)
                {
                    data.time = data.StartTime.ToString("yyyy.MM.dd") + " 至 " + data.EndTime.ToString("yyyy.MM.dd");

                    if (DateTime.Compare(data.EndTime.AddHours(24), DateTime.Now) > 0)
                        data.expire = 0;
                    else
                        data.expire = 1;

                    if (!string.IsNullOrEmpty(data.GoodsIds))
                        data.GoodsName = GetGoodsName(data.GoodsIds);
                    else
                        data.GoodsName = "";
                }

                return data;

            }
        }



        /// <summary>
        /// 获取我分享的优惠券领取列表
        /// </summary>
        /// <param name="couponId">The coupon identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetMyCouponGetList(int couponId, SearchModel model)
        {
            string strSql = @"select u.HeadImg,l.Name,l.GetTime,l.CouponNo ,l.IsUse,l.IsRecycle from CouponLog l
                                left join UserList u on u.UserId=l.UserId
                                 where l.ShareUserId=@UserId and l.CouponId=@CouponId";


            if (model.type == 1)
                strSql += " and l.IsRecycle=1";
            else if (model.type == 2)
                strSql += " and l.IsUse=1";

            var parm = new[] {
                new SqlParameter("@UserId",model.UserId),
                new SqlParameter("@CouponId",couponId)
            };
            //生成sql语句
            return getPageData<AppMyCouponGetModel>(model.PageSize, model.PageIndex, strSql, "l.CreateTime", parm, ((items) =>
             {
                 items.ForEach((item) =>
                 {
                     item.time = item.GetTime.ToString("yyyy.MM.dd HH:mm:ss");
                     if (item.IsUse == 1)
                         item.StatusText = "已使用";
                     else if (item.IsRecycle == 1)
                         item.StatusText = "回收";
                     else
                         item.StatusText = "未使用";
                 });
             }));

        }















        /// <summary>
        /// 获取优惠券剩余总张数
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetAppCouponTotalCount()
        {
            string strSql = @" select ISNULL(SUM(Amounts),0) from (
                     select c.Amounts-COUNT(l.ID) as Amounts from CouponList c
                    left join CouponLog l on l.CouponId=c.CouponId and l.IsRecycle=0 
                    where c.IsDel=0 and c.IsEnable=1
                    and CONVERT(nvarchar(10),c.StartTime,121)<=CONVERT(nvarchar(10),GETDATE(),121) 
                    and CONVERT(nvarchar(10),c.EndTime,121)>=CONVERT(nvarchar(10),GETDATE(),121) 
                    group by c.Amounts) as temp";


            object obj = DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, strSql);
            return obj != null ? Convert.ToInt32(obj) : 0;
        }


        /// <summary>
        /// 获取优惠券详情
        /// </summary>
        /// <param name="couponId">The coupon identifier.</param>
        /// <returns>AppCashCouponModel.</returns>
        public AppCashCouponModel GetCouponDetailById(int couponId)
        {
            string strSql = @"select c.CouponId,c.Title,c.Money,c.StartTime,c.EndTime,c.RebateMoney,c.Remark,c.Amounts-COUNT(l.ID) as Amounts,c.BrandId,b.Title as BrandName,c.GoodsIds from CouponList c
                            left join CouponLog l on l.CouponId=c.CouponId and l.IsRecycle=0 
                            left join BrandList b on b.BrandId=c.BrandId
                            where c.IsDel=0  and c.IsEnable=1 and c.CouponId=@CouponId
                            and CONVERT(nvarchar(10),c.StartTime,121)<=CONVERT(nvarchar(10),GETDATE(),121) 
                            and CONVERT(nvarchar(10),c.EndTime,121)>=CONVERT(nvarchar(10),GETDATE(),121) 
                            group by c.CouponId,c.Title,c.Money,c.StartTime,c.EndTime,c.Amounts,c.Remark,c.RebateMoney,c.BrandId,b.Title,c.GoodsIds ";
            var parm = new[] {
                new SqlParameter("@CouponId",couponId)
            };
            using (SqlDataReader dr = DbHelperSQLP.ExecuteReader(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm))
            {
                var data = DbHelperSQLP.GetEntity<AppCashCouponModel>(dr);
                if (data != null)
                    data.time = data.StartTime.ToString("yyyy.MM.dd") + " 至 " + data.EndTime.ToString("yyyy.MM.dd");


                if (!string.IsNullOrEmpty(data.GoodsIds))
                    data.GoodsName = GetGoodsName(data.GoodsIds);
                else
                    data.GoodsName = "";

                return data;
            }
        }

        public CashCouponModel GetCouponModel(int couponId)
        {
            string strSql = @"select * from CouponList where CouponId=@CouponId";
            var parm = new[] {
                new SqlParameter("@CouponId",couponId)
            };
            using (SqlDataReader dr = DbHelperSQLP.ExecuteReader(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm))
            {
                var data = DbHelperSQLP.GetEntity<CashCouponModel>(dr);
                if (data != null)
                    data.time = data.StartTime.ToString("yyyy.MM.dd") + " 至 " + data.EndTime.ToString("yyyy.MM.dd");
                return data;
            }
        }

        /// <summary>
        /// 删除现金券
        /// </summary>
        /// <param name="couponId">The coupon identifier.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool DeleteCashCoupon(int couponId)
        {
            string strSql = "update CouponList set IsDel=1 where CouponId=@CouponId";
            var parm = new[] {
                new SqlParameter("@CouponId",couponId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm) > 0;
        }
        /// <summary>
        /// 添加现金券
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int AddCashCoupon(CashCouponModel model)
        {
            string strSql = "insert into CouponList(Title,Money,StartTime,EndTime,IsEnable,BrandId,Amounts,RebateMoney,Remark,Goodsids,ShopIds) values(@Title,@Money,@StartTime,@EndTime,@IsEnable,@BrandId,@Amounts,@RebateMoney,@Remark,@Goodsids,@ShopIds);select @@IDENTITY;";
            var parm = new[] {
                new SqlParameter("@Title", model.Title),
                new SqlParameter("@Money", model.Money),
                new SqlParameter("@StartTime", model.StartTime),
                new SqlParameter("@EndTime", model.EndTime),
                new SqlParameter("@IsEnable", model.IsEnable),
                new SqlParameter("@BrandId", model.BrandId),
                new SqlParameter("@Amounts", model.Amounts),
                new SqlParameter("@RebateMoney", model.RebateMoney),
                new SqlParameter("@Remark", model.Remark),
                new SqlParameter("@Goodsids", model.GoodsIds),
                new SqlParameter("@ShopIds", model.ShopIds)
            };
            object obj = DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm);
            if (obj != null)
                return Convert.ToInt32(obj);
            return 0;
        }

        /// <summary>
        /// 修改现金券
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool UpdateCashCoupon(CashCouponModel model)
        {
            string strSql = "update CouponList set Title=@Title,Money=@Money,StartTime=@StartTime,EndTime=@EndTime,IsEnable=@IsEnable,BrandId=@BrandId,Amounts=@Amounts,RebateMoney=@RebateMoney,Remark=@Remark,ShopIds=@ShopIds,Goodsids=@Goodsids where CouponId=@CouponId";
            var parm = new[] {
                new SqlParameter("@Title", model.Title),
                new SqlParameter("@Money", model.Money),
                new SqlParameter("@StartTime", model.StartTime),
                new SqlParameter("@EndTime", model.EndTime),
                new SqlParameter("@IsEnable", model.IsEnable),
                new SqlParameter("@CouponId", model.CouponId),
                new SqlParameter("@BrandId", model.BrandId),
                new SqlParameter("@Amounts", model.Amounts),
                new SqlParameter("@RebateMoney", model.RebateMoney),
                new SqlParameter("@Remark", model.Remark),
                new SqlParameter("@ShopIds", model.ShopIds),
                new SqlParameter("@Goodsids", model.GoodsIds)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm) > 0;
        }
        /// <summary>
        /// 设置优惠券启用和禁用
        /// </summary>
        /// <param name="couponId">The coupon identifier.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool SetCouponEnable(int couponId)
        {
            string strSql = "update CouponList set IsEnable=ABS(IsEnable-1) where CouponId=@CouponId";
            var parm = new[] {
                new SqlParameter("@CouponId",couponId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm) > 0;
        }


        public List<BrandModel> GetBrandList()
        {
            string strSql = "select * from BrandList";
            using (SqlDataReader dr = DbHelperSQLP.ExecuteReader(WebConfig.getConnectionString(), CommandType.Text, strSql))
            {
                return DbHelperSQLP.GetEntityList<BrandModel>(dr);
            }
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="brandId">品牌ID</param>
        /// <returns>List&lt;GoodsModel&gt;.</returns>
        public List<GoodsModel> GetGoodsList(int brandId)
        {
            string strSql = "select * from GoodsList  where 1=1 ";
            if (brandId > 0)
                strSql += " and BrandId=@BrandId ";
            var parm = new[] {
                new SqlParameter("@BrandId",brandId)
            };
            using (SqlDataReader dr = DbHelperSQLP.ExecuteReader(WebConfig.getConnectionString(), CommandType.Text, strSql, parm))
            {
                return DbHelperSQLP.GetEntityList<GoodsModel>(dr);
            }
        }


        /// <summary>
        /// 设置用户冻结或激活
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool UpdateUserActive(int userid)
        {
            string strSql = "update UserList set IsActive=ABS(IsActive-1) where UserId=@UserId";
            var parm = new[] {
                new SqlParameter("@UserId",userid)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm) > 0;
        }


        /// <summary>
        /// 添加用户的锁定金额
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="money">The money.</param>
        /// <returns>System.Boolean.</returns>
        public bool addUserMoneyLocked(int userid, decimal money)
        {
            string strSql = "update UserList set MoneyLocked=MoneyLocked+@MoneyLocked where UserId=@UserId";
            var parm = new[] {
                new SqlParameter("@MoneyLocked",money),
                new SqlParameter("@UserId",userid)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm) > 0;
        }
        /// <summary>
        /// 添加用户金额
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="money">The money.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool addUserMoney(int userid, decimal money)
        {
            string strSql = "update UserList set Money=Money+@Money where UserId=@UserId";
            var parm = new[] {
                new SqlParameter("@Money",money),
                new SqlParameter("@UserId",userid)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm) > 0;
        }







        public bool DeleteBrand(int brandid)
        {
            string strSql = "delete from BrandList where brandId=@brandId";
            var parm = new[] {
                new SqlParameter("@brandId",brandid)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm) > 0;
        }

        public bool UpdateBrand(int brandid, string title)
        {
            string strSql = "update BrandList set Title=@Title where brandId=@brandId";
            var parm = new[] {
                new SqlParameter("@brandId",brandid),
                new SqlParameter("@Title",title)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm) > 0;
        }

        public bool AddBrand(string title)
        {
            string strSql = "insert into BrandList(Title) values(@Title)";
            var parm = new[] {
                new SqlParameter("@Title",title)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm) > 0;
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
            string strSql = "update UserList set ApplyStatus=@ApplyStatus,Remark=@Remark";

            if (status == 1)
                strSql += ",UserIdentity=1";
            strSql += "  where UserId=@UserId";
            var param = new[] {
                new SqlParameter("@ApplyStatus",status),
                new SqlParameter("@Remark",remark),
                new SqlParameter("@UserId",userId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }


        /// <summary>
        /// 设置用户身份
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="identity">The identity.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool SetUserIdentity(int userId, int identity)
        {
            string strSql = "update UserList set UserIdentity=@UserIdentity  where UserId=@UserId";
            var param = new[] {
                new SqlParameter("@UserIdentity",identity),
                new SqlParameter("@UserId",userId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }


        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool SetUserPwd(int userId, string password)
        {
            string strSql = "update UserList set LoginPassword=@LoginPassword  where UserId=@UserId";
            var param = new[] {
                new SqlParameter("@LoginPassword",password),
                new SqlParameter("@UserId",userId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
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
            ResultPageModel result = new ResultPageModel();
            if (model == null)
                return result;
            string strSql = @"select L.ID,L.UserId,L.CouponNo,L.CouponId,L.Name,L.Mobile,L.IsGet,L.GetTime,L.IsUse,L.UseTime,L.VerifyUserId,L.ShareUserId,L.IsRecycle,L.RecycleTime,L.CreateTime,C.StartTime,C.EndTime,C.Money from CouponLog L with(nolock)
                left join CouponList C on C.CouponId=L.CouponId
                where L.IsDel=0 ";



            strSql = @"select L.ID,L.UserId,L.CouponNo,c.Title as CouponName,c.RebateMoney,u.RealName,u.Mobile as UserMobile,b.Title as BrandName,s.ShopName,L.CouponId,L.Name,L.Mobile,L.IsGet,L.GetTime,L.IsUse,L.UseTime,L.VerifyUserId,L.ShareUserId,L.IsRecycle,L.RecycleTime,L.CreateTime,C.StartTime,C.EndTime,C.Money from CouponLog L with(nolock)
                        left join CouponList C on C.CouponId=L.CouponId
                        left join BrandList b on b.BrandId=C.BrandId
                        left join UserList u on {0}
                        left join ShopList s on s.ShopId=L.ShopId
                        where L.IsDel=0 ";


            strSql = string.Format(strSql, type == 1 ? " u.UserId=L.VerifyUserId " : " u.UserId=L.ShareUserId ");

            if (!string.IsNullOrEmpty(model.key))
                strSql += " and L.CouponNo like '%" + model.key + "%' ";

            if (!string.IsNullOrEmpty(model.mobile))
                strSql += " and L.Mobile=@Mobile ";

            if (!string.IsNullOrEmpty(model.name))
                strSql += " and u.RealName=@RealName ";

            if (!string.IsNullOrEmpty(model.shopname))
                strSql += " and s.ShopName=@ShopName ";


            if (couponId > 0)
                strSql += " and L.CouponId=@CouponId ";
            string orderbyField = "L.GetTime";
            if (type == 0) //领取
                strSql += " and L.IsGet=1 ";
            else if (type == 1)  //核销
            {
                strSql += " and L.IsUse=1 ";
                orderbyField = "L.UseTime";
            }
            else if (type == 2) //回收
            {
                strSql += " and L.IsRecycle=1 ";
                orderbyField = "L.RecycleTime";
            }

            if (!string.IsNullOrEmpty(model.startTime))
                strSql += " and CONVERT(nvarchar(10)," + orderbyField + ",121)>=CONVERT(nvarchar(10),@startTime,121) ";
            if (!string.IsNullOrEmpty(model.endTime))
                strSql += " and CONVERT(nvarchar(10)," + orderbyField + ",121)<=CONVERT(nvarchar(10),@endTime,121)";
            var param = new[] {
                new SqlParameter("@startTime", model.startTime),
                new SqlParameter("@endTime", model.endTime),
                new SqlParameter("@CouponId", couponId),
                new SqlParameter("@Mobile", model.mobile),
                new SqlParameter("@RealName", model.name),
                new SqlParameter("@ShopName", model.shopname)
            };
            //生成sql语句
            return getPageData<CashCouponLogModel>(model.PageSize, model.PageIndex, strSql, orderbyField, param, (items =>
            {
                items.ForEach(item =>
                {
                    item.time = item.StartTime.ToString("yyyy.MM.dd") + " 至 " + item.EndTime.ToString("yyyy.MM.dd");
                });
            }));
        }


        /// <summary>
        /// 添加优惠券分享日志
        /// </summary>
        /// <param name="CouponId">The coupon identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool AddCouponShareLog(int CouponId, int userId)
        {
            string strSql = @"if NOT EXISTS(select 1 from CouponShareLog where CouponId=@CouponId and UserId=@UserId)
                            begin
                                insert into CouponShareLog(CouponId,UserId) values(@CouponId,@UserId)
                            end
                            else
                            begin
                                update CouponShareLog  set ShareCount=ShareCount+1 where CouponId=@CouponId and UserId=@UserId
                            end";

            var param = new[] {
                new SqlParameter("@CouponId",CouponId),
                new SqlParameter("@UserId",userId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }



        /// <summary>
        /// 添加优惠券领取数量
        /// </summary>
        /// <param name="CouponId">The coupon identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public int AddCouponGetAmount(int CouponId, int userId)
        {
            string strSql = "update CouponShareLog  set Amount=Amount+1 where CouponId=@CouponId and UserId=@UserId";
            var param = new[] {
                new SqlParameter("@CouponId",CouponId),
                new SqlParameter("@UserId",userId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param);
        }

        /// <summary>
        /// 添加优惠券领取记录
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>System.Int32.</returns>
        public int AddCouponGetLog(CashCouponLogModel model)
        {
            string strSql = "insert into CouponLog(UserId,CouponId,CouponNo,Name,Mobile,IsGet,GetTime,ShareUserId,ShopId) values(@UserId,@CouponId,@CouponNo,@Name,@Mobile,@IsGet,@GetTime,@ShareUserId,@ShopId)";
            var parm = new[] {
                new SqlParameter("@UserId", model.UserId),
                new SqlParameter("@CouponId", model.CouponId),
                new SqlParameter("@CouponNo", model.CouponNo),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@Mobile", model.Mobile),
                new SqlParameter("@IsGet", model.IsGet),
                new SqlParameter("@GetTime", model.GetTime),
                new SqlParameter("@ShareUserId", model.ShareUserId),
                new SqlParameter("@ShopId", model.ShopId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm);
        }


        /// <summary>
        /// 修改优惠券使用状态
        /// </summary>
        /// <param name="logid">The logid.</param>
        /// <param name="verifyUserId">The verify user identifier.</param>
        /// <param name="isShare">该优惠券是否是有分享来源</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool HxCouponInfo(int logid, int verifyUserId, bool isShare = true)
        {
            string strSql = "update CouponLog set IsUse=1,UseTime=GETDATE(),VerifyUserId=@VerifyUserId";

            if (!isShare)
                strSql += " ,ShareUserId=@VerifyUserId";
            strSql += " where ID=@ID";
            var parm = new[] {
                new SqlParameter("@ID",logid),
                new SqlParameter("@VerifyUserId",verifyUserId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm) > 0;
        }



        #region 微信授权相关操作
        /// <summary>
        /// 添加授权后要跳转的网址
        /// </summary>
        /// <param name="nonceCode"></param>
        /// <param name="redirectUrl"></param>
        public void AddOAuthUrl(string nonceCode, string redirectUrl)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Hot_UserOAuthUrl(");
            strSql.Append("UOU_NonceCode,UOU_RedirectUrl,UOU_AddTime)");
            strSql.Append(" values (");
            strSql.Append("@UOU_NonceCode,@UOU_RedirectUrl,@UOU_AddTime)");
            SqlParameter[] parameters = {
                    new SqlParameter("@UOU_NonceCode", SqlDbType.VarChar,10),
                    new SqlParameter("@UOU_RedirectUrl", SqlDbType.VarChar,400),
                    new SqlParameter("@UOU_AddTime", SqlDbType.DateTime)};
            parameters[0].Value = nonceCode;
            parameters[1].Value = redirectUrl;
            parameters[2].Value = DateTime.Now;

            DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parameters);
        }

        public string GetOAuthUrl(string nonceCode)
        {
            string sql = string.Format(@"select UOU_RedirectUrl from Hot_UserOAuthUrl WITH (nolock) where UOU_NonceCode='{0}'",
                nonceCode);
            object obj = DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, sql);
            if (obj != null)
            {
                return obj.ToString();
            }
            return "";
        }
        /// <summary>
        /// 删除临时的跳转地址
        /// </summary>
        /// <param name="nonceCode"></param>
        public void DelOAuthUrl(string nonceCode)
        {
            string strSql = string.Format("delete from Hot_UserOAuthUrl where UOU_NonceCode='{0}'", nonceCode);
            DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql);
        }

        /// <summary>
        /// 得到第一个注册过的会员
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="identification"></param>
        /// <returns></returns>
        public int GetTopOAuthedUserId(string identification)
        {
            string sql = string.Format(@"select top 1 UserId from UserList where  openId='{0}'", identification);
            object obj = DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, sql);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }



        public WxAccessTokenTicket GetToken(int customerid)
        {
            string sql = "select id,token as value,gettime from Hot_WxAccessToken where customerid=@customerid";
            SqlParameter[] parameters = {
                    new SqlParameter("@customerid", customerid)
             };
            WxAccessTokenTicket model = new WxAccessTokenTicket();
            using (SqlDataReader dr = DbHelperSQLP.ExecuteReader(WebConfig.getConnectionString(), CommandType.Text, sql, parameters))
            {
                model = DbHelperSQLP.GetEntity<WxAccessTokenTicket>(dr);
            }
            return model;
        }
        public WxAccessTokenTicket GetTicket(int customerid)
        {
            string sql = "select id,ticket as value,gettime from Hot_WxTicket where customerid=@customerid";
            SqlParameter[] parameters = {
                    new SqlParameter("@customerid", customerid)
             };
            WxAccessTokenTicket model = new WxAccessTokenTicket();
            using (SqlDataReader dr = DbHelperSQLP.ExecuteReader(WebConfig.getConnectionString(), CommandType.Text, sql, parameters))
            {
                model = DbHelperSQLP.GetEntity<WxAccessTokenTicket>(dr);
            }
            return model;
        }
        public int UpdateToken(int customerid, string token)
        {
            string sql = "update Hot_WxAccessToken set token=@token,gettime=GETDATE()  where customerid=@customerid";
            SqlParameter[] parameters = {
                    new SqlParameter("@customerid", customerid),
                    new SqlParameter("@token", token)
             };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, sql, parameters);
        }

        public int updateTicket(int customerid, string ticket)
        {
            string sql = "update Hot_WxTicket set ticket=@ticket,gettime=GETDATE()  where customerid=@customerid";
            SqlParameter[] parameters = {
                    new SqlParameter("@customerid", customerid),
                    new SqlParameter("@ticket", ticket)
             };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, sql, parameters);
        }

        #endregion





        #region 返利



        /// <summary>
        /// 获取返利列表
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetRebateList(SearchModel model)
        {
            ResultPageModel result = new ResultPageModel();
            if (model == null)
                return result;
            string strSql = @"select r.ID,r.RebateMoney,r.Remark,u.NickName,u.HeadImg,u.RealName,u.Mobile as UserMobile,c.Title,r.CreateTime,l.Name as UseCouponName,l.Mobile as UseCouponMobile from RebateLog r 
                                left join UserList u on u.UserId=r.UserId
                                left join CouponList c on c.CouponId=r.CouponId
                                left join CouponLog l on l.ID=r.CouponGetId
                                where 1=1 and r.Income=1 ";

            if (!string.IsNullOrEmpty(model.key))
                strSql += " and u.Mobile=@Mobile ";

            if (!string.IsNullOrEmpty(model.startTime))
                strSql += " and CONVERT(nvarchar(10),r.CreateTime,121)>=CONVERT(nvarchar(10),@startTime,121) ";
            if (!string.IsNullOrEmpty(model.endTime))
                strSql += " and CONVERT(nvarchar(10),r.CreateTime,121)<=CONVERT(nvarchar(10),@endTime,121)";
            var param = new[] {
                new SqlParameter("@startTime", model.startTime),
                new SqlParameter("@endTime", model.endTime),
                new SqlParameter("@Mobile", model.key)
            };
            //生成sql语句
            return getPageData<RebateListModel>(model.PageSize, model.PageIndex, strSql, "r.CreateTime", param);
        }
        public ResultPageModel GetAppRebateList(SearchModel model)
        {
            ResultPageModel result = new ResultPageModel();
            if (model == null)
                return result;
            string strSql = @"select ID,UserId,RebateMoney,CouponId,CouponGetId,Income,Remark,CreateTime from RebateLog where UserId=@UserId ";

            var param = new[] {
                new SqlParameter("@UserId", model.UserId)
            };
            //生成sql语句
            return getPageData<RebateLogModel>(model.PageSize, model.PageIndex, strSql, "CreateTime", param, (items =>
            {
                items.ForEach(item =>
                {
                    item.Time = item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                });
            }));
        }
        /// <summary>
        /// 添加返利日志
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>System.Int32.</returns>
        public int AddRebateLog(RebateLogModel model)
        {
            string strSql = "insert into RebateLog(UserId,RebateMoney,CouponId,CouponGetId,Income,Remark) values(@UserId,@RebateMoney,@CouponId,@CouponGetId,@Income,@Remark)";
            var parm = new[] {
                new SqlParameter("@UserId", model.UserId),
                new SqlParameter("@RebateMoney", model.RebateMoney),
                new SqlParameter("@CouponId", model.CouponId),
                new SqlParameter("@CouponGetId", model.CouponGetId),
                new SqlParameter("@Income", model.Income),
                new SqlParameter("@Remark", model.Remark)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql.ToString(), parm);
        }


        /// <summary>
        /// 我的券领取的数量
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>System.Int32.</returns>
        public int MyCouponGetCount(int userId)
        {
            string strSql = "select  COUNT(1) from CouponLog  where ShareUserId=@UserId and IsGet=1";
            var param = new[] {
                new SqlParameter("@UserId",userId)
            };
            return Convert.ToInt32(DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, strSql, param));
        }
        /// <summary>
        /// 我的券使用的数量
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>System.Int32.</returns>
        public int MyCouponUseCount(int userId)
        {
            string strSql = "select  COUNT(1) from CouponLog  where ShareUserId=@UserId and IsUse=1";
            var param = new[] {
                new SqlParameter("@UserId",userId)
            };
            return Convert.ToInt32(DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, strSql, param));
        }
        /// <summary>
        /// 我的券回收的数量
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>System.Int32.</returns>
        public int MyCouponRecycleCount(int userId)
        {
            string strSql = "select  COUNT(1) from CouponLog  where ShareUserId=@UserId and IsRecycle=1";
            var param = new[] {
                new SqlParameter("@UserId",userId)
            };
            return Convert.ToInt32(DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, strSql, param));
        }




        #endregion




        #region 提现


        /// <summary>
        /// 获取返利列表
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetDrawMoneyList(SearchModel model)
        {
            ResultPageModel result = new ResultPageModel();
            if (model == null)
                return result;
            string strSql = @"select * from DrawMoneyList  where 1=1 ";

            if (!string.IsNullOrEmpty(model.key))
                strSql += " and UserMobile=@UserMobile ";

            if (model.Status != -100)
            {
                strSql += " and ApplyStatus=@ApplyStatus ";
            }
            if (model.UserId > 0)
                strSql += " and UserId=@UserId ";

            if (!string.IsNullOrEmpty(model.startTime))
                strSql += " and CONVERT(nvarchar(10),CreateTime,121)>=CONVERT(nvarchar(10),@startTime,121) ";
            if (!string.IsNullOrEmpty(model.endTime))
                strSql += " and CONVERT(nvarchar(10),CreateTime,121)<=CONVERT(nvarchar(10),@endTime,121)";
            var param = new[] {
                new SqlParameter("@startTime", model.startTime),
                new SqlParameter("@endTime", model.endTime),
                new SqlParameter("@UserMobile", model.key),
                new SqlParameter("@ApplyStatus", model.Status),
                new SqlParameter("@UserId", model.UserId)
            };
            //生成sql语句
            return getPageData<DrawMoneyModel>(model.PageSize, model.PageIndex, strSql, "CreateTime", param, (items =>
            {
                items.ForEach(item =>
                {
                    item.Time = item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                });
            }));
        }
        /// <summary>
        /// 添加提现记录
        /// </summary>
        /// <param name="CouponId">The coupon identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool AddDrawMoney(DrawMoneyModel model)
        {
            string strSql = @" insert into DrawMoneyList(UserId,UserName,UserMobile,PayAccount,ApplyMoney) values(@UserId,@UserName,@UserMobile,@PayAccount,@ApplyMoney)";

            var param = new[] {
                new SqlParameter("@UserId",model.UserId),
                new SqlParameter("@UserName",model.UserName),
                new SqlParameter("@UserMobile",model.UserMobile),
                new SqlParameter("@PayAccount",model.PayAccount),
                new SqlParameter("@ApplyMoney",model.ApplyMoney)

            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }


        /// <summary>
        /// 获取用户总提现金额
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>System.Decimal.</returns>
        public decimal GetUserDrawTotalMoney(int userId)
        {
            string strSql = "select ISNULL(SUM(ApplyMoney),0) from DrawMoneyList where UserId=@UserId and ApplyStatus in (1,2)";
            var param = new[] {
                new SqlParameter("@UserId",userId)
            };
            return Convert.ToDecimal(DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, strSql, param));
        }


        /// <summary>
        /// Gets the userv verify count.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>System.Decimal.</returns>
        public int GetUservVerifyCount(int userId)
        {
            string strSql = "select COUNT(1) FROM CouponLog WHERE VerifyUserId=@userId";
            var param = new[] {
                new SqlParameter("@UserId",userId)
            };
            return Convert.ToInt32(DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, strSql, param));
        }



        /// <summary>
        /// 获取提现数据实体
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>DrawMoneyModel.</returns>
        public DrawMoneyModel GetUserDrawMoneyModel(int Id)
        {
            string strSql = "select * from DrawMoneyList where ID=@ID";
            var param = new[] {
                new SqlParameter("@ID",Id)
            };
            using (SqlDataReader dr = DbHelperSQLP.ExecuteReader(WebConfig.getConnectionString(), CommandType.Text, strSql, param))
            {
                return DbHelperSQLP.GetEntity<DrawMoneyModel>(dr);
            }
        }


        /// <summary>
        /// Updates the draw money status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="status">The status.</param>
        /// <param name="remark">The remark.</param>
        /// <returns>System.Int32.</returns>
        public int UpdateDrawMoneyStatus(int id, int status, string remark)
        {
            string strSql = "update DrawMoneyList  set ApplyStatus=@ApplyStatus ";
            if (status == 3)
                strSql += " ,Remark=@Remark ";
            strSql += " where ID=@ID";
            var param = new[] {
                new SqlParameter("@ID",id),
                new SqlParameter("@ApplyStatus",status),
                new SqlParameter("@Remark",remark)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param);
        }



        #endregion



        #region 返利


        /// <summary>
        /// 获取用户总返利
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>System.Decimal.</returns>
        public decimal GetUserTotalRebate(int userId)
        {
            string strSql = "select ISNULL(SUM(RebateMoney),0) as Amount from RebateLog where UserId=@UserId and Income=1";
            var param = new[] {
                new SqlParameter("@UserId",userId)
            };
            return Convert.ToDecimal(DbHelperSQLP.ExecuteScalar(WebConfig.getConnectionString(), CommandType.Text, strSql, param));
        }

        #endregion




        public UserBaseInfoModel GetUserInfo(int userId)
        {
            string strSql = "select UserId,UserIdentity,RealName,NickName,HeadImg,IsActive,Mobile,ApplyStatus,Money-MoneyLocked as UserMoney,Address,shopId from UserList with(nolock)  where UserId=@UserId";
            var param = new[] {
                new SqlParameter("@UserId",userId)
            };
            using (SqlDataReader dr = DbHelperSQLP.ExecuteReader(WebConfig.getConnectionString(), CommandType.Text, strSql, param))
            {
                return DbHelperSQLP.GetEntity<UserBaseInfoModel>(dr);
            }
        }

        public UserBaseInfoModel GetUserInfo(string openid)
        {
            string strSql = "select UserId,UserIdentity,RealName,NickName,HeadImg,IsActive,Mobile,ApplyStatus,Money-MoneyLocked as UserMoney,Address,shopId from UserList with(nolock)  where openId=@openId";
            var param = new[] {
                new SqlParameter("@openId",openid)
            };
            using (SqlDataReader dr = DbHelperSQLP.ExecuteReader(WebConfig.getConnectionString(), CommandType.Text, strSql, param))
            {
                return DbHelperSQLP.GetEntity<UserBaseInfoModel>(dr);
            }
        }


        public ResultPageModel GetVerifyList(SearchModel model)
        {
            ResultPageModel result = new ResultPageModel();
            if (model == null)
                return result;
            string strSql = @"select l.CouponNo,c.Title as CouponName,l.UseTime from CouponLog l
                                left join CouponList c on c.CouponId=l.CouponId 
                                where l.VerifyUserId=@UserId
                                ";
            var param = new[] {
                new SqlParameter("@UserId", model.UserId)
            };
            //生成sql语句
            return getPageData<VerifyCouponModel>(model.PageSize, model.PageIndex, strSql, "l.UseTime", param, (items =>
            {
                items.ForEach(item =>
                {
                    item.Time = item.UseTime.ToString("yyyy-MM-dd HH:mm:ss");
                });
            }));
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
            string strSql = "update Manager set LoginPassword=@NewLoginPassword where ID=@ID and LoginPassword=@OldLoginPassword";
            SqlParameter[] param = {
                new SqlParameter("@NewLoginPassword",EncryptHelper.MD5(password)),
                new SqlParameter("@ID", userId),
                new SqlParameter("@OldLoginPassword",EncryptHelper.MD5(oldPassword))
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }

        /// <summary>
        /// 修改用户的门店
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>true if XXXX, false otherwise.</returns>
        public bool UpdateUserShopId(int shopId, int userId)
        {
            string strSql = "update UserList set shopId=@shopId where UserId=@UserId";
            var param = new[] {
                        new SqlParameter("@shopId",shopId),
                        new SqlParameter("@UserId",userId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }




        /// <summary>
        /// 获取门店
        /// </summary>
        /// <param name="BrandId">The brand identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetShopList(int BrandId, SearchModel model)
        {
            ResultPageModel result = new ResultPageModel();
            if (model == null)
                return result;
            string strSql = @"select  S.ShopID,S.BrandId,B.Title,S.ShopName,S.ShopAddress,S.ShopContacts,S.ShopTel,S.CreateTime  from ShopList S
                                left join BrandList B ON B.BrandId=S.BrandId
                                where 1=1  and S.IsDel=0 
                               ";

            if (BrandId > 0)
            {
                strSql += " and S.BrandId=@BrandId";
            }
            if (!string.IsNullOrEmpty(model.key))
            {
                strSql += string.Format(" and S.ShopName like '%{0}%' ", model.key);
            }

            if (!string.IsNullOrEmpty(model.startTime))
                strSql += " and CONVERT(nvarchar(10),S.CreateTime,121)>=CONVERT(nvarchar(10),@startTime,121) ";
            if (!string.IsNullOrEmpty(model.endTime))
                strSql += " and CONVERT(nvarchar(10),S.CreateTime,121)<=CONVERT(nvarchar(10),@endTime,121) ";

            var param = new[] {
                        new SqlParameter("@BrandId", BrandId),
                        new SqlParameter("@startTime", model.startTime),
                        new SqlParameter("@endTime", model.endTime)
            };

            //生成sql语句
            return getPageData<ShopModel>(model.PageSize, model.PageIndex, strSql, "S.CreateTime", false, param);

        }


        public ResultPageModel GetShopList(SearchModel model)
        {
            ResultPageModel result = new ResultPageModel();
            if (model == null)
                return result;
            string strSql = @"select shop.ShopID,shop.ShopName from ShopList shop
                            left join CouponLog s on s.ShopId=shop.ShopId
                            left join CouponList c on c.CouponId=s.CouponId
                            where  c.IsDel=0 and c.IsEnable=1 and s.IsUse=0 and s.IsRecycle=0 and s.ShopId>0
                               ";

            if (model.UserId > 0)
            {
                strSql += " and s.UserId=@UserId ";
            }
            strSql += "  group by shop.ShopID,shop.ShopName ,shop.CreateTime";
            var param = new[] {
                        new SqlParameter("@UserId", model.UserId)
            };

            //生成sql语句
            return getPageData<ShopModel>(model.PageSize, model.PageIndex, strSql, "shop.CreateTime", false, param);

        }






        public List<ShopModel> GetShopIds(int brandid)
        {
            string strSql = "select ShopId,BrandId,ShopName,ShopAddress,ShopTel,ShopContacts,CreateTime from ShopList where BrandId=@BrandId and IsDel=0";
            var param = new[] {
                        new SqlParameter("@BrandId", brandid)
            };
            using (SqlDataReader dr = DbHelperSQLP.ExecuteReader(WebConfig.getConnectionString(), CommandType.Text, strSql, param))
            {
                return DbHelperSQLP.GetEntityList<ShopModel>(dr);
            }
        }


        /// <summary>
        /// 更新门店信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateShopInfo(ShopModel model)
        {
            string strSql = @"update ShopList set ShopName=@ShopName,BrandId=@BrandId,ShopAddress=@ShopAddress,ShopContacts=@ShopContacts,ShopTel=@ShopTel where ShopID=@ShopID";
            var param = new[] {
                        new SqlParameter("@ShopName", model.ShopName),
                        new SqlParameter("@BrandId", model.BrandId),
                        new SqlParameter("@ShopAddress", model.ShopAddress),
                        new SqlParameter("@ShopContacts", model.ShopContacts),
                        new SqlParameter("@ShopTel", model.ShopTel),
                        new SqlParameter("@ShopID",model.ShopID)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }

        /// <summary>
        /// 添加门店
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddShopInfo(ShopModel model)
        {
            string strSql = @"insert into ShopList(ShopName,ShopAddress,ShopContacts,ShopTel,BrandId)
                                values (@ShopName,@ShopAddress,@ShopContacts,@ShopTel,@BrandId)";
            var param = new[] {
                        new SqlParameter("@ShopName", model.ShopName),
                        new SqlParameter("@ShopAddress", model.ShopAddress),
                        new SqlParameter("@ShopContacts", model.ShopContacts),
                        new SqlParameter("@ShopTel", model.ShopTel),
                        new SqlParameter("@BrandId", model.BrandId)
                };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param);

        }

        /// <summary>
        /// 删除门店
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool DeleteShopInfo(int shopId)
        {
            string strSql = "update ShopList set IsDel=1 where ShopID=@ShopID";
            var param = new[] {
                        new SqlParameter("@ShopID",shopId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }

        /// <summary>
        /// 添加优惠券和门店的关联
        /// </summary>
        /// <param name="shopId">The shop identifier.</param>
        /// <param name="couponId">The coupon identifier.</param>
        /// <returns>System.Int32.</returns>
        public bool AddShopCouponInfo(int shopId, int couponId)
        {
            string strSql = @"insert into ShopCouponList(ShopId,CouponId) values(@ShopId,@CouponId)";
            var param = new[] {
                        new SqlParameter("@ShopID",shopId),
                        new SqlParameter("@CouponId",couponId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;

        }



        public bool AddGoodsCouponInfo(int GoodsId, int couponId)
        {
            string strSql = @"insert into GoodsCouponList(GoodsId,CouponId) values(@GoodsId,@CouponId)";
            var param = new[] {
                        new SqlParameter("@GoodsId",GoodsId),
                        new SqlParameter("@CouponId",couponId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;

        }







        /// <summary>
        /// 根据优惠券ID，获取门店ID
        /// </summary>
        /// <param name="couponId">The coupon identifier.</param>
        /// <returns>List&lt;ShopJsonModel&gt;.</returns>
        public List<ShopJsonModel> GetShopListByCouponId(int couponId)
        {
            string strSql = @"select s.ShopName as title,s.ShopId as value,s.ShopId as description from ShopCouponList c
                            left join ShopList s on s.ShopId=c.ShopId
                            where CouponId=@CouponId and s.IsDel=0";
            var param = new[] {
                        new SqlParameter("@CouponId",couponId)
            };
            using (SqlDataReader dr = DbHelperSQLP.ExecuteReader(WebConfig.getConnectionString(), CommandType.Text, strSql, param))
            {
                return DbHelperSQLP.GetEntityList<ShopJsonModel>(dr);
            }
        }


        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="BrandId">The brand identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>ResultPageModel.</returns>
        public ResultPageModel GetGoodsList(int BrandId, SearchModel model)
        {
            ResultPageModel result = new ResultPageModel();
            if (model == null)
                return result;
            string strSql = @"select  S.GoodsId,S.BrandId,B.Title,S.GoodsName,S.CreateTime  from GoodsList S
                                left join BrandList B ON B.BrandId=S.BrandId
                                where 1=1  and S.IsDel=0 
                               ";

            if (BrandId > 0)
            {
                strSql += " and S.BrandId=@BrandId";
            }
            if (!string.IsNullOrEmpty(model.key))
            {
                strSql += string.Format(" and S.GoodsName like '%{0}%' ", model.key);
            }

            if (!string.IsNullOrEmpty(model.startTime))
                strSql += " and CONVERT(nvarchar(10),S.CreateTime,121)>=CONVERT(nvarchar(10),@startTime,121) ";
            if (!string.IsNullOrEmpty(model.endTime))
                strSql += " and CONVERT(nvarchar(10),S.CreateTime,121)<=CONVERT(nvarchar(10),@endTime,121) ";

            var param = new[] {
                        new SqlParameter("@BrandId", BrandId),
                        new SqlParameter("@startTime", model.startTime),
                        new SqlParameter("@endTime", model.endTime)
            };

            //生成sql语句
            return getPageData<GoodsModel>(model.PageSize, model.PageIndex, strSql, "S.CreateTime", false, param);

        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool DeleteGoodsInfo(int goodsId)
        {
            string strSql = "update GoodsList set IsDel=1 where GoodsId=@GoodsId";
            var param = new[] {
                        new SqlParameter("@GoodsId",goodsId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }

        /// <summary>
        /// 更新商品信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateGoodsInfo(GoodsModel model)
        {
            string strSql = @"update GoodsList set GoodsName=@GoodsName,BrandId=@BrandId where GoodsId=@GoodsId";
            var param = new[] {
                        new SqlParameter("@GoodsName", model.GoodsName),
                        new SqlParameter("@BrandId", model.BrandId),
                        new SqlParameter("@GoodsId",model.GoodsId)
            };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param) > 0;
        }
        /// <summary>
        /// 添加门店
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddGoodsInfo(GoodsModel model)
        {
            string strSql = @"insert into GoodsList(GoodsName,BrandId)
                                values (@GoodsName,@BrandId)";
            var param = new[] {
                        new SqlParameter("@GoodsName", model.GoodsName),
                        new SqlParameter("@BrandId", model.BrandId)
                };
            return DbHelperSQLP.ExecuteNonQuery(WebConfig.getConnectionString(), CommandType.Text, strSql, param);

        }
    }
}
