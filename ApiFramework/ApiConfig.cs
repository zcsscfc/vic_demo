using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ApiFramework.Extensions;

namespace ApiFramework
{
    public class ApiConfig
    {
        protected SelfHttpListenerBase HttpListener { get; private set; }
        protected Dictionary<string, RequestPath> RequestPathCollection { get; private set; }

        public ApiConfig()
        {
            RequestPathCollection = new Dictionary<string, RequestPath>();
        }
        protected void RegisterRequestPath(params Assembly[] assembliesWithServices)
        {
            if (assembliesWithServices != null)
            {
                List<Type> types = GetAssemblyTypes(assembliesWithServices);
                List<Type> serviceTypes = new List<Type>();
                foreach (var type in types)
                {
                    if (type.HasAttribute<RestControllerAttribute>())
                    {
                        AnalysisRequestPath(type);
                    }
                }
            }
        }

        private void AnalysisRequestPath(Type serviceType)
        {
            if (serviceType == null) return;
            string basePath = "";
            RequestPathAttribute reqAttribute = serviceType.GetCustomAttribute<RequestPathAttribute>();
            if (reqAttribute != null)
            {
                if (!string.IsNullOrWhiteSpace(reqAttribute.Path))
                {
                    basePath += reqAttribute.Path.Trim();
                }
            }

        }

        private List<Type> GetAssemblyTypes(Assembly[] assembliesWithServices)
        {
            var results = new List<Type>();

            try
            {
                foreach (var assembly in assembliesWithServices)
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        results.Add(type);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return results;
        }
    }
}
