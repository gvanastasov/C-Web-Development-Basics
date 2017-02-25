using System.Collections.Generic;
using System.IO;
using SimpleHttpServer.Models;
using SimpleHttpServer;
using SimpleHttpServer.Enums;

namespace SharpStore
{
    class SharpStore
    {
        static void Main(string[] args)
        {
            var routerConfig = new RoutesConfig();

            HttpServer httpServer = new HttpServer(8081, routerConfig.routes);
            httpServer.Listen();
        }
    }
}