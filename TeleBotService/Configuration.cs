using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBotService
{
    public static class Configuration
    {
        public readonly static string BotToken = ConfigurationSettings.AppSettings["TelegramBot_Key"];
#if USE_PROXY
        public static class Proxy
        {
            public readonly static string Host = "{PROXY_ADDRESS}";
            public readonly static int Port = 8080;
        }
#endif
    }
}
