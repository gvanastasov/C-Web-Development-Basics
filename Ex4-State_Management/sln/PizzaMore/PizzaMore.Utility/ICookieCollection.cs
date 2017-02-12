using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMore.Utility
{
    interface ICookieCollection
    {
        void AddCookie(Cookie cookie);

        void RemoveCookie(string cookieName);

        bool ContainsKey(string key);

        int Count { get; }

        Cookie this[string key] { get; set; }
    }
}
