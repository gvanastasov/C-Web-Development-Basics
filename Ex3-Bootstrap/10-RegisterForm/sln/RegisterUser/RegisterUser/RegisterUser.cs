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
                    // validate
                    var tokens = postInput.Split('&');
                    var firstName = tokens[0].Split('=')[1];
                    var lastName = tokens[1].Split('=')[1];
                    var email = tokens[2].Split('=')[1].Replace("%40", "@");
                    var emailConf = tokens[3].Split('=')[1].Replace("%40", "@");
                    var pass = tokens[4].Split('=')[1];
                    var passConf = tokens[5].Split('=')[1];

                    // should split this into 2 classes USER and FORM and handle exceptions instead of bool checks
                    User user = new User(firstName, lastName, email, emailConf, pass, passConf);
                    
                    if(user.invalidForm)
                    {
                        ErrorLog(user);
                        RegistrationFormView(user);
                    }
                    else
                    {
                        Console.WriteLine($"<h3>Successful registration, Welcome {user.FirstName} {user.LastName}!</h3>");
                    }
                }
            }

            Console.WriteLine(@"    </div>
                            </body>
                            </html>");
        }

        private static void ErrorLog(User user)
        {
            Console.WriteLine(@"<div class=""error-container"">");

            if (user.invalidName)
            {
                Console.WriteLine(@"<div class=""red-asterisk"">
                                    <span class=""glyphicon glyphicon-asterisk""></span>Invalid name length
                                </div>");
            }

            if (user.invalidEmail)
            {
                Console.WriteLine(@"<div class=""red-asterisk"">
                                    <span class=""glyphicon glyphicon-asterisk""></span>Invalid email format
                                </div>");
            }

            if (user.mismatchEmail)
            {
                Console.WriteLine(@"<div class=""red-asterisk"">
                                    <span class=""glyphicon glyphicon-asterisk""></span>Email confirmation mismatch
                                </div>");
            }

            if (user.invalidPassword)
            {
                Console.WriteLine(@"<div class=""red-asterisk"">
                                    <span class=""glyphicon glyphicon-asterisk""></span>Passowrd does not meet requirements
                                </div>");
            }

            if (user.mismatchedPassword)
            {
                Console.WriteLine(@"<div class=""red-asterisk"">
                                    <span class=""glyphicon glyphicon-asterisk""></span>Passowrd confirmation mismatch
                                </div>");
            }

            if (user.formNotCompleted)
            {
                Console.WriteLine(@"<div class=""red-asterisk"">
                                    <span class=""glyphicon glyphicon-asterisk""></span>Empty Require fields
                                </div>");
            }

            Console.WriteLine("</div>");
        }

        private static void RegistrationFormView(User user = null)
        {
            Console.WriteLine($@"<form action=""RegisterUser.exe"" method=""post"" class=""form-horizontal"">
            <div class=""input-group"">
                <input class=""form-control {((user != null && user.invalidName) ? "error" : "")}"" type=""text"" id=""first-name"" placeholder=""First Name"" name=""firstName"" value=""{((user != null) ? user.FirstName : string.Empty)}"">
                <span class=""glyphicon glyphicon-asterisk input-group-addon""></span>
            </div>
            <div class=""input-group"">
                <input class=""form-control {((user != null && user.invalidName) ? "error" : "")}"" type=""text"" id=""last-name"" placeholder=""Last Name""  name=""lastName"" value=""{((user != null) ? user.LastName : string.Empty)}"">
                <span class=""glyphicon glyphicon-asterisk input-group-addon""></span>
            </div>
            <div class=""input-group"">
                <input class=""form-control {((user != null && user.invalidEmail) ? "error" : "")}"" type=""email"" id=""full-email"" placeholder=""Email""  name=""email"" value=""{((user != null) ? user.Email : string.Empty)}"">
                <span class=""glyphicon glyphicon-asterisk input-group-addon asterisk""></span>
            </div>
            <div class=""input-group"">
                <input class=""form-control {((user != null && user.mismatchEmail) ? "error" : "")}"" type=""email"" id=""confirm-email"" placeholder=""Confirm Email""  name=""emailConfirm"" value=""{((user != null) ? user.EmailConfirm : string.Empty)}"">
                <span class=""glyphicon glyphicon-asterisk input-group-addon asterisk""></span>
            </div>
            <div class=""input-group"">
                <input class=""form-control {((user != null && user.invalidPassword) ? "error" : "")}"" type=""password"" id=""pass"" placeholder=""Password""  name=""pass"" value=""{((user != null) ? user.PasswordHash : string.Empty)}"">
                <span class=""glyphicon glyphicon-asterisk input-group-addon""></span>
            </div>
            <div class=""input-group"">
                <input class=""form-control {((user != null && user.mismatchedPassword) ? "error" : "")}"" type=""password"" id=""pass-confirm"" placeholder=""Confirm Password"" name=""passConfirm"" value=""{((user != null) ? user.PasswordConfirm : string.Empty)}"">
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
