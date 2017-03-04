using SimpleMVC.App.BindingModels;
using SimpleMVC.App.Data;
using SimpleMVC.App.Models;
using SimpleMVC.App.MVC.Attributes.Methods;
using SimpleMVC.App.MVC.Controllers;
using SimpleMVC.App.MVC.Interfaces;
using SimpleMVC.App.MVC.Interfaces.Generic;
using SimpleMVC.App.ViewModels;
using SimpleMVC.App.Views.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVC.App.Controllers
{
    public class NotesController : Controller
    {
        [HttpPost]
        public IActionResult<UserProfileViewModel> Addnote(int id, AddNoteBindingModel model)
        {
            var viewModel = new UserProfileViewModel();

            using (var ctx = new NotesAppContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.Id == id);

                if(user != null)
                {
                    var note = new Note()
                    {
                        Title = model.Title,
                        Content = model.Content
                    };

                    user.Notes.Add(note);
                    ctx.SaveChanges();

                    viewModel.Username = user.Username;
                    viewModel.Notes = user.Notes;
                }
            };

            return View(viewModel,"Users", "Profile");
        }
    }
}
