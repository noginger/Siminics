using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siminics.Model
{
    public class CompanyContentModel
    {
        /// <summary>
		/// auto_increment
        /// </summary>		
		private int _contentid;
        public int contentid
        {
            get { return _contentid; }
            set { _contentid = value; }
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
        /// <summary>
        /// title
        /// </summary>		
        private string _title;
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// content
        /// </summary>		
        private string _content;
        public string content
        {
            get { return _content; }
            set { _content = value; }
        }
        /// <summary>
        /// createtime
        /// </summary>		
        private DateTime _createtime;
        public DateTime createtime
        {
            get { return _createtime; }
            set { _createtime = value; }
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

        public int sort { get; set; }
    }
}
