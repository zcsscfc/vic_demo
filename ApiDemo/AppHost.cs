using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ApiFramework;

namespace ApiDemo
{
    class AppHost : AppHostHttpListenerBase
    {
        public AppHost(params Assembly[] assembliesWithServices)
            : base(assembliesWithServices)
        {

        }
    }
}
