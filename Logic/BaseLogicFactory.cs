using HotCoreUtils.Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class BaseLogicFactory : PageBaseHelper
    {/// <summary>
     /// 检查登录
     /// </summary>
     /// <param name="appCode"></param>
     /// <returns></returns>
        public static bool CheckLogin(ref ApiStatusCode appCode)
        {
            AdminLoginModel data = GetCurrentUser();
            if (data != null)
            {
                data = UserLogic.Instance.Login(data.LoginName, data.LoginPassword);
                if (data != null)
                {
                    if (data.UserStatus == 1)
                    {
                        WriteCookies(data);
                        appCode = ApiStatusCode.OK;
                        return true;
                    }
                    else
                        appCode = ApiStatusCode.账户已禁用;
                }
                else
                    appCode = ApiStatusCode.账户密码不正确;
            }
            else
                appCode = ApiStatusCode.没有登录;
            return false;
        }


        /// <summary>
        /// 反序列化cookie的值返回user对象
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>
        public static AdminLoginModel GetCurrentUser()
        {
            try
            {
                int userid = Convert.ToInt32(CookieHelper.GetCookieVal("SHOPID"));
                return SerializeHelper.BinaryDeserializeBase64StringToObject<AdminLoginModel>(CookieHelper.GetCookieVal(GetAdminUserCookieKey(userid)));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        /// <summary>
        /// 前端 user对象序列化后写入cookie,保留两周
        /// </summary>
        /// <param name="user"></param>
        public static void WriteCookies(AdminLoginModel user)
        {
            CookieHelper.SetCookieValByCurrentDomain(GetAdminUserCookieKey(user.ID), 20160, SerializeHelper.BinarySerializeObjectToBase64String(user));
            CookieHelper.SetCookieValByCurrentDomain("SHOPID", 20160, user.ID.ToString());
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        public static void ClearCookies()
        {
            try
            {
                int userid = Convert.ToInt32(CookieHelper.GetCookieVal("SHOPID"));
                CookieHelper.DelCookieValByCurrentDomain(GetAdminUserCookieKey(userid));
                CookieHelper.DelCookieValByCurrentDomain("SHOPID");
            }
            catch (Exception)
            {

            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="indexntity"></param>
        /// <returns></returns>
        private static string GetAdminUserCookieKey(int userid)
        {
            return "USER_" + userid;
        }
    }
}
