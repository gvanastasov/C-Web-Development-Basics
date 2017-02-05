using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailbox
{
    class Login
    {
        static void Main()
        {
            Console.WriteLine("Content-type: text/html\n\r");

            Console.WriteLine("<!DOCTYPE html> <html lang=\"en\"> <head> <title>Send Email</title> <meta charset=\"UTF-8\"> </head> <body>");

            var request = Environment.GetEnvironmentVariable("REQUEST_METHOD");

            Console.WriteLine("Request type: " + request);

            if (request == "GET")
            {
                GetLoginForm();
            }
            else if(request == "POST")
            {
                var credentials = Console.ReadLine();
                var uValid = "123";
                var pValid = "123";

                if(credentials != "")
                {
                    string[] tokens = credentials.Split(new char[] { '&', '=' }, StringSplitOptions.RemoveEmptyEntries);
                    string username = tokens[1];
                    string password = tokens[3];

                    if(username == uValid && password == pValid)
                    {
                        PostLoginForm();
                    }
                    else
                    {
                        GetLoginForm();
                        Console.WriteLine("<div>Invalid username or password!</div>");
                    }
                }

            }

            Console.WriteLine("</body></html>");
        }

        private static void GetLoginForm()
        {
            Console.WriteLine("<h2>Login</h2>");
            Console.WriteLine(@"<form action=""Mailbox.exe"" method=""post"">
                                    <label for="""">Username</label>
                                    <input type=""text"" name=""username""><br>
                                    <label for="""">Password</label>
                                    <input type=""password"" name=""pass""><br>
                                    <input type=""submit"" value=""Login""><br>
                                </form>");
        }

        private static void PostLoginForm()
        {
            Console.WriteLine(File.ReadAllText("temp.html"));
        }

        private static void PostEmailForm()
        {

        }
    }
}
