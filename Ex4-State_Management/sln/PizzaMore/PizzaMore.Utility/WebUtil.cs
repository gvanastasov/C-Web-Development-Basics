﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PizzaMore.Data.Models;
using PizzaMore.Data;
using System.IO;

namespace PizzaMore.Utility
{
    public static class WebUtil
    {
        public static bool IsGet()
        {
            var environmentVariable = Environment.GetEnvironmentVariable(Constants.RequestMethod);
            if (environmentVariable != null)
            {
                string requestMethod = environmentVariable.ToLower();
                if (requestMethod == "get")
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsPost()
        {
            var environmentVariable = Environment.GetEnvironmentVariable(Constants.RequestMethod);
            if (environmentVariable != null)
            {
                string requestMethod = environmentVariable.ToLower();
                if (requestMethod == "post")
                {
                    return true;
                }
            }
            return false;
        }

        public static IDictionary<string, string> RetrieveGetParameters()
        {
            string parametersString = WebUtility.UrlDecode(Environment.GetEnvironmentVariable(Constants.QueryString));

            return RetrieveRequestParameters(parametersString);
        }

        public static IDictionary<string, string> RetrievePostParameters()
        {
            string parametersString = WebUtility.UrlDecode(Console.ReadLine());

            return RetrieveRequestParameters(parametersString);
        }

        private static IDictionary<string, string> RetrieveRequestParameters(string parametersString)
        {
            Dictionary<string, string> resultParameters = new Dictionary<string, string>();
            var parameters = parametersString.Split('&');
            foreach (var param in parameters)
            {
                var pair = param.Split('=');
                var name = pair[0];
                string value = null;
                if (pair.Length > 1)
                {
                    value = pair[1];
                }

                resultParameters.Add(name, value);
            }

            return resultParameters;
        }

        public static ICookieCollection GetCookies()
        {
            string cookieString = Environment.GetEnvironmentVariable(Constants.CookieGet);
            if (string.IsNullOrEmpty(cookieString))
            {
                return new CookieCollection();
            }

            var cookies = new CookieCollection();
            string[] cookieSaves = cookieString.Split(';');
            foreach (var cookieSave in cookieSaves)
            {
                string[] cookiePair = cookieSave.Split('=').Select(x => x.Trim()).ToArray();
                var cookie = new Cookie(cookiePair[0], cookiePair[1]);
                cookies.AddCookie(cookie);
            }
            return cookies;
        }

        public static Session GetSession()
        {
            ICookieCollection cookiesCollection = GetCookies();

            if(cookiesCollection.ContainsKey(Constants.SessionIdKey) == false)
            {
                return null;
            }

            var sessionCookie = cookiesCollection[Constants.SessionIdKey];
            var ctx = new PizzaMoreContext();

            var session = ctx.Sessions
                .FirstOrDefault(s => s.Id == sessionCookie.Value);
            return session;
        }

        public static void PageNotAllowed()
        {
            PrintFileContent(Constants.NotAllowedPage);
        }

        public static void PrintFileContent(string path)
        {
            string content = File.ReadAllText(path, Encoding.UTF8);
            Console.WriteLine(content);
        }
    }
}
