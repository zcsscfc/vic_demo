using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }
    }
}
