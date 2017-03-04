using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleMVC.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVC.App.Views.Users
{
    public class Greet : IRenderable<GreetViewModel>
    {
        public GreetViewModel Model
        {
            get; set;
        }

        public string Render()
        {
            return $"<h3>Greetings user with session id: {Model.SessionId}</h3>";
        }
    }
}
