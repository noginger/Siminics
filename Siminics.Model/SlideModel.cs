using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siminics.Model
{
    public class SlideModel
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
        /// name
        /// </summary>		
        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// position
        /// </summary>		
        private int _position;
        public int position
        {
            get { return _position; }
            set { _position = value; }
        }
        /// <summary>
        /// link
        /// </summary>		
        private string _link;
        public string link
        {
            get { return _link; }
            set { _link = value; }
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
        /// imageurl
        /// </summary>		
        private string _imageurl;
        public string imageurl
        {
            get { return _imageurl; }
            set { _imageurl = value; }
        }

        public int Sort { get; set; }

        public string ImageHtml { get; set; }
    }
}
