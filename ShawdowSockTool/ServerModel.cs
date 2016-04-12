using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ShawdowSockTool
{
    public class ServerModel
    {
        public List<Server> configs { get; set; }
        public string strategy { get; set; }
        public int index { get; set; }
        public bool global { get; set; }
        public bool enabled { get; set; }
        public bool shareOverlan { get; set; }
        public bool isDefault { get; set; }
        public int localPort { get; set; }
        public string pacUrl { get; set; }
        public bool userOnlinePac { get; set; }
    }

    public class Server
    {
        public string server { get; set; }
        public int server_port { get; set; }
        public string password { get; set; }
        public string method { get; set; }
        public string remarks { get; set; }
    }
}
