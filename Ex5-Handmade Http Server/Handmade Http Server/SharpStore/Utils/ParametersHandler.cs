using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharpStore.Utils
{
    public class ParametersHandler
    {
        public static IDictionary<string, string> ParseRequestParameters(string queryString)
        {
            queryString = WebUtility.UrlDecode(queryString);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string[] tokens = queryString.Split('&');

            foreach (var token in tokens)
            {
                var kvp = token.Split('=');
                if (kvp.Length == 2 && string.IsNullOrEmpty(kvp[1]) == false)
                {
                    parameters.Add(kvp[0], kvp[1]);
                }
            }

            return parameters;
        }
    }
}
