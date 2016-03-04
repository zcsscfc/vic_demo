using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiFramework
{
    public class AppHostHttpListenerBase : SelfHttpListenerBase
    {
        protected AppHostHttpListenerBase(params Assembly[] assembliesWithServices)
            : base(assembliesWithServices)
        {

        }
        protected override void ProcessRequest(System.Net.HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;


            IApiRequest apiRequest = WrapApiRequest(request);

            IApiResponse apiResponse = WrapApiResponse();

            IApiHandler handler = GetHandler();
            handler.ProcessRequest(apiRequest, apiResponse);
            response.ContentType = apiResponse.ContentType;

            string json = JsonConvert.SerializeObject(apiResponse.Content);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);
            response.ContentType = apiResponse.ContentType;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }

        private IApiHandler GetHandler()
        {
            return new RestHandler { Config = Config };
        }

        private IApiResponse WrapApiResponse()
        {
            return new ApiResponse();
        }

        private IApiRequest WrapApiRequest(HttpListenerRequest request)
        {

            string rawUrl = request.RawUrl;
            if (string.IsNullOrWhiteSpace(rawUrl)) return null;
            int index = rawUrl.IndexOf("?");

            string path = rawUrl;
            if (index >= 0)
                path = rawUrl.Substring(0, index + 1);
            return new ApiRequest
            {
                Path = path.ToLower(),
                QueryString = request.QueryString,
                HttpMethod = request.HttpMethod,
                ContentType=request.ContentType,
                InputStream=request.InputStream
            };            
        }
    }
}
