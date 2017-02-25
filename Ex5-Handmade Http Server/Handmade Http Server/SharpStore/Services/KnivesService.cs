using SharpStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpStore.Services
{
    public class KnivesService
    {
        private SharpStoreContext context;

        public KnivesService()
        {
            this.context = new SharpStoreContext();
        }

        public IList<Knive> GetKnives()
        {
            return this.context.Knives.ToList();
        }
    }
}
