//==============================================================
//Copyright (C) 2016 Ctrip Corporation.. All rights reserved.
//Information Contained Herein is Proprietary and Confidential.
//==============================================================
//Create by zcs at 2016/8/12 15:14:48.
//Description:
//==============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class DelegeteDemo
    {
        private delegate void Test();

        public void Click()
        {
            Test t = null;
            t += Test1;
        }

        public void Test1()
        {

        }
    }
}
