using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddCake
{
    class AddCakeView
    {
        static void Main()
        {
            Console.WriteLine("Content-type:text/html\r\n\r\n");
            Console.WriteLine("<a href=\"10_ByTheCake.exe\">Back to home</a><br><br>");
            Console.WriteLine("<form action=\"AddCake.exe\" method=\"post\">");
            Console.WriteLine("Name: <input type=\"text\" name=\"name\"/>");
            Console.WriteLine("Price: <input type=\"text\" name=\"price\"/>");
            Console.WriteLine("<input type=\"submit\" value=\"Add Cake\"/>");
            Console.WriteLine("</form>");

            string input = Console.ReadLine();
            if (input != null)
            {

                string[] tokens = input.Split(new char[] { '&', '=' }, StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[1].Replace("+", " ");
                double price = double.Parse(tokens[3]);

                Console.WriteLine("name: {0}", name);
                Console.WriteLine("price: {0}", price);

                var cake = new Cake()
                {
                    Name = name,
                    Price = price
                };

                File.AppendAllText("database.csv", $"{cake.Name},{cake.Price}{Environment.NewLine}");
            }

        }
    }
}
