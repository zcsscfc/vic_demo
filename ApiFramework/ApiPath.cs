using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFramework
{
    public class ApiPath
    {
        public string Path { get; set; }
        public Type ServiceType { get; set; }
        public string Operation { get; set; }

        public Type[] Parameters { get; set; }
    }
}
