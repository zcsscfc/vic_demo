using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace ShawdowSockTool
{
    class Program
    {
        static void Main(string[] args)
        {
            HandlConfig();
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

                string json =JsonConvert.SerializeObject(servModel);

                using (StreamWriter writer = new StreamWriter("guiConfig.json"))
                {
                    writer.Write(json);
                }
            }
        }
    }
}
