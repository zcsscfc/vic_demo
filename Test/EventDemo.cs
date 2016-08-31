//==============================================================
//Copyright (C) 2016 Ctrip Corporation.. All rights reserved.
//Information Contained Herein is Proprietary and Confidential.
//==============================================================
//Create by zcs at 2016/8/12 15:00:02.
//Description:
//==============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class EventDemo
    {
        public delegate void Click();
        public Click ccc;
        public event Click ClickHander;

        public void ClickStart()
        {
            if (ClickHander != null)
            {
                ClickHander();
            }
        }

        public void ClickStart1()
        {
            if (ccc != null)
            {
                ccc();
            }
        }
    }
}
