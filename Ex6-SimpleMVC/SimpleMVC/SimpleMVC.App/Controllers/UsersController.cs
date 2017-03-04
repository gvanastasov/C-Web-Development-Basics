using SimpleMVC.App.MVC.Attributes.Methods;
using SimpleMVC.App.MVC.Controllers;
using SimpleMVC.App.MVC.Interfaces;
using SimpleMVC.App.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMVC.App.Models;
using SimpleMVC.App.Data;
using SimpleMVC.App.ViewModels;
using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleHttpServer.Models;

namespace SimpleMVC.App.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model)
        {
            var user = new User()
            {
                Username = model.Username,
                Passsword = model.Password
            };

            using (var context = new NotesAppContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            return View();
        }

        [HttpGet]
        public IActionResult<AllUsernamesViewModel> All()
        {
            var viewModel = new AllUsernamesViewModel();

            using (var ctx = new NotesAppContext())
            {
                foreach (var user in ctx.Users)
                {
                    viewModel.Users.Add(user.Id, user.Username);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult<UserProfileViewModel> Profile(int id)
        {
            var viewModel = new UserProfileViewModel();

            using (var ctx = new NotesAppContext())
            {
                viewModel.UserId = id;
                viewModel.Username = ctx.Users.FirstOrDefault(u => u.Id == id).Username;
                viewModel.Notes = ctx.Notes.Where(n => n.Owner.Id == id).ToList();
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult<GreetViewModel> Greet(HttpSession session)
        {
            var viewModel = new GreetViewModel()
            {
                SessionId = session.Id
            };

            return View(viewModel);
        }
    }
}
