using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFramework
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class RequestPathAttribute : Attribute
    {
        public string Method { get; set; }
        public string Path { get; set; }
    }
}
