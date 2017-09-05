using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siminics.Model
{
    public class NewsContentModel
    {
        /// <summary>
        /// auto_increment
        /// </summary>		

        public int NewsId { get; set; }

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
        /// imageurl
        /// </summary>		
        private string _imageurl;
        public string imageurl
        {
            get { return _imageurl; }
            set { _imageurl = value; }
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
        /// shortdesc
        /// </summary>		
        private string _shortdesc;
        public string shortdesc
        {
            get { return _shortdesc; }
            set { _shortdesc = value; }
        }

        public int ProductId { get; set; }

        public int Sort { get; set; }
    }
}
