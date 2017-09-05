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
		private int _ProductId;
        public int ProductId
        {
            get { return _ProductId; }
            set { _ProductId = value; }
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
        private int _sort;
        public int sort
        {
            get { return _sort; }
            set { _sort = value; }
        }
    }
}
