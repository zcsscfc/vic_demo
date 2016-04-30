using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumDemo
{
    public enum Status
    {
        AA = 0,
        BB = 1,
        CC = 2
    }

    public class Test
    {
        private string name = "victor";
        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }

    }
    class Program
    {

        static void Main(string[] args)
        {
            Test t = new Test();
            //t.Name = "sharp";
            Console.WriteLine(t.Name);
            Console.ReadKey();
        }
    }
}
