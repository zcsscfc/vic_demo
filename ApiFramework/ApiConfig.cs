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
        public Dictionary<string, ApiPath> RequestPathCollection { get; private set; }

        public ApiConfig()
        {
            RequestPathCollection = new Dictionary<string, ApiPath>();
        }
        public void RegisterRequestPath(params Assembly[] assembliesWithServices)
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

            MethodInfo[] methods = serviceType.GetMethods();
            foreach (var method in methods)
            {
                RequestPathAttribute mReqAttribute = method.GetCustomAttribute<RequestPathAttribute>();
                if (mReqAttribute == null)
                {
                    continue;
                }

                string _path = basePath + mReqAttribute.Path.Trim();
                ParameterInfo[] paras = method.GetParameters();
                List<Type> paraTypes = new List<Type>();
                if (paras != null)
                {
                    foreach (ParameterInfo paraInfo in paras)
                    {
                        paraTypes.Add(paraInfo.ParameterType);
                    }
                }

                ApiPath requestPath = new ApiPath()
                {
                    Operation = method.Name,
                    Path = _path,
                    ServiceType = serviceType,
                    Parameters = paraTypes.ToArray()
                };

                if (RequestPathCollection.ContainsKey(_path))
                {
                    throw new Exception("存在多个RequestPath");
                }
                RequestPathCollection.Add(_path, requestPath);
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
