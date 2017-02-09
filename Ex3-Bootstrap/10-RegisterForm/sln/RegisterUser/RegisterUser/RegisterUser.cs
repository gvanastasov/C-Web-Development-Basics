using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _RegisterUser
{
    class RegisterUser
    {
        static void Main()
        {
            Console.WriteLine("Content-type:text/html\r\n\r\n");
            Console.WriteLine(@"<!DOCTYPE html>
                                <html lang=""en"">

                                <head>
                                    <title>Contacts</title>
                                    <meta charset=""UTF-8"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"">
                                    <script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js""></script>
                                    <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js""></script>
                                    <link rel=""stylesheet"" type=""text/css"" href=""/styles/register.css"">
                                </head>

                                <body>
                                    <div class=""container col-md-6 col-xs-offset-3"">
                                        <h3 class=""headline"">Contact Us</h3>
                                        <div class=""input-group form-control h4""><span class=""glyphicon glyphicon-asterisk""></span>Required field</div>
                                ");

            var requestType = Environment.GetEnvironmentVariable("REQUEST_METHOD");
            if(requestType == "GET")
            {
                // render form
                RegistrationFormView();
            }
            else if(requestType == "POST")
            {
                var postInput = Console.ReadLine();

                if(postInput != null)
                {
                    Console.WriteLine($"<div>Input: {postInput}</div>");
                    // validate
                    var tokens = postInput.Split('&');
                    var firstName = tokens[0].Split('=')[1];
                    var lastName = tokens[1].Split('=')[1];
                    var email = tokens[2].Split('=')[1];
                    var emailConf = tokens[3].Split('=')[1];
                    var pass = tokens[4].Split('=')[1];
                    var passConf = tokens[5].Split('=')[1];

                    var error = string.Empty;
                    User user = new User(firstName, lastName, email, emailConf, pass, passConf);

                    
                    if(user.invalidForm)
                    {
                        Console.WriteLine(@"<div class=""error-container"">");

                        if (user.invalidName)
                        {
                            Console.WriteLine(@"<div class=""red-asterisk"">
                                    <span class=""glyphicon glyphicon-asterisk""></span>Invalid name
                                </div>");
                        }

                        Console.WriteLine("</div>");
                        RegistrationFormView();
                    }

                    

                    //if(valid)
                    //{
                    //    // render successful msg
                    //}
                    //else
                    //{
                    //    // render error block
                    //    // render form
                    //    RegistrationFormView();
                    //}
                }
            }

            Console.WriteLine(@"    </div>
                            </body>
                            </html>");
        }

        private static void RegistrationFormView()
        {
            Console.WriteLine(@"<form action=""RegisterUser.exe"" method=""post"" class=""form-horizontal"">
            <div class=""input-group"">
                <input class=""form-control"" type=""text"" id=""first-name"" placeholder=""First Name"" name=""firstName"">
                <span class=""glyphicon glyphicon-asterisk input-group-addon""></span>
            </div>
            <div class=""input-group"">
                <input class=""form-control"" type=""text"" id=""last-name"" placeholder=""Last Name""  name=""lastName"">
                <span class=""glyphicon glyphicon-asterisk input-group-addon""></span>
            </div>
            <div class=""input-group"">
                <input class=""form-control"" type=""email"" id=""full-email"" placeholder=""Email""  name=""email"">
                <span class=""glyphicon glyphicon-asterisk input-group-addon asterisk""></span>
            </div>
            <div class=""input-group"">
                <input class=""form-control"" type=""email"" id=""confirm-email"" placeholder=""Confirm Email""  name=""emailConfirm"">
                <span class=""glyphicon glyphicon-asterisk input-group-addon asterisk""></span>
            </div>
            <div class=""input-group"">
                <input class=""form-control"" type=""password"" id=""pass"" placeholder=""Password""  name=""pass"">
                <span class=""glyphicon glyphicon-asterisk input-group-addon""></span>
            </div>
            <div class=""input-group"">
                <input class=""form-control"" type=""password"" id=""pass-confirm"" placeholder=""Confirm Password"" name=""passConfirm"">
                <span class=""glyphicon glyphicon-asterisk input-group-addon""></span>
            </div>
            <div class=""row"">
                <div class=""col-xs-offset-6 col-md-6"">
                    <button type=""reset"" class=""col-md-5 btn btn-default"">Clear</button>
                    <button type=""submit"" class=""col-xs-offset-2 col-md-5 btn btn-primary"">Register</button>
                </div>
            </div>
        </form>");
        }
    }
}
