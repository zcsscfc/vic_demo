using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiFramework;
namespace Controller
{
    [RestController]
    [RequestPath(Path = "/home")]
    public class HomerController
    {
        [RequestPath(Path = "/index", Method = HttpMethod.Get)]
        public Response Index()
        {
            return new Response().Success("hello world");
        }

        [RequestPath(Path = "/about")]
        public Response About(Person person)
        {
            return new Response().Success(new { Name = "victor" });
        }
    }
}
