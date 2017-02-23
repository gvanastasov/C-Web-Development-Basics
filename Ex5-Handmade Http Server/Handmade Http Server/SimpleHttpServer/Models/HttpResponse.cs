using SimpleHttpServer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Models
{
    public class HttpResponse
    {
        public ResponseStatusCode StatusCode { get; set; }
        public string StatusMessage
        {
            get { return StatusCode.ToString(); }
        }
        public Header Header { get; set; }
        public byte[] Content { get; set; }

        public HttpResponse()
        {
            this.Header = new Header(HeaderType.HttpResponse);
        }

        public override string ToString()
        {
            var response = new StringBuilder();

            response.AppendLine($"HTTP/1.0 {StatusCode} {StatusMessage}");
            response.AppendLine($"{Header}");

            return response.ToString();
        }
    }
}
