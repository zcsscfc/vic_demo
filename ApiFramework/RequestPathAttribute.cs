using System;

namespace ApiFramework
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public class RequestPathAttribute : Attribute
    {
        public string Method { get; set; }
        public string Path { get; set; }
    }
}