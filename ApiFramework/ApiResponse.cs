using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ApiFramework
{
    public class ApiResponse : IApiResponse
    {
        private string contentType;
        private object content;
        public ApiResponse()
        {
        }
         public string ContentType
        {
            get
            {
                return this.contentType;
            }
            set
            {
                this.contentType = value;
            }
        }


        public object Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }
    }
}
