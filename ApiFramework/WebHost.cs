//==============================================================
//Copyright (C) 2016 Ctrip Corporation.. All rights reserved.
//Information Contained Herein is Proprietary and Confidential.
//==============================================================
//Create by zcs at 2016/3/22 17:05:16.
//Description:
//==============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiFramework
{
    public class WebHost
    {
        public WebHost(params Assembly[] assembliesWithServices)
        {
            var apiConfig = ApiConfig.GetInstance();
            apiConfig.RegisterRequestPath(assembliesWithServices);
        }
    }
}