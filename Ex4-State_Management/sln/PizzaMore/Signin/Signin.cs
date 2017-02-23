using PizzaMore.Data;
using PizzaMore.Data.Models;
using PizzaMore.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Signin
{
    class Signin
    {
        private static IDictionary<string,string> requestParams;
        private static Header header = new Header();
        private static string signinView = "../htdocs/PizzaMore/signin.html";

        static void Main()
        {

            if(WebUtil.IsPost())
            {
                TryLogin();
            }

            ServeHtmlSignIn();
        }

        private static void TryLogin()
        {
            requestParams = WebUtil.RetrievePostParameters();

            var email = requestParams["email"];
            var password = requestParams["pass"];

            using (var ctx = new PizzaMoreContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.Email == email);
                if(user != null)
                {
                    if(user.Password == PasswordHasher.Hash(password))
                    {
                        // success
                        Session session = new Session()
                        {
                            User = user,
                            UserId = user.Id,
                            Id = new Random().Next().ToString(),
                        };

                        header.AddCookie(new Cookie("sid", session.Id));
                        ctx.Sessions.Add(session);
                        ctx.SaveChanges();
                    }
                }
            }
        }

        private static void ServeHtmlSignIn()
        {
            header.Print();
            WebUtil.PrintFileContent(signinView);
        }
    }
}
