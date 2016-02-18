using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiFramework;
namespace Controller
{
    [RestController]
    [RequestPath(Path="/home")]
    public class HomerController
    {
        [RequestPath(Path = "/index", Method = HttpMethod.Get)]
        public string Index()
        {
            return "helloworld";
        }
    }
}
