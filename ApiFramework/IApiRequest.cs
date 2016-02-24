using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFramework
{
    public interface IApiRequest
    {
        string Path { get; set; }
    }
}
