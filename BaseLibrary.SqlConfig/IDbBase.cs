using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.SqlConfig
{
    public interface IDbBase
    {
        void InitDataConfig(IDictionary<string, string> dictionary);
    }
}
