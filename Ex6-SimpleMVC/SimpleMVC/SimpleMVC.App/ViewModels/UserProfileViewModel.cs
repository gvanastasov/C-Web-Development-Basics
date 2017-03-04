using SimpleMVC.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVC.App.ViewModels
{
    public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
