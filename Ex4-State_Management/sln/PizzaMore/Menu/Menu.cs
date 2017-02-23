using PizzaMore.Utility;
using PizzaMore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaMore.Data.Models;

namespace _Menu
{
    class Menu
    {
        private static IDictionary<string, string> requestParams;
        private static Header header = new Header();
        private static Session session;

        private static string menuTopPartialView = "../htdocs/PizzaMore/menu-top.html";
        private static string menuBottomPartialView = "../htdocs/PizzaMore/menu-bottom.html";

        static void Main()
        {

            header.Print();

            session = WebUtil.GetSession();
            if (session == null)
            {
                WebUtil.PageNotAllowed();
                return;
            }

            Console.WriteLine($"<div>Session: {session.ToString()}</div>");

            if (WebUtil.IsGet())
            {
                WebUtil.PrintFileContent(menuTopPartialView);
                GenerateNavbar();
                GenerateAllSuggestions();
                WebUtil.PrintFileContent(menuBottomPartialView);
            }
        }

        static void GenerateNavbar()
        {
            Console.WriteLine("<nav class=\"navbar navbar-default\">" +
                "<div class=\"container-fluid\">" +
                "<div class=\"navbar-header\">" +
                "<a class=\"navbar-brand\" href=\"Home.exe\">PizzaMore</a>" +
                "</div>" +
                "<div class=\"collapse navbar-collapse\" id=\"bs-example-navbar-collapse-1\">" +
                "<ul class=\"nav navbar-nav\">" +
                "<li ><a href=\"AddPizza.exe\">Suggest Pizza</a></li>" +
                "<li><a href=\"YourSuggestions.exe\">Your Suggestions</a></li>" +
                "</ul>" +
                "<ul class=\"nav navbar-nav navbar-right\">" +
                "<p class=\"navbar-text navbar-right\"></p>" +
                "<p class=\"navbar-text navbar-right\"><a href=\"Home.exe?logout=true\" class=\"navbar-link\">Sign Out</a></p>" +
                $"<p class=\"navbar-text navbar-right\">Signed in as {session.User.Email}</p>" +
                "</ul> </div></div></nav>");
        }

        static void GenerateAllSuggestions()
        {
            var ctx = new PizzaMoreContext();
            var pizzas = ctx.PizzaSuggestions.ToList();

            Console.WriteLine("<div class=\"card-deck\">");
            foreach (var pizza in pizzas)
            {
                Console.WriteLine("<div class=\"card\">");
                Console.WriteLine($"<img class=\"card-img-top\" src=\"{pizza.ImageUrl}\" width=\"200px\"alt=\"Card image cap\">");
                Console.WriteLine("<div class=\"card-block\">"); Console.WriteLine($"<h4 class=\"card-title\">{pizza.Title}</h4>");
                Console.WriteLine($"<p class=\"card-text\"><a href=\"DetailsPizza.exe?pizzaid={pizza.Id}\">Recipe</a></p>");
                Console.WriteLine("<form method=\"POST\">");
                Console.WriteLine($"<div class=\"radio\"><label><input type = \"radio\" name=\"pizzaVote\" value=\"up\">Up</label></div>"); Console.WriteLine($"<div class=\"radio\"><label><input type = \"radio\" name=\"pizzaVote\" value=\"down\">Down</label></div>"); Console.WriteLine($"<input type=\"hidden\" name=\"pizzaid\" value=\"{pizza.Id}\" />");
                Console.WriteLine("<input type=\"submit\" class=\"btn btn-primary\" value=\"Vote\" />");
                Console.WriteLine("</form>");
                Console.WriteLine("</div>");
                Console.WriteLine("</div>");
            }
            Console.WriteLine("</div>");
        }
    }
}
