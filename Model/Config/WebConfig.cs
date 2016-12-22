/*
 * 版权所有:杭州火图科技有限公司
 * 地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
 * (c) Copyright Hangzhou Hot Technology Co., Ltd.
 * Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
 * 2013-2016. All rights reserved.
 * author guomw
**/


using HotCoreUtils.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WebConfig
    {
        /// <summary>
        /// 数据库连接字符串（默认MssqlDBConnectionString）
        /// </summary>
        /// <returns>返回连接字符串</returns>
        public static string getConnectionString()
        {
            return ConfigHelper.MssqlDBConnectionString;
        }

        /// <summary>
        /// 判断判断是否debug模式
        /// </summary>
        /// <returns></returns>
        public static bool debugMode()
        {
            try
            {
                string debug = ConfigHelper.GetConfigString("debugMode", "false");
                if (!string.IsNullOrEmpty(debug))
                    return Convert.ToBoolean(debug);
                return false;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 资源网站域名
        /// </summary>
        /// <returns>System.String.</returns>
        public static string reswebsite()
        {
            return ConfigHelper.GetConfigString("reswebsite", "");
        }
    }
}
