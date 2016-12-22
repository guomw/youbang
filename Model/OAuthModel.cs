/*
 * 版权所有:杭州火图科技有限公司
 * 地址:浙江省杭州市滨江区西兴街道阡陌路智慧E谷B幢4楼在地图中查看
 * (c) Copyright Hangzhou Hot Technology Co., Ltd.
 * Floor 4,Block B,Wisdom E Valley,Qianmo Road,Binjiang District
 * 2013-2016. All rights reserved.
 * author guomw
**/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 微信授权后获得用户信息
    /// </summary>
    [Serializable]
    public class WeixinOAuthUserInfoModel
    {
        public string City { get; set; }

        public string Country { get; set; }

        public string Headimgurl { get; set; }

        public string Nickname { get; set; }

        public string Openid { get; set; }

        public string[] Privilege { get; set; }

        public string Province { get; set; }

        public int Sex { get; set; }

        public string UnionID { get; set; }
    }


    /// <summary>
    /// senpac中老版OAuthUserInfo没有unionid，这里暂时扩展
    /// </summary>
    public class OAuthUserInfoPlus
    {
        public OAuthUserInfoPlus()
        { }

        public string city { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public string nickname { get; set; }
        public string openid { get; set; }
        public string[] privilege { get; set; }
        public string province { get; set; }
        public int sex { get; set; }
        public string unionid { get; set; }
    }



    public class WxAccessTokenTicket
    {
        public int id { get; set; }

        public int customerid { get; set; }

        public string value { get; set; }

        public DateTime gettime { get; set; }
    }
}
