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
            //string content = File.ReadAllText("Resources/Pages/500.html");
            var response = new HttpResponse()
            {
                StatusCode = ResponseStatusCode.InternalServerError,
            };
            response.Content = File.ReadAllBytes("./Pages/500.html");

            return response;
        }

        public static HttpResponse NotFound()
        {
            var response = new HttpResponse();
            response.Content = File.ReadAllBytes("./Pages/404.html");

            return response;
        }
    }
}
