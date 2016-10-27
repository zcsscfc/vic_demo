//==============================================================
//Copyright (C) 2016 Ctrip Corporation.. All rights reserved.
//Information Contained Herein is Proprietary and Confidential.
//==============================================================
//Create by zcs at 2016/9/18 10:12:06.
//Description:
//==============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class AClass
    {
        public AClass()
        {
            Console.WriteLine("this ia Aclass");
        }
    }

    public class BClass : AClass
    {
        public BClass()
        {
            Console.WriteLine("this is Bcalss");
        }
    }

    public class CClass : BClass
    {
        public CClass()
        {
            Console.WriteLine("this is Ccalss");
        }
    }
}
