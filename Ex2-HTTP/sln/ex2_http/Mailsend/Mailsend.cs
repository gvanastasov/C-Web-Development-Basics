using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Mailsend
{
    class Mailsend
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Content-type: text/html\n\r");

            Console.WriteLine("<form action=\"Login.exe\" method=\"post\">");

            Console.WriteLine("Username<input type=\"text\" name=\"username\"><br>");
            Console.WriteLine("Password<input type=\"password\" name=\"pass\"><br>");
            Console.WriteLine("<input type=\"submit\" value\"Login\">");

            Console.WriteLine("</form>");
        }
    }
}
