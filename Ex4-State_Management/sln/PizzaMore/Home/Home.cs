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
            ICookieCollection environmentCookies = WebUtil.GetCookies();

            // get request no cookies
            if (environmentCookies.ContainsKey("lang") == false)
            {
                Header.AddCookie(new Cookie("lang", "EN"));
                Language = "EN";

                Header.Print();
                ShowPage();
                return;
            }

            // get/post request with cookies
            if (WebUtil.IsGet())
            {
                Language = environmentCookies["lang"].Value;
            }
            else if (WebUtil.IsPost())
            {
                RequestParameters = WebUtil.RetrievePostParameters();
                Header.AddCookie(new Cookie("lang", RequestParameters["language"]));
                Language = RequestParameters["language"];
            }

            Header.Print();
            ShowPage();
        }

        private static void ShowPage()
        {
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
            
        }
    }
}
