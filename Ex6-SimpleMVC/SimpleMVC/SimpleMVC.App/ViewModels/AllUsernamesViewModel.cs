using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVC.App.ViewModels
{
    public class AllUsernamesViewModel
    {
        public AllUsernamesViewModel()
        {
            Users = new Dictionary<int, string>();
        }
        public IDictionary<int,string> Users { get; set; }
    }
}
