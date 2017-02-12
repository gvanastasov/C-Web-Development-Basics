﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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


    }
}