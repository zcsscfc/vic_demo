using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFramework
{
    public class ApiConfig
    {
        protected SelfHttpListenerBase HttpListener { get; private set; }
        protected Dictionary<string, RestHandler> Handlers { get; private set; }

        protected void RegisterHanders()
        {

        }
    }
}
