//==============================================================
//Copyright (C) 2016 Ctrip Corporation.. All rights reserved.
//Information Contained Herein is Proprietary and Confidential.
//==============================================================
//Create by zcs at 2016/4/29 18:14:41.
//Description:
//==============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRedis.Client;

namespace ClearRedis.Common
{
    public class CRedisUtility
    {
        private static string _clusterName = "FncMayFly";

        public static string GetValue(string key)
        {
            List<string> values = null;
            if (!string.IsNullOrWhiteSpace(key))
            {
                try
                {
                    ICacheProvider cache = CacheFactory.GetPorvider(_clusterName);
                    values = cache.GetAllItemsFromList(key);
                }
                catch (Exception e)
                {
                    return e.Message;
                }

            }
            if (values != null && values.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string v in values)
                {
                    sb.AppendLine(v);
                }
                return sb.ToString();
            }
            return string.Empty;
        }

        public static string ClearValue(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                try
                {
                    ICacheProvider cache = CacheFactory.GetPorvider(_clusterName);
                    cache.Remove(key);
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            return string.Empty;
        }
    }
}
