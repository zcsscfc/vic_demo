using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using ApiDemo.Listener;

namespace ApiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //listener.Prefixes.Add("http://localhost:1337/");
            //listener.Start();

            SelfHttpListener listener = new SelfHttpListener();
            listener.Start("http://localhost:1337/");



            Console.ReadKey();
        }
    }
}
