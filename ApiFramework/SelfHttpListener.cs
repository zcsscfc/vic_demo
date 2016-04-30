using System;
using System.Net;
using System.Text;

namespace ApiFramework
{
    public class SelfHttpListener : SelfHttpListenerBase
    {
        protected override void ProcessRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;


            var strResponse = string.Format("<HTML><BODY> {0}</BODY></HTML>", DateTime.Now);
            var buffer = Encoding.UTF8.GetBytes(strResponse);
            //对客户端输出相应信息.
            response.ContentLength64 = buffer.Length;
            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            //关闭输出流，释放相应资源
            output.Close();
        }
    }
}