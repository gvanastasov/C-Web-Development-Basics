using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMore.Utility
{
    class CookieCollection : ICookieCollection
    {
        public IDictionary<string, Cookie> Cookies { get; private set; }

        public CookieCollection()
        {
            this.Cookies = new Dictionary<string, Cookie>();
        }

        public Cookie this[string key]
        {
            get
            {
                return Cookies[key];
            }

            set
            {
                if (this.Cookies.ContainsKey(key))
                {
                    this.Cookies[key] = value;
                }
                else
                {
                    this.Cookies.Add(key, value);
                }
            }
        }

        public int Count
        {
            get
            {
                return Cookies.Count;
            }
        }

        public void AddCookie(Cookie cookie)
        {
            if (!this.Cookies.ContainsKey(cookie.Name))
            {
                this.Cookies.Add(cookie.Name, new Cookie());
            }

            this.Cookies[cookie.Name] = cookie;
        }

        public bool ContainsKey(string key)
        {
            return Cookies.ContainsKey(key);
        }

        public void RemoveCookie(string cookieName)
        {
            if (this.Cookies.ContainsKey(cookieName))
            {
                this.Cookies.Remove(cookieName);
            }
        }
    }
}
