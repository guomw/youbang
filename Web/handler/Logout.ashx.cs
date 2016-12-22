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
    /// Logout 的摘要说明
    /// </summary>
    public class Logout : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            BaseLogicFactory.ClearCookies();
            string json = JsonHelper.JsonSerializer(new ResultModel(ApiStatusCode.OK));
            context.Response.ContentType = "application/json";
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}