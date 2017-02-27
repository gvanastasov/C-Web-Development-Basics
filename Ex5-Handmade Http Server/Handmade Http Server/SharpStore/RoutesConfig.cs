using SimpleHttpServer.Enums;
using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpStore.Utils;
using SharpStore.Pages;
using System.Reflection;

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
                    Method = RequestMethod.GET,
                    UrlRegex = "^/home$",
                    Callable = (request) =>
                    {
                        var page = new HomePage();
                        page.request = request;

                        return new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = page.ToString()
                        };
                    }
                },
                new Route()
                   {Name = "Home Directory",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/.+?\\?theme=.+$",
                    Callable = (request) =>
                    {
                        var indexOfQuestion = request.Url.IndexOf('?');
                        IDictionary<string, string> themeParam = ParametersHandler.ParseRequestParameters(request.Url.Substring(indexOfQuestion + 1));

                        var htmlFileName = request.Url.Substring(1, indexOfQuestion - 1);
                        var typeOfWantedPage = Assembly.GetAssembly(typeof(HomePage))
                                    .GetTypes()
                                    .FirstOrDefault(type =>
                                        type.Name.Contains(
                                                htmlFileName[0].ToString().ToUpper()
                                                + htmlFileName.Substring(1)));

                        Page instance = (Page)Activator.CreateInstance(typeOfWantedPage);
                        instance.AddStyleToHtml(themeParam["theme"]);
                        var responce = new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = instance.ToString()
                        };

                        responce.Header.Cookies.AddCookie(new Cookie("theme", themeParam["theme"]));

                        return responce;
                    }
                },
                new Route()
                {
                    Name = "Theme CSS",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/content/css/.+.css$",
                    Callable = (request) =>
                    {
                        string styleFileName = request.Url.Substring(request.Url.LastIndexOf('/') + 1);
                        var response = new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText($"../../content/css/{styleFileName}")
                        };
                        response.Header.ContentType = "text/css";
                        return response;
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
                        var page = new AboutPage();
                        page.request = request;
                        return new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = page.ToString()
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

                        var knivesPage = new ProductsPage(knives);
                        knivesPage.request = request;

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
                        knivesPage.request = request;
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
                        var page = new ContactsPage();
                        page.request = request;

                        return new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = page.ToString()
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

                        var page = new ContactsPage();
                        page.request = request;

                        return new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            ContentAsUTF8 = page.ToString()
                        };
                    }
                },
                new Route()
                {
                    Name = "Images",
                    Method = RequestMethod.GET,
                    UrlRegex = @"^/images/.+\.jpg",
                    Callable = (request) =>
                    {
                        var file = request.Url.Substring(request.Url.LastIndexOf('/') + 1);

                        var response = new HttpResponse()
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            Content = File.ReadAllBytes($"../../content/images/{file}")
                        };
                        response.Header.ContentType = "image/jpeg";
                        response.Header.ContentLength = response.Content.Length.ToString();

                        return response;
                    }
                }

            };
        }
    }
}
