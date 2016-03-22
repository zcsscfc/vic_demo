using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ApiFramework.Extensions;

namespace ApiFramework
{
    public class ApiConfig
    {
        public ApiConfig()
        {
            RequestPathCollection = new Dictionary<string, ApiPath>();
        }

        public Dictionary<string, ApiPath> RequestPathCollection { get; private set; }

        public void RegisterRequestPath(params Assembly[] assembliesWithServices)
        {
            if (assembliesWithServices == null) return;
            var types = GetAssemblyTypes(assembliesWithServices);
            foreach (var type in types.Where(type => type.HasAttribute<RestControllerAttribute>()))
            {
                AnalysisRequestPath(type);
            }
        }

        private void AnalysisRequestPath(Type serviceType)
        {
            if (serviceType == null) return;
            var basePath = "";
            var reqAttribute = serviceType.GetCustomAttribute<RequestPathAttribute>();
            if (reqAttribute != null)
            {
                if (!string.IsNullOrWhiteSpace(reqAttribute.Path))
                {
                    basePath += reqAttribute.Path.Trim();
                }
            }

            var methods = serviceType.GetMethods();
            foreach (var method in methods)
            {
                var mReqAttribute = method.GetCustomAttribute<RequestPathAttribute>();
                if (mReqAttribute == null)
                {
                    continue;
                }

                var path = basePath + mReqAttribute.Path.Trim();
                var paras = method.GetParameters();

                var requestPath = new ApiPath
                {
                    Operation = method.Name,
                    Path = path,
                    ServiceType = serviceType,
                    Parameters = paras.Select(paraInfo => paraInfo.ParameterType).ToArray()
                };

                if (RequestPathCollection.ContainsKey(path))
                {
                    throw new Exception("存在多个RequestPath");
                }
                RequestPathCollection.Add(path, requestPath);
            }
        }

        private static IEnumerable<Type> GetAssemblyTypes(Assembly[] assembliesWithServices)
        {
            var results = new List<Type>();

            try
            {
                results.AddRange(assembliesWithServices.SelectMany(assembly => assembly.GetTypes()));
            }
            catch (Exception)
            {
                // ignored
            }
            return results;
        }
    }
}