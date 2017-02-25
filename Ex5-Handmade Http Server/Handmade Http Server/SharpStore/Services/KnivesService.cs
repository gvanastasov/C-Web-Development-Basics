using SharpStore.Data;
using SharpStore.Utils;
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

        public IList<Knive> GetKnives(string url)
        {
            var getQueryString = url.Split('?');

            var searchTerm = "";
            if(getQueryString.Length > 0)
            {
                var parameters = ParametersHandler.ParseRequestParameters(getQueryString[1]);
                searchTerm = parameters["knife"];
            }

            return this.context.Knives.Where(k => string.IsNullOrEmpty(searchTerm) ? false : k.Name.Contains(searchTerm)).ToList();
        }
    }
}
