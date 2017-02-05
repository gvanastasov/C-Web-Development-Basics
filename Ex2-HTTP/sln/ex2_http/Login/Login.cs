using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Login
{
    class Login
    {
        static void Main()
        {
            Console.WriteLine("Content-type: text/html\n\r");

            Console.WriteLine("<form action=\"Login.exe\" method=\"post\">");

            Console.WriteLine("Username<input type=\"text\" name=\"username\"><br>");
            Console.WriteLine("Password<input type=\"password\" name=\"pass\"><br>");
            Console.WriteLine("<input type=\"submit\" value\"Login\">");

            Console.WriteLine("</form>");

            var credentials = Console.ReadLine();

            if(string.IsNullOrEmpty(credentials) == false)
            {
                string[] tokens = credentials.Split(new char[] { '&', '=' }, StringSplitOptions.RemoveEmptyEntries);
                string username = tokens[1];
                string password = tokens[3];

                Console.WriteLine("<div>Hi {0}, your password is {1}</div>", username, password);
            }
        }
    }
}
