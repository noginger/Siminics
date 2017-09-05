using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Siminics.WebMain.Models
{
    public class ContentViewModel
    {
        public int ContentId { get; set; }

        public int TypeId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public string ImageHtml { get; set; }

        public int Sort { get; set; }

        public int ProductId { get; set; }

        public string ShortDesc { get; set; }
    }
}