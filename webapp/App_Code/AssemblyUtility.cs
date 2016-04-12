using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

public class AssemblyUtility
{
    public static System.Reflection.Assembly[] GetServicesAssemblys()
    {
        string assPath;

        assPath = AppDomain.CurrentDomain.BaseDirectory + @"bin\Controller.dll";
        return new Assembly[] { Assembly.LoadFrom(assPath) };
    }
}