using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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


            ApiRequest apiRequest = GetApiRequest(request);

            ApiResponse apiResponse = WrapApiResponse(response);

            IApiHandler handler = GetHandler();
            handler.ProcessRequest(apiRequest, apiResponse);


        }

        private IApiHandler GetHandler()
        {
            return new RestHandler { Config = Config };
        }

        private ApiResponse WrapApiResponse(HttpListenerResponse response)
        {
            return new ApiResponse(response.OutputStream);
        }

        private ApiRequest GetApiRequest(HttpListenerRequest request)
        {
            string rawUrl = request.RawUrl;
            if (string.IsNullOrWhiteSpace(rawUrl)) return null;
            int index = rawUrl.IndexOf("?");

            string path = rawUrl;
            if (index >= 0)
                path = rawUrl.Substring(0, index + 1);
            return new ApiRequest
            {
                Path = path.ToLower()
            };
        }
    }
}
