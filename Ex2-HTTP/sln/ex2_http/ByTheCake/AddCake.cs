using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByTheCake
{
    class AddCake
    {
        static void Main()
        {
            Console.WriteLine("Content-Type: text/html \n\r");
            Console.WriteLine(@"<!DOCTYPE html> <html lang=""en"">");

            Console.WriteLine(@"<head> <title>By The Cake</title> <meta charset=""UTF-8""> <meta name=""description"" content=""Buy the cake in By the Cake""> <meta name=""keywords"" content=""cakes, buy""> <meta name=""author"" content=""John Doe""> </head>");

            Console.WriteLine("<body>");

            // 11. Form


            // close html
            Console.WriteLine("</body> </html>");
        }
    }
}
