using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Models
{
    public class CookieCollection : IEnumerable<Cookie>
    {
        // name -> cookie
        public IDictionary<string, Cookie> Cookies { get; private set; }
        public int Count
        {
            get { return this.Cookies.Count; }
        }
        public Cookie this[string key]
        {
            get
            {
                return this.Cookies[key];
            }
            set
            {
                if (this.Contains(key))
                {
                    this.Cookies[key] = value;
                }
                else
                {
                    this.Cookies.Add(key, value);
                }
            }
        }




        public CookieCollection()
        {
            this.Cookies = new Dictionary<string, Cookie>();
        }

        public void AddCookie(Cookie cookie)
        {
            if(this.Cookies.ContainsKey(cookie.Name) == false)
            {
                this.Cookies.Add(cookie.Name, new Cookie());
            }

            this.Cookies[cookie.Name] = cookie;
        }

        public bool Contains(string key)
        {
            return Cookies.ContainsKey(key);
        }

        

        public IEnumerator<Cookie> GetEnumerator()
        {
            return this.Cookies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join("; ", Cookies.Values);
        }
    }
}
