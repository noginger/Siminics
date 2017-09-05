using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siminics.Model
{
    public class LeaveMessageModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Phone { get; set; }

        public string Company { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
