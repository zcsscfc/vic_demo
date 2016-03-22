using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ApiFramework
{
    public class RestHandler : IApiHandler, IHttpHandler
    {
        public void ProcessRequest(IApiRequest request, IApiResponse response)
        {
            if (string.IsNullOrWhiteSpace(request.Path)) return;
            ApiPath apiPath;
            ApiConfig config = ApiConfig.GetInstance();
            if (!config.RequestPathCollection.TryGetValue(request.Path, out apiPath)) return;
            if (apiPath == null) return;
            var apiInstance = Activator.CreateInstance(apiPath.ServiceType);
            var method = apiPath.ServiceType.GetMethod(apiPath.Operation);

            var paraTypes = apiPath.Parameters;
            List<object> parameters = null;
            if (paraTypes != null)
            {
                parameters = paraTypes.Select(paraType => GetParameter(paraType, request)).ToList();
            }

            var _response = method.Invoke(apiInstance, parameters == null ? null : parameters.ToArray());
            if (_response == null) throw new ArgumentNullException("request");
            response.Content = _response;
            response.ContentType = "text/json";
        }

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private object GetParameter(Type paraType, IApiRequest request)
        {
            object para = null;

            //NameValueCollection queryString = request.QueryString;
            //if (queryString != null && queryString.Count > 0)
            //{

            //}

            var reader = new StreamReader(request.InputStream);
            var body = reader.ReadToEnd();

            if (!string.IsNullOrWhiteSpace(body))
            {
                para = JsonConvert.DeserializeObject(body, paraType);
            }
            return para;
        }
    }
}