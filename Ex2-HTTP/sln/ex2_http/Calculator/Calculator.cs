using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Calculator
{
    class Calculator
    {
        static void Main()
        {
            Console.WriteLine("Content-type: text/html\n\r");

            Console.WriteLine("<form action=\"Calculator.exe\" method=\"post\">");

            Console.WriteLine("<input type=\"number\" name=\"num1\">");
            Console.WriteLine("<input type=\"text\" name=\"func\">");
            Console.WriteLine("<input type=\"number\" name=\"num2\">");
            Console.WriteLine("<input type=\"submit\" value\"Calculate\">");

            Console.WriteLine("</form>");

            var input = Console.ReadLine();

            if(string.IsNullOrEmpty(input) == false)
            {
                string[] tokens = input.Split(new char[] { '&', '=' }, StringSplitOptions.RemoveEmptyEntries);

                string func = tokens[3];

                double num1 = double.Parse(tokens[1]);
                double num2 = double.Parse(tokens[5]);

                switch (func)
                {
                    case "%2B":
                        Console.WriteLine("<div>Result: {0}</div>", (num1 + num2));
                        break;
                    case "-":
                        Console.WriteLine("<div>Result: {0}</div>", (num1 - num2));
                        break;
                    case "*":
                        Console.WriteLine("<div>Result: {0}</div>", (num1 * num2));
                        break;
                    case "%2F":
                        Console.WriteLine("<div>Result: {0}</div>", (num1 / num2));
                        break;
                    default:
                        Console.WriteLine("<div>Invalid Sign!</div>");
                        break;
                }
            }

        }
    }
}
