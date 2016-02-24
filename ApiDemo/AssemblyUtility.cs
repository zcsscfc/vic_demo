using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiDemo
{
    class AssemblyUtility
    {
        public static System.Reflection.Assembly[] GetServicesAssemblys()
        {
            string assPath;

            assPath = AppDomain.CurrentDomain.BaseDirectory + @"Controller.dll";
            return new Assembly[] { Assembly.LoadFrom(assPath) };
        }
    }
}
