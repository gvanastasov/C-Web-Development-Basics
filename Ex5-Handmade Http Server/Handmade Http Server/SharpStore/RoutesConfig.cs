using SimpleHttpServer.Enums;
using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpStore
{
    public class RoutesConfig
    {
        public IList<Route> routes { get; private set; }
        public RoutesConfig()
        {
            this.routes = new List<Route>()
            {
                new Route()
                {
                    Name = "Home Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/home$",
                    Callable = (request) =>
                    {
                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/home.html")
                        };
                    }
                },
                new Route()
                {
                    Name = "Carousel CSS",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/content/css/carousel.css$",
                    Callable = (request) =>
                    {
                        var response = new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/css/carousel.css")
                        };
                        response.Header.ContentType = "text/css";
                        return response;
                    }
                },
                new Route()
                {
                    Name = "Bootstrap JS",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/bootstrap/js/bootstrap.min.js$",
                    Callable = (request) =>
                    {
                        var response = new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/bootstrap/js/bootstrap.min.js")
                        };
                        response.Header.ContentType = "application/x-javascript";
                        return response;
                    }
                },
                //TODO Add route for bootstrap.min.css
                new Route()
                {
                    Name = "Bootstrap CSS",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/bootstrap/css/bootstrap.min.css$",
                    Callable = (request) =>
                    {
                        var response = new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/bootstrap/css/bootstrap.min.css")
                        };
                        response.Header.ContentType = "text/css";
                        return response;
                    }
                },
                new Route()
                {
                    Name = "About Directory",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/about$",
                    Callable = (request) =>
                    {
                        return new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/about.html")
                        };
                    }
                },
                new Route()
                {
                    Name = "Products Directory",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/products$",
                    Callable = (request) =>
                    {
                        var knivesService = new Services.KnivesService();
                        var knives = knivesService.GetKnives();

                        var knivesPage = new Pages.ProductsPage(knives);

                        return new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = knivesPage.ToString()
                        };
                    }
                },
                new Route()
                {
                    Name = "Product Search",
                    Method = RequestMethod.GET,
                    UrlRegex = @"^/products\?knife=.*$",
                    Callable = (request) =>
                    {
                        var knivesService = new Services.KnivesService();
                        var knives = knivesService.GetKnives(request.Url);

                        var knivesPage = new Pages.ProductsPage(knives);

                        return new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = knivesPage.ToString()
                        };
                    }
                },
                new Route()
                {
                    Name = "Contacts Directory",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/contacts$",
                    Callable = (request) =>
                    {
                        return new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/contacts.html")
                        };
                    }
                },
                new Route()
                {
                    Name = "Contacts Message Sent",
                    Method = RequestMethod.POST,
                    UrlRegex = "^/contacts$",
                    Callable = (request) =>
                    {
                        var messageService = new Services.MessagesService();
                        messageService.AddMessageFromFormData(request.Content);

                        return new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/contacts.html")
                        };
                    }
                }

            };
        }
    }
}
