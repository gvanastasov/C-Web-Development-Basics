using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Models
{
    public class HttpSession
    {
        private IDictionary<string, string> parameters;

        public HttpSession(string id)
        {
            this.Id = id;
            this.parameters = new Dictionary<string, string>();
        }

        public string Id { get; private set; }
        public string this[string key]
        {
            get
            {
                return parameters[key];
            }
        }

        public void Clear()
        {
            parameters.Clear();
        }

        public void Add(string key, string value)
        {
            if(parameters.ContainsKey(key))
            {
                parameters[key] = value;
            }
            else
            {
                parameters.Add(key, value);
            }
        }
    }
}
