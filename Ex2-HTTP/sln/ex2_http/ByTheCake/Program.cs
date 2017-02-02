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
            Console.WriteLine("Content-Type: text/html \n\r");
            Console.WriteLine(@"<!DOCTYPE html>
                                <html lang=""en"">
                                    <head>
                                        <title>By The Cake</title>
                                        <meta charset=""UTF-8"">
                                    </head>
                                    <body>
                                        <h3>By The Cake</h3>
                                        <h4 style=""border-bottom: 1px solid black"">Enjoy our awesome cakes</h4>
                                    </body>
                                </html>");
        }
    }
}
