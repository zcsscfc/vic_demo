using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ApiFramework
{
    public interface IApiResponse
    {
        Stream OutputStream { get;  set; }
    }
}
