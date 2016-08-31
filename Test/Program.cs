using Ctrip.IFinance.CBU.Services.Core.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Web;

namespace Test
{
    class Victor
    {
        public void Test(int i)
        {
            lock (this)
            {
                if (i > 10)
                {
                    i--;
                    Test(i);
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Victor v = new Victor();
            v.Test(15);
            Console.ReadKey();
        }


        private static string Sign()
        {
            SortedDictionary<string, string> dicArgs = new SortedDictionary<string, string>();
            dicArgs.Add("mch_id", "CTRP");
            dicArgs.Add("appid", "411003");
            dicArgs.Add("plan_id", "147258");
            dicArgs.Add("contract_code", "");
            dicArgs.Add("request_serial", "");
            dicArgs.Add("contract_display_account", "");
            dicArgs.Add("notify_url", "http://www.baidu.com");
            dicArgs.Add("version", "1.0");
            dicArgs.Add("timestamp", "123456789");

            int platform = 2;

            StringBuilder sbA = new StringBuilder();
            StringBuilder sbEncodeA = new StringBuilder();
            foreach (var obj in dicArgs)
            {
                if (!string.IsNullOrWhiteSpace(obj.Value))
                {
                    sbA.AppendFormat("{0}={1}&", obj.Key, obj.Value);

                    if (obj.Key.Equals("notify_url"))
                    {
                        string value;
                        if (platform == 1)
                        {
                            value = HttpUtility.UrlEncode(HttpUtility.UrlEncode(obj.Value));
                        }
                        else
                        {
                            value = HttpUtility.UrlEncode(obj.Value);
                        }
                        sbEncodeA.AppendFormat("{0}={1}&", obj.Key, value);
                        continue;
                    }
                    sbEncodeA.AppendFormat("{0}={1}&", obj.Key, obj.Value);
                }
            }

            string strA = sbA.ToString();
            string strAWithKey = string.Format("{0}key={1}", strA, "123456");

            string strSign = SecurityUtility.MD5Encrypt(strAWithKey);
            string result = string.Format("{0}sign={1}", sbEncodeA.ToString(), strSign);

            Console.WriteLine(strSign);
            Console.WriteLine(result);
            return result;
        }




        private static string GetContent()
        {
            string path = @"E:\ftp\SCTCD_CTRP_20160804_00.txt";
            StreamReader reader = new StreamReader(path);
            string content = reader.ReadToEnd();
            return SecurityUtility.MD5Encrypt(content);
        }
    }
}
