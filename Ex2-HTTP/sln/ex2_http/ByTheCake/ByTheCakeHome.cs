using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ByTheCake
{
    class ByTheCakeHome
    {
        static void Main()
        {
            // 01. doc
            Console.WriteLine("Content-Type: text/html \n\r");
            Console.WriteLine(@"<!DOCTYPE html> <html lang=""en"">");

            // meta, 07. head extension
            Console.WriteLine(@"<head> <title>By The Cake</title> <meta charset=""UTF-8""> <meta name=""description"" content=""Buy the cake in By the Cake""> <meta name=""keywords"" content=""cakes, buy""> <meta name=""author"" content=""John Doe""> </head>"); 

            Console.WriteLine(@"<body> <h3>By The Cake</h3> <h4 style=""border-bottom: 1px solid black"">Enjoy our awesome cakes</h4>");

            // 02. nav, 05. anchors
            Console.WriteLine("<ul>");

            Console.WriteLine("<li><a href=''>Home</a><ol>");
            Console.WriteLine("<li><a href='#cakes'>Our Cakes</a></li>");
            Console.WriteLine("<li><a  href='#stores'>Our Stores</a></li>");
            Console.WriteLine("</ol></li>");

            Console.WriteLine("<li><a href='AddCake.exe'>Add Cake</a></li>");
            Console.WriteLine("<li><a href='BrowseCakes.exe'>Browse Cakes</a></li>");
            Console.WriteLine("<li><a href='#about'>About Us</a></li>");

            Console.WriteLine("</ul>");

            // 03. sections, 04. refs, 05. anchors
            Console.WriteLine("<h2>Home</h2>");
            Console.WriteLine("<section>");
            Console.WriteLine("<h3 id='cakes'>Our Cakes</h3>");

            Console.WriteLine("<p><strong><em>Cake</em></strong> is a form of <strong><em>sweet dessert</em></strong> that is typically baked. In its oldest forms, cakes were modifications of breads, but cakes now cover a wide range of preparations that can be simple or elaborate, and that share features with other desserts such as pastries, meringues, custards, and pies.</p>");

            Console.WriteLine("<img src='http://www.bbcgoodfood.com/sites/default/files/styles/recipe/public/recipe/recipe-image/2016/06/lemon-drizzle-slices.jpg?itok=RbEfdaQL'>");
            Console.WriteLine("</section>");

            Console.WriteLine("<section>");
            Console.WriteLine("<h3 id='stores'>Our Stores</h3>");
            Console.WriteLine("<p>Our stores are located in 21 cities all over the world. Come and see what we have for you.</p>"
                .Replace(@"sweet dessert", "<em><strong>sweet dessert</strong></em>")
                .Replace(@"^cake$", "<em><strong>cake</strong></em>")
                .Replace(@"store", "<em><strong>store</strong></em>"));
            Console.WriteLine("<img src='http://www.bbcgoodfood.com/sites/default/files/recipe-collections/collection-image/2013/05/matcha-mousse-cake.jpg'>");
            Console.WriteLine("</section>");

            // 09. about us info
            Console.WriteLine("<h2 id='about'>About us</h2>");
            Console.WriteLine("<dl>");
            Console.WriteLine("<dt>By the Cake Ltd.</dt>");
            Console.WriteLine("<dd>Compnay Name</dd>");
            Console.WriteLine("<dt>John Smith</dt>");
            Console.WriteLine("<dd>Owner</dd>");
            Console.WriteLine("</dl>");

            // 08. stores info
            Console.WriteLine("<pre style='background-color: #F94F80;'>");
            Console.WriteLine("City: Hong Kong        City: Salzburg");
            Console.WriteLine("Address: ChoCoLad 18   Address: SchokoLeiden 73");
            Console.WriteLine("Phone: +78952804429    Phone: +49241432990</pre>");

            // 06. footer
            Console.WriteLine("<footer><div style='border-top: 1px solid black; text-align: center; padding: 20px 0px'>&copy; All Rights Reserved.</div></footer>");

            // close html
            Console.WriteLine("</body> </html>");
        }
    }
}
