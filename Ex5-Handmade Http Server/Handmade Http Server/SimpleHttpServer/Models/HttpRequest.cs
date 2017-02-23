using SimpleHttpServer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Models
{
    public class HttpRequest
    {
        public RequestMethod Method { get; set; }
        public string Url { get; set; }
        public Header Header { get; set; }
        public string Content { get; set; }

        public HttpRequest()
        {
            this.Header = new Header(HeaderType.HttpRequest);
        }

        public override string ToString()
        {
            StringBuilder req = new StringBuilder();

            req.AppendLine($"{Method} {Url} HTTP/1.0");
            req.AppendLine($"{Header}");

            if(string.IsNullOrEmpty(Content) == false)
            {
                req.AppendLine($"{Content}");
            }

            return req.ToString();
        }
    }
}
