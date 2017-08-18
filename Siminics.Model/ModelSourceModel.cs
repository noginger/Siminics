using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siminics.Model
{
    public class ModelSourceModel
    {
        /// <summary>
		/// auto_increment
        /// </summary>		
		private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// imageurl
        /// </summary>		
        private string _imageurl;
        public string imageurl
        {
            get { return _imageurl; }
            set { _imageurl = value; }
        }
        /// <summary>
        /// modelid
        /// </summary>		
        private int _modelid;
        public int modelid
        {
            get { return _modelid; }
            set { _modelid = value; }
        }

        /// <summary>
        /// 1:图片   2：扩展图
        /// </summary>
        public int TypeId { get; set; }

    }
}
