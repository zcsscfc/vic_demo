using System.Collections.Specialized;
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