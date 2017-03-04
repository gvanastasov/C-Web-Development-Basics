using SimpleMVC.App.MVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHttpServer.Models;
using System.Net;
using System.Reflection;
using SimpleMVC.App.MVC.Controllers;
using SimpleMVC.App.MVC.Attributes.Methods;
using SimpleHttpServer.Enums;

namespace SimpleMVC.App.MVC.Routers
{
    /// <summary>
    /// Transforms requests into responses
    /// </summary>
    public class ControllerRouter : IHandleable
    {
        private HttpRequest request;
        private HttpResponse response;

        public ControllerRouter()
        {
            this.getParams = new Dictionary<string, string>();
            this.postParams = new Dictionary<string, string>();
            this.request = new HttpRequest();
            this.response = new HttpResponse();
        }

        private IDictionary<string, string> getParams;
        private IDictionary<string, string> postParams;
        private string requestMethod;
        private string controllerName;
        private string actionName;
        private object[] methodParams;

        /// <summary>
        /// 1. Parse input from request
        /// 2. Reflect - create controler, invoke method inside it
        /// 3. Setup response and return it
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public HttpResponse Handle(HttpRequest request)
        {
            this.request = request;
            this.response = new HttpResponse();

            this.ParseInput();

            IInvocable actionResult = (IInvocable)this
                .GetMethod(requestMethod)
                .Invoke(this.GetController(controllerName), methodParams);


            if(string.IsNullOrEmpty(response.Header.Location))
            {
                response.StatusCode = ResponseStatusCode.Ok;
                response.ContentAsUTF8 = actionResult.Invoke();
            }

            this.postParams = new Dictionary<string, string>();
            this.getParams = new Dictionary<string, string>();

            return response;
        }

        private void ParseInput()
        {
            string uri = WebUtility.UrlDecode(this.request.Url);

            var uriTokens = uri.Split('?');
            string route = uriTokens[0];
            string query = string.Empty;

            if(uriTokens.Length > 1)
            {
                query = uriTokens[1];
            }

            // retrieve GET params
            var queryTokens = query.Split('&');
            if(queryTokens.Length > 0)
            {
                foreach (var token in queryTokens)
                {
                    if (token.Contains("="))
                    {
                        var kvp = token.Split('=');
                        if(kvp.Length == 2)
                        {
                            this.getParams.Add(kvp[0], kvp[1]);
                        }
                    }
                }
            }

            // retrieve POST params
            if(string.IsNullOrEmpty(this.request.Content) == false)
            {
                var contentTokens = this.request.Content.Split('&');
                if(contentTokens.Length > 0)
                {
                    foreach (var token in contentTokens)
                    {
                        if(token.Contains("="))
                        {
                            var kvp = token.Split('=');
                            if(kvp.Length == 2)
                            {
                                this.postParams.Add(kvp[0], kvp[1]);
                            }
                        }
                    }
                }
            }

            // retrieve REQUEST_METHOD type
            this.requestMethod = this.request.Method.ToString();

            // retrieve CONTROLLER and ACTION names
            var routeTokens = route.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            this.controllerName = 
                routeTokens[0].First().ToString().ToUpper() + 
                routeTokens[0].Substring(1).ToLower() + 
                MvcContext.Current.ControllersSuffix;

            this.actionName = 
                routeTokens[1].First().ToString().ToUpper() + 
                routeTokens[1].Substring(1).ToLower();

            // retrieve METHOD
            MethodInfo method = this.GetMethod(this.requestMethod);
            if(method == null)
            {
                throw new NotSupportedException("No such method");
            }

            // prepare METHOD_PARAMS
            IEnumerable<ParameterInfo> parameters = method.GetParameters();

            this.methodParams = new object[parameters.Count()];
            int index = 0;
            foreach (var param in parameters)
            {
                if(param.ParameterType.IsPrimitive || param.ParameterType == typeof(string))
                {
                    object value = this.getParams[param.Name];
                    this.methodParams[index] = Convert.ChangeType(value, param.ParameterType);
                    index++;
                }
                else if(param.ParameterType == typeof(HttpRequest))
                {
                    this.methodParams[index] = this.request;
                    index++;
                }
                else if(param.ParameterType == typeof(HttpSession))
                {
                    this.methodParams[index] = this.request.Session;
                    index++;
                }
                else if(param.ParameterType == typeof(HttpResponse))
                {
                    this.methodParams[index] = this.response;
                    index++;
                }
                else
                {
                    Type bindModelType = param.ParameterType;
                    object bindModel = Activator.CreateInstance(bindModelType);

                    IEnumerable<PropertyInfo> properties = bindModelType.GetProperties();
                    foreach (var property in properties)
                    {
                        property.SetValue(
                            bindModel, 
                            Convert.ChangeType(postParams[property.Name], property.PropertyType));
                    }

                    this.methodParams[index] = Convert.ChangeType(bindModel, bindModelType);
                    index++;
                }
            }
        }


        #region find method
        private MethodInfo GetMethod(string requestMethodType)
        {
            MethodInfo method = null;
            foreach (var methodInfo in GetMethods(this.controllerName, this.actionName))
            {
                IEnumerable<Attribute> methodAttributes = methodInfo.GetCustomAttributes().Where(a => a is HttpMethodAttribute);

                if(!methodAttributes.Any() && requestMethodType == "GET")
                {
                    return methodInfo;
                }

                foreach (HttpMethodAttribute attribute in methodAttributes)
                {
                    if(attribute.IsValid(requestMethodType))
                    {
                        return methodInfo;
                    }
                }
            }
            return method;
        }

        private Controller GetController(string controllerName)
        {
            var controllerType = string.Format(
                "{0}.{1}.{2}",
                MvcContext.Current.AssemblyName,
                MvcContext.Current.ControllersFolder,
                controllerName);

            var controller = (Controller)Activator.CreateInstance(Type.GetType(controllerType));
            return controller;
        }

        private IEnumerable<MethodInfo> GetMethods(string controllerName, string methodName)
        {
            return this.GetController(controllerName)
                .GetType()
                .GetMethods()
                .Where(m => m.Name == methodName);
        }
        #endregion


    }
}
