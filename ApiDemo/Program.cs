using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using ApiFramework;
using Controller;
namespace ApiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            AppHost appHost = new AppHost(AssemblyUtility.GetServicesAssemblys());
            string urlBase = "http://localhost:1337/";
            appHost.Start(urlBase);

            Console.WriteLine(string.Format("listener started on {0}", urlBase));
            Console.ReadKey();
        }
    }
}
