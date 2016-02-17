using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using ApiFramework;
namespace ApiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            SelfHttpListener listener = new SelfHttpListener();
            string urlBase = "http://localhost:1337/";
            listener.Start(urlBase);
            Console.WriteLine(string.Format("listener started on {0}", urlBase));
            Console.ReadKey();
        }
    }
}
