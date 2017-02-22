using PizzaMore.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaMore.Data;
using PizzaMore.Data.Models;

namespace _SignUp
{
    class SignUp
    {
        public static IDictionary<string, string> RequestParameters;
        public static Header Header = new Header();

        static void Main()
        {
            if(WebUtil.IsPost())
            {
                //RegisterUser();
                Console.WriteLine("Content-type: text/html\n\n");
                Console.WriteLine("<div>Successful reg: </div>");
                RequestParameters = WebUtil.RetrievePostParameters();
                foreach (var param in RequestParameters)
                {
                    Console.WriteLine($"<div>Successful reg: {param.Key} : {param.Value}</div>");

                }
                var email = RequestParameters["email"];
                var password = RequestParameters["pass"];
                var user = new User()
                {
                    Email = email,
                    Password = PasswordHasher.Hash(password)
                };

                using (var ctx = new PizzaMoreContext())
                {
                    Console.WriteLine($"<div>Before reg: {ctx.Users.Count()}</div>");
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                    Console.WriteLine($"<div>AFter reg: {ctx.Users.Count()}</div>");
                }
            }
            else
            {
                ServeHtmlSignUp();
            }
        }

        private static void ServeHtmlSignUp()
        {
            Header.Print();
            WebUtil.PrintFileContent(Constants.SignUp);
        }

        private static void RegisterUser()
        {
            RequestParameters = WebUtil.RetrievePostParameters();
            var email = RequestParameters["email"];
            var password = RequestParameters["password"];
            var user = new User()
            {
                Email = email,
                Password = PasswordHasher.Hash(password)
            };

            using (var ctx = new PizzaMoreContext())
            {
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }
        }

    }
}
