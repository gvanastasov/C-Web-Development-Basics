using SimpleMVC.App.MVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVC.App.Views.Home
{
    public class Index : IRenderable
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder("<h2>Notes App</h2>");
            sb.AppendLine("<a href=\"/users/all\">All users</a> <a href=\"/users/register\">Register Users</a>");
            sb.AppendLine("<a href=\"/users/logout\">Logout</a>");
            return sb.ToString();
        }
    }
}
