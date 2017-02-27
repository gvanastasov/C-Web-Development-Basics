using SimpleHttpServer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Models
{
    public class Header
    {
        public Header(HeaderType type)
        {
            this.Type = type;
            this.ContentType = "text/html";
            this.Cookies = new CookieCollection();
            this.OtherParameters = new Dictionary<string, string>();
        }

        public HeaderType Type { get; set; }
        public string ContentType { get; set; }
        public string ContentLength { get; set; }
        public Dictionary<string,string> OtherParameters { get; set; }
        public CookieCollection Cookies { get; private set; }

        public override string ToString()
        {
            StringBuilder header = new StringBuilder();

            // 1.ContentType
            // 2.ContentLength
            // 3.Cookies (based on type req)
            // 4.Others
            // 5.2xNewline

            header.AppendLine($"Content-type: {this.ContentType}");
            
            if(Cookies.Count > 0)
            {
                if (Type == HeaderType.HttpResponse)
                {
                    foreach (var cookie in this.Cookies)
                    {
                        header.AppendLine($"Set-Cookie: {cookie}");
                    }
                }
                else if(Type == HeaderType.HttpRequest)
                {
                    header.AppendLine($"Cookie: {this.Cookies}");
                }
            }

            if(this.ContentLength != null)
            {
                header.AppendLine("Content-Length: " + this.ContentLength);
            }

            foreach (var other in OtherParameters)
            {
                header.AppendLine($"{other.Key}: {other.Value}");
            }

            return header.ToString();
        }
    }
}
