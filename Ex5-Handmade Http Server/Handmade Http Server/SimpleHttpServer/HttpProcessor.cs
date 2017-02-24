using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SimpleHttpServer.Models;
using SimpleHttpServer.Enums;
using SimpleHttpServer.Utilities;
using System.Text.RegularExpressions;

namespace SimpleHttpServer
{
    public class HttpProcessor
    {
        private IList<Route> Routes;
        private HttpRequest Request;
        private HttpResponse Response;

        public HttpProcessor(IEnumerable<Route> routes)
        {
            this.Routes = new List<Route>(routes);
        }

        public void HandleClient(TcpClient tcpClient)
        {
            using (var stream = tcpClient.GetStream())
            {
                // ge the request sent to the webserver
                Request = GetRequest(stream);

                // create a response, through routing
                Response = RouteRequest();

                // return the response
                StreamUtils.WriteResponse(stream, Response);
            }
        }

        public HttpRequest GetRequest(NetworkStream inputStream)
        {
            // read 1st line for the request identification
            string requestLine = StreamUtils.ReadLine(inputStream);
            string[] tokens = requestLine.Split(' ');

            RequestMethod method = (RequestMethod)Enum.Parse(typeof(RequestMethod), tokens[0].ToUpper());
            string url = tokens[1];
            string protocolVersion = tokens[2];

            // read headers
            Header header = new Header(HeaderType.HttpRequest);
            string line;
            while ((line = StreamUtils.ReadLine(inputStream)) != null)
            {

                if (line.Equals(""))
                {
                    break;
                }

                // extract name: value
                int separatorIndex = line.IndexOf(':');
                if (separatorIndex == -1)
                {
                    throw new Exception("invalid http header line: " + line);
                }

                string name = line.Substring(0, separatorIndex);

                int pos = separatorIndex + 1;
                while ((pos < line.Length) && (line[pos] == ' '))
                {
                    pos++;
                }
                string value = line.Substring(pos, line.Length - pos);

                // read cookies
                if (name == "Cookie")
                {
                    string[] cookieSaves = value.Split(';');
                    foreach (var cookieSave in cookieSaves)
                    {
                        string[] cookiePair = cookieSave.Split('=').Select(x => x.Trim()).ToArray();
                        var cookie = new Cookie(cookiePair[0], cookiePair[1]);
                        header.Cookies.AddCookie(cookie);
                    }
                }
                // read content-length
                else if (name == "Content-Length")
                {
                    header.ContentLength = value;
                }
                // read others
                else
                {
                    header.OtherParameters.Add(name, value);
                }
            }

            // read content (if present,ex. POST)
            string content = null;
            if(header.ContentLength != null)
            {
                int totalBytes = Convert.ToInt32(header.ContentLength);
                int bytesLeft = totalBytes;
                byte[] bytes = new byte[totalBytes];

                while (bytesLeft > 0)
                {
                    byte[] buffer = new byte[bytesLeft > 1024 ? 1024 : bytesLeft];
                    int n = inputStream.Read(buffer, 0, buffer.Length);
                    buffer.CopyTo(bytes, totalBytes - bytesLeft);

                    bytesLeft -= n;
                }

                content = Encoding.ASCII.GetString(bytes);
            }

            // return actual request
            var request = new HttpRequest()
            {
                Method = method,
                Url = url,
                Header = header,
                Content = content
            };

            Console.WriteLine("-REQUEST-----------------------------");
            Console.WriteLine(request);
            Console.WriteLine("------------------------------");

            return request;
        }

        private HttpResponse RouteRequest()
        {
            // check the request url, if inside any route
            var routes = this.Routes
                .Where(x => Regex.Match(Request.Url, x.UrlRegex).Success)
                .ToList();

            if (!routes.Any())
                return HttpResponseBuilder.NotFound();


            // check if route allows if the request type can be sent to it
            var route = routes.FirstOrDefault(x => x.Method == Request.Method);

            if (route == null)
                return new HttpResponse()
                {
                    StatusCode = ResponseStatusCode.MethodNotAllowed
                };

            // extract the path if there is one
            //var match = Regex.Match(Request.Url, route.UrlRegex);
            //if (match.Groups.Count > 1)
            //{
            //    Request.Path = match.Groups[1].Value;
            //}
            //else
            //{
            //    Request.Path = Request.Url;
            //}

            // trigger the route handler, if during the creation of
            // the response, an error occurs, throw that and reroute to
            // internal server error view
            try
            {
                return route.Callable(Request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return HttpResponseBuilder.InternalServerError();
            }

        }

    }
}
