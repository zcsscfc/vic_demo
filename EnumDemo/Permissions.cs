using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnumDemo
{
    [Flags]
    public enum Permissions
    {
        Insert = 1,
        Delete = 2,
        Update = 4,
        Query = 8
    }
}