using System;
using System.Net;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace ApiFramework
{
    public class AppHostHttpListenerBase : SelfHttpListenerBase
    {
        protected AppHostHttpListenerBase(params Assembly[] assembliesWithServices)
            : base(assembliesWithServices)
        {
        }

        protected override void ProcessRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;


            var apiRequest = WrapApiRequest(request);

            var apiResponse = WrapApiResponse();

            var handler = GetHandler();
            handler.ProcessRequest(apiRequest, apiResponse);
            response.ContentType = apiResponse.ContentType;

            var json = JsonConvert.SerializeObject(apiResponse.Content);
            var buffer = Encoding.UTF8.GetBytes(json);
            response.ContentType = apiResponse.ContentType;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }

        private IApiHandler GetHandler()
        {
            return new RestHandler {Config = Config};
        }

        private IApiResponse WrapApiResponse()
        {
            return new ApiResponse();
        }

        private static IApiRequest WrapApiRequest(HttpListenerRequest request)
        {
            var rawUrl = request.RawUrl;
            if (string.IsNullOrWhiteSpace(rawUrl)) return null;
            var index = rawUrl.IndexOf("?", StringComparison.Ordinal);

            var path = rawUrl;
            if (index >= 0)
                path = rawUrl.Substring(0, index + 1);
            return new ApiRequest
            {
                Path = path.ToLower(),
                QueryString = request.QueryString,
                HttpMethod = request.HttpMethod,
                ContentType = request.ContentType,
                InputStream = request.InputStream
            };
        }
    }
}