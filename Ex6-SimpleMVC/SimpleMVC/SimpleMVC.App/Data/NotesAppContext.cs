namespace SimpleMVC.App.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using MVC.Interfaces.Security;

    public class NotesAppContext : DbContext, IDbIdentityContext
    {

        public NotesAppContext()
            : base("NotesAppContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Login> Logins { get; set; }

        void IDbIdentityContext.SaveChanges()
        {
            this.SaveChanges();
        }
    }
}