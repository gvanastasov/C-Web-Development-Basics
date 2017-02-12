using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMore.Utility
{
    public class Header
    {
        public string Type { get; set; }
        public string Location { get; set; }
        public ICookieCollection Cookies { get; set; }

        public Header()
        {
            this.Type = "Content-type: text/html";
            Cookies = WebUtil.GetCookies();
        }

        public void AddLocation(string location)
        {
            this.Location = $"Location:{location}";
        }

        public void AddCookie(Cookie cookie)
        {
            this.Cookies.AddCookie(cookie);
        }

        public override string ToString()
        {
            StringBuilder header = new StringBuilder();

            header.AppendLine(this.Type);

            if (this.Cookies.Count > 0)
            {
                foreach (var cookie in this.Cookies)
                {
                    header.AppendLine($"Set-Cookie: {cookie.ToString()}");
                }
            }

            if(string.IsNullOrEmpty(this.Location) == false)
            {
                header.AppendLine(this.Location);
            }

            header.AppendLine();
            header.AppendLine();

            return header.ToString();
        }
    }
}
