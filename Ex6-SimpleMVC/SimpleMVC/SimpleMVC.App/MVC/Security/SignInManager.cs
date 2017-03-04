using SimpleHttpServer.Models;
using SimpleMVC.App.MVC.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVC.App.MVC.Security
{
    public class SignInManager
    {
        private IDbIdentityContext dbContext;

        public SignInManager(IDbIdentityContext context)
        {
            this.dbContext = context;
        }

        public bool IsAuthenticated(HttpSession session)
        {
            return (session == null)
                ? false
                : dbContext.Logins.Any(l => l.SessionId == session.Id);
        }

        public void Logout(HttpSession session)
        {
            var activeSession = dbContext.Logins.FirstOrDefault(l => l.SessionId == session.Id && l.IsActive == true);

            if(activeSession != null)
            {
                activeSession.IsActive = false;
                this.dbContext.SaveChanges();
            }
        }
    }
}
