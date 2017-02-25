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

        public Page(string htmlPath)
        {
            htmlOfPage = new StringBuilder();
            htmlOfPage.Append(File.ReadAllText(htmlPath));
        }

        public override string ToString()
        {
            return this.htmlOfPage.ToString();
        }
    }
}
