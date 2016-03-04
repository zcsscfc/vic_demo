using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.IO;

namespace ApiFramework
{
    public class ApiRequest : IApiRequest
    {
        public string Path { get; set; }



        public NameValueCollection QueryString
        {
            get;
            set;
        }

        public string HttpMethod
        {
            get;
            set;
        }


        public NameValueCollection Headers
        {
            get;
            set;
        }

        public string ContentType { get; set; }
        public Stream InputStream { get; set; }
    }
}
