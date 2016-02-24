using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Reflection;
using Newtonsoft.Json;

namespace ApiFramework
{
    public class RestHandler : IApiHandler, IHttpHandler
    {
        public ApiConfig Config { get; set; }
        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public void ProcessRequest(IApiRequest request, IApiResponse response)
        {
            if (!string.IsNullOrWhiteSpace(request.Path))
            {
                ApiPath apiPath;
                if (Config.RequestPathCollection.TryGetValue(request.Path, out apiPath))
                {
                    if (apiPath != null)
                    {
                        var apiInstance = Activator.CreateInstance(apiPath.ServiceType);
                        var method = apiPath.ServiceType.GetMethod(apiPath.Operation);
                        object _response = method.Invoke(apiInstance, null);
                        string json = JsonConvert.SerializeObject(_response);

                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);
                        response.OutputStream.Write(buffer, 0, buffer.Length);
                        response.OutputStream.Close();
                    }
                }
            }            
        }
    }
}
