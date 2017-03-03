using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpleMVC.App.MVC.Interfaces
{
    /// <summary>
    /// Class implementing this is responsible for handling request and returning a response
    /// </summary>
    public interface IHandleable
    {
        HttpResponse Handle(HttpRequest request);
    }
}
