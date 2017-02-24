using SimpleHttpServer.Enums;
using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer
{
    public static class HttpResponseBuilder
    {
        public static HttpResponse InternalServerError()
        {
            
            string content = File.ReadAllText("Resources/Pages/500.html");
            var response = new HttpResponse()
            {
                StatusCode = ResponseStatusCode.InternalServerError,
                ContentAsUTF8 = content
            };

            return response;
        }

        public static HttpResponse NotFound()
        {
            string content = File.ReadAllText("Resources/Pages/404.html");

            return new HttpResponse()
            {
                StatusCode = ResponseStatusCode.NotFound,
                ContentAsUTF8 = content
            };
        }
    }
}
