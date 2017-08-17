using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.Common;

namespace Siminics.Model
{
    [Serializable]
    public class SysUserInfo : BaseEntityModel
    {
        /// <summary>
        /// auto_increment
        /// </summary>		 
        public int UserId { get; set; }

        /// <summary>
        /// UserName
        /// </summary>		 
        public string UserName { get; set; }

        /// <summary>
        /// PassWord
        /// </summary>		 
        public string PassWord { get; set; }

        /// <summary>
        /// RealName
        /// </summary>		 
        public string RealName { get; set; }

        /// <summary>
        /// IsSupperAdmin
        /// </summary>		 
        public int IsSupperAdmin { get; set; }

        /// <summary>
        /// LastLoginIp
        /// </summary>		 
        public string LastLoginIp { get; set; }

        /// <summary>
        /// LastLoginDate
        /// </summary>		 
        public DateTime LastLoginDate { get; set; }

        /// <summary>
        /// LoginIp
        /// </summary>		 
        public string LoginIp { get; set; }

        /// <summary>
        /// LoginDate
        /// </summary>		 
        public DateTime LoginDate { get; set; }

        /// <summary>
        /// LoginCount
        /// </summary>		 
        public int LoginCount { get; set; }

        /// <summary>
        /// IsActive
        /// </summary>		 
        public int IsActive { get; set; }
    }
}
