using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Net.NetworkInformation;

namespace ShawdowSockTool
{
    class Program
    {
        static void Main(string[] args)
        {
            HandlConfig();
            //PingServer();
            Console.ReadKey();
        }

        static void HandlConfig()
        {
            using (StreamReader reader = new StreamReader("serverFile.txt"))
            {
                ServerModel servModel = new ServerModel();
                servModel.strategy = null;
                servModel.index = 16;
                servModel.global = false;
                servModel.enabled = true;
                servModel.shareOverlan = true;
                servModel.isDefault = false;
                servModel.localPort = 1080;
                servModel.pacUrl = null;
                servModel.userOnlinePac = false;
                servModel.configs = new List<Server>();
                string strLine;
                while (!string.IsNullOrWhiteSpace(strLine = reader.ReadLine()))
                {
                    string[] arrConfig = strLine.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                    if (arrConfig.Length > 0)
                    {
                        Server server = new Server();
                        server.remarks = arrConfig[0] + arrConfig[1];
                        server.method = arrConfig[2];
                        server.server = arrConfig[3];
                        server.password = "zcsscfc";
                        server.server_port = 13015;
                        servModel.configs.Add(server);
                    }
                }

                string json = JsonConvert.SerializeObject(servModel);

                using (StreamWriter writer = new StreamWriter("guiConfig.json"))
                {
                    writer.Write(json);
                }
            }
        }

        static void PingServer()
        {
            using (StreamReader reader = new StreamReader("guiConfig.json"))
            {
                string serverJson = reader.ReadToEnd();
                ServerModel serverModel = JsonConvert.DeserializeObject<ServerModel>(serverJson);

                if (serverModel != null && serverModel.configs != null && serverModel.configs.Count > 0)
                {
                    foreach (Server server in serverModel.configs)
                    {
                        string strIp = server.server;
                        Ping ping = new Ping();
                        PingOptions options = new PingOptions();
                        options.DontFragment = true;
                        //测试数据  
                        string data = "test data abcabc";
                        byte[] buffer = Encoding.ASCII.GetBytes(data);
                        //设置超时时间  
                        int timeout = 2000;
                        //调用同步 send 方法发送数据,将返回结果保存至PingReply实例  
                        PingReply reply = ping.Send(strIp, timeout, buffer, options);
                        if (reply.Status == IPStatus.Success)
                        {
                            Console.WriteLine(string.Format("{0}---delay: {1}ms", server.remarks, reply.RoundtripTime));
                        }
                    }
                }
            }
        }
    }
}
