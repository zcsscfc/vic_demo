using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFramework
{
    public class Response
    {
        private object data;
        public object Data
        {
            get
            {
                return data;
            }
            set
            {
                this.data = value;
            }
        }
        public Meta Meta { get; set; }
        public Response Success(object obj = null)
        {
            Response response = new Response();
            response.Meta = new Meta(true, "success");
            response.data = obj;
            return response;
        }
      
        public Response Failure(string message = "failure")
        {
            Response response = new Response();
            response.Meta = new Meta(false, message);
            return response;
        }
    }

    public class Meta
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public Meta(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
