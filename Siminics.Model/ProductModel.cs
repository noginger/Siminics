using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siminics.Model
{
    public class ProductModel
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
        /// productname
        /// </summary>		
        private string _productname;
        public string productname
        {
            get { return _productname; }
            set { _productname = value; }
        }
        /// <summary>
        /// typeid
        /// </summary>		
        private int _typeid;
        public int typeid
        {
            get { return _typeid; }
            set { _typeid = value; }
        }
    }
}
