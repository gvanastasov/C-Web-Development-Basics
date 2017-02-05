using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greetings
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Content-Type: text/html\n\r");

            Console.WriteLine(@"<!DOCTYPE html>
                                <html lang=""en"">
                                    <head>
                                        <title>Greetings</title>
                                        <meta charset=""UTF-8"">
                                    </head>
                                    <body>");

            var request = Environment.GetEnvironmentVariable("REQUEST_METHOD");
            Console.WriteLine("Request type: " + request);

            if (request == "GET")
            {
                Form(request);
            }
            else if(request == "POST")
            {
                var input = Console.ReadLine();

                string[] tokens = input.Split(new char[] { '&', '=' });
                string firstName = tokens[1];
                string lastName = tokens[3];
                string age = tokens[5];

                foreach (var token in tokens)
                {
                    Console.WriteLine("<div>Token: " + token + "</div>");
                }

                Form(request, firstName, lastName, age);
            }

            Console.WriteLine(@"</body>
                                </html>");
        }

        static void Form(string requestType , string fname = "", string lname = "", string age = "")
        {
            Console.WriteLine(@"<form action=""Greetings.exe"" method=""POST"">");

            if (fname == "" || requestType == "GET")
            {
                Console.WriteLine(@"<span>First Name: </span>
                                    <input type=""text"" name=""firstName"">
                                    <input type=""hidden"" name=""lastName"">
                                    <input type=""hidden"" name=""age"">
                                    <input type=""submit"" value=""Next>"">");
            }
            else
            {
                if (lname == "")
                {
                    Console.WriteLine(@"<span>Last Name: </span>
                                    <input type=""hidden"" name=""firstName"" value=""{0}"">
                                    <input type=""text"" name=""lastName"">
                                    <input type=""hidden"" name=""age"">
                                    <input type=""submit"" value=""Next>"">", fname);
                }
                else
                {
                    if (age == "")
                    {
                        Console.WriteLine(@"<span>Age: </span>s
                                        <input type=""hidden"" name=""firstName"" value=""{0}"">
                                        <input type=""hidden"" name=""lastName"" value=""{1}"">
                                        <input type=""int"" name=""age"">
                                        <input type=""submit"" value=""Greet ME>"">", fname, lname);
                    }
                    else
                    {
                        Console.WriteLine("<h2>Greetings {0} {1} at age {2}</h2>",fname, lname, age);
                    }
                }
            }

            Console.WriteLine("</form>");
        }
    }
}
