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
            Permissions per = Permissions.Delete | Permissions.Insert;
            Console.WriteLine(per.HasFlag(Permissions.Insert));
            Console.WriteLine(per == Permissions.Delete);
            Console.WriteLine(per == Permissions.Insert);
            Console.WriteLine(per.ToString());
            Console.ReadKey();
        }
    }
}
