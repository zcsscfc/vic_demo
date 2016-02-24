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
        private Stream outputStream;
        public ApiResponse(Stream outputStream)
        {
            this.outputStream = outputStream;
        }
        public Stream OutputStream
        {
            get
            {
                return outputStream;
            }
             set
            {
                this.outputStream = value;
            }
        }
    }
}
