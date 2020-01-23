using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBotService
{
    public static class Configuration
    {
        public readonly static string BotToken = "348411365:AAHhFV9WuRzzIgHyU9o-4BCdBGn83hyGp-A";
#if USE_PROXY
        public static class Proxy
        {
            public readonly static string Host = "{PROXY_ADDRESS}";
            public readonly static int Port = 8080;
        }
#endif
    }
}
