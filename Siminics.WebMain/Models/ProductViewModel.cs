using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Siminics.WebMain.Models
{
    public class ProductViewModel
    {
        /// <summary>
        /// auto_increment
        /// </summary>		 
        public int ProductId { get; set; }

        public int ModelId { get; set; }

        [Required(ErrorMessage = "请填写产品名称")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "请选择产品类型")]
        public int ProductType { get; set; }
        
        /// <summary>
        /// ShowImage
        /// </summary>		 
        public string ShowImage { get; set; }

        public string AnotherImages { get; set; }

        public string AnotherImageHtml { get; set; }

        /// <summary>
        /// 图片html
        /// </summary>
        public string ImagesHtml { get; set; }

        /// <summary>
        /// DownAddress
        /// </summary>		 
        public string DownAddress { get; set; }

        /// <summary>
        /// ShortDesc
        /// </summary>		 
        public string ShortDesc { get; set; }

        /// <summary>
        /// IsSale
        /// </summary>		 
        public int IsSale { get; set; }

        /// <summary>
        /// ViewCount
        /// </summary>		 
        public int ViewCount { get; set; }
        
        /// <summary>
        /// DownCount
        /// </summary>		 
        public int DownCount { get; set; }

        /// <summary>
        /// DeleteTime
        /// </summary>		 
        public long DeleteTime { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>		 
        public long CreateTime { get; set; }
        
        /// <summary>
        /// Infomation
        /// </summary>
        public string Infomation { get; set; }

        public int Sort { get; set; }
    }
}