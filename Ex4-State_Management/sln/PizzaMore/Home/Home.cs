using PizzaMore.Data.Models;
using PizzaMore.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home
{
    public class Home
    {
        private static IDictionary<string, string> RequestParameters;
        private static Session Session;
        private static Header Header = new Header();
        private static string Language;

        static void Main()
        {
            //AddDefaultLanguageCookie();

            //if (WebUtil.IsGet())
            //{
            //    RequestParameters = WebUtil.RetrieveGetParameters();
            //    Language = WebUtil.GetCookies()["lang"].Value;
            //}
            //else if (WebUtil.IsPost())
            //{
            //    RequestParameters = WebUtil.RetrievePostParameters();
            //    Header.AddCookie(new Cookie("lang", RequestParameters["language"]));
            //    Language = RequestParameters["language"];
            //}
            //Header.Print();
            ServeHtmlEn();

            //ShowPage();
        }

        private static void ShowPage()
        {
            Header.Print();
            if (Language.Equals("BG"))
            {
                ServeHtmlBg();
            }
            else
            {
                ServeHtmlEn();
            }
        }

        private static void ServeHtmlBg()
        {
            WebUtil.PrintFileContent(Constants.HomeBG);
        }

        private static void ServeHtmlEn()
        {
            WebUtil.PrintFileContent(Constants.HomeEN);
        }

        private static void AddDefaultLanguageCookie()
        {
            if (!WebUtil.GetCookies().ContainsKey("lang"))
            {
                Header.AddCookie(new Cookie("lang", "EN"));
                Language = "EN";
            }
        }
    }
}
