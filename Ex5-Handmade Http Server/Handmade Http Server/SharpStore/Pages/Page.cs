using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpStore.Pages
{
    public class Page
    {
        private StringBuilder htmlOfPage;

        public HttpRequest request { get; set; }

        public Page(string htmlPath)
        {
            htmlOfPage = new StringBuilder();
            htmlOfPage.Append(File.ReadAllText(htmlPath));
        }

        public void AddStyleToHtml(string styleName)
        {
            int insertionIndex = htmlOfPage.ToString().IndexOf("</head>");

            this.htmlOfPage = htmlOfPage.Insert(insertionIndex, $"<link href=\"../../content/css/{styleName}.css\" rel=\"stylesheet\">");
        }

        public override string ToString()
        {
            if(this.request != null && request.Header.Cookies.Contains("theme"))
            {
                AddStyleToHtml(request.Header.Cookies["theme"].Value);
            }

            return this.htmlOfPage.ToString();
        }
    }
}
