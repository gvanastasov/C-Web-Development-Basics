using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Models
{
    /// <summary>
    /// kvp type that hold user specific page data/settings
    /// </summary>
    public class Cookie
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public Cookie() : this(null, null)
        {

        }
        public Cookie(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public override string ToString()
        {
            return $"{this.Name}={this.Value}";
        }
    }
}
