using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siminics.Model
{
    public class SurportTypeModel
    {
        public int Id { get; set; }

        public string TypeName { get; set; }

        public int ParentId { get; set; }

        public string ShortDesc { get; set; }

        public int TypeId { get; set; }

        public int Sort { get; set; }

        public string ImageUrl { get; set; }

        public string ImageHtml { get; set; }
    }
}
