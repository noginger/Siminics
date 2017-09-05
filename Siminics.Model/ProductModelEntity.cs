using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siminics.Model
{
    public class ProductModelEntity
    {
        /// <summary>
		/// auto_increment
        /// </summary>		
		private int _ModelId;
        public int ModelId
        {
            get { return _ModelId; }
            set { _ModelId = value; }
        }
        /// <summary>
        /// productid
        /// </summary>		
        private int _productid;
        public int productid
        {
            get { return _productid; }
            set { _productid = value; }
        }
        /// <summary>
        /// modelname
        /// </summary>		
        private string _modelname;
        public string modelname
        {
            get { return _modelname; }
            set { _modelname = value; }
        }
        /// <summary>
        /// apply
        /// </summary>		
        private string _apply;
        public string apply
        {
            get { return _apply; }
            set { _apply = value; }
        }
        /// <summary>
        /// desc
        /// </summary>		
        private string _desc;
        public string desc
        {
            get { return _desc; }
            set { _desc = value; }
        }
        /// <summary>
        /// downurl
        /// </summary>		
        private string _downurl;
        public string downurl
        {
            get { return _downurl; }
            set { _downurl = value; }
        }

        public int sort { get; set; }

        public IList<ModelSourceModel> Images { get; set; }

        public string productname { get; set; }
    }
}
