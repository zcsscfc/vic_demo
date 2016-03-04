using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Specialized;

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

                        Type[] paraTypes = apiPath.Parameters;
                        List<object> parameters = null;
                        if (paraTypes != null)
                        {
                            parameters = new List<object>();
                            foreach (Type paraType in paraTypes)
                            {
                                parameters.Add(GetParameter(paraType, request));
                            }
                        }

                        object _response = method.Invoke(apiInstance, parameters == null ? null : parameters.ToArray());
                        response.Content = _response;
                        response.ContentType = "text/json";
                    }
                }
            }
        }

        private object GetParameter(Type paraType, IApiRequest request)
        {
            object para = null;

            //NameValueCollection queryString = request.QueryString;
            //if (queryString != null && queryString.Count > 0)
            //{

            //}

            StreamReader reader = new StreamReader(request.InputStream);
            string body = reader.ReadToEnd();

            if (!string.IsNullOrWhiteSpace(body))
            {
                para = JsonConvert.DeserializeObject(body, paraType);
            }
            return para;
        }
    }
}
