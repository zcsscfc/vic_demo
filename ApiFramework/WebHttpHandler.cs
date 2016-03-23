//==============================================================
//Copyright (C) 2016 Ctrip Corporation.. All rights reserved.
//Information Contained Herein is Proprietary and Confidential.
//==============================================================
//Create by zcs at 2016/3/22 15:24:03.
//Description:
//==============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace ApiFramework
{
    public class WebHttpHandler : IGetApiHandler,IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            IApiRequest apiRequest = WrapApiRequest(request);
            IApiResponse apiResponse = WrapApiResponse(response);

            IApiHandler handler = GetApiHandler();
            handler.ProcessRequest(apiRequest,apiResponse);
            response.ContentType = apiResponse.ContentType;

            var json = JsonConvert.SerializeObject(apiResponse.Content);
            var buffer = Encoding.UTF8.GetBytes(json);
            response.ContentType = apiResponse.ContentType;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }
        private IApiResponse WrapApiResponse(HttpResponse response)
        {
            return new ApiResponse();
        }

        private IApiRequest WrapApiRequest(HttpRequest request)
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

        public IApiHandler GetApiHandler()
        {
            return new RestHandler();
        }
    }
}