using System.Data.Entity;

namespace SharpStore.Data
{
    public class SharpStoreContext : DbContext
    {
        public SharpStoreContext() : base("SharpStoreContext")
        {

        }

        public DbSet<Knive> Knives { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
