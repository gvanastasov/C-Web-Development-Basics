using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByTheCake
{
    class Program
    {
        static void Main()
        {
            // 01. doc
            Console.WriteLine("Content-Type: text/html \n\r");
            Console.WriteLine(@"<!DOCTYPE html> <html lang=""en""> <head> <title>By The Cake</title> <meta charset=""UTF-8""> </head> <body> <h3>By The Cake</h3> <h4 style=""border-bottom: 1px solid black"">Enjoy our awesome cakes</h4> </body> </html>");

            // 02. nav
            Console.WriteLine(@"<ul> <li><a href=""#"">Home</a> <ol> <li>Our Cakes</li> <li>Our Stores</li> </ol> </li> <li><a href=""AddCake.exe"">Add Cake</a></li> <li><a href=""BrowseCakes.exe"">Browse Cakes</a></li> <li>About Us</li> </ul>");

            // 03. sections
            Console.WriteLine(@"<h2>Home</h2> <section> <h3>Our cakes</h3> <p>Cake is a form of sweet dessert that is typically baked. In its oldest forms, cakes were modifications of breads, but cakes now cover a wide range of preparations that can be simple or elaborate, and that share features with other desserts such as pastries, meringues, custards, and pies.</p> <img src=""http://www.bbcgoodfood.com/sites/default/files/styles/recipe/public/recipe/recipe-image/2016/06/lemon-drizzle-slices.jpg?itok=RbEfdaQL""> </section> <section> <h3>Our Stores</h3> <p>Our stores are located in 21 cities all over the world. Come and see what we have for you.</p> <img src=""http://www.bbcgoodfood.com/sites/default/files/recipe-collections/collection-image/2013/05/matcha-mousse-cake.jpg""> </section>");
        }
    }
}
