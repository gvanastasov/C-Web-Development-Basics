using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMore.Utility
{
    public class Cookie
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public Cookie()
        {

        }

        public Cookie(string cookieName, string parameterValue)
        {
            this.Name = cookieName;
            this.Value = parameterValue;
        }

        public override string ToString()
        {
            return $"{this.Name}={this.Value}";
        }
    }
}
