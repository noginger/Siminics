using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cee.Tools.Config
{
    public class Add
    {
        /// <summary>
        /// this attribute in configuration for sys appSettings node 
        /// </summary>
        [XmlAttribute("key")]
        public string Key { get; set; }

        /// <summary>
        /// this attribute in configuration for sys appSettings node 
        /// </summary>
        [XmlAttribute("value")]
        public string Value { get; set; }
    }

    /// <summary>
    /// configuration for sys appSettings node 
    ///     and this node inherit from root
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 默认项
        /// </summary>
        [XmlAttribute("defaultItem")]
        public string DefaultItem { get; set; }

        /// <summary>
        /// add集合
        /// </summary>
        [XmlElement("add")]
        public AddCollection Adds { get; set; }

        /// <summary>
        /// appsettings default items
        /// </summary> 
        private Add DefaultAdd
        {
            get { return Adds[DefaultItem]; }
        }
    }

    public class AddCollection : KeyedCollection<string, Add>
    {
        protected override string GetKeyForItem(Add item)
        {
            return item.Key;
        }
    }
}
