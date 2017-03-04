using SimpleMVC.App.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVC.App.MVC.Interfaces.Security
{
    public interface IDbIdentityContext
    {
        DbSet<Login> Logins { get; }
        DbSet<User> Users { get; }

        void SaveChanges();
    }
}
