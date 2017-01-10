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
    public class ShopModel
    {
        /// <summary>
        /// Gets or sets the shop identifier.
        /// </summary>
        /// <value>The shop identifier.</value>
        public int ShopID { get; set; }

        public int BrandId { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the name of the shop.
        /// </summary>
        /// <value>The name of the shop.</value>
        public string ShopName { get; set; }
        /// <summary>
        /// 店铺地址
        /// </summary>
        public string ShopAddress { get; set; }

        public string ShopTel { set; get; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string ShopContacts { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }


    public class ShopJsonModel
    {
        public string title { get; set; }

        public int value { get; set; }

        //public string description { get; set; }

    }



    public class GoodsModel
    {
        public int GoodsId { get; set; }

        public int BrandId { get; set; }


        public string Title { get; set; }


        public string GoodsName { get; set; }

        public DateTime CreateTime { get; set; }


    }

}
