using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            string a = "ABC";
            string b = a;
            a = "CDE";
            Console.WriteLine(b);

            Console.ReadKey();
        }
    }
}
