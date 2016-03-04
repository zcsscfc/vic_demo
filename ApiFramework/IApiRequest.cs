using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ApiFramework
{
    public interface IApiRequest
    {
        string Path { get; set; }
        NameValueCollection QueryString { get; set; }
        string HttpMethod { get; set; }
        NameValueCollection Headers { get; set; }
        string ContentType { get; set; }
        Stream InputStream { get; set; }
    }
}
